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
            ToDoCommand = "Перевірка штрих-коду";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;
            const int delta = 27;

            top += delta;
            infoLabel = MainProcess.CreateLabel(string.Empty, 0, top, 240,
                MobileFontSize.Little, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            additionalInfoLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top = 230;
            taskLabel = MainProcess.CreateLabel(string.Empty, 10, top, 220,
               MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Warning, FontStyle.Bold);

            top += delta * 2;
            MainProcess.CreateLabel("Завершення операції  -  Esc", 10, top, 220,
               MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Default, FontStyle.Regular);

            showStartProcessMessage();
            }

        private void showStartProcessMessage()
            {
            taskLabel.Text = "Скануйте продукцію";
            additionalInfoLabel.Text = string.Empty;
            wareBarcode = null;
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            var waitForWareBarcode = string.IsNullOrEmpty(wareBarcode);
            if (waitForWareBarcode)
                {
                if (barcode.IsSticker() || barcode.Length != 13)
                    {
                    "Відскануйте одиницю товару!".ShowMessage();
                    return;
                    }
                handleWareBarcode(barcode);
                }
            else
                {
                if (!barcode.IsSticker())
                    {
                    "Скануйте палету!".ShowMessage();
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

            infoLabel.Text = recordWasAdded ? "Штрих-код записано" : "Штрих-код існує";
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
                var wareName =table.Rows[0]["Description"] as string;
                infoLabel.Text = wareName.Substring(0, Math.Min(wareName.Length, 40));
                }
            additionalInfoLabel.Text = string.Format("Всього позицій: {0}", table.Rows.Count);
            taskLabel.Text = "Скануйте палету";
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