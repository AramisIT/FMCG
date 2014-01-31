using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class IsPalletFull : BusinessProcess
        {
        private MobileLabel taskLabel;

        public IsPalletFull()
            : base(1)
            {
            ToDoCommand = "Перевірка штрих-коду";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;
            const int delta = 27;

            top = 230;
            taskLabel = MainProcess.CreateLabel("Скануйте палету", 10, top, 220,
                MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Warning, FontStyle.Bold);
            }


        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");
            if (!barcode.IsSticker())
                {
                "Скануйте палету!".ShowMessage();
                return;
                }

            var fullPallet = "Палета запакована?".Ask();

            new ServerInteraction().SetPalletStatus(barcode.ToBarcodeData().StickerId, fullPallet);
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