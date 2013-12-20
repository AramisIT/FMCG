using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.WindowsCE.Forms;

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
            Movement,
            StickerRepeating
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
                            new TableData((long) Processes.Inventory, "Інвентаризація", string.Empty),
                            new TableData((long) Processes.Selection, "Відбір", movementDocCount),
                            new TableData((long) Processes.Movement, "Переміщення", string.Empty),
                            new TableData((long) Processes.StickerRepeating, "Повтор етикетки", string.Empty)
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
            BusinessProcess process = null;
            Processes SelectedProcess = (Processes)selectedIndex;

            switch (SelectedProcess)
                {
                case Processes.Acceptance:
                    MainProcess.ClearControls();
                    process = new Acceptance();
                    break;
                case Processes.Movement:
                    MainProcess.ClearControls();
                    process = new Movement();
                    break;
                case Processes.Selection:
                    process = tryStartPicking();
                    break;
                case Processes.Inventory:
                    MainProcess.ClearControls();
                    process = new Inventory();
                    break;
                case Processes.StickerRepeating:
                    MainProcess.ClearControls();
                    process = new StickerRepeating();
                    break;
                }

            if (process != null)
                {
                MainProcess.Process = process;
                }
            }

        private Picking tryStartPicking()
            {
            var docs = new ServerInteraction().GetPickingDocuments();

            if (docs.Rows.Count == 0) return null;

            CatalogItem item;
            if (!SelectFromList(docs.ToItemsList(), out item)) return null;

            MainProcess.ClearControls();
            return new Picking(item.Id);
            }

        #endregion
        }
    }