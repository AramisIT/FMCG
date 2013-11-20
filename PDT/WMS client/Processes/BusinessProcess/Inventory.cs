using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Inventory : BusinessProcess
        {
        private class AcceptancePalletControls : HideableControlsCollection
            {
            public MobileTextBox packsCountTextBox;
            public MobileTextBox unitsCountTextBox;
            public MobileTextBox linersQuantityTextBox;

            public MobileLabel nomenclatureLabel;
            public MobileLabel cellLabel;
            public MobileLabel stickerIdInfoLabel;

            public MobileButton trayButton;
            public MobileButton linerButton;
            public MobileLabel packsLabel;
            public MobileLabel unitsLabel;
            public MobileLabel linersLabel;
            public MobileLabel cellCaptionLabel;
            }

        private class ScanPalletControls : HideableControlsCollection
            {
            public MobileLabel WillLabel;
            }

        /// <summary>
        /// Редактирование паллеты
        /// </summary>
        private AcceptancePalletControls palletEditControls;

        /// <summary>
        /// Приглашение отсканировать следующую паллету
        /// </summary>
        private ScanPalletControls scanNextPalletControls;

        private BarcodeData currentBarcodeData;
        private long lastStickerId;
        private BarcodeData startBarcodeData;
        private bool cellDefined;
        private long documentId;
        private DataTable resultTable;
        private Dictionary<long, bool> processedPallets = new Dictionary<long, bool>();

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        public Inventory()
            : base(1)
            {
            ToDoCommand = "ІНВЕНТАРИЗАЦІЯ";
            checkAcceptanceCache();

            resultTable = new DataTable();
            resultTable.Columns.AddRange(new DataColumn[] { 
            new DataColumn("Nomenclature", typeof(long)),
            new DataColumn("PalletCode", typeof(long)), 
            new DataColumn("StartCodeOfPreviousPallet", typeof(long)), 
            new DataColumn("FinalCodeOfPreviousPallet", typeof(long)), 
            new DataColumn("PlanValue", typeof(int)), 
            new DataColumn("FactValue", typeof(int)), 
            new DataColumn("StartCell", typeof(long)), 
            new DataColumn("FinalCell", typeof(long))});
            }

        private void checkAcceptanceCache()
            {
            var repository = new Repository();
            repository.GetTraysList();
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            createPalletControls();

            createScanPalletLabelControls();

            startScanNextPallet();
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (scanNextPalletControls.Visible)
                {
                scanNextPalletOnBarcode(barcode);
                }
            else if (palletEditControls.Visible)
                {
                editPalletOnBarcode(barcode);
                }
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            if (palletEditControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Esc:
                        startScanNextPallet();
                        return;

                    case KeyAction.Proceed:
                        if (!cellDefined)
                            {
                            "Відскануйте попередню палету чи комірку, якщо палета перша".ShowMessage();
                            return;
                            }
                        if (saveFact())
                            {
                            processedPallets.Add(currentBarcodeData.StickerId, true);
                            startScanNextPallet();
                            }
                        return;
                    }
                }
            else if (scanNextPalletControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Complate:
                        complateProcess();
                        return;

                    case KeyAction.Esc:
                        if ("Завершити операцію?".Ask())
                            {
                            MainProcess.ClearControls();
                            MainProcess.Process = new SelectingProcess();
                            }
                        return;
                    }
                }
            }

        #endregion

        private void scanNextPalletOnBarcode(string barcode)
            {
            if (!barcode.IsSticker()) return;

            var barcodeData = barcode.ToBarcodeData();

            if (processedPallets.ContainsKey(barcodeData.StickerId))
                {
                "Ця палета вже була оброблена".ShowMessage();
                return;
                }

            if (!readStickerInfo(barcodeData)) return;

            startBarcodeData = barcodeData;
            currentBarcodeData = barcodeData.GetCopy();

            ShowControls(palletEditControls);

            updateStickerData();
            }

        private void editPalletOnBarcode(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();
                processedPallets = new Dictionary<long, bool>();

                var samePallet = currentBarcodeData != null && currentBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                onAnotherStickerScan(barcodeData);
                }
            else if (barcode.IsCell())
                {
                onCellScan(barcode.ToCell());
                }
            }

        private void onAnotherStickerScan(BarcodeData barcodeData)
            {
            if (barcodeData.StickerId == currentBarcodeData.PreviousStickerCode) return;

            readStickerInfo(barcodeData);
            bool cellFounded = barcodeData.Cell.Id != 0;
            if (!cellFounded)
                {
                "Відсканованої палети нема на залишках".Warning();
                return;
                }

            if (!string.Format(@"Розмістити у комірці ""{0}"" після палети № {1}", barcodeData.Cell.Description, barcodeData.StickerId).Ask()) return;

            this.currentBarcodeData.Cell = barcodeData.Cell;
            this.currentBarcodeData.PreviousStickerCode = barcodeData.StickerId;
            notifyCellUpdated();
            }

        private void onCellScan(CatalogItem scannedCell)
            {
            if (scannedCell.Id == startBarcodeData.Cell.Id)
                {
                if (startBarcodeData.PreviousStickerCode == 0)
                    {
                    notifyCellUpdated();
                    }
                else if ("Зараз палета перша у комірці?".Ask())
                    {
                    currentBarcodeData.PreviousStickerCode = 0;
                    notifyCellUpdated();
                    }
                }
            else if (string.Format(@"Розмістити палету першою у комірці ""{0}""?", scannedCell.Description).Ask())
                {
                currentBarcodeData.Cell = scannedCell;
                currentBarcodeData.PreviousStickerCode = 0;
                notifyCellUpdated();
                }
            }

        private void notifyCellUpdated()
            {
            cellDefined = true;
            updateCellDescription();
            palletEditControls.cellLabel.SetFontColor(MobileFontColors.Warning);
            }

        private void startScanNextPallet()
            {
            currentBarcodeData = null;
            ShowControls(scanNextPalletControls);
            palletEditControls.cellLabel.SetFontColor(MobileFontColors.Disable);
            cellDefined = false;
            }

        private void createScanPalletLabelControls()
            {
            scanNextPalletControls = new ScanPalletControls();

            int top = 140;
            const int delta = 27;

            top += delta;
            scanNextPalletControls.WillLabel = MainProcess.CreateLabel("Відскануйте палету", 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            }

        private void createPalletControls()
            {
            palletEditControls = new AcceptancePalletControls();
            int top = 42;
            const int delta = 27;

            top += delta;
            palletEditControls.nomenclatureLabel = MainProcess.CreateLabel("<номенклатура>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;

            palletEditControls.packsLabel = MainProcess.CreateLabel("упаковок:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.packsCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            palletEditControls.unitsLabel = MainProcess.CreateLabel("+ шт.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.unitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            palletEditControls.stickerIdInfoLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += delta;
            palletEditControls.trayButton = MainProcess.CreateButton("", 5, top, 230, 35, "modelButton", trayButton_Click);
            updateTrayDescription();

            top += delta + delta;
            palletEditControls.linersLabel = MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            palletEditControls.linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click);
            updateLinerButton();

            top += delta + delta;
            palletEditControls.cellCaptionLabel = MainProcess.CreateLabel("Комірка:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void complateProcess()
            {
            if (!"Завершить операцию?".Ask())
                {
                return;
                }

            string errorDescription;
            if (!new ServerInteraction().ComplateInventory(documentId, false, out errorDescription))
                {
                CANT_COMPLATE_OPERATION.Warning();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            }

        private void updateLinerButton()
            {
            var liner = (currentBarcodeData ?? new BarcodeData()).Liner ?? new CatalogItem();
            palletEditControls.linerButton.Text = liner.Id > 0 ? liner.Description : "<тип прокладки>";
            }

        private void trayButton_Click()
            {
            selectFromCatalog(new Repository().GetTraysList(), (selectedItem) =>
                {
                    currentBarcodeData.Tray = selectedItem;
                    updateTrayDescription();
                });
            }

        private void updateTrayDescription()
            {
            var tray = ((currentBarcodeData ?? new BarcodeData()).Tray ?? new CatalogItem());
            palletEditControls.trayButton.Text = tray.Id == 0 ? "без піддону" : tray.Description;
            }

        private void selectFromCatalog(List<CatalogItem> itemsList, Action<CatalogItem> onSelect)
            {
            CatalogItem selectedItem;
            if (SelectFromList(itemsList, out selectedItem))
                {
                onSelect(selectedItem);
                }
            }

        private void linerButton_Click()
            {
            selectFromCatalog(new Repository().GetLinersList(), (selectedItem) =>
            {
                currentBarcodeData.Liner = selectedItem;
                updateLinerButton();
            });
            }

        private bool saveFact()
            {
            if (documentId == 0 && !initDocument())
                {
                return false;
                }

            if (currentBarcodeData == null) return true;

            currentBarcodeData.LinersAmount = linersCount;
            currentBarcodeData.UnitsQuantity = unitsCount + packsCount * currentBarcodeData.UnitsPerBox;

            resultTable.Rows.Clear();

            appendResult(currentBarcodeData.Nomenclature.Id, currentBarcodeData.Nomenclature.Id, startBarcodeData.UnitsQuantity, currentBarcodeData.UnitsQuantity, false);

            if (currentBarcodeData.LinersAmount > 0 || startBarcodeData.LinersAmount > 0)
                {
                appendResult(startBarcodeData.Liner.Id, currentBarcodeData.Liner.Id, startBarcodeData.LinersAmount, currentBarcodeData.LinersAmount, true);
                }

            if (currentBarcodeData.Tray.Id > 0 || startBarcodeData.Tray.Id > 0)
                {
                appendResult(startBarcodeData.Tray.Id, currentBarcodeData.Tray.Id, startBarcodeData.Tray.Id > 0 ? 1 : 0, currentBarcodeData.Tray.Id > 0 ? 1 : 0, true);
                }

            return new ServerInteraction().WriteInventoryResult(documentId, resultTable);
            }

        private void appendResult(long startId, long finalId, long plan, long fact, bool isTare)
            {
            if (startId == finalId)
                {
                appendResultToTable(startId, plan, fact, startBarcodeData.Cell.Id, currentBarcodeData.Cell.Id, isTare);
                return;
                }

            if (startId > 0)
                {
                appendResultToTable(startId, plan, 0, startBarcodeData.Cell.Id, startBarcodeData.Cell.Id, isTare);
                }

            if (finalId > 0)
                {
                appendResultToTable(finalId, 0, fact, currentBarcodeData.Cell.Id, currentBarcodeData.Cell.Id, isTare);
                }
            }

        private void appendResultToTable(long startId, long plan, long fact, long startCell, long finalCell, bool isTare)
            {
            var row = resultTable.NewRow();

            row["Nomenclature"] = startId;
            row["PlanValue"] = plan;
            row["FactValue"] = fact;

            row["PalletCode"] = currentBarcodeData.StickerId;
            row["StartCodeOfPreviousPallet"] = isTare ? 0L : startBarcodeData.PreviousStickerCode;
            row["FinalCodeOfPreviousPallet"] = isTare ? 0L : currentBarcodeData.PreviousStickerCode;
            row["StartCell"] = startCell;
            row["FinalCell"] = finalCell;

            resultTable.Rows.Add(row);
            }

        private bool initDocument()
            {
            return new ServerInteraction().GetNewInventoryId(0, out documentId);
            }

        public int packsCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletEditControls.packsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletEditControls.packsCountTextBox.Text);
                }
            set
                {
                palletEditControls.packsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        public int unitsCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletEditControls.unitsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletEditControls.unitsCountTextBox.Text);
                }
            set
                {
                palletEditControls.unitsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        public byte linersCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletEditControls.linersQuantityTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToByte(palletEditControls.linersQuantityTextBox.Text);
                }
            set
                {
                palletEditControls.linersQuantityTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        private void updateStickerData()
            {
            updateCellDescription();

            palletEditControls.nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            palletEditControls.trayButton.Text = currentBarcodeData.Tray.Description;
          
            palletEditControls.stickerIdInfoLabel.Text = string.Format("Код палети: {0}", currentBarcodeData.StickerId);

            if (currentBarcodeData.UnitsPerBox > 0)
                {
                packsCount = (currentBarcodeData.UnitsQuantity / currentBarcodeData.UnitsPerBox);
                unitsCount = (currentBarcodeData.UnitsQuantity % currentBarcodeData.UnitsPerBox);
                }
            else
                {
                packsCount = 0;
                unitsCount = 0;
                }

            linersCount = currentBarcodeData.LinersAmount;
            updateLinerButton();
            updateTrayDescription();
            }

        private void updateCellDescription()
            {
            palletEditControls.cellLabel.Text = (currentBarcodeData.Cell != null && currentBarcodeData.Cell.Id > 0) ? currentBarcodeData.Cell.Description : "<?>";
            }

        private bool readStickerInfo(BarcodeData barcodeData)
            {
            string nomenclatureDescription;
            string trayDescription;
            long trayId;
            long linerId;
            byte linersAmount;
            int unitsPerBox;
            string cellDescription;
            long cellId;
            long previousPalletCode;
            if (
                !new ServerInteraction().GetPalletBalance(barcodeData.StickerId,
                    out nomenclatureDescription, out trayId, out linerId, out linersAmount,
                    out unitsPerBox, out cellId, out cellDescription, out previousPalletCode))
                {
                barcodeData.Cell = new CatalogItem();
                barcodeData.StickerId = 0;
                return false;
                }

            barcodeData.PreviousStickerCode = previousPalletCode;
            barcodeData.Nomenclature.Description = nomenclatureDescription;
            barcodeData.Tray = new CatalogItem()
            {
                Id = trayId,
                Description = new Repository().GetTrayDescription(trayId)
            };

            barcodeData.Liner = new CatalogItem()
            {
                Id = linerId,
                Description = new Repository().GetLinerDescription(linerId)
            };

            barcodeData.LinersAmount = linersAmount;

            barcodeData.UnitsPerBox = Convert.ToInt32(unitsPerBox);
            barcodeData.Cell = new CatalogItem() { Description = cellDescription, Id = cellId };

            return true;
            }




        }
    }