using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Приймання товару</summary>
    public class Acceptance : BusinessProcess
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

        private CatalogItem trayItem;
        private CatalogItem linerItem;
        private BarcodeData currentBarcodeData;
        private long lastStickerId;
        private long acceptanceId;

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        /// <summary>Приймання товару</summary>
        public Acceptance()
            : base(1)
            {
            ToDoCommand = "Приймання товару";
            checkAcceptanceCache();
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
                        if (acceptanceId == 0 || "Скасувати операцію?".Ask())
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

            if (acceptanceId == 0 && !initAcceptance(barcodeData.StickerId))
                {
                return;
                }

            bool currentAcceptance;
            readStickerInfo(acceptanceId, barcodeData, out currentAcceptance);
            if (barcodeData.StickerId == 0)
                {
                return;
                }

            if (!currentAcceptance)
                {
                "Відсканований піддон не входить до документу!".Warning();
                return;
                }

            currentBarcodeData = barcodeData;

            ShowControls(palletEditControls);

            updateStickerData();
            }

        private void editPalletOnBarcode(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();

                var samePallet = currentBarcodeData != null && currentBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                bool currentAcceptance;
                readStickerInfo(acceptanceId, barcodeData, out currentAcceptance);

                bool cellFounded = barcodeData.Cell.Id != 0;
                if (!cellFounded)
                    {
                    "Відсканованої палети нема на залишках".Warning();
                    return;
                    }

                trySetCell(barcodeData.Cell);
                }
            else if (barcode.IsCell())
                {
                trySetCell(barcode.ToCell());
                }
            }

        private void startScanNextPallet()
            {
            currentBarcodeData = null;
            ShowControls(scanNextPalletControls);
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
            palletEditControls.trayButton = MainProcess.CreateButton("<піддон>", 5, top, 230, 35, "modelButton", trayButton_Click);

            top += delta + delta;
            palletEditControls.linersLabel = MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            linerItem = new CatalogItem();
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
            if (!new ServerInteraction().ComplateAcceptance(acceptanceId, false, out errorDescription))
                {
                CANT_COMPLATE_OPERATION.Warning();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            }

        private void updateLinerButton()
            {
            palletEditControls.linerButton.Text = linerItem.Id > 0 ? linerItem.Description : "<тип прокладки>";
            }

        private void trayButton_Click()
            {
            selectFromCatalog(new Repository().GetTraysList(), (selectedItem) =>
                {
                    palletEditControls.trayButton.Text = selectedItem.Description;
                    trayItem = selectedItem;
                });
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
                linerItem = selectedItem;
                updateLinerButton();
            });
            }

        private void trySetCell(CatalogItem cell)
            {
            bool cellApproved = currentBarcodeData.Cell.Id == cell.Id || currentBarcodeData.Cell.Id == 0;
            if (cellApproved || string.Format(@"Розмістити у комірці ""{0}""?", cell.Description).Ask())
                {
                if (saveFact(cell))
                    {
                    startScanNextPallet();
                    }
                else
                    {
                    "Необхідно знаходитись у зоні Wi-Fi".Warning();
                    return;
                    }
                }
            }

        private bool saveFact(CatalogItem cell)
            {
            if (currentBarcodeData == null) return true;

            var palletChanged = linerItem.Id > 0;
            palletChanged |= currentBarcodeData.Tray.Id != trayItem.Id;
            palletChanged |= currentBarcodeData.FullPacksCount != packsCount;
            palletChanged |= currentBarcodeData.UnitsRemainder != unitsCount;

            if (!new ServerInteraction().WriteStickerFact(acceptanceId, currentBarcodeData.StickerId, palletChanged,
                (cell ?? new CatalogItem()).Id, trayItem.Id, linerItem.Id, linersCount, packsCount, unitsCount + packsCount * currentBarcodeData.UnitsPerBox))
                {
                return false;
                }

            return true;
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

        public int linersCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletEditControls.linersQuantityTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletEditControls.linersQuantityTextBox.Text);
                }
            set
                {
                palletEditControls.linersQuantityTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        private bool initAcceptance(long stickerId)
            {
            return new ServerInteraction().GetAcceptanceId(stickerId,
                out acceptanceId);
            }



        private void updateStickerData()
            {
            palletEditControls.cellLabel.Text = (currentBarcodeData.Cell != null && currentBarcodeData.Cell.Id > 0) ? currentBarcodeData.Cell.Description : "<?>";
            palletEditControls.nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            palletEditControls.trayButton.Text = currentBarcodeData.Tray.Description;
            trayItem = currentBarcodeData.Tray;

            palletEditControls.stickerIdInfoLabel.Text = string.Format("Код палети: {0}", currentBarcodeData.StickerId);

            if (currentBarcodeData.UnitsPerBox > 0)
                {
                packsCount = currentBarcodeData.FullPacksCount;
                unitsCount = currentBarcodeData.UnitsRemainder;
                }
            else
                {
                packsCount = 0;
                unitsCount = 0;
                }

            updateLinerButton();
            }

        private void readStickerInfo(long acceptanceId, BarcodeData barcodeData, out bool currentAcceptance)
            {
            string nomenclatureDescription;
            long trayId;
            int unitsPerBox;
            string cellDescription;
            long cellId;

            if (
                !new ServerInteraction().GetStickerData(acceptanceId, barcodeData.StickerId,
                    out nomenclatureDescription, out trayId,
                    out unitsPerBox, out cellId, out cellDescription, out currentAcceptance))
                {
                barcodeData.Cell = new CatalogItem();
                barcodeData.StickerId = 0;
                return;
                }

            barcodeData.Nomenclature.Description = nomenclatureDescription;
            barcodeData.Tray = new CatalogItem()
            {
                Id = trayId,
                Description = new Repository().GetTrayDescription(trayId)
            };

            barcodeData.UnitsPerBox = Convert.ToInt32(unitsPerBox);
            barcodeData.Cell = new CatalogItem() { Description = cellDescription, Id = cellId };
            }




        }
    }