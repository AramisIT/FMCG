using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Extensions;
using Aramis.UI.WinFormsDevXpress;
using AramisInfostructure.Queries;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.TouchScreen.Events;
using Catalogs;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Documents;
using FMCG.TouchScreen.Controls.Editors;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Редагування документу "План приходу"</summary>
    public partial class EditAcceptancePlanDoc : UserControl, IVerticalScroll
        {
        #region Veriables

        /// <summary>Колонки таблиці для редагування</summary>
        private enum EditedColumns
            {
            Description = 1,
            Quantity,
            Date,
            ShelfLife,
            StandartPalletsCount,
            NonStandartPalletsCount,
            StandartPalletCountPer1,
            NonStandartPalletCountPer1,
            UnitsOnNotFullPallet,
            UnitsOnNotFullNonStandartPallet
            }

        /// <summary>Максимальна к-сть літер, що відображається в кнопці</summary>
        private const int MAX_BTN_TEXT_LENGTH = 13;
        /// <summary>Документ "План приходу"</summary>
        private AcceptancePlan Document;
        /// <summary>Дія при завершені роботи з документом</summary>
        private readonly FinishEditAcceptancePlanDelegate onFinish;
        /// <summary>Список номеналутари (аналог таблиці)</summary>
        private List<NomenclatureData> list;
        /// <summary>Початкові дані для визначення документу</summary>
        private readonly AcceptancePlanData plannedData;
        /// <summary>Чи завантажено форму</summary>
        private bool isLoaded;
        /// <summary>Обрана строка для редагування</summary>
        private NomenclatureData selectedRow
            {
            get
                {
                if (list != null
                    && mainView.FocusedRowHandle <= list.Count && mainView.FocusedRowHandle >= 0)
                    {
                    return list[mainView.GetFocusedDataSourceRowIndex()];
                    }

                return NomenclatureData.ZeroValue;
                }
            }
        #endregion

        #region Init
        /// <summary>Редагування документу "План приходу"</summary>
        public EditAcceptancePlanDoc()
            {
            InitializeComponent();
            assignScrollHandlers(this);
            foreach (BandedGridColumn column in mainView.Columns)
                {
                column.OptionsColumn.AllowGroup = DefaultBoolean.False;
                column.OptionsColumn.AllowSort = DefaultBoolean.False;
                column.OptionsColumn.AllowMove = false;
                column.OptionsFilter.AllowFilter = false;
                }
            }

        /// <summary>Редагування документу "План приходу"</summary>
        /// <param name="data">Початкові дані для визначення документу</param>
        /// <param name="finish">Дія при завершені роботи з документом</param>
        public EditAcceptancePlanDoc(AcceptancePlanData data, FinishEditAcceptancePlanDelegate finish)
            : this()
            {
            plannedData = data;
            onFinish = finish;
            }

        private const int VISIBLE_ROWS_COUNT = 8;

        private void updateVerticalScrollVisibility()
            {
            bool showVerticalScroll = list != null && list.Count > VISIBLE_ROWS_COUNT;
            scrollUp.Visible = showVerticalScroll;
            scrollDown.Visible = showVerticalScroll;
            }

        private void EditAcceptancePlanDoc_Load(object sender, EventArgs e)
            {
            grid.Width = 425;
            updateSelectedRowInfo();
            mainView.HorzScrollVisibility = ScrollVisibility.Never;

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
        private void fillInitData(AcceptancePlanData data)
            {
            //Document
            Document = new AcceptancePlan() { ReadingId = data.Invoice.Key };

            //Fields
            invoiceDate.Text = NavigatedButton.SPACES_FOR_ICOx32 + Document.Date.ToShortDateString();
            invoiceNumber.Text = Document.IncomeNumber;
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
            assignScrollHandlers(enterField);
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
            assignScrollHandlers(dateEdit);
            editControlsArea.Controls.Add(dateEdit);
            }

        private void assignScrollHandlers(IVerticalScroll verticalScroll)
            {
            if (list == null || list.Count <= VISIBLE_ROWS_COUNT)
                {
                return;
                }

            verticalScroll.ScrollUp += () =>
                {
                    mainView.TopRowIndex--;
                };

            verticalScroll.ScrollDown += () =>
                {
                    mainView.TopRowIndex++;
                };
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
            assignScrollHandlers(selectDriver);
            editControlsArea.Controls.Add(selectDriver);
            }

        private void SelectDriverValue(KeyValuePair<long, string> value)
            {
            Drivers driverValue = new Drivers() { ReadingId = value.Key };
            Document.Driver = driverValue;
            driver.Text = setValueIntoButton(Document.Driver.Description);
            showMessage("Обрано нового водія!");
            }

        private DataTable UpdateDriver(string enterValue)
            {
            IQuery query = DB.NewQuery(
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
            assignScrollHandlers(selectDriver);
            editControlsArea.Controls.Add(selectDriver);
            }

        private void SelectCarValue(KeyValuePair<long, string> value)
            {
            Cars driverValue = new Cars() { ReadingId = value.Key };
            Document.Car = driverValue;
            car.Text = setValueIntoButton(Document.Car.Description);
            showMessage("Обрано нову машину!");
            }

        private DataTable UpdateCar(string enterValue)
            {
            IQuery query = DB.NewQuery(
                "SELECT Id,RTRIM(Description)Description FROM Cars WHERE Description like '%'+@Car+'%' AND IsGroup=0 ORDER BY Description");
            query.AddInputParameter("Car", enterValue);
            DataTable table = query.SelectToTable();

            return table;
            }
        #endregion

        #region Save
        /// <summary>Отримати словник партій для всієї таблиці</summary>
        private Dictionary<Product, Parties> getPartyForTable()
            {
            var partyDic = new Dictionary<Product, Parties>();

            foreach (NomenclatureData element in waresList)
                {
                var product = new Product(element.Description.Id, element.Date.Date);
                if (element.Description != null && !partyDic.ContainsKey(product))
                    {
                    Parties party = getPartyForNomenclatureByDate(element.Date, element.Description.Id, element.ShelfLifeDays);
                    partyDic.Add(product, party);
                    }
                }

            return partyDic;
            }

        /// <summary>Отримати партію для номенталатури на обрану дату</summary>
        /// <param name="date">Дата</param>
        /// <param name="nomenclatureId">Номенклатура</param>
        /// <returns>Партія</returns>
        private Parties getPartyForNomenclatureByDate(DateTime date, long nomenclatureId, int shelfLifeDays)
            {
            Parties party = Parties.Find(nomenclatureId, date, shelfLifeDays);

            if (party.Id == 0)
                {
                party.DateOfManufacture = date;
                party.Nomenclature = new Nomenclature() { ReadingId = nomenclatureId };
                party.FillAddData(shelfLifeDays);
                party.Write();
                }

            return party;
            }

        struct Product
            {
            long NomenclatureId;
            private DateTime ProductionDate;

            public Product(long nomenclatureId, DateTime productionDate)
                {
                this.NomenclatureId = nomenclatureId;
                this.ProductionDate = productionDate;
                }
            }

        /// <summary>Конвертація списку елементів в таблицю</summary>
        private void convertListsToTables()
            {
            Dictionary<Product, Parties> partyDic = getPartyForTable();
            Document.NomenclatureInfo.Rows.Clear();
            foreach (NomenclatureData data in waresList)
                {
                if (data.Description != null && data.Description.Id != 0)
                    {
                    DataRow newRow = Document.NomenclatureInfo.GetNewRow(Document);
                    newRow.SetRefValueToRowCell(Document, Document.Nomenclature, data.Description.Id, typeof(Nomenclature));
                    newRow[Document.NomenclatureCount] = data.Quantity;
                    newRow.SetRefValueToRowCell(Document, Document.NomenclatureParty, partyDic[new Product(data.Description.Id, data.Date.Date)]);
                    newRow.AddRowToTable(Document);

                    var nomenclature = new Nomenclature() { ReadingId = data.Description.Id };
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

            }

        private bool isLastPlan(AcceptancePlan document)
            {
            var q = DB.NewQuery(@"with caps as (
select Id from AcceptancePlan

where CAST([Date] as date) = @Date
and Driver = @Driver
and Car = @Car
and MarkForDeleting = 0
), stickersCount as ( 
 select caps.Id, count(stickers.LineNumber) stickersCount
 
 from caps
 left join SubAcceptancePlanStickers stickers on caps.Id = stickers.IdDoc
 group by caps.Id
 )
 
 select count(*) quantity from stickersCount where stickersCount = 0");
            q.AddInputParameter("Date", document.Date.StartOfDay());
            q.AddInputParameter("Driver", document.Driver.Id);
            q.AddInputParameter("Car", document.Car.Id);
            var plansCount = q.SelectScalar();

            return q.ThrowedException == null && Convert.ToInt32(plansCount) == 0;
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

            currentNomenclatureLabel.Text = (selectedRow.Description ?? new ObjectValue(string.Empty, 0)).Description;
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
                        updateEditControl(() => installNumberEditEditior(selectedRow.Quantity, (enteredValue) =>
                        {
                            selectedRow.Quantity = enteredValue;
                            selectedRow.UpdatePalletQuantity();
                        }, "Кіль-сть змінено!"));
                        break;
                    case EditedColumns.Date:
                        updateEditControl(installDateEditiors);
                        break;

                    case EditedColumns.ShelfLife:
                        updateEditControl(() => installNumberEditEditior(selectedRow.ShelfLifeDays, (enteredValue) =>
                            {
                                selectedRow.ShelfLifeDays = enteredValue;
                            }, "Термін придатності змінено!"));
                        break;

                    case EditedColumns.StandartPalletsCount:
                        updateEditControl(() => installNumberEditEditior(selectedRow.StandartPalletsCount, (enteredValue) =>
                            {
                                selectedRow.StandartPalletsCount = enteredValue;
                                selectedRow.UpdateQuantity();
                            }, "Кіль-сть стандартних палет змінено!"));
                        break;

                    case EditedColumns.NonStandartPalletsCount:
                        updateEditControl(() => installNumberEditEditior(selectedRow.NonStandartPalletsCount, (enteredValue) =>
                            {
                                selectedRow.NonStandartPalletsCount = enteredValue;
                                selectedRow.UpdateQuantity();
                            }, "Кіль-сть нестандартних палет змінено!"));
                        break;

                    case EditedColumns.StandartPalletCountPer1:
                        updateEditControl(() => installNumberEditEditior(selectedRow.UnitsAmountInOneStandartPallet, (enteredValue) =>
                        {
                            selectedRow.UnitsAmountInOneStandartPallet = enteredValue;
                            selectedRow.UpdateQuantity();
                        }, "Кількість од. на стандарт. пал. змінено!"));
                        break;

                    case EditedColumns.NonStandartPalletCountPer1:
                        updateEditControl(() => installNumberEditEditior(selectedRow.UnitsAmountInOneNonStandartPallet, (enteredValue) =>
                            {
                                selectedRow.UnitsAmountInOneNonStandartPallet = enteredValue;
                                selectedRow.UpdateQuantity();
                            }, "Кількість од. на нестандарт. пал. змінено!"));
                        break;

                    case EditedColumns.UnitsOnNotFullPallet:
                        updateEditControl(() => installNumberEditEditior(selectedRow.UnitsOnNotFullPallet, (enteredValue) =>
                            {
                                selectedRow.UnitsOnNotFullPallet = enteredValue;
                                selectedRow.UpdateQuantity();
                            }, "Кількість од. на неповній палеті змінено!"));
                        break;

                    case EditedColumns.UnitsOnNotFullNonStandartPallet:
                        updateEditControl(() => installNumberEditEditior(selectedRow.UnitsOnNotFullNonStandartPallet, (enteredValue) =>
                            {
                                selectedRow.UnitsOnNotFullNonStandartPallet = enteredValue;
                                selectedRow.UpdateQuantity();
                            }, "Кіль-ть од. на неповній нестандартній пал. змінено!"));
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
            assignScrollHandlers(selectNomenclature);
            editControlsArea.Controls.Add(selectNomenclature);
            selectNomenclature.FocusField();
            }

        private DataTable UpdateNomenclature(string enterValue, bool isTare)
            {
            IQuery query = DB.NewQuery(
                @"SELECT Id,RTRIM(Description)Description FROM Nomenclature 
                WHERE MarkForDeleting = 0 and IsTare = @IsTare 
                and (@IsTare = 1 or (UnitsQuantityPerPallet>0 and UnitsQuantityPerPack>0))
                and Description like '%'+@Description+'%' 
                ORDER BY Description");
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
                //foreach (NomenclatureData data in list)
                //    {
                //    if (data.Description != null &&
                //        data.Description.Id == value.Key &&
                //        data.LineNumber != currentRow.LineNumber)
                //        {
                //        string message = string.Format(
                //            "\r\nВ таблиці вже існує рядок №{0} з такою номенлатурою..", data.LineNumber);
                //        showMessage("Повторення номенклатури!", message);
                //        return;
                //        }
                //    }

                selectedRow.Description = new ObjectValue(value);

                var nomenclature = new Nomenclature() { ReadingId = value.Key };
                selectedRow.UnitsAmountInOneStandartPallet = nomenclature.UnitsQuantityPerPallet;
                selectedRow.WithoutTray = nomenclature.WithoutTray;
                selectedRow.UnitsAmountInOneNonStandartPallet = 0;
                selectedRow.UpdatePalletQuantity();

                grid.RefreshDataSource();
                showMessage("Обрано нову номенклатуру!", value.Value);
                }

            updateSelectedRowInfo();
            }
        #endregion

        #region Date
        private void installDateEditiors()
            {
            DateEdit dateEdit = new DateEdit(selectedRow.Date);
            dateEdit.DateIsChanged += date_DateIsChanged;
            assignScrollHandlers(dateEdit);
            editControlsArea.Controls.Add(dateEdit);
            }

        private void date_DateIsChanged(object sender, ValueIsChangedArgs<DateTime> e)
            {
            selectedRow.Date = e.Value;
            grid.RefreshDataSource();
            }
        #endregion

        private void installNumberEditEditior(int startValue, Action<int> setResultValue, string endMessageText)
            {
            NumberEdit quantityEdit = new NumberEdit(startValue, Back, (newValue) => showMessage(endMessageText));
            quantityEdit.ValueIsChanged += (sender, e) =>
                {
                    setResultValue(e.Value);
                    grid.RefreshDataSource();
                };

            assignScrollHandlers(quantityEdit);
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
            updateVerticalScrollVisibility();
            }

        /// <summary>Додавання нового рядка</summary>
        private void addRow_Click(object sender, EventArgs e)
            {
            list.Add(new NomenclatureData { LineNumber = list.Count + 1 });
            grid.RefreshDataSource();
            updateSelectedRowInfo();
            updateVerticalScrollVisibility();
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
            var isProductuin = waresTypes == WaresTypes.Production;
            setVisibilityForMainColumns(true);

            if (isProductuin)
                {
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

                shelfLifeDaysGridColumn.RowIndex = 1;

                nonStandartPalletsCountColumn.RowIndex = 1;
                nonStandartPalletCountPer1Column.RowIndex = 1;
                unitsOnNotFullNonStandartPalletColumn.RowIndex = 1;
                }
            else
                {
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
                }


            dateColumn.Visible = isProductuin;
            shelfLifeDaysGridColumn.Visible = isProductuin;

            standartPalletsCountColumn.Visible = isProductuin;
            nonStandartPalletsCountColumn.Visible = isProductuin;

            standartPalletCountPer1Column.Visible = isProductuin;
            nonStandartPalletCountPer1Column.Visible = isProductuin;

            unitsOnNotFullPalletColumn.Visible = isProductuin;
            unitsOnNotFullNonStandartPalletColumn.Visible = isProductuin;


            palletsCountButton.Enabled = isProductuin;

            grid.DataSource = list;
            editControlsArea.Controls.Clear();
            updateVerticalScrollVisibility();
            editControlsArea.Controls.Add(scrollUp);
            editControlsArea.Controls.Add(scrollDown);

            updateSelectedRowInfo();
            }

        private void fillEditableTable(WaresTypes waresTypes)
            {
            var isTare = waresTypes == WaresTypes.Tare;
            var table = isTare ? Document.TareInfo : Document.NomenclatureInfo;

            list = new List<NomenclatureData>();

            if (table.Rows.Count > 0)
                {
                foreach (DataRow row in table.Rows)
                    {
                    long nomemclatureId = (long)row[isTare ? Document.Tare : Document.Nomenclature];

                    string nomenclatureDescription = null;
                    bool withoutTray = false;
                    var shelfLifeDays = 0;
                    var unitsQuantityPerPallet = 0;

                    if (isTare)
                        {
                        nomenclatureDescription =
                            FastInputDataCache.GetCashedData(typeof(Nomenclature).Name).GetDescription(nomemclatureId);
                        }
                    else
                        {
                        var nomenclature = new Nomenclature() { ReadingId = nomemclatureId };
                        nomenclatureDescription = nomenclature.Description;
                        shelfLifeDays = nomenclature.ShelfLife;
                        unitsQuantityPerPallet = nomenclature.UnitsQuantityPerPallet;
                        withoutTray = nomenclature.WithoutTray;
                        }

                    NomenclatureData element = new NomenclatureData
                        {
                            LineNumber = Convert.ToInt64(row["LineNumber"]),
                            Description = new ObjectValue(nomenclatureDescription, nomemclatureId),
                            ShelfLifeDays = shelfLifeDays,
                            UnitsAmountInOneStandartPallet = unitsQuantityPerPallet,
                            WithoutTray = withoutTray
                        };

                    element.Quantity = Convert.ToInt32(row[isTare ? Document.TareCount : Document.NomenclatureCount]);

                    if (!isTare)
                        {
                        Parties party = new Parties() { ReadingId = row[Document.NomenclatureParty] };
                        element.Date = party.DateOfManufacture;
                        element.UpdatePalletQuantity();
                        }

                    list.Add(element);
                    }
                }
            else if (isTare)
                {
                computeTare();
                }
            }

        private void computeTare()
            {
            var tareDictionary = new Dictionary<long, NomenclatureData>();

            foreach (var nomenclatureData in waresList)
                {
                NomenclatureData tare;
                if (getBoxes(nomenclatureData.Description.Id, nomenclatureData.Quantity, out tare))
                    {
                    NomenclatureData existsTare;
                    if (tareDictionary.TryGetValue(tare.Description.Id, out existsTare))
                        {
                        existsTare.Quantity += tare.Quantity;
                        }
                    else
                        {
                        tareDictionary.Add(tare.Description.Id, tare);
                        }
                    }

                var standartTrays = nomenclatureData.StandartPalletsCount + (nomenclatureData.UnitsOnNotFullPallet > 0 ? 1 : 0);
                addTrays(tareDictionary, standartTrays, Consts.StandartTray);

                var nonStandartTrays = nomenclatureData.NonStandartPalletsCount + (nomenclatureData.UnitsOnNotFullNonStandartPallet > 0 ? 1 : 0);
                addTrays(tareDictionary, nonStandartTrays, Consts.NonStandartTray);
                }

            long lineNumber = 0;
            foreach (var tare in tareDictionary.Values)
                {
                lineNumber++;
                tare.LineNumber = lineNumber;
                list.Add(tare);
                }
            }

        private void addTrays(Dictionary<long, NomenclatureData> tareDictionary, int standartTrays, Nomenclature nomenclatureTray)
            {
            if (standartTrays < 1) return;
            NomenclatureData tare;
            if (tareDictionary.TryGetValue(nomenclatureTray.Id, out tare))
                {
                tare.Quantity += standartTrays;
                }
            else
                {
                tare = new NomenclatureData
                    {
                        Description = new ObjectValue(nomenclatureTray.Description, nomenclatureTray.Id),
                        Quantity = standartTrays
                    };
                tareDictionary.Add(nomenclatureTray.Id, tare);
                }
            }

        private bool getBoxes(long nomenclatureId, int units, out NomenclatureData tare)
            {
            var nomenclature = new Nomenclature() { ReadingId = nomenclatureId };

            if (nomenclature.IsKeg() || nomenclature.BoxType.Empty)
                {
                tare = null;
                return false;
                }

            tare = new NomenclatureData();
            tare.Description = new ObjectValue(nomenclature.BoxType.Description, nomenclature.BoxType.Id);
            tare.Quantity = units / nomenclature.UnitsQuantityPerPack;

            return true;
            }

        private void mainView_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
            {
            object value = null;

            if (e.Column == dateColumn)
                {
                var currentDate = (DateTime)e.Value;
                value = DateTime.MinValue.Equals(currentDate) ? string.Empty : currentDate.ToString("dd.MM.yy");
                }

            if (value != null)
                {
                e.DisplayText = value.ToString();
                }
            }

        private void scrollUp_Click(object sender, EventArgs e)
            {
            if (ScrollUp != null)
                {
                ScrollUp();
                }
            }

        private void scrollDown_Click(object sender, EventArgs e)
            {
            if (ScrollDown != null)
                {
                ScrollDown();
                }
            }

        public event Action ScrollUp;

        public event Action ScrollDown;

        private bool mainColumnsVisible = true;

        private void palletsCountButton_Click(object sender, EventArgs e)
            {
            mainColumnsVisible = !mainColumnsVisible;
            setVisibilityForMainColumns(mainColumnsVisible);
            }

        private void setVisibilityForMainColumns(bool visible)
            {
            mainView.MakeColumnVisible(visible ? this.LineNumber : this.unitsOnNotFullPalletColumn);
            }

        private void finishButton_SingleClick(object sender, EventArgs e)
            {
            if (WritingDocument())
                {
                onFinish(true, Document);
                if (!Consts.Don_tPrintStickers)
                    {
                    Document.PrintStickers(waresList);
                    }
                if (isLastPlan(Document))
                    {
                    AcceptanceOfGoods.CreateNewAcceptance(Document);
                    }
                }
            }

        private bool WritingDocument()
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

                return false;
                }

            return true;
            }

        private void saveButton_SingleClick(object sender, EventArgs e)
            {
            WritingDocument();
            }
        }
    }