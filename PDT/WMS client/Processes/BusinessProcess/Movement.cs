//using WMS_client.Processes.BaseScreen;
//using System;

//namespace WMS_client.Processes
//    {
//    /// <summary>Переміщення</summary>
//    public class Movement : Process<MovementData>
//        {
//        /// <summary>Переміщення</summary>
//        public Movement()
//            : base()
//            {
//            Data.Topic = "Переміщення";
//            isLoading = true;
//            DrawControls();
//            }

//        #region Override methods
//        public override sealed void DrawControls()
//            {
//            if (isLoading)
//                {
//                chooseGoods();
//                }
//            }

//        public override void OnBarcode(string Barcode)
//            {
//            afterGoodsScanin(Barcode);
//            }
//        #endregion

//        #region Stages
//        private void back()
//            {
//            OnHotKey(KeyAction.Esc);
//            }

//        private void chooseGoods()
//            {
//            MainProcess.ClearControls();
//            MainProcess.ToDoCommand = Data.Topic;
//            MainProcess.CreateLabel(
//                "Відскануйте товар", 0, 175, 240, MobileFontSize.Multiline, MobileFontPosition.Center,
//                MobileFontColors.Info);

//            MainProcess.CreateButton("Завершити (F10)", 5, 285, 230, 30, string.Empty, back);
//            }

//        /// <summary>Паллету відскановано. Далі</summary>
//        /// <param name="barcode">Штрих-код паллети</param>
//        private void afterGoodsScanin(string barcode)
//            {
//            PerformQuery("CheckPalletBarcodeForMoving", barcode);

//            if (IsAnswerIsTrue)
//                {
//                Data.PalletId = Convert.ToInt64(Parameters[1]);
//                chooseNewPosition();
//                }
//            }

//        /// <summary>Обрати нову позиції</summary>
//        private void chooseNewPosition()
//        {
//            string goods;
//            string date;
//            double boxCount;
//            double bottleCount;

//            if (GetDataAboutMovingPallet((int)Data.PalletId, out goods, out date, out boxCount, out bottleCount))
//                {
//                Data.GoodsDescription = goods;
//                Data.Date = date;
//                Data.BoxCount = boxCount;
//                Data.BottleCount = bottleCount;
//                }

//            MainProcess.ClearControls();
//            MainProcess.Process = new MovingPallet(MainProcess, Data, afterChooseNewPosition);
//            }

//        /// <summary>Позицію обрано</summary>
//        /// <param name="barcode">Штрих-код нової позиції</param>
//        private void afterChooseNewPosition(string barcode)
//            {
//            //Перевірка
//            long palletId;
//            bool isCell;

//            if (CheckPalletBarcodeForMoving(barcode, true, out palletId, out isCell))
//                {
//                Data.PreviousPalletId = palletId;
//                Data.IsCell = isCell;

//                if (Data.PalletId != Data.PreviousPalletId || Data.IsCell)
//                    {
//                    //Збереження
//                    SetMoving(Data.PalletId, Data.PreviousPalletId, Data.IsCell);
//                    //Очещення даних
//                    Data = new MovementData();
//                    //Далі
//                    MainProcess.Process = this;
//                    chooseGoods();
//                    }
//                else
//                    {
//                    ShowMessage("Ви відсканували палету яку переміщуюте!");
//                    }
//                }
//            else
//                {
//                ShowMessage("Штрих-код не вірний! Відскануйте паллету чи комірку");
//                }
//            }
//        #endregion
//        }
//    }