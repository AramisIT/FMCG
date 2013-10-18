using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;
using DevExpress.XtraGrid.Views.Base;
using FMCG.TouchScreen.Controls.Editors;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Вибір зі списку об'єктів</summary>
    public partial class SelectFromObjectList : UserControl, IVerticalScroll
        {
        #region Veriables
        /// <summary>Оновлення списку</summary>
        private readonly UpdateSelectionTableDelegate updateSelectionTable;
        /// <summary>Значення обрано (перейти далі)</summary>
        private readonly SelectValueFromListDelegate selectValueFromList;
        /// <summary>Назад</summary>
        private readonly GoDelegate goBack;
        #endregion

        #region Init
        /// <summary>Вибір зі списку об'єктів</summary>
        public SelectFromObjectList()
            {
            InitializeComponent();
            inputField.LostFocus += inputField_LostFocus;
            }

        /// <summary>Вибір зі списку об'єктів</summary>
        /// <param name="nameSelection">Ім'я вибору (Формат: Оберіть [Ім'я вибору])</param>
        /// <param name="descriptionColumnCaption">Заголовок для колонки об'эктыв</param>
        /// <param name="update">Оновлення списку</param>
        /// <param name="selectValue">Значення обрано (перейти далі)</param>
        /// <param name="back">Назад </param>
        public SelectFromObjectList(string nameSelection, string descriptionColumnCaption, UpdateSelectionTableDelegate update, SelectValueFromListDelegate selectValue, GoDelegate back)
            : this()
            {
            topicLabel.Text = string.Format("Оберіть {0}", nameSelection);
            Description.Caption = descriptionColumnCaption;
            updateSelectionTable = update;
            selectValueFromList = selectValue;
            goBack = back;
            updateListData();
            inputField.Focus();
            }
        #endregion

        #region Support
        public void FocusField()
            {
            this.Focus();
            inputField.Focus();
            inputField.SelectAll();
            }

        /// <summary>Id обраної строки</summary>
        public bool SelectedRowValue(out KeyValuePair<long, string> value)
            {
            DataRow selectedRow = gridView.GetDataRow(gridView.FocusedRowHandle);

            if (selectedRow != null)
                {
                object rowIdValue = selectedRow[Id.Name];
                object rowDescValue = selectedRow[Description.Name];

                value = new KeyValuePair<long, string>(Convert.ToInt64(rowIdValue), rowDescValue.ToString());
                return true;
                }

            value = new KeyValuePair<long, string>();
            return false;
            }

        #endregion

        #region Events
        /// <summary>Змінено обраний рядок</summary>
        private void gridView_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
            {
            if (!grid.Visible)
                {
                grid.Visible = true;
                }

            DataRow selectedRow = gridView.GetDataRow(gridView.FocusedRowHandle);

            if (selectedRow != null)
                {
                string description = selectedRow[Description.Name].ToString();
                selectedRowLabel.Text = string.Format("Обраний рядок: {0}", description);
                }
            else
                {
                selectedRowLabel.Text = "Рядок не обрано...";
                }
            }

        /// <summary>Було змінено текст пошуку/порівняння</summary>
        private void inputField_TextChanged(object sender, EventArgs e)
            {
            updateListData();
            }

        /// <summary>Оновлення списку</summary>
        private void updateListData()
            {
            grid.DataSource = updateSelectionTable(inputField.Text);
            gridView_FocusedRowChanged(gridView, null);
            }

        /// <summary>Перейти далі</summary>
        private void goNext_Click(object sender, EventArgs e)
            {
            KeyValuePair<long, string> selectedRowValue;

            if (SelectedRowValue(out selectedRowValue))
                {
                selectValueFromList(selectedRowValue);
                }
            else
                {
                grid.Visible = false;
                }
            }

        private void goPrev_Click(object sender, EventArgs e)
            {
            goBack();
            }

        void inputField_LostFocus(object sender, EventArgs e)
            {
            inputField.Focus();
            }
        #endregion

        #region Scroll
        private void scrollUp_Click(object sender, EventArgs e)
            {
            gridView.TopRowIndex = gridView.TopRowIndex - 1;
            }

        private void scrollDown_Click(object sender, EventArgs e)
            {
            gridView.TopRowIndex = gridView.TopRowIndex + 1;
            }
        #endregion

        public event Action ScrollUp;

        public event Action ScrollDown;

        private void button2_Click(object sender, EventArgs e)
            {
            if (ScrollUp != null)
                {
                ScrollUp();
                }
            }

        private void button1_Click(object sender, EventArgs e)
            {
            if (ScrollDown != null)
                {
                ScrollDown();
                }
            }

        private void SelectFromObjectList_Paint(object sender, PaintEventArgs e)
            {
            this.Paint -= SelectFromObjectList_Paint;
            button1.Visible = ScrollUp != null;
            button2.Visible = ScrollUp != null;
            }
        }
    }
