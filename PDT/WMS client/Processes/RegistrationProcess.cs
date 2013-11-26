using AramisPDTClient.UpdatingSoft;
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
            MainProcess.CreateButton("Вхід", 10, 275, 220, 35, "Вхід", () => OnBarcode(string.Empty));
            }

        public override void OnBarcode(string Barcode)
            {
            //new SoftUpdater();
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
