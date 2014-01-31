using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.WindowsCE.Forms;
using WMS_client.HelperClasses;

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
            StickerRepeating,
            BarcodeChecking,


            // temp processes
            IsPalletFull
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
                            new TableData((long) Processes.StickerRepeating, "Повтор етикетки", string.Empty),
                            new TableData((long) Processes.BarcodeChecking, "Перевірка штрих-коду", string.Empty),


                            new TableData((long) Processes.IsPalletFull, "Чи розпакована палета?", string.Empty)
                        };
                MainProcess.ClearControls();
                MainProcess.Process = new SelectTableList(
                    MainProcess, selectProcess, "Процеси", listOfElements, string.Empty, false);
                }
            else
                {
                OnHotKey(KeyAction.Esc);
                }
            }

        public override void OnBarcode(string barcode)
            {
            if (!barcode.IsEmployee()) return;
            var userCode = barcode.ToEmployeeCode();
            if (userCode == 0) return;

            MainProcess.User = userCode;
            MainProcess.UserName = new ServerInteraction().GetUserName(MainProcess.User);
            ToDoCommand = MainProcess.UserName;
            }

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
            if (MainProcess.User <= 0)
                {
                "Необхідно авторизуватися (відсканувати себе)!".Warning();
                return;
                }
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
                case Processes.BarcodeChecking:
                    MainProcess.ClearControls();
                    process = new BarcodeChecking();
                    break;

                case Processes.IsPalletFull:
                    MainProcess.ClearControls();
                    process = new IsPalletFull();
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