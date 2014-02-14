using System;
using System.Data;
using WMS_client.HelperClasses;

namespace WMS_client.Processes
    {
    /// <summary>����������� ��� �����</summary>
    public class RegistrationProcess : BusinessProcess
        {
        #region Public methods
        /// <summary>����������� ��� �����</summary>
        public RegistrationProcess()
            : base(1)
            {
            BusinessProcessType = ProcessType.Registration;
            MainProcess.User = 0;
            }

        public override void DrawControls()
            {
            MainProcess.CreateLabel("������ �� �������", 19, 165, 211, MobileFontSize.Large);
            MainProcess.ToDoCommand = "����������";

            MainProcess.CreateButton("�������� ������������", 10, 205, 220, 35, "", createShipment);

            MainProcess.CreateButton("����", 10, 275, 220, 35, "����", startSelectingProcess);
            }

        public void createShipment()
            {
            if (new ServerInteraction().CreatePickingDocuments())
                {
                "�� ������� ��������� �����������!".Warning();
                }
            else
                {
                Warning_CantComplateOperation();
                }
            }

        public override void OnBarcode(string barcode)
            {
            if (!barcode.IsEmployee()) return;
            var userCode = barcode.ToEmployeeCode();
            if (userCode == 0) return;

            MainProcess.User = userCode;
            MainProcess.UserName = new ServerInteraction().GetUserName(MainProcess.User);
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
