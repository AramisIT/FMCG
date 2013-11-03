using System;
using WMS_client.Delegates;

namespace WMS_client.Processes.BaseScreen
    {
    /// <summary>Переміщення палети</summary>
    internal class SelectedGoodsInfo : BusinessProcess
        {
        #region Properties
        private readonly SelectionData processData;
        private readonly ReadBarcodeDelegate navigateToScreen;
        private readonly bool editMode;
        private MobileTextBox boxes;
        private MobileTextBox bottle;
        #endregion

        /// <summary>Переміщення палети</summary>
        public SelectedGoodsInfo(WMSClient MainProcess, SelectionData data, ReadBarcodeDelegate nextScr)
            : base(MainProcess, 1)
            {
            processData = data;
            MainProcess.ToDoCommand = data.Topic;
            navigateToScreen = nextScr;
            editMode = false;
            isLoading = true;
            DrawControls();
            }

        /// <summary>Переміщення палети</summary>
        public SelectedGoodsInfo(WMSClient MainProcess, SelectionData data, ReadBarcodeDelegate nextScr, bool edit)
            : base(MainProcess, 1)
        {
            processData = data;
            MainProcess.ToDoCommand = data.Topic;
            navigateToScreen = nextScr;
            editMode = edit;
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
                string cell = string.Concat("Комірка: ", processData.Cell);
                MainProcess.CreateLabel(cell, 5, 60, 230, MobileFontColors.Default);
                //Назва
                MainProcess.CreateLabel(
                    processData.GoodsDescription, 5, 90, 230, MobileFontSize.Normal,
                    MobileFontPosition.Center, MobileFontColors.Info);
                //Дата
                string date = string.Format("Дата: {0}", processData.Date);
                MainProcess.CreateLabel(date, 5, 120, 230, MobileFontColors.Default);
                //К-сть
                if (editMode)
                    {
                    //К-сть ящ
                    MainProcess.CreateLabel("К-сть ящ.:", 5, 150, 90, MobileFontColors.Default);
                    boxes =
                        (MobileTextBox)
                        MainProcess.CreateTextBox(100, 150, 135, string.Empty, ControlsStyle.LabelH2Red, null, false);
                    boxes.Text = processData.BoxCount.ToString();
                    //К-сть бут
                    MainProcess.CreateLabel("К-сть бут.:", 5, 180, 90, MobileFontColors.Default);
                    bottle =
                        (MobileTextBox)
                        MainProcess.CreateTextBox(100, 180, 135, string.Empty, ControlsStyle.LabelH2Red, null, false);
                    bottle.Text = processData.UnitCount.ToString();
                    //Далі
                    MainProcess.CreateButton("Далі (F10)", 5, 285, 230, 30, string.Empty, MobileButtonClick);
                    }
                else
                    {
                    string count = string.Format("К-сть: {0}ящ. по {1} {2}", processData.BoxCount, processData.BoxSize,
                                                 processData.UnitCount == 0
                                                     ? string.Empty
                                                     : string.Format(" + {0}од.", processData.UnitCount));
                    MainProcess.CreateLabel(count, 5, 150, 230, MobileFontColors.Default);
                    //Повідомлення
                    MainProcess.CreateLabel(
                        "Відскануйте палету", 5, 260, 230,
                        MobileFontSize.Multiline, MobileFontPosition.Center, MobileFontColors.Info);
                    }
                }
            }

        private void MobileButtonClick()
            {
            processData.BoxCount = string.IsNullOrEmpty(boxes.Text) ? 0: Convert.ToDouble(boxes.Text);
            processData.UnitCount = string.IsNullOrEmpty(bottle.Text) ? 0 : Convert.ToDouble(bottle.Text);
            navigateToScreen(string.Empty);
            }

        public override void OnBarcode(string Barcode)
            {
            if(!editMode)
                {
                navigateToScreen(Barcode);
                }
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
        }
    }