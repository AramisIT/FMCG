using WMS_client.Processes.BaseScreen;
using System.Collections.Generic;

namespace WMS_client.Processes
    {
    /// <summary>���� �������</summary>
    public class SelectingProcess : BusinessProcess
        {
        public enum Processes : long
            {
            Acceptance,
            Inventory,
            Selection,
            Movement
            }

        /// <summary>���� �������</summary>
        public SelectingProcess(WMSClient MainProcess)
            : base(MainProcess, 1) {}

        #region Override methods
        public override sealed void DrawControls()
            {
            string acceptanceDocCount;
            string inventoryDocCount;
            string selectionDocCount;
            string movementDocCount;
            GetCountOfDocuments(
                out acceptanceDocCount, out inventoryDocCount, out selectionDocCount, out movementDocCount);

            if (IsExistParameters)
                {
                List<TableData> listOfElements =
                    new List<TableData>
                        {
                            new TableData((long) Processes.Acceptance, "��������� ������", acceptanceDocCount),
                            new TableData((long) Processes.Inventory, "��������������", inventoryDocCount),
                            new TableData((long) Processes.Selection, "³���", selectionDocCount),
                            new TableData((long) Processes.Movement, "����������", movementDocCount)
                        };
                MainProcess.ClearControls();
                MainProcess.Process = new SelectTableList(
                    MainProcess, selectProcessDelegate, "������ ������", "�������", listOfElements, string.Empty, false);
                }
            else
                {
                OnHotKey(KeyAction.Esc);
                }
            }

       

        public override void OnBarcode(string Barcode) {}

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                    case KeyAction.Esc:
                        MainProcess.ClearControls();
                        MainProcess.Process = new RegistrationProcess(MainProcess);
                        break;
                }
            }
        #endregion

        #region Delegates
        private void selectProcessDelegate(long selectedIndex, string description)
            {
            MainProcess.ClearControls();
            BusinessProcess process;
            Processes SelectedProcess = (Processes) selectedIndex;

            switch (SelectedProcess)
                {
                    case Processes.Acceptance:
                        process = new Acceptance(MainProcess);
                        break;
                    case Processes.Movement:
                        process = new Movement(MainProcess);
                        break;
                    case Processes.Selection:
                        process = new Selection(MainProcess);
                        break;
                    case Processes.Inventory:
                        process = new Inventory(MainProcess);
                        break;
                    default:
                        ShowMessage("������ �� �� ����������!");
                        process = new SelectingProcess(MainProcess);
                        break;
                }

            MainProcess.Process = process;
            }
        #endregion
        }
    }