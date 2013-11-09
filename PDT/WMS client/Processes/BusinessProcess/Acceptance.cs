using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using WMS_client.Processes.BaseScreen;
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

        private AcceptancePalletControls palletControls;
        private CatalogItem trayItem;
        private CatalogItem linerItem;
        private CatalogItem cellItem;
        private BarcodeData currentBarcodeData;

        private long acceptanceId;
        private ScanPalletControls scanPalletLabelControls;

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

            ShowControls(scanPalletLabelControls);
            }

        private void createScanPalletLabelControls()
            {
            scanPalletLabelControls = new ScanPalletControls();

            int top = 140;
            const int delta = 27;

            top += delta;
            scanPalletLabelControls.WillLabel = MainProcess.CreateLabel("Відскануйте палету", 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            }

        private void createPalletControls()
            {
            palletControls = new AcceptancePalletControls();
            int top = 42;
            const int delta = 27;

            top += delta;
            palletControls.nomenclatureLabel = MainProcess.CreateLabel("<номенклатура>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;

            palletControls.packsLabel = MainProcess.CreateLabel("упаковок:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletControls.packsCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            palletControls.unitsLabel = MainProcess.CreateLabel("+ шт.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletControls.unitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            palletControls.stickerIdInfoLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += delta;
            palletControls.trayButton = MainProcess.CreateButton("<піддон>", 5, top, 230, 35, "modelButton", trayButton_Click,
               new PropertyButtonInfo() { PropertyName = "Tray", PropertyDescription = "Тип піддону" });

            top += delta + delta;
            palletControls.linersLabel = MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletControls.linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            linerItem = new CatalogItem();
            palletControls.linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click,
               new PropertyButtonInfo() { PropertyName = "Liner", PropertyDescription = "Тип прокладки" });
            updateLinerButton();

            top += delta + delta;
            palletControls.cellCaptionLabel = MainProcess.CreateLabel("Комірка:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletControls.cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        public override void OnHotKey(KeyAction TypeOfAction)
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

        private void complateProcess()
            {
            if (!"Завершить операцию?".Ask())
                {
                return;
                }

            if (!saveFact())
                {
                CANT_COMPLATE_OPERATION.Warning();
                return;
                }

            string errorDescription;
            if (!WMSClient.ServerInteraction.ComplateAcceptance(acceptanceId, false, out errorDescription))
                {
                CANT_COMPLATE_OPERATION.Warning();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            return;
            }

        private void updateLinerButton()
            {
            palletControls.linerButton.Text = linerItem.Id > 0 ? linerItem.Description : "<тип прокладки>";
            }

        private void trayButton_Click(object sender)
            {
            selectFromCatalog(new Repository().GetTraysList(), (selectedItem) =>
                {
                    palletControls.trayButton.Text = selectedItem.Description;
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

        private void linerButton_Click(object sender)
            {
            selectFromCatalog(new Repository().GetLinersList(), (selectedItem) =>
            {
                linerItem = selectedItem;
                updateLinerButton();
            });
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();

                var samePallet = currentBarcodeData != null && currentBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                if (acceptanceId == 0 && !initAcceptance(barcodeData.StickerId))
                    {
                    return;
                    }
                CatalogItem cell;
                readStickerInfo(acceptanceId, barcodeData, out cell);
                if (barcodeData.StickerId == 0)
                    {
                    return;
                    }

                ShowControls(palletControls);

                var scannedNextSticker = cell.Id == 0;
                if (scannedNextSticker)
                    {
                    if (!saveFact())
                        {
                        "Необхідно знаходитись у зоні Wi-Fi".Warning();
                        return;
                        }
                    currentBarcodeData = barcodeData;
                    cellItem = new CatalogItem();
                    }
                else
                    {
                    cellItem = cell;
                    }
                updateStickerData();
                }
            else if (barcode.IsCell())
                {
                cellItem = barcode.ToCell();
                //if (cellItem.Id > 0)
                //    {
                //    ShowControls(scanPalletLabelControls);
                //    }
                }

            updateCellData();
            }

        private bool saveFact()
            {
            if (currentBarcodeData == null) return true;

            var palletChanged = linerItem.Id > 0;
            palletChanged |= currentBarcodeData.Tray.Id != trayItem.Id;
            palletChanged |= (currentBarcodeData.UnitsQuantity / currentBarcodeData.UnitsPerBox) != packsCount;
            palletChanged |= (currentBarcodeData.UnitsQuantity % currentBarcodeData.UnitsPerBox) != unitsCount;

            if (!WMSClient.ServerInteraction.WriteStickerFact(acceptanceId, currentBarcodeData.StickerId, palletChanged,
                (cellItem ?? new CatalogItem()).Id, trayItem.Id, linerItem.Id, linersCount, packsCount, unitsCount + packsCount * currentBarcodeData.UnitsPerBox))
                {
                return false;
                }

            return true;
            }

        public int packsCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletControls.packsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletControls.packsCountTextBox.Text);
                }
            set
                {
                palletControls.packsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        public int unitsCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletControls.unitsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletControls.unitsCountTextBox.Text);
                }
            set
                {
                palletControls.unitsCountTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        public int linersCount
            {
            get
                {
                if (string.IsNullOrEmpty(palletControls.linersQuantityTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(palletControls.linersQuantityTextBox.Text);
                }
            set
                {
                palletControls.linersQuantityTextBox.Text = (value == 0) ? string.Empty : value.ToString();
                }
            }

        private bool initAcceptance(long stickerId)
            {
            return WMSClient.ServerInteraction.GetAcceptanceId(stickerId,
                out acceptanceId);
            }

        private void updateCellData()
            {
            palletControls.cellLabel.Text = (cellItem != null && cellItem.Id > 0) ? cellItem.Description : "<?>";
            }

        private void updateStickerData()
            {
            palletControls.nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            palletControls.trayButton.Text = currentBarcodeData.Tray.Description;
            trayItem = currentBarcodeData.Tray;

            palletControls.stickerIdInfoLabel.Text = string.Format("Код палети: {0}", currentBarcodeData.StickerId);

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

            updateLinerButton();
            }

        private void readStickerInfo(long acceptanceId, BarcodeData barcodeData, out CatalogItem cell)
            {
            string nomenclatureDescription;
            string trayDescription;
            long trayId;
            int unitsPerBox;
            string cellDescription;
            long cellId;

            if (
                !WMSClient.ServerInteraction.GetStickerData(acceptanceId, barcodeData.StickerId,
                    out nomenclatureDescription, out trayDescription, out trayId,
                    out unitsPerBox, out cellId, out cellDescription))
                {
                cell = new CatalogItem();
                barcodeData.StickerId = 0;
                return;
                }

            barcodeData.Nomenclature.Description = nomenclatureDescription;
            barcodeData.Tray = new CatalogItem()
                {
                    Id = trayId,
                    Description = trayDescription
                };

            barcodeData.UnitsPerBox = Convert.ToInt32(unitsPerBox);
            cell = new CatalogItem() { Description = cellDescription, Id = cellId };
            }

        #endregion


        }
    }