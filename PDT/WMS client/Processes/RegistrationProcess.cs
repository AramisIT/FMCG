using WMS_client.HelperClasses;

namespace WMS_client.Processes
{
    /// <summary>����������� ��� �����</summary>
    public class RegistrationProcess : BusinessProcess
    {
        #region Public methods
        /// <summary>����������� ��� �����</summary>
        public RegistrationProcess(WMSClient MainProcess)
            : base(MainProcess, 1)
        {
            BusinessProcessType = ProcessType.Registration;
        }

        public override void DrawControls()
        {
            MainProcess.CreateLabel("������ �� �������", 19, 165, 211, MobileFontSize.Large);
            MainProcess.ToDoCommand = "����������";
            MainProcess.CreateButton("����", 10, 275, 220, 35, "����", () => OnBarcode(string.Empty));
        }

        public override void OnBarcode(string Barcode)
        {
            //if (Barcode.IsValidBarcode())
            //{
                //����������� �������!
                MainProcess.User = 0;
                MainProcess.ClearControls();
                //������� ���� ������ ��������
                MainProcess.Process = new SelectingProcess(MainProcess);
            //}
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
