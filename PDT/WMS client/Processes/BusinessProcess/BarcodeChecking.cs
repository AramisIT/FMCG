using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class BarcodeChecking : BusinessProcess
        {
        private MobileLabel infoLabel;
        private MobileLabel additionalInfoLabel;
        private MobileLabel taskLabel;
        private string wareBarcode;

        public BarcodeChecking()
            : base(1)
            {
            ToDoCommand = "�������� �����-����";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;
            const int delta = 27;

            top += delta;
            infoLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
                MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            additionalInfoLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top = 250;
            taskLabel = MainProcess.CreateLabel(string.Empty, 10, top, 220,
               MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Warning, FontStyle.Bold);

            showStartProcessMessage();
            }

        private void showStartProcessMessage()
            {
            taskLabel.Text = "�������� �����-��� ������";
            additionalInfoLabel.Text = string.Empty;
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            var waitForWareBarcode = string.IsNullOrEmpty(wareBarcode);
            if (waitForWareBarcode)
                {
                if (barcode.IsSticker() || barcode.Length != 13)
                    {
                    "³��������� ������� ������!".ShowMessage();
                    return;
                    }
                handleWareBarcode(barcode);
                }
            else
                {
                if (!barcode.IsSticker())
                    {
                    "�������� ������!".ShowMessage();
                    return;
                    }
                handlePalletBarcode(barcode);
                }
            }

        private void handlePalletBarcode(string barcode)
            {
            bool recordWasAdded;
            if (!new ServerInteraction().SetBarcode(wareBarcode, barcode.ToBarcodeData().StickerId, out recordWasAdded))
                {
                return;
                }

            infoLabel.Text = recordWasAdded ? "�����-��� ��������" : "�����-��� ����";
            showStartProcessMessage();
            }

        private void handleWareBarcode(string barcode)
            {
            var table = new ServerInteraction().GetWares(barcode);
            if (table == null)
                {
                return;
                }

            wareBarcode = barcode;
            if (table.Rows.Count > 0)
                {
                infoLabel.Text = table.Rows[0]["Description"] as string;
                }
            additionalInfoLabel.Text = string.Format("������ �������: {0}", table.Rows.Count);
            taskLabel.Text = "�������� ������";
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Complate:
                    exit();
                    return;

                case KeyAction.Esc:
                    exit();
                    return;
                }
            }

        #endregion

        private void exit()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new SelectingProcess();
            }
        }
    }