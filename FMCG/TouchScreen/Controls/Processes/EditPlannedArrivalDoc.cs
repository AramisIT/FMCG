using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.TouchScreen.Events;
using Catalogs;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Documents;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Редагування документу "План приходу"</summary>
    public partial class EditPlannedArrivalDoc : UserControl
        {
        #region Veriables
        /// <summary>Колонки таблиці для редагування</summary>
        private enum EditedColumns { Description = 1, Quantity, Date, ShelfLife }
        /// <summary>Максимальна к-сть літер, що відображається в кнопці</summary>
        private const int MAX_BTN_TEXT_LENGTH = 13;
        /// <summary>Документ "План приходу"</summary>
        private PlannedArrival Document;
        /// <summary>Дія при завершені роботи з документом</summary>
        private readonly FinishEditPlannedArrivalDelegate onFinish;
        /// <summary>Список номеналутари (аналог таблиці)</summary>
        private List<NomenclatureData> list;
        /// <summary>Початкові дані для визначення документу</summary>
        private readonly PlannedArrivalData plannedData;
        /// <summary>Чи завантажено форму</summary>
        private bool isLoaded;
        /// <summary>Обрана строка для редагування</summary>
        private NomenclatureData selectedRow
            {
            get
                {
                return mainView.FocusedRowHandle <= list.Count && mainView.FocusedRowHandle >= 0
                           ? list[mainView.FocusedRowHandle]
                           : null;
                }
            }
        #endregion

        #region Init
        /// <summary>Редагування документу "План приходу"</summary>
        public EditPlannedArrivalDoc()
            {
            InitializeComponent();
            }

        /// <summary>Редагування документу "План приходу"</summary>
        /// <param name="data">Початкові дані для визначення документу</param>
        /// <param name="finish">Дія при завершені роботи з документом</param>
        public EditPlannedArrivalDoc(PlannedArrivalData data, FinishEditPlannedArrivalDelegate finish)
            : this()
            {
            plannedData = data;
            onFinish = finish;
            }

        private void EditPlannedArrivalDoc_Load(object sender, EventArgs e)
            {
            if (!isLoaded)
                {
                fillInitData(plannedData);
                isLoaded = true;
                }
            }
        #endregion

        #region Fill
        /// <summary>Заповнення форми даними ініціалізації</summary>
        /// <param name="data">Дані</param>
        private void fillInitData(PlannedArrivalData data)
            {
            //Document
            Document = new PlannedArrival();
            Document.Read(data.Invoice.Key);

            //Fields
            invoiceDate.Text = NavigatedButton.SPACES_FOR_ICOx32 + Document.Date.ToShortDateString();
            invoiceNumber.Text = NavigatedButton.SPACES_FOR_ICOx32 + Document.IncomeNumber;
            driver.Text = setValueIntoButton(Document.Driver.Description);
            car.Text = setValueIntoButton(Document.Car.Description);
            choseWare(WaresTypes.Production);
            }

        /// <summary>Встановити значення для кнопки</summary>
        /// <param name="value">Значення</param>
        /// <returns>Текст для кнопки</returns>
        private string setValueIntoButton(string value)
            {
            //Для того щоб текст був рівно після іконки він вирівнян по лівому краю і додано відповідна кількість пробілів
            //Ще довга назва обрізається, щоб не було переходу на іншу строку
            return string.Concat(NavigatedButton.SPACES_FOR_ICOx32,
                                 value.Length <= MAX_BTN_TEXT_LENGTH
                                     ? value
                                     : string.Concat(value.Substring(0, MAX_BTN_TEXT_LENGTH), "..."));
            }
        #endregion

        #region Edit
        /// <summary>Оновити контрол редагування даних</summary>
        /// <param name="action">Дія з додаванням контролу</param>
        private void updateEditControl(Action action)
            {
            int editControls = editControlsArea.Controls.Count;

            //Додавання контролу
            if (action != null)
                {
                action();
                }

            //Очищення старих контролів (щоб зменшити ефекти переходів)
            for (int i = 0; i < editControls; i++)
                {
                editControlsArea.Controls.RemoveAt(0);
                }
            }

        /// <summary>Відобразити повідомлення</summary>
        /// <param name="message">Повідомлення</param>
        private void showMessage(string message)
            {
            showMessage(message, string.Empty);
            }

        /// <summary>Відобразити повідомлення</summary>
        /// <param name="message">Повідомлення</param>
        /// <param name="detailInfo">Детальна інформація</param>
        private void showMessage(string message, string detailInfo)
            {
            int editControls = editControlsArea.Controls.Count;

            editControlsArea.Controls.Add(new Message(message, detailInfo) { Dock = DockStyle.Fill });

            for (int i = 0; i < editControls; i++)
                {
                editControlsArea.Controls.RemoveAt(0);
                }
            }

        /// <summary>"Назад" (очишення панелі для контролів редагування)</summary>
        private void Back()
            {
            editControlsArea.Controls.Clear();
            }

        #region invoiceNumber
        private void invoiceNumber_Click(object sender, EventArgs e)
            {
            updateEditControl(installInvoiceNumberEditiors);
            }

        private void installInvoiceNumberEditiors()
            {
            EnterField enterField = new EnterField("Введіть номер", Document.IncomeNumber)
                                        {
                                            Dock = DockStyle.Fill
                                        };
            enterField.FieldValueIsChanged += enterField_FieldValueIsChanged;
            editControlsArea.Controls.Add(enterField);
            }

        private void enterField_FieldValueIsChanged(object sender, ValueIsChangedArgs<string> e)
            {
            Document.IncomeNumber = e.Value;
            invoiceNumber.Text = NavigatedButton.SPACES_FOR_ICOx32 + Document.IncomeNumber;
            }
        #endregion

        #region invoiceDate
        private void invoiceDate_Click(object sender, EventArgs e)
            {
            updateEditControl(installInvoiceDateEditiors);
            }

        private void installInvoiceDateEditiors()
            {
            DateEdit dateEdit = new DateEdit(Document.Date);
            dateEdit.DateIsChanged += dateEdit_DateIsChanged;
            editControlsArea.Controls.Add(dateEdit);
            }

        private void dateEdit_DateIsChanged(object sender, ValueIsChangedArgs<DateTime> e)
            {
            Document.Date = e.Value;
            invoiceDate.Text = NavigatedButton.SPACES_FOR_ICOx32 + Document.Date.ToShortDateString();
            }
        #endregion

        #region Driver
        private void driver_Click(object sender, EventArgs e)
            {
            updateEditControl(installDriverEditors);
            }

        private void installDriverEditors()
            {
            SelectFromObjectList selectDriver = new SelectFromObjectList(
                "водія", "Водій", UpdateDriver, SelectDriverValue, Back) { Dock = DockStyle.Fill };
            editControlsArea.Controls.Add(selectDriver);
            }

        private void SelectDriverValue(KeyValuePair<long, string> value)
            {
            Drivers driverValue = new Drivers();
            driverValue.Read(value.Key);
            Document.Driver = driverValue;
            driver.Text = setValueIntoButton(Document.Driver.Description);
            showMessage("Обрано нового водія!");
            }

        private DataTable UpdateDriver(string enterValue)
            {
            Query query = DB.NewQuery(
                "SELECT Id,RTRIM(Description)Description FROM Drivers WHERE Description like '%'+@Driver+'%' AND IsGroup=0 ORDER BY Description");
            query.AddInputParameter("Driver", enterValue);
            DataTable table = query.SelectToTable();

            return table;
            }
        #endregion

        #region Car
        private void car_Click(object sender, EventArgs e)
            {
            updateEditControl(installCarEditors);
            }

        private void installCarEditors()
            {
            SelectFromObjectList selectDriver = new SelectFromObjectList(
                "машину", "Машина", UpdateCar, SelectCarValue, Back) { Dock = DockStyle.Fill };
            editControlsArea.Controls.Add(selectDriver);
            }

        private void SelectCarValue(KeyValuePair<long, string> value)
            {
            Cars driverValue = new Cars();
            driverValue.Read(value.Key);
            Document.Car = driverValue;
            car.Text = setValueIntoButton(Document.Car.Description);
            showMessage("Обрано нову машину!");
            }

        private DataTable UpdateCar(string enterValue)
            {
            Query query = DB.NewQuery(
                "SELECT Id,RTRIM(Description)Description FROM Cars WHERE Description like '%'+@Car+'%' AND IsGroup=0 ORDER BY Description");
            query.AddInputParameter("Car", enterValue);
            DataTable table = query.SelectToTable();

            return table;
            }
        #endregion

        #region Save
        /// <summary>Отримати словник партій для всієї таблиці</summary>
        private Dictionary<long, Parties> getPartyForTable()
            {
            Dictionary<long, Parties> partyDic = new Dictionary<long, Parties>();

            foreach (NomenclatureData element in waresList)
                {
                if (element.Description != null && !partyDic.ContainsKey(element.Description.Id))
                    {
                    Parties party = getPartyForNomenclatureByDate(element.Date, element.Description.Id, element.ShelfLifeDays);
                    partyDic.Add(element.Description.Id, party);
                    }
                }

            return partyDic;
            }

        /// <summary>Отримати партію для номенталатури на обрану дату</summary>
        /// <param name="date">Дата</param>
        /// <param name="nomenclature">Номенлатура</param>
        /// <returns>Партія</returns>
        private Parties getPartyForNomenclatureByDate(DateTime date, long nomenclature, int shelfLifeDays)
            {
            Query query = DB.NewQuery(@"SELECT Id FROM Parties WHERE Nomenclature=@Nomenclature AND CAST(DateOfManufacture AS DATE)=@Date");
            query.AddInputParameter("Date", date.Date);
            query.AddInputParameter("Nomenclature", nomenclature);
            object id = query.SelectScalar();
            Parties party = new Parties();

            if (id == null)
                {
                party.DateOfManufacture = date;
                party.Nomenclature = (Nomenclature)new Nomenclature().Read(nomenclature);
                party.FillAddData(shelfLifeDays);
                party.Write();
                }
            else
                {
                party.Read(id);
                }

            return party;
            }

        /// <summary>Конвертація списку елементів в таблицю</summary>
        private void convertListsToTables()
            {
            Dictionary<long, Parties> partyDic = getPartyForTable();
            Document.NomenclatureInfo.Rows.Clear();
            foreach (NomenclatureData data in waresList)
                {
                if (data.Description != null && data.Description.Id != 0)
                    {
                    DataRow newRow = Document.NomenclatureInfo.GetNewRow(Document);
                    newRow.SetRefValueToRowCell(Document, Document.Nomenclature, data.Description.Id, typeof(Nomenclature));
                    newRow[Document.NomenclatureCount] = data.Quantity;
                    newRow.SetRefValueToRowCell(Document, Document.NomenclatureParty, partyDic[data.Description.Id]);
                    newRow.AddRowToTable(Document);

                    var nomenclature = new Nomenclature();
                    nomenclature.Read(data.Description.Id);
                    if (nomenclature.ShelfLife != data.ShelfLifeDays)
                        {
                        nomenclature.ShelfLife = data.ShelfLifeDays;
                        nomenclature.Write();
                        }
                    }
                }
            Document.SetSubtableModified(Document.NomenclatureInfo.TableName);



            Document.TareInfo.Rows.Clear();
            tareList = tareList ?? new List<NomenclatureData>();
            foreach (NomenclatureData data in tareList)
                {
                if (data.Description != null && data.Description.Id != 0)
                    {
                    DataRow newRow = Document.TareInfo.GetNewRow(Document);
                    newRow.SetRefValueToRowCell(Document, Document.Tare, data.Description.Id, typeof(Nomenclature));
                    newRow[Document.TareCount] = data.Quantity;
                    newRow.AddRowToTable(Document);
                    }
                }
            Document.SetSubtableModified(Document.TareInfo.TableName);
            }

        /// <summary>Завершити/Зберегти</summary>
        private void finish_Click(object sender, EventArgs e)
            {
            convertListsToTables();
            WritingResult result = Document.Write();

            if (result != WritingResult.Success)
                {
                if (isEditMode)
                    {
                    changeEditMode(false);
                    }
                showMessage("Помилка при збережені даних!");
                }
            else
                {
                onFinish(true, Document);
                Document.PrintStickers();
                }
            }
        #endregion

        #region Exit
        /// <summary>Вихід без збереження даних</summary>
        private void exit_Click(object sender, EventArgs e)
            {
            onFinish(false, Document);
            }
        #endregion
        #endregion

        #region EditTableData
        private void mainView_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
            {
            startCellValueEditig();
            }

        private void mainView_RowClick(object sender, RowClickEventArgs e)
            {
            GridHitInfo hitInfo = mainView.CalcHitInfo(new Point(e.X, e.Y));

            if (ModifierKeys != Keys.None)
                {
                return;
                }

            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                {
                if (!isEditMode)
                    {
                    startCellValueEditig();
                    }

                updateSelectedRowInfo();
                }
            }

        private void updateSelectedRowInfo()
            {
            NomenclatureData selectedData = selectedRow;
            selectedRowInfo.Text = selectedData != null
                                       ? string.Format(
                                           "Обрано рядок №{0}\r\n{1} / {2} / {3}",
                                           selectedData.LineNumber,
                                           selectedData.Description,
                                           selectedData.Quantity,
                                           selectedData.Date.ToShortDateString())
                                       : string.Empty;
            }

        private void startCellValueEditig()
            {
            NomenclatureData currSelectedRow = selectedRow;

            if (currSelectedRow != null)
                {
                EditedColumns selectedColumn = (EditedColumns)mainView.FocusedColumn.VisibleIndex;

                switch (selectedColumn)
                    {
                    case EditedColumns.Description:
                        updateEditControl(changeNomenclatureData);
                        break;
                    case EditedColumns.Quantity:
                        updateEditControl(installQuantityEditiors);
                        break;
                    case EditedColumns.Date:
                        updateEditControl(installDateEditiors);
                        break;

                    case EditedColumns.ShelfLife:
                        updateEditControl(installShelfLifeEditiors);
                        break;
                    }
                }
            }

        #region Edit table mode
        /// <summary>Режим редагування таблиці</summary>
        private bool isEditMode;

        private List<NomenclatureData> tareList;
        private List<NomenclatureData> waresList;

        /// <summary>Зміна режиму роботи з таблицею</summary>
        /// <param name="start"></param>
        private void changeEditMode(bool start)
            {
            isEditMode = start;
            invoiceNumber.Enabled = !isEditMode;
            invoiceDate.Enabled = !isEditMode;
            car.Enabled = !isEditMode;
            driver.Enabled = !isEditMode;
            editControlsArea.Visible = !isEditMode;
            }

        /// <summary>Редагувати тиблицю (SingleClick!)</summary>
        private void editMode_SingleClick(object sender, EventArgs e)
            {
            changeEditMode(!isEditMode);
            }
        #endregion

        #region Nomenclature
        private void changeNomenclatureData()
            {
            SelectFromObjectList selectNomenclature = new SelectFromObjectList(
                "номенклатуру", "Номенклатура", (enterValue) => UpdateNomenclature(enterValue, list.Equals(tareList)), SelectNomenclatureValue, Back) { Dock = DockStyle.Fill };
            editControlsArea.Controls.Add(selectNomenclature);
            selectNomenclature.FocusField();
            }

        private DataTable UpdateNomenclature(string enterValue, bool isTare)
            {
            Query query = DB.NewQuery(
                "SELECT Id,RTRIM(Description)Description FROM Nomenclature WHERE MarkForDeleting = 0 and IsTare = @IsTare and Description like '%'+@Description+'%' ORDER BY Description");
            query.AddInputParameter("Description", enterValue);
            query.AddInputParameter("IsTare", isTare);
            DataTable table = query.SelectToTable();

            return table;
            }

        private void SelectNomenclatureValue(KeyValuePair<long, string> value)
            {
            NomenclatureData currentRow = selectedRow;

            if (currentRow.Description != null && currentRow.Description.Id == value.Key)
                {
                editControlsArea.Controls.Clear();
                }
            else
                {
                foreach (NomenclatureData data in list)
                    {
                    if (data.Description != null &&
                        data.Description.Id == value.Key &&
                        data.LineNumber != currentRow.LineNumber)
                        {
                        string message = string.Format(
                            "\r\nВ таблиці вже існує рядок №{0} з такою номенлатурою..", data.LineNumber);
                        showMessage("Повторення номенклатури!", message);
                        return;
                        }
                    }

                selectedRow.Description = new ObjectValue(value);
                grid.RefreshDataSource();
                showMessage("Обрано нову номенклатуру!", value.Value);
                }
            }
        #endregion

        #region Date
        private void installDateEditiors()
            {
            DateEdit dateEdit = new DateEdit(selectedRow.Date);
            dateEdit.DateIsChanged += date_DateIsChanged;
            editControlsArea.Controls.Add(dateEdit);
            }

        private void date_DateIsChanged(object sender, ValueIsChangedArgs<DateTime> e)
            {
            selectedRow.Date = e.Value;
            grid.RefreshDataSource();
            }
        #endregion

        #region Quantity
        private void installQuantityEditiors()
            {
            NumberEdit quantityEdit = new NumberEdit((int)selectedRow.Quantity, Back, FinishQuantityEdit);
            quantityEdit.ValueIsChanged += quantityEdit_ValueIsChanged;
            editControlsArea.Controls.Add(quantityEdit);
            }



        private void FinishQuantityEdit(string newValue)
            {
            showMessage("К-сть змінено!");
            }

        private void quantityEdit_ValueIsChanged(object sender, ValueIsChangedArgs<int> e)
            {
            selectedRow.Quantity = e.Value;
            grid.RefreshDataSource();
            }
        #endregion

        // Shelf life
        private void installShelfLifeEditiors()
            {
            NumberEdit quantityEdit = new NumberEdit((int)selectedRow.ShelfLifeDays, Back, (newValue) => showMessage("Термін придатності змінено!"));
            quantityEdit.ValueIsChanged += (sender, e) =>
            {
                selectedRow.ShelfLifeDays = e.Value;
                grid.RefreshDataSource();
            };
            editControlsArea.Controls.Add(quantityEdit);
            }

        #endregion

        #region Edit table
        /// <summary>Видалення обраного рядка</summary>
        private void deleteRow_Click(object sender, EventArgs e)
            {
            NomenclatureData selectedElement = selectedRow;

            //Якщо строка виділена
            if (selectedElement != null)
                {
                //Видалення
                list.Remove(selectedElement);
                //Оновлення номерів рядків
                long index = selectedElement.LineNumber;

                for (int i = (int)selectedElement.LineNumber - 1; i < list.Count; i++)
                    {
                    NomenclatureData element = list[i];
                    element.LineNumber = index++;
                    }

                //Оновлення дангих на формі
                grid.RefreshDataSource();
                updateSelectedRowInfo();
                }
            }

        /// <summary>Додавання нового рядка</summary>
        private void addRow_Click(object sender, EventArgs e)
            {
            list.Add(new NomenclatureData { LineNumber = list.Count + 1 });
            grid.RefreshDataSource();
            updateSelectedRowInfo();
            }

        /// <summary>Вихід з режиму редагування таблиці</summary>
        private void finishEditMode_Click(object sender, EventArgs e)
            {
            changeEditMode(false);
            }
        #endregion

        private void waresButton_Click(object sender, EventArgs e)
            {
            choseWare(WaresTypes.Production);
            }

        enum WaresTypes
            {
            Tare,
            Production
            }

        private void tareButton_Click(object sender, EventArgs e)
            {
            choseWare(WaresTypes.Tare);
            }

        private void choseWare(WaresTypes waresTypes)
            {
            switch (waresTypes)
                {
                case WaresTypes.Production:
                    waresButton.Enabled = false;
                    tareButton.Enabled = true;
                    tareList = list;
                    if (waresList != null)
                        {
                        list = waresList;
                        }
                    else
                        {
                        fillEditableTable(waresTypes);
                        waresList = list;
                        }
                    break;

                case WaresTypes.Tare:
                    tareButton.Enabled = false;
                    waresButton.Enabled = true;
                    waresList = list;
                    if (tareList != null)
                        {
                        list = tareList;
                        }
                    else
                        {
                        fillEditableTable(waresTypes);
                        tareList = list;
                        }
                    break;
                }

            grid.DataSource = list;
            editControlsArea.Controls.Clear();
            }

        private void fillEditableTable(WaresTypes waresTypes)
            {
            var isTare = waresTypes == WaresTypes.Tare;
            var table = isTare ? Document.TareInfo : Document.NomenclatureInfo;

            list = new List<NomenclatureData>();

            if (isTare)
                {
                Date.Visible = false;
                shelfLifeDaysGridColumn.Visible = false;
                }

            foreach (DataRow row in table.Rows)
                {
                long nomemclatureId = (long)row[isTare ? Document.Tare : Document.Nomenclature];

                string nomenclatureDescription = null;
                var shelfLifeDays = 0;

                if (isTare)
                    {
                    nomenclatureDescription = FastInput.GetCashedData(typeof(Nomenclature).Name).GetDescription(nomemclatureId);
                    }
                else
                    {
                    var nomenclature = new Nomenclature();
                    nomenclature.Read(nomemclatureId);
                    nomenclatureDescription = nomenclature.Description;
                    shelfLifeDays = nomenclature.ShelfLife;
                    }

                NomenclatureData element = new NomenclatureData
                    {
                        LineNumber = Convert.ToInt64(row["LineNumber"]),
                        Description = new ObjectValue(nomenclatureDescription, nomemclatureId),
                        Quantity = Convert.ToDecimal(row[isTare ? Document.TareCount : Document.NomenclatureCount]),
                        ShelfLifeDays = shelfLifeDays
                    };

                if (!isTare)
                    {
                    Parties party = new Parties();
                    party.Read(row[Document.NomenclatureParty]);
                    element.Date = party.DateOfManufacture;
                    }

                list.Add(element);
                }
            }

        private void mainView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
            {
            object value = null;

            if (e.Column == Quantity)
                {
                value = ((int)Math.Round(Convert.ToDecimal(e.Value), 0));
                }
            else if (e.Column == Date)
                {
                var currentDate = (DateTime)e.Value;
                value = DateTime.MinValue.Equals(currentDate) ? string.Empty : currentDate.ToString("dd.MM.yy");
                }

            if (value != null)
                {
                e.DisplayText = value.ToString();
                }
            }



        }
    }