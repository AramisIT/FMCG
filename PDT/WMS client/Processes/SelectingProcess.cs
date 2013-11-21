using System;
using System.Collections.Generic;

namespace WMS_client.Processes
    {
    /// <summary>Вибір процесу</summary>
    public class SelectingProcess : BusinessProcess
        {
        public enum Processes : long
            {
            Acceptance,
            Inventory,
            Selection,
            Movement
            }

        /// <summary>Вибір процесу</summary>
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
                            new TableData((long) Processes.Acceptance, "Приймання товару", acceptanceDocCount),
                            new TableData((long) Processes.Inventory, "Інвентаризація", inventoryDocCount),
                            new TableData((long) Processes.Selection, "Відбір", movementDocCount),
                            new TableData((long) Processes.Movement, "Переміщення", string.Empty)
                        };
                MainProcess.ClearControls();
                MainProcess.Process = new SelectTableList(
                    MainProcess, selectProcess, "Оберіть процес", "Процеси", listOfElements, string.Empty, false);
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
                    "Процес ще не реалізовано!".ShowMessage();
                    process = new SelectingProcess();
                    break;
                }

            MainProcess.Process = process;
            }
        #endregion
        }
    }