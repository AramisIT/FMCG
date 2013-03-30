using System;
using System.Collections.Generic;
using System.Data;
using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using Catalogs;

namespace AtosFMCG.HelperClasses.DCT
    {
    public static class ReceiveMessages
        {
        public static object[] ReceiveMessage(string procedure, object[] parameters)
            {
            switch (procedure)
                {
                    case "GetCarsForAcceptance":
                        return GetCarsForAcceptance();
                    case "CheckBarcodeForExistUser":
                        return CheckBarcodeForExistUser(parameters);
                    case "CheckBarcodeForExistGoods":
                        return CheckBarcodeForExistGoods(parameters);
                    case "GetAdditionalInfoAboutAccepnedGoods":
                        return GetAdditionalInfoAboutAccepnedGoods(parameters);
                case "GetPlaceDataFromCode":
                        return GetPlaceDataFromCode(parameters);
                case "SetAcceptanceData":
                        SetAcceptanceData(parameters);
                        break;
                }

            return new object[0];
            }

        #region Get
        /// <summary>Отримати список машин для "Приймання товару"</summary>
        /// <returns>Таблиця (Id, Description)</returns>
        private static object[] GetCarsForAcceptance()
            {
            Query query = DB.NewQuery(@"SELECT DISTINCT c.Id, RTRIM(c.Description)Description
FROM AcceptanceOfGoods a
JOIN PlannedArrival p ON p.Id=a.Source
JOIN Cars c ON c.Id=p.Car
WHERE a.MarkForDeleting=0 AND a.State=0");
            DataTable table = query.SelectToTable();

            return new object[] { table };
            }

        private static object[] GetPlaceDataFromCode(IList<object> parameters)
            {
            string barcode = parameters[0].ToString();
            Type type = BarcodeWorker.GetTypeOfData(barcode);
            long id;
            bool result = BarcodeWorker.GetPartsFromBarcode(barcode, out id);

            return new object[] {result, type, id};
            }

        #region GetAdditionalInfoAboutAccepnedGoods
        /// <summary>Отримати додаткову інформацію по прийманню товару</summary>
        private static object[] GetAdditionalInfoAboutAccepnedGoods(IList<object> parameters)
            {
            double count = Convert.ToDouble(parameters[0]);
            long goods = Convert.ToInt64(parameters[1]);
            long car = Convert.ToInt64(parameters[2]);
            long party = Convert.ToInt64(parameters[3]);
            long incomeDoc = getIncomeDoc(count, goods, car);
            long palett;
            DateTime date = Party.GetDateOfManufactureById(party);
            KeyValuePair<long, string> cell;
            Cells.GetNewCellForGoods(goods, date, out cell, out palett);

            return new object[] {incomeDoc, date, cell.Key, cell.Value, palett};
            }

        /// <summary>ІД документу плану приймання</summary>
        /// <param name="count">Кількість товару</param>
        /// <param name="goods">ІД товару</param>
        /// <param name="car">ІД машини</param>
        /// <returns>ІД документу плану приймання</returns>
        private static long getIncomeDoc(double count, long goods, long car)
            {
            Query query = DB.NewQuery(@"--DECLARE @Goods	BIGINT=1
--DECLARE @Car	BIGINT=2;

WITH
PlanData AS (
	SELECT a.Id,SUM(n.NomenclatureCount) Count
	FROM AcceptanceOfGoods a
	JOIN PlannedArrival p ON p.Id=a.Source
	JOIN SubPlannedArrivalNomenclatureInfo n ON n.IdDoc=p.Id
	WHERE 
		a.MarkForDeleting=0 AND 
		a.State=0 AND 
		p.Car=@Car AND
		n.Nomenclature=@Goods
	GROUP BY a.Id)
,FactData AS(
	SELECT a.Id,SUM(n.NomenclatureCount) Count
	FROM AcceptanceOfGoods a
	JOIN PlannedArrival p ON p.Id=a.Source
	JOIN SubAcceptanceOfGoodsNomenclatureInfo n ON n.IdDoc=a.Id
	WHERE
		a.MarkForDeleting=0 AND
		a.State=0 AND
		p.Car=@Car AND
		n.Nomenclature=@Goods
	GROUP BY a.Id)

SELECT 
	a.Id,
	p.Count PlanCount,
	f.Count FactCount
FROM AcceptanceOfGoods a 
LEFT JOIN PlanData p ON p.Id=a.Id
LEFT JOIN FactData f ON f.Id=a.Id
--WHERE p.Count>f.Count
");
            query.AddInputParameter("Goods", goods);
            query.AddInputParameter("Car", car);
            DataTable table = query.SelectToTable();

            if (table != null && table.Rows.Count > 0)
                {
                foreach (DataRow row in table.Rows)
                    {
                    double plan = Convert.ToDouble(row["PlanCount"]);
                    double fact = Convert.ToDouble(row["FactCount"]);

                    if (plan >= fact + count)
                        {
                        return Convert.ToInt64(row["Id"]);
                        }
                    }

                //Якщо нема документу, що задовільняє умови - взяти перший
                return Convert.ToInt64(table.Rows[0]["Id"]);
                }

            return 0L;
            } 
        #endregion
        #endregion

        #region Set
        /// <summary>Зберегти дані по прийманню товару</summary>
        private static void SetAcceptanceData(IList<object> parameters)
            {
            long nomenclature = Convert.ToInt64(parameters[0]);
            long party = Convert.ToInt64(parameters[1]);
            long measure = Convert.ToInt64(parameters[2]);
            //DateTime date = Convert.ToDateTime(parameters[3]);
            double count = Convert.ToDouble(parameters[4]);
            long cell = Convert.ToInt64(parameters[5]);
            long planId = Convert.ToInt64(parameters[6]);

            Query query = DB.NewQuery("EXEC AddRowIntoAcceptanceOfGoods @PlanId,@Nomenclature,@Cell,@Count,@Measure,@Party");
            query.AddInputParameter("PlanId",planId);
            query.AddInputParameter("Nomenclature", nomenclature);
            query.AddInputParameter("Cell",cell);
            query.AddInputParameter("Count",count);
            query.AddInputParameter("Measure",measure);
            query.AddInputParameter("Party",party);
            query.Execute();
            }
        #endregion

        #region Check
        /// <summary>Перевірити чи існує користувач з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistUser(IList<object> parameters)
            {
            return new object[] { BarcodeWorker.CheckMatchingBarcodeAndType<Users>(parameters[0]) };
            }

        /// <summary>Перевірити чи існує товар з таким штрих-кодом</summary>
        private static object[] CheckBarcodeForExistGoods(IList<object> parameters)
            {
            long id;
            string description;
            string barcode = parameters[0].ToString();

            return BarcodeWorker.GetCutDataByBarcode<Nomenclature>(barcode, out id, out description)
                       ? new object[] {true, id, description}
                       : new object[] {false};
            }
        #endregion
        }
    }