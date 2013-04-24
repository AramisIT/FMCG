using System;
using System.Collections.Generic;
using WMS_client.Delegates;

namespace WMS_client.Processes.BaseScreen
    {
    /// <summary>Вибір з таблиці</summary>
    internal class Placing : BusinessProcess
        {
        #region Properties
        private readonly AcceptanceData processData;
        private readonly EnterDataDelegate navigateToScreen;
        private MobileTextBox boxes;
        private MobileTextBox bottle;
        private const string CELLS_TYPE_NAME = "AtosFMCG.DatabaseObjects.Catalogs.Cells";
        #endregion

        /// <summary>Вибір з таблиці</summary>
        public Placing(WMSClient MainProcess, AcceptanceData data, EnterDataDelegate nextScr)
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
                //Комірка
                string cell = processData.Cell.Key ==0  ? "<?>" : processData.Cell.Value;
                MainProcess.CreateLabel("Комірка: ", 5, 60, 60, MobileFontColors.Default);
                MainProcess.CreateLabel(
                    cell, 70, 60, 200, MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Info);
                //Назва
                MainProcess.CreateLabel(
                    processData.Goods.Value, 5, 90, 230, MobileFontSize.Normal,
                    MobileFontPosition.Center, MobileFontColors.Info);
                //Дата
                MainProcess.CreateLabel("Дата: ", 5, 120, 60, MobileFontColors.Default);
                MainProcess.CreateLabel(
                    processData.Date, 70, 120, 200,
                    MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Info);
                //К-сть ящ
                MainProcess.CreateLabel("К-сть ящ.:", 5, 150, 90, MobileFontColors.Default);
                boxes = (MobileTextBox)MainProcess.CreateTextBox(100, 150, 135, string.Empty, ControlsStyle.LabelH2Red, null , false);
                boxes.Text = processData.BoxCount.ToString();
                //К-сть бут
                MainProcess.CreateLabel("К-сть бут.:", 5, 180, 90, MobileFontColors.Default);
                bottle = (MobileTextBox)MainProcess.CreateTextBox(100, 180, 135, string.Empty, ControlsStyle.LabelH2Red, null, false);
                bottle.Text = processData.BottleCount.ToString();
                
                int infoTop;
                if(processData.PermitInstallPalletManually)
                    {
                    infoTop = 215;
                    MainProcess.CreateButton("Встановити вручну", 5, 280, 230, 30, string.Empty, installManual);
                    }
                else
                    {
                    infoTop = 260;
                    }

                //Інфо
                MainProcess.CreateLabel(
                    "Відскануйте комірку чи палету", 5, infoTop, 230,
                    MobileFontSize.Multiline, MobileFontPosition.Center, MobileFontColors.Info);
                boxes.Focus();
                }
            }

        public override void OnBarcode(string Barcode)
        {
            string message = string.Format(
                "Відсканована палета має бути в комірці '{0}'. Відскануйте комірку чи палету повторно!",
                processData.Cell.Value);
            palletInstalation(Barcode, false, message);
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

        private void installManual()
        {
            MainProcess.ClearControls();
            MainProcess.Process = new ReadBarcode(MainProcess, "Відскануйте паллету чи комірку", "Відміна", cellIsSelected);
        }

        private void cellIsSelected(string barcode)
        {
            palletInstalation(barcode, true, "Не вірний штрих-код!");
        }

        /// <summary>Встановлення паллети</summary>
        /// <param name="barcode">Штрихкод</param>
        /// <param name="manualInstall">Дозволено встановлення палети вручну</param>
        /// <param name="message">Повідомлення при невірній дії</param>
        private void palletInstalation(string barcode, bool manualInstall, string message)
        {
            string type;
            long id;

            //Note: зараз 2D кодів ще нема, тому сканується або комірка або палета
            if (GetPlaceDataFromCode(barcode, out type, out id))
                {
                bool valid = manualInstall
                                 ? (type == typeof (long).ToString() || type == CELLS_TYPE_NAME) && id != 0
                                 : (type == typeof (long).ToString()
                                        ? processData.Place.Plan == id
                                        : processData.Cell.Key == id);

                if (valid)
                    {
                    processData.IsCell = type != typeof (long).ToString();
                    processData.Place = new PlaningData<long>(processData.Place.Plan, id);

                    if (manualInstall && processData.IsCell)
                        {
                        processData.Cell = new KeyValuePair<long, string>(id, string.Empty);
                        }

                    navigateToScreen();
                    return;
                    }
                }

            ShowMessage(message);
        }
        }
    }