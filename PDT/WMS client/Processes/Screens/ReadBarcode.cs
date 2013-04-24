using WMS_client.Delegates;

namespace WMS_client.Processes.BaseScreen
    {
    /// <summary>Сканування штрих-коду</summary>
    internal class ReadBarcode : BusinessProcess
        {
        #region Properties
        private readonly ReadBarcodeDelegate navigateToNextScreen;
        private readonly string message;
        private readonly string infoMessage;
        private readonly string goBackMessage;
        #endregion

        /// <summary>Сканування штрих-коду</summary>
        public ReadBarcode(WMSClient MainProcess, string msg, string backMsg, ReadBarcodeDelegate nextScr)
            : base(MainProcess, 1)
            {
            message = msg;
            goBackMessage = backMsg;
            navigateToNextScreen = nextScr;

            IsLoad = true;
            DrawControls();
            }

        /// <summary>Сканування штрих-коду</summary>
        public ReadBarcode(WMSClient MainProcess, string msg, string info, string backMsg, ReadBarcodeDelegate nextScr)
            : base(MainProcess, 1)
        {
            message = msg;
            infoMessage = info;
            goBackMessage = backMsg;
            navigateToNextScreen = nextScr;

            IsLoad = true;
            DrawControls();
        }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (IsLoad)
                {
                int top = 175;

                if (!string.IsNullOrEmpty(goBackMessage))
                    {
                    MainProcess.CreateButton(goBackMessage, 5, 285, 230, 30, string.Empty, MobileButtonClick);
                    top -= 25;
                    }

                if (!string.IsNullOrEmpty(infoMessage))
                    {
                    MainProcess.CreateLabel(
                        infoMessage, 0, 250, 240, MobileFontSize.Normal, MobileFontPosition.Center,MobileFontColors.Info);
                    top -= 25;
                    }

                MainProcess.CreateLabel(
                    message, 0, top, 240, MobileFontSize.Multiline, MobileFontPosition.Center, MobileFontColors.Info);
                }
            }

        public override void OnBarcode(string Barcode)
        {
            navigateToNextScreen(Barcode);
        }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                    case KeyAction.Esc:
                        MainProcess.ClearControls();
                        MainProcess.Process = new SelectingProcess(MainProcess);
                        break;
                }
            }
        #endregion

        private void MobileButtonClick()
            {
            OnHotKey(KeyAction.Esc);
            }
        }
    }