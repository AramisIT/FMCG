using WMS_client.Delegates;

namespace WMS_client.Processes.BaseScreen
    {
    /// <summary>Переміщення палети</summary>
    internal class MovingPallet : BusinessProcess
        {
        #region Properties
        private readonly MovementData processData;
        private readonly ReadBarcodeDelegate navigateToScreen;
        #endregion

        /// <summary>Переміщення палети</summary>
        public MovingPallet(WMSClient MainProcess, MovementData data, ReadBarcodeDelegate nextScr)
            : base(1)
            {
            processData = data;
            MainProcess.ToDoCommand = data.Topic;
            navigateToScreen = nextScr;

            isLoading = true;
            DrawControls();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (isLoading)
                {
                MainProcess.ToDoCommand = processData.Topic;
                //Комірка
                MainProcess.CreateLabel("Комірка: <?>", 5, 60, 230, MobileFontColors.Default);
                //Назва
                MainProcess.CreateLabel(
                    processData.GoodsDescription, 5, 90, 230, MobileFontSize.Normal,
                    MobileFontPosition.Center, MobileFontColors.Info);
                //Дата
                string date = string.Format("Дата: {0}", processData.Date);
                MainProcess.CreateLabel(date, 5, 120, 230, MobileFontColors.Default);
                //К-сть
                string boxCoutn = string.Format("К-сть ящ.: {0:#,0.##}", processData.BoxCount);
                string bottleCoutn = string.Format("К-сть бут.: {0:#,0.##}", processData.BottleCount);
                MainProcess.CreateLabel(boxCoutn, 5, 150, 230, MobileFontColors.Default);
                MainProcess.CreateLabel(bottleCoutn, 5, 180, 230, MobileFontColors.Default);
                MainProcess.CreateLabel(
                    "Відскануйте комірку чи палету", 5, 260, 230,
                    MobileFontSize.Multiline, MobileFontPosition.Center, MobileFontColors.Info);
                }
            }

        public override void OnBarcode(string Barcode)
            {
            navigateToScreen(Barcode);
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                    case KeyAction.Esc:
                        MainProcess.ClearControls();
                        MainProcess.Process = new SelectingProcess();
                        break;
                }
            }
        #endregion
        }
    }