using System.Collections.Generic;
using WMS_client.Processes.BaseScreen;
using System;

namespace WMS_client.Processes
    {
    /// <summary>��������������</summary>
    public class Inventory : Process<InventoryData>
        {
        public int countOfDiscrepancy;

        /// <summary>��������������</summary>
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
            Data = new InventoryData("��������������");
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

                MainProcess.CreateLabel("������:", 0, 100, 240, MobileFontSize.Large, MobileFontPosition.Center);
                MainProcess.CreateLabel(Data.Cell.Value, 0, 140, 240, MobileFontSize.Large, MobileFontPosition.Center,
                                        MobileFontColors.Info);
                MainProcess.CreateLabel("³��������� ������ �������", 0, 225, 240, MobileFontSize.Normal,
                                        MobileFontPosition.Center);
                MainProcess.CreateButton("�� ������ ������� (Esc)", 5, 285, 230, 30, string.Empty, back);
                }
            else
                {
                string finishMessage = countOfDiscrepancy == 0
                                           ? "�������� ���������."
                                           : string.Format(
                                               "�������� ���������.\r\n�������� ����������� � {0} �������",
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
                    ShowMessage(string.Format("³���������� ������� �� ������� � ����������..."));
                    }
                }
            else
                {
                ShowMessage("³��������� �������!");
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
                            "�������� ��������� � ������ �������. �������� ������������� (�������� {0} �����)!",
                            Data.AttemptsRemaining);
                    ShowMessage(message);
                    Data.NumberOfAttempts++;
                    return;
                    }

                ShowMessage("�������� ��������� � ������ �������. ���� ������ ��������...");
                countOfDiscrepancy++;
                }

            PerformQuery("SetInventory", Data.DocInfo.Key, Data.DocInfo.Value, Data.Count.Fact);
            MainProcess.Process = this;
            palletScaning();
            }
        #endregion
        }
    }