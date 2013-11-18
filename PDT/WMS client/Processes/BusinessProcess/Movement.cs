//using WMS_client.Processes.BaseScreen;
//using System;

//namespace WMS_client.Processes
//    {
//    /// <summary>����������</summary>
//    public class Movement : Process<MovementData>
//        {
//        /// <summary>����������</summary>
//        public Movement()
//            : base()
//            {
//            Data.Topic = "����������";
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
//                "³��������� �����", 0, 175, 240, MobileFontSize.Multiline, MobileFontPosition.Center,
//                MobileFontColors.Info);

//            MainProcess.CreateButton("��������� (F10)", 5, 285, 230, 30, string.Empty, back);
//            }

//        /// <summary>������� �����������. ���</summary>
//        /// <param name="barcode">�����-��� �������</param>
//        private void afterGoodsScanin(string barcode)
//            {
//            PerformQuery("CheckPalletBarcodeForMoving", barcode);

//            if (IsAnswerIsTrue)
//                {
//                Data.PalletId = Convert.ToInt64(Parameters[1]);
//                chooseNewPosition();
//                }
//            }

//        /// <summary>������ ���� �������</summary>
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

//        /// <summary>������� ������</summary>
//        /// <param name="barcode">�����-��� ���� �������</param>
//        private void afterChooseNewPosition(string barcode)
//            {
//            //��������
//            long palletId;
//            bool isCell;

//            if (CheckPalletBarcodeForMoving(barcode, true, out palletId, out isCell))
//                {
//                Data.PreviousPalletId = palletId;
//                Data.IsCell = isCell;

//                if (Data.PalletId != Data.PreviousPalletId || Data.IsCell)
//                    {
//                    //����������
//                    SetMoving(Data.PalletId, Data.PreviousPalletId, Data.IsCell);
//                    //�������� �����
//                    Data = new MovementData();
//                    //���
//                    MainProcess.Process = this;
//                    chooseGoods();
//                    }
//                else
//                    {
//                    ShowMessage("�� ����������� ������ ��� ����������!");
//                    }
//                }
//            else
//                {
//                ShowMessage("�����-��� �� �����! ³��������� ������� �� ������");
//                }
//            }
//        #endregion
//        }
//    }