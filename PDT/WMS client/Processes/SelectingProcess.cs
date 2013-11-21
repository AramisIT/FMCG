using System;
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
        public SelectingProcess()
            : base(1)
            {
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            string acceptanceDocCount;
            string inventoryDocCount;
            string selectionDocCount;
            string movementDocCount;

            if (new ServerInteraction().GetCountOfDocuments(
                out acceptanceDocCount, out inventoryDocCount, out selectionDocCount, out movementDocCount))
                {
                List<TableData> listOfElements =
                    new List<TableData>
                        {
                            new TableData((long) Processes.Acceptance, "��������� ������", acceptanceDocCount),
                            new TableData((long) Processes.Inventory, "��������������", inventoryDocCount),
                            new TableData((long) Processes.Selection, "³���", movementDocCount),
                            new TableData((long) Processes.Movement, "����������", string.Empty)
                        };
                MainProcess.ClearControls();
                MainProcess.Process = new SelectTableList(
                    MainProcess, selectProcess, "������ ������", "�������", listOfElements, string.Empty, false);
                }
            else
                {
                OnHotKey(KeyAction.Esc);
                }
            }

        public override void OnBarcode(string Barcode) { }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Esc:
                    MainProcess.ClearControls();
                    MainProcess.Process = new RegistrationProcess();
                    break;
                }
            }
        #endregion

        #region Delegates
        private void selectProcess(long selectedIndex, string description)
            {
            MainProcess.ClearControls();
            BusinessProcess process;
            Processes SelectedProcess = (Processes)selectedIndex;

            switch (SelectedProcess)
                {
                case Processes.Acceptance:
                    process = new Acceptance();
                    break;
                case Processes.Movement:
                    process = new Movement();
                    break;
                case Processes.Selection:
                    process = new Selection();
                    break;
                case Processes.Inventory:
                    process = new Inventory();
                    break;
                default:
                    "������ �� �� ����������!".ShowMessage();
                    process = new SelectingProcess();
                    break;
                }

            MainProcess.Process = process;
            }
        #endregion
        }
    }