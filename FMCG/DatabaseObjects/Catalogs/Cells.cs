using System;
using System.Collections.Generic;
using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Catalogs;

namespace Catalogs
    {
    /// <summary>Комірки зберігання</summary>
    [Catalog(Description = "Комірки зберігання", GUID = "A4BDEF67-D02D-469E-928E-F5BE8FFCA0A1", HierarchicType = HierarchicTypes.GroupsAndElements, ShowCodeFieldInForm = false)]
    public class Cells : CatalogTable
        {
        #region Properties
        /// <summary>Тип комірки</summary>
        [DataField(Description = "Тип комірки", ShowInList = true)]
        public TypesOfCell TypeOfCell
            {
            get
                {
                return (TypesOfCell)GetValueForObjectProperty("TypeOfCell");
                }
            set
                {
                SetValueForObjectProperty("TypeOfCell", value);
                }
            }

        /// <summary>Класифікатор</summary>
        [DataField(Description = "Класифікатор", ShowInList = true)]
        public ClassifierUnits Classifier
            {
            get
                {
                return (ClassifierUnits)GetValueForObjectProperty("Classifier");
                }
            set
                {
                SetValueForObjectProperty("Classifier", value);
                }
            }
        #endregion

        #region Предопределенные элементы
        private const string CATALOG_NAME = "Cells";

        /// <summary>Викуп</summary>
        public static DBObjectRef Buyout
            {
            get
                {
                return z_Buyout ?? (z_Buyout = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "Викуп"));
                }
            }
        private static DBObjectRef z_Buyout;
        #endregion

        #region Static
        /// <summary>Отримати нову позицію для товару в залежності від дати виготовництва</summary>
        /// <param name="goods">ІД номенклатури товару</param>
        /// <param name="date">Дата виготовництво</param>
        /// <param name="cell">Комірка (Ід, назва)</param>
        /// <param name="palett">ІД попередньої палети (ІД коду товару)</param>
        /// <returns>Чи була знайдена необхідна комірка</returns>
        public static bool GetNewCellForGoods(long goods, DateTime date, out KeyValuePair<long, string> cell, out long palett)
            {
            //Пошук здійснюється по пріорітетам:
            //1 - Остання палета в комірці с тією ж номенклатурою і таким же терміном придатності
            //2 - Остання палета в комірці с тією ж номенклатурою і більш свіжою продукцією
            //3 - Порожня комірка, перша відповідно порядку
            Query query = DB.NewQuery(@"
--DECLARE @Goods BIGINT=1;
--DECLARE @Date  DATETIME2='2013-03-31';

WITH
--Список комірок з такою ж номенклатурою
AllowedCells AS (
	SELECT DISTINCT f.PalletCode,f.PreviousCode,a.NomenclatureCell,p.DateOfManufacture NomenclatureDate
	FROM FilledCell f
	JOIN SubAcceptanceOfGoodsNomenclatureInfo a ON f.PalletCode=a.NomenclatureCode
	JOIN Parties p ON p.Id=a.NomenclatureParty
	WHERE a.Nomenclature=@Goods)
--
,PreparedData AS (
	SELECT 
		a1.NomenclatureCell,
		RTRIM(c.Description)Description,
		a1.PalletCode,
		CAST(a1.NomenclatureDate AS DATE) NomenclatureDate,
		a1.PreviousCode,
		toc.NumberOfPallets,
		ROW_NUMBER() OVER (PARTITION BY a1.NomenclatureCell,a1.NomenclatureDate ORDER BY a1.NomenclatureCell,a1.NomenclatureDate,a1.PreviousCode DESC) RowNumber
	FROM AllowedCells a1
	LEFT JOIN AllowedCells a2 ON a2.PreviousCode=a1.PalletCode
	LEFT JOIN Cells c ON c.Id=a1.NomenclatureCell
	LEFT JOIN TypesOfCell toc ON toc.Id=c.TypeOfCell
	WHERE a2.NomenclatureCell IS NULL)
--Кількість паллет в не порожніх комірках
,PalletCountInNotEmptyCells AS (
	SELECT c.NomenclatureCell Cell,COUNT(1)Count
	FROM AllowedCells c
	GROUP BY c.NomenclatureCell)
--Список порожных комірок	
,EmptyCells AS (
	SELECT Id
	FROM Cells 
	EXCEPT
	SELECT DISTINCT c.Id
	FROM FilledCell f
	JOIN SubAcceptanceOfGoodsNomenclatureInfo a ON f.PalletCode=a.NomenclatureCode
	JOIN Cells c ON c.Id=a.NomenclatureCell)
--Перша порожня комірка (згідно з порядком обходу)
,FirstEmptyCell AS(
	SELECT TOP 1 NomenclatureCell,Description
	FROM (
		SELECT 
			c.Id NomenclatureCell,
			RTRIM(c.Description)Description,
			ROW_NUMBER() OVER (ORDER BY c.Description) RowNumber
		FROM Cells c 
		JOIN EmptyCells e ON e.Id=c.Id)t)

SELECT TOP 1 Priority,NomenclatureCell,RTRIM(Description)Description,PalletCode
FROM(
	--Пріорітет 1 
	--Остання палета в комірці с тією ж номенклатурою і таким же терміном придатності
	SELECT 1 Priority,d.NomenclatureCell,d.Description,d.PalletCode,d.NomenclatureDate
	FROM PreparedData d
	JOIN PalletCountInNotEmptyCells c ON c.Cell=d.NomenclatureCell
	WHERE d.NomenclatureDate=@Date AND RowNumber=1 AND c.Count<d.NumberOfPallets
	UNION ALL
	--Пріорітет 2
	--Остання палета в комірці с тією ж номенклатурою і більш свіжою продукцією
	SELECT 2 Priority,NomenclatureCell,Description,PalletCode,MAX(NomenclatureDate)NomenclatureDate
	FROM PreparedData
	WHERE RowNumber=1 AND NomenclatureDate<>@Date
	GROUP BY NomenclatureCell,Description,PalletCode
	UNION ALL 
	--Пріорітет 3 
	--Порожня комірка, перша відповідно порядку
	SELECT 3 Priority,NomenclatureCell,Description,0 PalletCode,'0001-01-01'NomenclatureDate
	FROM FirstEmptyCell)t
WHERE NomenclatureCell IS NOT NULL
ORDER BY Priority,Description");
            query.AddInputParameter("Goods", goods);
            query.AddInputParameter("Date", date.Date);
            QueryResult result = query.SelectRow();

            if (result != null)
                {
                cell = new KeyValuePair<long, string>(Convert.ToInt64(result["NomenclatureCell"]), result["Description"].ToString());
                palett = Convert.ToInt64(result["PalletCode"]);

                return true;
                }

            cell = new KeyValuePair<long, string>();
            palett = 0;

            return false;
            }
        #endregion
        }
    }