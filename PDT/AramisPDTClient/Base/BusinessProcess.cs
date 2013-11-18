using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using AramisPDTClient;
using WMS_client.Processes;
using WMS_client.Utils;

namespace WMS_client
    {
    public abstract class BusinessProcess
        {
        public static event Action OnProcessCreated;

        protected bool isLoading;

        #region Constructors

        protected const string CANT_COMPLATE_OPERATION = "Невдала спроба завершення операції, спробуйте ще раз в зоні WiFi!";

        protected BusinessProcess(int FormNumber)
            : this("", "", FormNumber) { }

        protected BusinessProcess(string CellName, string CellBarcode)
            : this(CellName, CellBarcode, 0) { }

        protected BusinessProcess(string CellName, string CellBarcode, int FormNumber)
            {
            this.MainProcess = WMSClient.Current;

            if (SystemInfo.ReleaseMode)
                {
                if (BatteryChargeStatus.Low)
                    {
                    MessageBox.Show("Акумулятор розряджений. Необхідно зарядити термінал!");
                    TerminateApplication(MainProcess);
                    return;
                    }

                if (OnProcessCreated != null)
                    {
                    OnProcessCreated();
                    }

                //if (Configuration.Current.TimeToBackUp)
                //    {
                //    bool lowPower;
                //    if (Configuration.Current.Repository.IsIntactDatabase(out lowPower))
                //        {
                //        if (!lowPower)
                //            {
                //            var backUpCreator = new BackUpCreator();
                //            if (backUpCreator.CreateBackUp())
                //                {
                //                "Создана копия базы!".ShowMessage();
                //                Configuration.Current.FixBackUpTime();
                //                }
                //            }
                //        }
                //    else
                //        {
                //        "База даних пошкоджена. Необхідно звернутись до адміністратора.".ShowMessage();
                //        TerminateApplication(MainProcess);
                //        return;
                //        }
                //    }

                if (applicationIsClosing)
                    {
                    return;
                    }
                }

            ShowProgress(1, 1);
            this.FormNumber = CellName == "" ? FormNumber : 0;
            if (FormNumber == 0)
                {
                this.CellName = CellName;
                this.CellBarcode = CellBarcode;
                }

            
            Start();
            }

        #endregion

        protected string ToDoCommand
            {
            get
                {
                return WMSClient.Current.ToDoCommand;
                }
            set
                {
                WMSClient.Current.ToDoCommand = value;
                }
            }

        #region Implemention of IRemoteCommunications

        private List<HideableControlsCollection> hideableControlsCollectionsSet;
        protected bool applicationIsClosing;
        private int FormNumber;
        protected WMSClient MainProcess;

        public abstract void DrawControls();
        public abstract void OnBarcode(string barcode);
        public abstract void OnHotKey(KeyAction key);
        public string CellName;
        public string CellBarcode;
        public int NextFormNumber = 1;
        public object[] ResultParameters;
        public bool IsExistParameters { get { return ResultParameters != null && ResultParameters.Length > 0 && ResultParameters[0] != null; } }
        public bool IsAnswerIsTrue { get { return IsExistParameters && Convert.ToBoolean(ResultParameters[0]); } }
        public ProcessType BusinessProcessType;

        protected bool SuccessQueryResult
            {
            get
                {
                return ResultParameters != null
                       && ResultParameters.GetType() == typeof(object[])
                       && ResultParameters.Length > 0
                       && ResultParameters[0] is bool
                       && (bool)ResultParameters[0];
                }
            }

        protected void ClearControls()
            {
            MainProcess.ClearControls();
            }

        public void BarCodeByHands()
            {
            MainProcess.MainForm.BarCodeByHands();
            }

        protected bool OnLine
            {
            get
                {
                return MainProcess.ConnectionAgent.OnLine;
                }
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

        private void OnCellBarcode(string Barcode)
            {
            if (CellBarcode == Barcode)
                {
                OnCellHotKey(KeyAction.Proceed);
                }
            else
                {
                "Отсканируйте требуемый объект".Warning();
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

        private void TerminateApplication(WMSClient MainProcess)
            {
            MainProcess.ConnectionAgent.CloseAll();
            MainProcess.MainForm.Close();
            Application.Exit();
            applicationIsClosing = true;
            }

        private void checkHideableControlsCollectionsList()
            {
            if (hideableControlsCollectionsSet != null) return;

            hideableControlsCollectionsSet = new List<HideableControlsCollection>();

            var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var fieldInfo in fields)
                {
                var fieldValue = fieldInfo.GetValue(this);
                if (fieldValue is HideableControlsCollection)
                    {
                    hideableControlsCollectionsSet.Add(fieldValue as HideableControlsCollection);
                    }
                }
            }

        /// <summary>
        /// Отображает переданную коллекцию и прячет все прочие коллекции
        /// </summary>
        /// <param name="hideableControls"></param>
        public void ShowControls(HideableControlsCollection hideableControls)
            {
            checkHideableControlsCollectionsList();

            foreach (var controlsCollection in hideableControlsCollectionsSet)
                {
                if (hideableControls == controlsCollection)
                    {
                    continue;
                    }
                controlsCollection.Hide();
                }

            if (!hideableControls.Visible)
                {
                hideableControls.Show();
                }
            }

        public void PerformQuery(string QueryName, params object[] parameters)
            {
            ResultParameters = null;
            if (!MainProcess.OnLine && MainProcess.MainForm.IsMainThread)
                {
                "Нет подключения к серверу".Warning();
                return;
                }

            ResultParameters = MainProcess.PerformQuery(QueryName, parameters);
            }

        protected bool SelectFromList(List<CatalogItem> list, out CatalogItem selectedItem)
            {
            return SelectFromList(list, -1, out selectedItem);
            }

        protected bool SelectFromList(List<CatalogItem> list, int selectedIndex, out CatalogItem selectedItem)
            {
            var selectingItemForm = new WMS_client.Base.Visual.SelectingItem();
            selectingItemForm.DataSource = list;
            selectingItemForm.SelectedIndex = selectedIndex;

            if (selectingItemForm.ShowDialog() == DialogResult.OK)
                {
                selectedItem = list[selectingItemForm.SelectedIndex < 0 ? 0 : selectingItemForm.SelectedIndex];
                return true;
                }

            selectedItem = null;
            return false;
            }

        protected void StopNetworkConnection()
            {
            bool release = true;
#if DEBUG
            release = false;
#endif
            if (release)
                {
                bool startStatus = MainProcess.ConnectionAgent.WifiEnabled;
                if (startStatus)
                    {
                    MainProcess.ConnectionAgent.StopConnection();
                    }
                }
            }

        protected void StartNetworkConnection()
            {
            bool startStatus = MainProcess.ConnectionAgent.WifiEnabled;
            if (!startStatus)
                {
                MainProcess.StartConnectionAgent();
                System.Threading.Thread.Sleep(1500);
                }
            }

        /// <summary>
        /// show progress bar status
        /// </summary>
        /// <param name="value">from 0 to 100 value</param>
        protected void ShowProgress(int currentValue, int total)
            {
            if (MainProcess == null)
                {
                return;
                }

            if (total <= currentValue || total == 0)
                {
                MainProcess.MainForm.ShowProgress(100);
                }
            else
                {
                var percent = (int)(100 * currentValue / total);
                MainProcess.MainForm.ShowProgress(percent);
                }
            }

        #endregion
        }
    }