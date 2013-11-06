using System;
using System.Windows.Forms;

namespace WMS_client
    {
    public abstract class BaseProcess
        {
        #region Public fields
        protected bool isLoading;
        public object[] Parameters;
        protected WMSClient MainProcess;
        public ProcessType BusinessProcessType;
        public string DocumentNumber;
        public string CellBarcode;
        public string CellName;
        public int FormNumber = 0;
        public int NextFormNumber = 1;
        public bool IsExistParameters
            {
            get { return Parameters != null && Parameters.Length > 0 && Parameters[0] != null; }
            }
        public bool IsAnswerIsTrue
            {
            get { return IsExistParameters && Convert.ToBoolean(Parameters[0]); }
            }
        #endregion

        #region Public methods
        #region Constructor
        protected BaseProcess() {}

        protected BaseProcess(int FormNumber)
            : this(string.Empty, string.Empty, FormNumber) {}

        protected BaseProcess(string CellName, string CellBarcode, int FormNumber)
            {
            this.FormNumber = CellName == string.Empty ? FormNumber : 0;
            if (FormNumber == 0)
                {
                this.CellName = CellName;
                this.CellBarcode = CellBarcode;
                }

            this.MainProcess = WMSClient.Current;
            Start();
            }
        #endregion

        public void PerformQuery(string QueryName, params object[] parameters)
            {
            Parameters = null;
            if (!MainProcess.OnLine && MainProcess.MainForm.IsMainThread)
                {
                ShowMessage("Нет подключения к серверу");
                return;
                }

            Parameters = MainProcess.PerformQuery(QueryName, parameters);
            }

        public void ShortQuery(string QueryName, params object[] parameters)
            {
            object[] NewParameters = new object[parameters.Length + 3];
            NewParameters[0] = "#<PDT>#";
            NewParameters[1] = (int) BusinessProcessType;
            NewParameters[2] = DocumentNumber;
            for (int i = 0; i < parameters.Length; i++)
                {
                NewParameters[i + 3] = parameters[i];
                }
            PerformQuery(QueryName, NewParameters);
            }

        public void BarCodeByHands()
            {
            MainProcess.MainForm.BarCodeByHands();
            }

        public virtual void Start()
            {
            SetEventHendlers();

            if (FormNumber == 0)
                {
                LocalizationStep();
                }
            else
                {
                DrawControls();
                }

            }

        public void ShowMessage(string message)
            {
            WMSClient.Current.ShowMessage(message);
            }

        public bool ShowQuery(string msg)
            {
            return
                MessageBox.Show(msg.ToUpper(), "aramis wms", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1) == DialogResult.Yes;
            }
        #endregion

        #region Private methods
        private void SetEventHendlers()
            {
            if (FormNumber == 0)
                {
                MainProcess.OnBarcode = OnCellBarcode;
                MainProcess.MainForm.SetOnHotKeyPressed(OnCellHotKey);
                //MainProcess.HotKeyAgent.OnHotKeyPressed = OnCellHotKey;
                }
            else
                {
                MainProcess.OnBarcode = OnBarcode;
                MainProcess.MainForm.SetOnHotKeyPressed(OnHotKey);
                //MainProcess.HotKeyAgent.OnHotKeyPressed = OnHotKey;
                }
            }

        private void LocalizationStep()
            {
            MainProcess.ClearControls();
            MainProcess.MainForm.ShowCellName(CellName);
            //MainProcess.MainForm.HotKeyAgent.SetHotKey(KeyAction.Proceed);
            MainProcess.ToDoCommand = "Подойти к ячейке";
            }

        private void OnCellBarcode(string Barcode)
            {
            if (CellBarcode == Barcode)
                {
                OnCellHotKey(KeyAction.Proceed);
                }
            else
                {
                ShowMessage("Отсканируйте требуемый объект");
                }
            }

        private void OnCellHotKey(KeyAction Key)
            {
            if (Key == KeyAction.Proceed)
                {
                FormNumber = NextFormNumber;
                MainProcess.ClearControls();
                MainProcess.OnBarcode = OnBarcode;
                MainProcess.MainForm.SetOnHotKeyPressed(OnHotKey);

                Start();
                }
            }
        #endregion

        #region Abstract methods
        public abstract void DrawControls();
        public abstract void OnBarcode(string Barcode);
        public abstract void OnHotKey(KeyAction Key);
        #endregion
        }
    }