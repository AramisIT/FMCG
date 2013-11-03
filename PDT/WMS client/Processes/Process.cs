using System;
namespace WMS_client.Processes
    {
    public abstract class Process<T> : BusinessProcess where T : IProcessData
        {
        public const string ID_COLUMN_NAME = "Id";
        public const string DESCRIPTION_COLUMN_NAME = "Description";
        public T Data { get; set; }

        /// <summary>Вибір процесу</summary>
        protected Process(WMSClient MainProcess)
            : base(MainProcess, 1)
            {
            Data = Activator.CreateInstance<T>();
            }

      

        #region Override methods
        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                    case KeyAction.Esc:
                        MainProcess.ClearControls();
                        MainProcess.Process = new SelectingProcess(MainProcess);
                        break;
                }
            }
        #endregion
        }
    }