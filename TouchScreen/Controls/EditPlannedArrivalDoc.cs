using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.TouchScreen.Events;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Controls
    {
    public partial class EditPlannedArrivalDoc : UserControl
        {
        #region Veriables
        private PlannedArrival Document;
        private readonly FinishEditPlannedArrivalDelegate onFinish;
        private List<NomenclatureData> list;
        private readonly PlannedArrivalData plannedData;
        private bool isLoaded; 
        #endregion

        #region Init
        public EditPlannedArrivalDoc()
            {
            InitializeComponent();
            }

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
        private void fillInitData(PlannedArrivalData data)
            {
            //Document
            Document = new PlannedArrival();
            Document.Read(data.Invoice.Key);

            //Fields
            invoiceDate.Text = NavigatedButton.SPACES_FOR_x32 + Document.Date.ToShortDateString();
            invoiceNumber.Text = NavigatedButton.SPACES_FOR_x32 + Document.IncomeNumber;
            driver.Text = NavigatedButton.SPACES_FOR_x32 + Document.Driver.Description;
            car.Text = NavigatedButton.SPACES_FOR_x32 + Document.Car.Description;

            //Table
            list = new List<NomenclatureData>();

            foreach (DataRow row in Document.NomenclatureInfo.Rows)
                {
                Party party = new Party();
                party.Read(row[Document.NomenclatureParty]);
                Nomenclature nomenclature = new Nomenclature();
                nomenclature.Read(row[Document.Nomenclature]);

                NomenclatureData element = new NomenclatureData
                                               {
                                                   LineNumber = row["LineNumber"].ToString(),
                                                   Description = nomenclature.Description,
                                                   Quantity = Convert.ToDouble(row[Document.NomenclatureCount]),
                                                   Date = party.DateOfManufacture,
                                               };
                list.Add(element);
                }

            grid.DataSource = list;
            } 
        #endregion

        #region Edit
        private void updateEditControl(Action action)
            {
            int  editControls = editControlsArea.Controls.Count;
            
            action();

            for (int i = 0; i < editControls; i++)
                {
                editControlsArea.Controls.RemoveAt(0);
                }
            }

        private void showMessage(string message)
            {
            int editControls = editControlsArea.Controls.Count;

            editControlsArea.Controls.Add(new Message(message) {Dock = DockStyle.Fill});

            for (int i = 0; i < editControls; i++)
                {
                editControlsArea.Controls.RemoveAt(0);
                }
            }

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

        void enterField_FieldValueIsChanged(object sender, FieldValueIsChangedArgs e)
            {
            Document.IncomeNumber = e.Value;
            invoiceNumber.Text = NavigatedButton.SPACES_FOR_x32 + Document.IncomeNumber;
            } 
        #endregion

        #region MyRegion
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

        void dateEdit_DateIsChanged(object sender, DateIsChangedArgs e)
            {
            Document.Date = e.Value;
            invoiceDate.Text = NavigatedButton.SPACES_FOR_x32 + Document.Date.ToShortDateString();
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
            driver.Text = NavigatedButton.SPACES_FOR_x32 + Document.Driver.Description;
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
            car.Text = NavigatedButton.SPACES_FOR_x32 + Document.Car.Description;
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

        private void editMode_Click(object sender, EventArgs e)
            {

            }

        private void finish_Click(object sender, EventArgs e)
            {
            //do..
            //
            onFinish(Document);
            } 
        #endregion
        }
    }
