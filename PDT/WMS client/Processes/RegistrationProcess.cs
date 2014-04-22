using System;
using System.Data;
using System.Diagnostics;
using WMS_client.HelperClasses;

namespace WMS_client.Processes
    {
    /// <summary>Регистрация при входе</summary>
    public class RegistrationProcess : BusinessProcess
        {
        #region Public methods

        /// <summary>Регистрация при входе</summary>
        public RegistrationProcess()
            : base(1)
            {
            BusinessProcessType = ProcessType.Registration;
            MainProcess.User = 0;
            }

        public override void DrawControls()
            {
            MainProcess.CreateLabel("Увійдіть до системи", 19, 165, 211, MobileFontSize.Large);
            MainProcess.ToDoCommand = "Регістрація";

            MainProcess.CreateButton("Створити відвантаження", 10, 205, 220, 35, "", createShipment);

            MainProcess.CreateButton("Вхід", 10, 275, 220, 35, "Вхід", startSelectingProcess);
            }

        public void createShipment()
            {
            if (Program.AramisSystem.CreatePickingDocuments())
                {
                "Всі існуючі документи заплановані!".Warning();
                }
            else
                {
                Warning_CantComplateOperation();
                }
            }

        protected override void OnBarcode(string barcode)
            {
            if (!barcode.IsEmployee()) return;
            var userCode = barcode.ToEmployeeCode();
            if (userCode == 0) return;

            string userName;
            if (!Program.AramisSystem.GetUserName(userCode, out userName)) return;
            
            MainProcess.User = userCode;
            MainProcess.UserName = userName;
            startSelectingProcess();
            }

        private void startSelectingProcess()
            {
            new SoftUpdater();

            if (!initConsts())
                {
                Warning_CantComplateOperation();
                return;
                }

            MainProcess.ClearControls();
            MainProcess.Process = new SelectingProcess();
            }

        private bool initConsts()
            {
            DataTable constsTable;
            if (!Program.AramisSystem.ReadConsts(out constsTable))
                {
                return false;
                }

            foreach (DataRow row in constsTable.Rows)
                {
                string constName = row[0].ToString();
                string constValueStr = row[1].ToString();
                setConst(constName, constValueStr);
                }

            return true;
            }

        private void setConst(string constName, string constValueStr)
            {
            switch (constName.ToLower())
                {
                case "woodlinerid":
                    Program.Consts.WoodLinerId = Convert.ToInt64(constValueStr);
                    return;
                }
            }

        protected override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Proceed:
                    break;
                }
            }
        #endregion
        }
    }
