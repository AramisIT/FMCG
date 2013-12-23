using System;
using System.Data;
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
            }

        public override void DrawControls()
            {
            MainProcess.CreateLabel("Увійдіть до системи", 19, 165, 211, MobileFontSize.Large);
            MainProcess.ToDoCommand = "Регістрація";

            MainProcess.CreateButton("Створити відвантаження", 10, 205, 220, 35, "", createShipment);

            MainProcess.CreateButton("Вхід", 10, 275, 220, 35, "Вхід", () => OnBarcode(string.Empty));
            }

        public void createShipment()
            {
            if (new ServerInteraction().CreatePickingDocuments())
                {
                "Всі існуючі документи заплановані!".Warning();
                }
            else
                {
                Warning_CantComplateOperation();
                }
            }

        public override void OnBarcode(string Barcode)
            {
            new SoftUpdater();

            if (!initConsts())
                {
                Warning_CantComplateOperation();
                return;
                }
            //return;
            //if (Barcode.IsValidBarcode())
            //{
            //Регистрация успешна!
            MainProcess.User = 0;
            MainProcess.ClearControls();
            //Открыть окно выбора процесса
            MainProcess.Process = new SelectingProcess();
            //}
            }

        private bool initConsts()
            {
            DataTable constsTable;
            if (!new ServerInteraction().ReadConsts(out constsTable))
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

        public override void OnHotKey(KeyAction TypeOfAction)
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
