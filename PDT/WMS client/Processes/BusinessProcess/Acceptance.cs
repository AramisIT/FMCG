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
        private MobileLabel nomenclatureLabel;
        private MobileTextBox packsCountTextBox;
        private MobileTextBox unitsCountTextBox;
        private MobileButton trayButton;
        private MobileTextBox linersQuantityTextBox;
        private MobileButton linerButton;
        private MobileLabel cellLabel;
        private CatalogItem trayItem;
        private CatalogItem linerItem;
        private CatalogItem cellItem;
        private BarcodeData currentBarcodeData;
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
            //repository.GetTraysList();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            int top = 42;
            const int delta = 27;

            top += delta;
            nomenclatureLabel = MainProcess.CreateLabel("<номенклатура>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;

            MainProcess.CreateLabel("упаковок:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            packsCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            MainProcess.CreateLabel("+ шт.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            unitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta + delta;
            trayButton = MainProcess.CreateButton("<піддон>", 5, top, 230, 35, "modelButton", trayButton_Click,
               new PropertyButtonInfo() { PropertyName = "Tray", PropertyDescription = "Тип піддону" });

            top += delta + delta;
            MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            linerItem = new CatalogItem();
            linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click,
               new PropertyButtonInfo() { PropertyName = "Liner", PropertyDescription = "Тип прокладки" });
            updateLinerButton();

            top += delta + delta;
            MainProcess.CreateLabel("Комірка:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Complate:
                    complateProcess();
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
            linerButton.Text = linerItem.Id > 0 ? linerItem.Description : "<тип прокладки>";
            }

        private void trayButton_Click(object sender)
            {
            selectFromCatalog(new Repository().GetTraysList(), (selectedItem) =>
                {
                    trayButton.Text = selectedItem.Description;
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
                linerButton.Text = selectedItem.Description;
                linerItem = selectedItem;
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

                var scannedNextSticker = cell.Id == 0;
                if (scannedNextSticker)
                    {
                    if (!saveFact())
                        {
                        "Необхідно знаходитись у зоні Wi-Fi".Warning();
                        return;
                        }
                    currentBarcodeData = barcodeData;
                    updateStickerData();
                    }
                else
                    {
                    cellItem = cell;
                    }
                }
            else if (barcode.IsCell())
                {
                cellItem = barcode.ToCell();
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
                (cellItem ?? new CatalogItem()).Id, trayItem.Id, linerItem.Id, linersQuantity, packsCount, unitsCount + packsCount * currentBarcodeData.UnitsPerBox))
                {
                return false;
                }

            return true;
            }

        public int packsCount
            {
            get
                {
                if (string.IsNullOrEmpty(packsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(packsCountTextBox.Text);
                }
            set
                {
                packsCountTextBox.Text = value.ToString();
                }
            }

        public int unitsCount
            {
            get
                {
                if (string.IsNullOrEmpty(unitsCountTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(unitsCountTextBox.Text);
                }
            set
                {
                unitsCountTextBox.Text = value.ToString();
                }
            }

        public int linersQuantity
            {
            get
                {
                if (string.IsNullOrEmpty(linersQuantityTextBox.Text))
                    {
                    return 0;
                    }

                return Convert.ToInt32(linersQuantityTextBox.Text);
                }
            }

        private bool initAcceptance(long stickerId)
            {
            return WMSClient.ServerInteraction.GetAcceptanceId(stickerId,
                out acceptanceId);
            }

        private void updateCellData()
            {
            cellLabel.Text = cellItem.Id > 0 ? cellItem.Description : "<?>";
            }

        private void updateStickerData()
            {
            nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            trayButton.Text = currentBarcodeData.Tray.Description;
            trayItem = currentBarcodeData.Tray;

            packsCount = (currentBarcodeData.UnitsQuantity / currentBarcodeData.UnitsPerBox);
            unitsCount = (currentBarcodeData.UnitsQuantity % currentBarcodeData.UnitsPerBox);
            linersQuantityTextBox.Text = string.Empty;
            linerItem = new CatalogItem();
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
                cell = null;
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