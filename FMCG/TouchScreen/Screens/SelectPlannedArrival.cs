using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.TouchScreen.Controls;
using AtosFMCG.TouchScreen.Screens.Base;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens
    {
    /// <summary>Сортована продукція</summary>
    public partial class SelectPlannedArrival : BasePlannedArrival
        {
        #region Init
        /// <summary>Сортована продукція</summary>
        public SelectPlannedArrival(PlannedArrivalData data)
            : base(data)
            {
            InitializeComponent();
            initScreen();
            }

        /// <summary>Ініціалізація</summary>
        protected override sealed void initScreen()
            {
            installCarPage();
            }
        #endregion

        #region Support
        /// <summary>Встановлення нової "сторінки"</summary>
        /// <param name="page">Нова сторінка</param>
        private void installNewPage(Control page)
            {
            //Note: можна вынести в родительский класс
            Controls.Add(page);

            if (Controls.Count > 1)
                {
                Controls.RemoveAt(0);
                }
            }

        private void goToStartPage()
            {
            GoToScreen(new StartScreen());
            }
        #endregion

        #region Car
        private void installCarPage()
            {
            SelectFromObjectList selectNomenclature = new SelectFromObjectList(
                "номер машины", "Машини", updateCarList,selectCarValue, goToStartPage)
                                                          {
                                                              Dock = DockStyle.Fill
                                                          };
            installNewPage(selectNomenclature);
            }

        private static DataTable updateCarList(string enterValue)
            {
            Query query = DB.NewQuery(
                    "SELECT Id,RTRIM(Description)Description FROM Cars WHERE Description like '%'+@CarNumber+'%'");
            query.AddInputParameter("CarNumber", enterValue);
            DataTable table = query.SelectToTable();

            return table;
            }

        private void selectCarValue(KeyValuePair<long, string> value)
            {
            ScreenData.Car = value;
            installInvoicePage();
            }
        #endregion

        #region Invoice
        private void installInvoicePage()
            {
            SelectFromObjectList selectInvoice = new SelectFromObjectList(
                "накладну", "Накладні", updateIncoiceList, selectInvoiceValue, installCarPage)
                                                     {
                                                         Dock = DockStyle.Fill
                                                     };
            installNewPage(selectInvoice);
            }

        private DataTable updateIncoiceList(string enterValue)
            {
            Query query = DB.NewQuery(@"
SELECT p.Id,RTRIM('№'+CAST(p.IncomeNumber AS VARCHAR)) Description 
FROM PlannedArrival p 
WHERE 
    CAST(p.IncomeNumber AS VARCHAR) like '%'+@Number+'%'
    AND Car=@Car");
            query.AddInputParameter("Number", enterValue);
            query.AddInputParameter("Car", ScreenData.Car.Key);
            DataTable table = query.SelectToTable();

            return table;
            }

        private void selectInvoiceValue(KeyValuePair<long, string> value)
            {
            ScreenData.Invoice = value;
            installNewPage(new EditPlannedArrivalDoc(ScreenData, Finish));
            }

        private void Finish(bool isSaved, PlannedArrival document)
            {
            goToStartPage();
            }
        #endregion
        }
    }
