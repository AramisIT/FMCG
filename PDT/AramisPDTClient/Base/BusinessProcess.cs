using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using AramisPDTClient;
using WMS_client.Base.Visual;
using WMS_client.Processes;
using WMS_client.Utils;

namespace WMS_client
    {
    public abstract class BusinessProcess
        {
        public static event Action<BusinessProcess> OnProcessCreated;

        public const int TOPMOST_CONTROL_Y_POSITION = 80;

        public const int VERTICAL_DISTANCE_BETWEEN_CONTROLS = 27;

        protected bool isLoading;

        protected bool lastQueryIsSuccessfull
            {
            get { return WMSClient.Current.LastQueryIsSuccessfull; }
            }

        #region Constructors

        private const string CANT_COMPLATE_OPERATION = "������� ������ ��������� ��������, ��������� �� ��� � ���� WiFi!";

        protected void Warning_CantComplateOperation()
            {
            CANT_COMPLATE_OPERATION.Warning();
            }

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
                    MessageBox.Show("���������� �����������. ��������� �������� �������!");
                    TerminateApplication();
                    return;
                    }

                if (OnProcessCreated != null)
                    {
                    OnProcessCreated(this);
                    }

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

        protected void SelectFromCatalog(List<CatalogItem> itemsList, Action<CatalogItem> onSelect)
            {
            CatalogItem selectedItem;
            if (SelectFromList(itemsList, out selectedItem))
                {
                onSelect(selectedItem);
                }
            }

        protected void showConnectionInterraptedMessage()
            {
            "��� ����� � ��������!".Warning();
            }

        protected void notifyUserIfConnectionIsBroken()
            {
            if (!lastQueryIsSuccessfull)
                {
                showConnectionInterraptedMessage();
                }
            }

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

        protected bool IsLoad;
        public abstract void DrawControls();
        protected abstract void OnBarcode(string barcode);
        protected abstract void OnHotKey(KeyAction key);
        public string CellName;
        public string CellBarcode;
        public int NextFormNumber = 1;
        public object[] ResultParameters;
        public bool IsExistParameters { get { return ResultParameters != null && ResultParameters.Length > 0 && ResultParameters[0] != null; } }
        public bool IsAnswerIsTrue { get { return IsExistParameters && Convert.ToBoolean(ResultParameters[0]); } }
        public ProcessType BusinessProcessType;

        public void HandleBarcode(string barcode)
            {
            if (selectingFromList) return;

            OnBarcode(barcode);
            }

        public void HandleHotKey(KeyAction key)
            {
            if (selectingFromList)
                {
                if (key == KeyAction.Esc)
                    {
                    selectingItemForm.CancelSelecting();
                    }
                return;
                }

            OnHotKey(key);
            }

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
                "������������ ��������� ������".Warning();
                }
            }

        private void OnCellHotKey(KeyAction Key)
            {
            if (Key == KeyAction.Proceed)
                {
                FormNumber = NextFormNumber;
                MainProcess.ClearControls();
                MainProcess.OnBarcode = HandleBarcode;
                MainProcess.MainForm.SetOnHotKeyPressed(HandleHotKey);

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
                MainProcess.OnBarcode = HandleBarcode;
                MainProcess.MainForm.SetOnHotKeyPressed(HandleHotKey);
                //MainProcess.HotKeyAgent.OnHotKeyPressed = OnHotKey;
                }
            }

        private void LocalizationStep()
            {
            MainProcess.ClearControls();
            MainProcess.MainForm.ShowCellName(CellName);
            //MainProcess.MainForm.HotKeyAgent.SetHotKey(KeyAction.Proceed);
            MainProcess.ToDoCommand = "������� � ������";
            }

        public void TerminateApplication()
            {
            var currentWmsClient = WMSClient.Current;
            currentWmsClient.ConnectionAgent.CloseAll();
            currentWmsClient.MainForm.Close();
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
        /// ���������� ���������� ��������� � ������ ��� ������ ���������
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

        public void TEST_AAAAAAAAAAAAAAAAAAAA()
            {
            int isTare;
            int ware;
            int zall;
            int bb;
            int dV;
            int dec = 0;
            int str = 0;
            int time;
            DataTable table = new DataTable();

            int w1, w2, w3;
            AAAAAAAAAAAAAAAtestEmit(54, out zall, out bb, out dV, out dec, out str, out w1, out w2, out w3, out time);
            }

        public bool AAAAAAAAAAAAAAAtestEmit(int t, out int a1, out int a2, out int a3, out int a4, out int a5, out int a6, out int a7, out int a8, out int a9)
            {
            bool success;
            bool functionResult;
            var parameters = new object[0];
            //parameters[0] = t;

            var result = WMSClient.Current._PerformQueryForInteractionProvider("FinishCellInventory",
                out success, out functionResult, parameters);

            a1 = Convert.ToInt32(result[0]);
            a2 = Convert.ToInt32(result[0]);
            a3 = Convert.ToInt32(result[0]);
            a4 = Convert.ToInt32(result[0]);
            a5 = Convert.ToInt32(result[0]);
            a6 = Convert.ToInt32(result[0]);
            a7 = Convert.ToInt32(result[0]);
            a8 = Convert.ToInt32(result[0]);
            a9 = Convert.ToInt32(result[0]);
            //a10 = Convert.ToInt32(result[0]);
            //a11 = Convert.ToInt32(result[0]);
            //a12 = Convert.ToInt32(result[0]);
            return true;

            //resultB = Convert.ToBoolean(result[9]);

            //trer = 0;
            //longV = 0;
            //byteV = 0;
            //doubleV = 0L;
            //decimalV = 0M;
            //str = "";
            //time = (DateTime)(result[9]);
            //table = result[9] as DataTable;
            //return false;
            //if (success)
            //    {
            //    return functionResult;
            //    }
            //else
            //    {
            //    return false;
            //    }
            }

        public void PerformQuery(string QueryName, params object[] parameters)
            {
            ResultParameters = MainProcess.PerformQuery(QueryName, parameters);
            }

        protected bool SelectFromList(List<CatalogItem> list, out CatalogItem selectedItem)
            {
            return SelectFromList(list, -1, out selectedItem);
            }

        protected bool SelectFromList(List<CatalogItem> list, int selectedIndex, out CatalogItem selectedItem)
            {
            const int defaultRowHeight = 25;
            return SelectFromList(list, selectedIndex, defaultRowHeight, out selectedItem);
            }

        private volatile bool selectingFromList;
        private SelectingItem selectingItemForm;

        protected bool SelectFromList(List<CatalogItem> list, int selectedIndex, int rowHeight, out CatalogItem selectedItem)
            {
            var _selectingItemForm = new WMS_client.Base.Visual.SelectingItem();
            _selectingItemForm.SetRowHeight(rowHeight);
            _selectingItemForm.DataSource = list;
            _selectingItemForm.SelectedIndex = selectedIndex;

            this.selectingItemForm = _selectingItemForm;
            selectingFromList = true;
            var selectResult = _selectingItemForm.ShowDialog();
            selectingFromList = false;
            this.selectingItemForm = null;

            if (selectResult == DialogResult.OK)
                {
                selectedItem = list[_selectingItemForm.SelectedIndex < 0 ? 0 : _selectingItemForm.SelectedIndex];
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