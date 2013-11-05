using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>��������� ������</summary>
    public class Acceptance : Process<AcceptanceData>
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
        private const string INVALID_BARCODE_MSG = "³����������� �����-��� �� �����";

        /// <summary>��������� ������</summary>
        public Acceptance(WMSClient MainProcess)
            : base(MainProcess)
            {
            ToDoCommand = "��������� ������";
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
            nomenclatureLabel = MainProcess.CreateLabel("<������������>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;

            MainProcess.CreateLabel("��������:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            packsCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            MainProcess.CreateLabel("+ ��.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            unitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta + delta;
            trayButton = MainProcess.CreateButton("<�����>", 5, top, 230, 35, "modelButton", trayButton_Click,
               new PropertyButtonInfo() { PropertyName = "Tray", PropertyDescription = "��� ������" });

            top += delta + delta;
            MainProcess.CreateLabel("ʳ������ ���������:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            linerItem = new CatalogItem();
            linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click,
               new PropertyButtonInfo() { PropertyName = "Liner", PropertyDescription = "��� ���������" });
            updateLinerButton();

            top += delta + delta;
            MainProcess.CreateLabel("������:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void updateLinerButton()
            {
            linerButton.Text = linerItem.Id > 0 ? linerItem.Description : "<��� ���������>";
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

                if (acceptanceId == 0 && !initAcceptance(barcodeData.StickerId))
                    {
                    return;
                    }

                currentBarcodeData = readStickerInfo(acceptanceId, barcodeData);
                if (currentBarcodeData.StickerId == 0)
                    {
                    return;
                    }
                updateStickerData();
                }
            else if (barcode.IsCell())
                {
                cellItem = barcode.ToCell();

                cellLabel.Text = cellItem.Id > 0 ? cellItem.Description : "<?>";
                }
            }

        private bool initAcceptance(long stickerId)
            {
            return WMSClient.ServerInteraction.GetAcceptanceId(stickerId,
                out acceptanceId);
            }

        private void updateStickerData()
            {
            nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            trayButton.Text = currentBarcodeData.Tray.Description;
            trayItem = currentBarcodeData.Tray;

            packsCountTextBox.Text = (currentBarcodeData.UnitsQuantity / currentBarcodeData.UnitsPerBox).ToString();
            unitsCountTextBox.Text = (currentBarcodeData.UnitsQuantity % currentBarcodeData.UnitsPerBox).ToString();
            linersQuantityTextBox.Text = string.Empty;
            linerItem = new CatalogItem();
            }

        private BarcodeData readStickerInfo(long acceptanceId, BarcodeData barcodeData)
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
                return new BarcodeData();
                }

            barcodeData.Nomenclature.Description = nomenclatureDescription;
            barcodeData.Tray = new CatalogItem()
                {
                    Id = trayId,
                    Description = trayDescription
                };

            barcodeData.UnitsPerBox = Convert.ToInt32(unitsPerBox);

            return barcodeData;
            }

        #endregion

        #region Stages
        private void selectCar(long selectedIndex, string description)
            {
            Data.Car = selectedIndex;
            readUserBarcode();
            }

        private void readUserBarcode()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new ReadBarcode(
                MainProcess, "³��������� ��� �����-���", string.Empty, afterReadUserBarcode);
            }

        private void afterReadUserBarcode(string barcode)
            {
            if (CheckBarcodeForExistUser(barcode))
                {
                Data.UserBarcode = barcode;
                readGoodsBarcode();
                }
            else
                {
                ShowMessage(INVALID_BARCODE_MSG);
                readUserBarcode();
                }
            }

        private void readGoodsBarcode()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new ReadBarcode(MainProcess, "³��������� �����", "³����", afterReadGoodsBarcode);
            }

        private void afterReadGoodsBarcode(string barcode)
            {
            long goodsId;
            string goodsDesc;

            if (CheckBarcodeForExistGoods(barcode, out goodsId, out goodsDesc))
                {
                //������ ��������� ���������� ��� �����������:
                // - ������
                // - ������������
                // - ���� 
                // - �-��� �����, �����
                // - �����

                //Note: ��������� (��� �� ���� QRcode)
                Random rand = new Random();
                Data.BoxCount = rand.Next(0, 100);
                Data.BottleCount = rand.Next(0, 100);
                Data.Party = rand.Next(1, 5);
                Data.Goods = new KeyValuePair<long, string>(goodsId, goodsDesc);

                long incomeDoc;
                string date;
                long cellId;
                string cell;
                long palett;

                PerformQuery("GetAdditionalInfoAboutAccepnedGoods", Data.BoxCount, Data.Goods.Key, Data.Car, Data.Party);

                if (GetAdditionalInfoAboutAccepnedGoods(Data.BoxCount, Data.Goods.Key, Data.Car, Data.Party,
                                                        out incomeDoc, out date, out cellId, out cell, out palett))
                    {
                    Data.ConsignmentNote = incomeDoc;
                    Data.Date = date;
                    Data.Place = new PlaningData<long>(palett);
                    Data.Cell = new KeyValuePair<long, string>(cellId, cell);

                    checkCell();
                    }
                }
            else
                {
                ShowMessage(INVALID_BARCODE_MSG);
                readGoodsBarcode();
                }
            }

        private void checkCell()
            {
            Data.PermitInstallPalletManually = GetPermitInstallPalletManually();
            MainProcess.ClearControls();
            MainProcess.Process = new Placing(MainProcess, Data, finish);
            }

        private void finish()
            {
            save();
            readGoodsBarcode();
            }

        private void save()
            {
            SetAcceptanceData(
                Data.Goods.Key,
                Data.Party,
                Data.BoxCount,
                Data.BottleCount,
                Data.ConsignmentNote,
                Data.Place.Fact,
                Data.Cell.Key,
                Data.IsCell);
            }
        #endregion
        }
    }