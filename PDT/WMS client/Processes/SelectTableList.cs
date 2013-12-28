﻿using System;
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

    internal class SelectTableList : BusinessProcess
        {
        public delegate void SelectFromListDelegate(long selectedIndex, string description);

        #region Properties
        private readonly string headerOfDescriptionColumn;
        private readonly SelectFromListDelegate navigateToNextScreen;
        private readonly IEnumerable<TableData> listOfElements;
        private readonly string buttonText;
        private readonly bool isBackButton;
        /// <summary>Таблица для MobileTable...</summary>
        private readonly DataTable sourceTable;
        /// <summary>Отображаемый контрол с данными о картах</summary>
        private MobileTable visualTable;
        private MobileLabel selectedInfo;
        private long selectedIndex;
        private string selectedDescription
            {
            get { return selectedInfo.Text; }
            }
        private const string ID_COLUMN = "Id";
        private const string VALUE_COLUMN = "Value";
        private const string DESCRIPTION_COLUMN = "Description";
        #endregion

        /// <summary>Вибір з таблиці</summary>
        public SelectTableList(WMSClient MainProcess, SelectFromListDelegate nextScr, string descriptionHeader, IEnumerable<TableData> list, string btnText, bool back)
            : base(1)
            {
            ToDoCommand = MainProcess.UserName;

            headerOfDescriptionColumn = descriptionHeader;
            navigateToNextScreen = nextScr;
            listOfElements = list;
            buttonText = btnText;
            isBackButton = back;
           
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

        #region Override methods
        public override sealed void DrawControls()
            {
            if (isLoading)
                {
                int tableHeight = 230;

                if (!string.IsNullOrEmpty(buttonText))
                    {
                    tableHeight -= 35;
                    MainProcess.CreateButton(buttonText, 3, 290, 234, 25, string.Empty, MobileButtonClick);
                    }

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
            navigateToNextScreen(selectedIndex, selectedDescription);
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
                    MainProcess.Process = new SelectingProcess();
                    break;
                case KeyAction.Enter:
                    navigateToNextScreen(selectedIndex, selectedDescription);
                    break;
                }
            }
        #endregion

        private void visualTable_OnChangeSelectedRow(object param1, OnChangeSelectedRowEventArgs e)
            {
            selectedIndex = (long)e.SelectedRow[ID_COLUMN];
            selectedInfo.Text = e.SelectedRow[DESCRIPTION_COLUMN].ToString();
            }

        private void MobileButtonClick()
            {
            if (isBackButton)
                {
                OnHotKey(KeyAction.Esc);
                }
            else
                {
                navigateToNextScreen(selectedIndex, selectedDescription);
                }
            }
        }
    }