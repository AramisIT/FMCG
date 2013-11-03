using System.Drawing;
using System.Linq;
using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Приймання товару</summary>
    public class Acceptance : Process<AcceptanceData>
        {
        private MobileLabel nomenclatureLabel;
        private MobileControl packsCountTextBox;
        private MobileControl unitsCountTextBox;
        private MobileButton trayButton;
        private MobileControl linersQuantityTextBox;
        private MobileButton linerButton;
        private MobileLabel cellLabel;
        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        /// <summary>Приймання товару</summary>
        public Acceptance(WMSClient MainProcess)
            : base(MainProcess)
            {
            ToDoCommand = "Приймання товару";
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
            unitsCountTextBox = MainProcess.CreateTextBox(185, top, 50, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta + delta;
            trayButton = MainProcess.CreateButton("<піддон>", 5, top, 230, 35, "modelButton", trayButton_Click,
               new PropertyButtonInfo() { PropertyName = "Tray", PropertyDescription = "Тип піддону" });

            top += delta + delta;
            MainProcess.CreateLabel("Кількість прокладок:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            linersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += delta;
            linerButton = MainProcess.CreateButton("<тип прокладки>", 5, top, 230, 35, "modelButton", linerButton_Click,
               new PropertyButtonInfo() { PropertyName = "Liner", PropertyDescription = "Тип прокладки" });

            top += delta + delta;
            MainProcess.CreateLabel("Комірка:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void trayButton_Click(object sender)
            {

            }

        private void linerButton_Click(object sender)
            {

            }

        public override void OnBarcode(string Barcode) { }
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
                MainProcess, "Відскануйте свій штрих-код", string.Empty, afterReadUserBarcode);
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
            MainProcess.Process = new ReadBarcode(MainProcess, "Відскануйте товар", "Відміна", afterReadGoodsBarcode);
            }

        private void afterReadGoodsBarcode(string barcode)
            {
            long goodsId;
            string goodsDesc;

            if (CheckBarcodeForExistGoods(barcode, out goodsId, out goodsDesc))
                {
                //Перелік необхідної інформації для продовження:
                // - Комірка
                // - Номенклатура
                // - Дата 
                // - К-сть ящиків, банок
                // - Партія

                //Note: тимчасово (так як нема QRcode)
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