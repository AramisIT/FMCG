using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Aramis.DatabaseConnector;
using AramisInfostructure.Queries;
using AtosFMCG.DatabaseObjects.Documents;
using AtosFMCG.TouchScreen.Controls;
using AtosFMCG.TouchScreen.Screens.Base;
using Documents;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens
    {
    public partial class SelectAcceptancePlan : BaseAcceptancePlan
        {
        #region Init
        /// <summary>Сортована продукція</summary>
        public SelectAcceptancePlan(AcceptancePlanData data)
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
            IQuery query = DB.NewQuery(
                    @"
select distinct pa.Car Id, RTRIM(c.Description) Description from AcceptancePlan pa 
join Cars c on c.Id = pa.Car
where pa.State < 2 and pa.MarkForDeleting = 0 and c.Description like '%'+@CarNumber+'%'
order by RTRIM(c.Description)
");

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
            IQuery query = DB.NewQuery(@"
SELECT p.Id,RTRIM('№ '+CAST(p.SupplierIncomeNumber AS VARCHAR)) Description 
FROM AcceptancePlan p 
WHERE 
    CAST(p.SupplierIncomeNumber AS VARCHAR) like '%'+@Number+'%'
    AND Car=@Car");
            query.AddInputParameter("Number", enterValue);
            query.AddInputParameter("Car", ScreenData.Car.Key);
            DataTable table = query.SelectToTable();

            return table;
            }

        private void selectInvoiceValue(KeyValuePair<long, string> value)
            {
            if (checkNomenclature(value.Key))
                {
                ScreenData.Invoice = value;
                installNewPage(new EditAcceptancePlanDoc(ScreenData, Finish));
                }
            }

        private bool checkNomenclature(long acceptancePlanId)
            {
            const string sql = @"select rtrim(nom.Description) Nomenclature from SubAcceptancePlanNomenclatureInfo n 
join Nomenclature nom on n.IdDoc = @Id and nom.Id = n.Nomenclature
where nom.UnitsQuantityPerPack=0 or nom.UnitsQuantityPerPallet=0
order by nom.Description";
            var q = DB.NewQuery(sql);
            q.AddInputParameter("Id", acceptancePlanId);
            var wrongNomenclatureList = q.SelectToList<string>();

            if (wrongNomenclatureList.Count == 0)
                {
                return true;
                }

            var warningMessage = new StringBuilder("Необходимо ввести количество на паллете, количество в упаковке для следующей номенклатуры:\r\n");
            warningMessage.AppendLine();
            wrongNomenclatureList.ForEach(nomenclature => warningMessage.AppendLine(nomenclature));
            warningMessage.ToString().WarningBox();
            return false;
            }

        private void Finish(bool isSaved, AcceptancePlan document)
            {
            goToStartPage();
            }
        #endregion
        }
    }
