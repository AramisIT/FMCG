using WMS_client.Delegates;

namespace WMS_client.Processes.BaseScreen
    {
    /// <summary>Інвентаризація паллет</summary>
    internal class InventoryOfPallet : BusinessProcess
        {
        #region Properties
        private readonly InventoryData processData;
        private readonly EnterDataDelegate navigateToScreen;
        private MobileTextBox countBox;
        #endregion

        /// <summary>Інвентаризація паллет</summary>
        public InventoryOfPallet(WMSClient MainProcess, InventoryData data, EnterDataDelegate nextScr)
            : base(MainProcess, 1)
            {
            processData = data;
            MainProcess.ToDoCommand = data.Topic;
            navigateToScreen = nextScr;

            IsLoad = true;
            DrawControls();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (IsLoad)
                {
                MainProcess.ToDoCommand = processData.Topic;
                //Назва
                MainProcess.CreateLabel(
                    processData.Goods, 5, 90, 230, MobileFontSize.Normal,
                    MobileFontPosition.Center, MobileFontColors.Info);
                //Дата
                string date = string.Format("Дата: {0}", processData.Date);
                MainProcess.CreateLabel(date, 5, 120, 230, MobileFontColors.Default);
                //К-сть
                string countLabel = string.Format("К-сть ({0}):", processData.Measure);
                MainProcess.CreateLabel(countLabel, 5, 150, 110, MobileFontColors.Default);
                countBox = (MobileTextBox)MainProcess.CreateTextBox(115, 150, 120, string.Empty, ControlsStyle.LabelNormal, null, false);
                countBox.Text = "0";
                //Інфо
                MainProcess.CreateLabel(
                    "Перерахуйте і вкажіть кількість", 5, 200, 230,
                    MobileFontSize.Multiline, MobileFontPosition.Center, MobileFontColors.Info);
                //Кнопка
                MainProcess.CreateButton("Далі (F1)", 5, 285, 230, 30, string.Empty, next);

                countBox.Focus();
                }
            }

        public override void OnBarcode(string Barcode)
            {
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

        private void next()
            {
            processData.Count = new PlaningData<double>(processData.Count.Plan, double.Parse(countBox.Text));
            navigateToScreen();
            }
        }
    }