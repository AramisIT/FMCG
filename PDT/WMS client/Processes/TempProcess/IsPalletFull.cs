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
            ToDoCommand = "�������� �����-����";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;
            

            top = 230;
            taskLabel = MainProcess.CreateLabel("�������� ������", 10, top, 220,
                MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Warning, FontStyle.Bold);
            }


        protected override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");
            if (!barcode.IsSticker())
                {
                "�������� ������!".ShowMessage();
                return;
                }

            var fullPallet = "������ ����������?".Ask();

            if (!Program.AramisSystem.SetPalletStatus(barcode.ToBarcodeData().StickerId, fullPallet))
                {
                "��� ����� � ��������".Warning();
                }
            }

        protected override void OnHotKey(KeyAction TypeOfAction)
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