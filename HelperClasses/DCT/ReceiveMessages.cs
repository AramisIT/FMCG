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

        #region GetAdditionalInfoAboutAccepnedGoods
        private static object[] GetAdditionalInfoAboutAccepnedGoods(object[] parameters)
            {
            double count = Convert.ToDouble(parameters[0]);
            long goods = Convert.ToInt64(parameters[1]);
            long car = Convert.ToInt64(parameters[2]);
            long incomeDoc = getIncomeDoc(count, goods, car);
            string date = getIncomeDate(incomeDoc, goods);
            KeyValuePair<long, string> cell = Cells.GetNewCellForGoods(goods);

            return new object[] { incomeDoc, date, cell.Key, cell.Value };
            }

        private static string getIncomeDate(long incomeDoc, long goods)
            {
            if (incomeDoc != 0)
                {
                Query query = DB.NewQuery(@"
--DECLARE @Goods	BIGINT=1
--DECLARE @Id		BIGINT=1

SELECT CONVERT(VARCHAR(10),s.NomenclatureDate,104)
FROM AcceptanceOfGoods a
JOIN SubAcceptanceOfGoodsNomenclatureInfo s ON s.IdDoc=a.Id
WHERE Id=@Id AND s.Nomenclature=@Goods");
                query.AddInputParameter("Id", incomeDoc);
                query.AddInputParameter("Goods", goods);
                object o = query.SelectScalar();

                if (o != null)
                    {
                    return o.ToString();
                    }
                }

            return string.Empty;
            }

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

        #region Check
        private static object[] CheckBarcodeForExistUser(IList<object> parameters)
            {
            return new object[] { BarcodeWorker.CheckMatchingBarcodeAndType<Users>(parameters[0]) };
            }

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