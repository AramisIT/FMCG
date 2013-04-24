using System.Collections.Generic;
using WMS_client.Processes.BaseScreen;
using System;

namespace WMS_client.Processes
    {
    /// <summary>Інвентаризація</summary>
    public class Inventory : Process<InventoryData>
        {
        public int countOfDiscrepancy;

        /// <summary>Інвентаризація</summary>
        public Inventory(WMSClient MainProcess)
            : base(MainProcess)
            {
            IsLoad = true;
            DrawControls();
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (IsLoad)
                {
                palletScaning();
                }
            }

        public override void OnBarcode(string Barcode)
            {
            enterScreen(Barcode);
            }
        #endregion

        #region Stages
        private void back()
            {
            OnHotKey(KeyAction.Esc);
            }

        private void palletScaning()
            {
            PerformQuery("GetDataForInventory");
            Data = new InventoryData("Інвентаризація");
            MainProcess.ClearControls();

            if (IsAnswerIsTrue)
                {
                Data.Count = new PlaningData<double>(Convert.ToDouble(Parameters[1]));
                Data.Goods = Parameters[2].ToString();
                Data.Date = Parameters[3].ToString();
                Data.PalletId = Convert.ToInt64(Parameters[4]);
                Data.Measure = Parameters[5].ToString();
                Data.DocInfo = new KeyValuePair<long, long>(Convert.ToInt64(Parameters[6]),
                                                            Convert.ToInt64(Parameters[7]));
                Data.Cell = new KeyValuePair<long, string>(Convert.ToInt64(Parameters[8]), Parameters[9].ToString());

                MainProcess.CreateLabel("Комірка:", 0, 100, 240, MobileFontSize.Large, MobileFontPosition.Center);
                MainProcess.CreateLabel(Data.Cell.Value, 0, 140, 240, MobileFontSize.Large, MobileFontPosition.Center,
                                        MobileFontColors.Info);
                MainProcess.CreateLabel("Відскануйте крайню паллету", 0, 225, 240, MobileFontSize.Normal,
                                        MobileFontPosition.Center);
                MainProcess.CreateButton("До вибору процесів (Esc)", 5, 285, 230, 30, string.Empty, back);
                }
            else
                {
                string finishMessage = countOfDiscrepancy == 0
                                           ? "Операція завершена."
                                           : string.Format(
                                               "Операція завершена.\r\nВиявлено розходження в {0} комірках",
                                               countOfDiscrepancy);

                ShowMessage(finishMessage);
                back();
                }
            }

        private void enterScreen(string barcode)
            {
            bool validPallet;

            if (CheckInventoryPallet(barcode, Data.Cell.Key, out validPallet))
                {
                if (validPallet)
                    {
                    showEnterScreen();
                    }
                else
                    {
                    ShowMessage(string.Format("Відсканована паллета не співпадає з очікуваною..."));
                    }
                }
            else
                {
                ShowMessage("Відскануйте паллету!");
                }
            }

        private void showEnterScreen()
        {
            MainProcess.ClearControls();
            MainProcess.Process = new InventoryOfPallet(MainProcess, Data, afterEntedData);
        }

        private void afterEntedData()
            {
            if (!Data.Count.IsEqual)
                {
                if (Data.CanRepeatAttempt)
                    {
                    string message =string.Format(
                            "Виявлено розбіжність з даними системи. Повторіть перерахування (лишилось {0} спроб)!",
                            Data.AttemptsRemaining);
                    ShowMessage(message);
                    Data.NumberOfAttempts++;
                    return;
                    }

                ShowMessage("Виявлено розбіжність з даними системи. Данні будуть збережені...");
                countOfDiscrepancy++;
                }

            PerformQuery("SetInventory", Data.DocInfo.Key, Data.DocInfo.Value, Data.Count.Fact);
            MainProcess.Process = this;
            palletScaning();
            }
        #endregion
        }
    }