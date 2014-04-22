using System;
using System.Data;
using System.Collections.Generic;
using WMS_client.HelperClasses;

namespace WMS_client.Processes
    {
    public struct TableData
        {
        public long Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public TableData(long id, string description, string value)
            : this()
            {
            Id = id;
            Description = description;
            Value = value;
            }
        }

    public enum Processes : long
        {
        Acceptance,
        Inventory,
        Selection,
        Movement,
        StickerRepeating,
        BarcodeChecking,
        ReturnFromHaul,

        // temp processes
        IsPalletFull
        }

    internal class SelectingProcess : BusinessProcess
        {

        #region Properties
        private readonly string headerOfDescriptionColumn = "Процеси";
        private IEnumerable<TableData> listOfElements;

        /// <summary>Таблица для MobileTable...</summary>
        private readonly DataTable sourceTable;
        /// <summary>Отображаемый контрол с данными о картах</summary>
        private MobileTable visualTable;
        private MobileLabel selectedInfo;
        private long selectedIndex;
        private string selectedDescription
            {
            get { return selectedInfo.Text; }
            set { selectedInfo.Text = value; }
            }
        private const string ID_COLUMN = "Id";
        private const string VALUE_COLUMN = "Value";
        private const string DESCRIPTION_COLUMN = "Description";
        #endregion

        /// <summary>Вибір з таблиці</summary>
        public SelectingProcess()
            : base(1)
            {
            fillTable();
            ToDoCommand = MainProcess.UserName;

            sourceTable = new DataTable();
            sourceTable.Columns.AddRange(new[]
                                             {
                                                 new DataColumn(DESCRIPTION_COLUMN, typeof (string)),
                                                 new DataColumn(VALUE_COLUMN, typeof (string)),
                                                 new DataColumn(ID_COLUMN, typeof (long))
                                             });

            isLoading = true;
            DrawControls();
            }

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

                case Processes.ReturnFromHaul:
                    MainProcess.ClearControls();
                    process = new ReturnFromHaul();
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
            DataTable docs;
            if (!Program.AramisSystem.GetPickingDocuments(out docs) || docs.Rows.Count == 0)
                {
                return null;
                }

            CatalogItem item;
            if (!SelectFromList(docs.ToItemsList(), out item)) return null;

            MainProcess.ClearControls();
            return new Picking(item.Id);
            }

        private void fillTable()
            {
            string acceptanceDocCount;
            string inventoryDocCount;
            string selectionDocCount;
            string movementDocCount;
            if (Program.AramisSystem.GetCountOfDocuments(
               out acceptanceDocCount, out inventoryDocCount, out selectionDocCount, out movementDocCount))
                {
                listOfElements =
                    new List<TableData>
                        {
                            new TableData((long) Processes.Acceptance, "Приймання товару", acceptanceDocCount),
                            new TableData((long) Processes.Inventory, "Інвентаризація", string.Empty),
                            new TableData((long) Processes.Selection, "Відбір", movementDocCount),
                            new TableData((long) Processes.Movement, "Переміщення", string.Empty),
                            new TableData((long) Processes.ReturnFromHaul, "Повернення з рейсу", string.Empty),
                            new TableData((long) Processes.StickerRepeating, "Повтор етикетки", string.Empty),
                            new TableData((long) Processes.BarcodeChecking, "Перевірка штрих-коду", string.Empty),


                            new TableData((long) Processes.IsPalletFull, "Чи розпакована палета?", string.Empty)
                        };
                }
            else
                {
                listOfElements = new List<TableData>();
                }
            }

        #region Override methods
        public override sealed void DrawControls()
            {
            if (isLoading)
                {
                int tableHeight = 230;

                selectedInfo = MainProcess.CreateLabel(
                    "<не обрано>", 0, 60, 240, MobileFontSize.Normal, MobileFontPosition.Center, MobileFontColors.Info);
                visualTable = MainProcess.CreateTable("Table", tableHeight, 85);
                visualTable.DT = sourceTable;
                visualTable.OnChangeSelectedRow += visualTable_OnChangeSelectedRow;
                visualTable.AddColumn(headerOfDescriptionColumn, DESCRIPTION_COLUMN, 194);
                visualTable.AddColumn(string.Empty, VALUE_COLUMN, 20);
                visualTable.OnRowSelected += visualTable_OnRowSelected;
                if (listOfElements != null)
                    {
                    foreach (TableData element in listOfElements)
                        {
                        visualTable.AddRow(element.Description, element.Value, element.Id);
                        }
                    }

                visualTable.Focus();
                }
            }

        void visualTable_OnRowSelected(object param1, OnRowSelectedEventArgs param2)
            {
            selectProcess(selectedIndex, selectedDescription);
            }

        protected override void OnBarcode(string barcode)
            {
            if (!barcode.IsEmployee()) return;
            var userCode = barcode.ToEmployeeCode();
            if (userCode == 0) return;

            string userName;
            if (!Program.AramisSystem.GetUserName(userCode, out userName)) return;

            MainProcess.User = userCode;
            MainProcess.UserName = userName;
            ToDoCommand = MainProcess.UserName;
            }

        protected override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Esc:
                    MainProcess.ClearControls();
                    MainProcess.Process = new SelectingProcess();
                    break;
                case KeyAction.Enter:
                    selectProcess(selectedIndex, selectedDescription);
                    break;
                }
            }
        #endregion

        private void visualTable_OnChangeSelectedRow(object param1, OnChangeSelectedRowEventArgs e)
            {
            selectedIndex = (long)e.SelectedRow[ID_COLUMN];
            selectedDescription = e.SelectedRow[DESCRIPTION_COLUMN].ToString();
            }
        }
    }