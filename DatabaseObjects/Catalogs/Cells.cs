using System;
using System.Collections.Generic;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;

namespace AtosFMCG.DatabaseObjects.Catalogs
    {
    /// <summary>������ ���������</summary>
    [Catalog(Description = "������ ���������", GUID = "A4BDEF67-D02D-469E-928E-F5BE8FFCA0A1")]
    public class Cells : CatalogTable
        {
        #region Properties
        /// <summary>��� ������</summary>
        [DataField(Description = "��� ������", ShowInList = true)]
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
        #endregion

        #region Static
        /// <summary>�������� ���� ������� ��� ������ � ��������� �� ���� �������������</summary>
        /// <param name="goods">�� ������������ ������</param>
        /// <param name="date">���� �������������</param>
        /// <param name="cell">������ (��, �����)</param>
        /// <param name="palett">�� ���������� ������ (�� ���� ������)</param>
        /// <returns>�� ���� �������� ��������� ������</returns>
        public static bool GetNewCellForGoods(long goods, DateTime date, out KeyValuePair<long, string> cell, out long palett)
            {
            //����� ����������� �� ���������:
            //1 - ������� ������ � ������ � �� � ������������� � ����� �� ������� ����������
            //2 - ������� ������ � ������ � �� � ������������� � ���� ����� ����������
            //3 - ������� ������, ����� �������� �������
            Query query = DB.NewQuery(@"
--DECLARE @Goods BIGINT=1;
--DECLARE @Date  DATETIME2='2013-03-31';

WITH
--������ ������ � ����� � �������������
AllowedCells AS (
	SELECT f.GoodsCode,f.ParentCode,a.NomenclatureCell,p.DateOfManufacture NomenclatureDate
	FROM FilledCell f
	JOIN SubAcceptanceOfGoodsNomenclatureInfo a ON f.GoodsCode=a.NomenclatureCode
	JOIN Party p ON p.Id=a.NomenclatureParty
	WHERE a.Nomenclature=@Goods)
--
,PreparedData AS (
	SELECT 
		a1.NomenclatureCell,
		RTRIM(c.Description)Description,
		a1.GoodsCode,
		CAST(a1.NomenclatureDate AS DATE) NomenclatureDate,
		a1.ParentCode,
		ROW_NUMBER() OVER (PARTITION BY a1.NomenclatureCell,a1.NomenclatureDate ORDER BY a1.NomenclatureCell,a1.NomenclatureDate,a1.ParentCode DESC) RowNumber
	FROM AllowedCells a1
	LEFT JOIN AllowedCells a2 ON a2.ParentCode=a1.GoodsCode
	LEFT JOIN Cells c ON c.Id=a1.NomenclatureCell
	WHERE a2.NomenclatureCell IS NULL)
--������ �������� ������	
,EmptyCells AS (
	SELECT Id
	FROM Cells 
	EXCEPT
	SELECT DISTINCT c.Id
	FROM FilledCell f
	JOIN SubAcceptanceOfGoodsNomenclatureInfo a ON f.GoodsCode=a.NomenclatureCode
	JOIN Cells c ON c.Id=a.NomenclatureCell)
--����� ������� ������ (����� � �������� ������)
,FirstEmptyCell AS(
	SELECT TOP 1 NomenclatureCell,Description
	FROM (
		SELECT 
			c.Id NomenclatureCell,
			RTRIM(c.Description)Description,
			ROW_NUMBER() OVER (ORDER BY c.Description) RowNumber
		FROM Cells c 
		JOIN EmptyCells e ON e.Id=c.Id)t)

SELECT TOP 1 NomenclatureCell,RTRIM(Description)Description,GoodsCode
FROM(
	--������� 1 
	--������� ������ � ������ � �� � ������������� � ����� �� ������� ����������
	SELECT 1 Priority,NomenclatureCell,Description,GoodsCode,NomenclatureDate
	FROM PreparedData
	WHERE NomenclatureDate=@Date AND RowNumber=1
	UNION ALL
	--������� 2
	--������� ������ � ������ � �� � ������������� � ���� ����� ����������
	SELECT 2 Priority,NomenclatureCell,Description,GoodsCode,MAX(NomenclatureDate)NomenclatureDate
	FROM PreparedData
	WHERE RowNumber=1 AND NomenclatureDate<>@Date
	GROUP BY NomenclatureCell,Description,GoodsCode
	UNION ALL 
	--������� 3 
	--������� ������, ����� �������� �������
	SELECT 3 Priority,NomenclatureCell,Description,0 GoodsCode,'0001-01-01'NomenclatureDate
	FROM FirstEmptyCell)t
WHERE NomenclatureCell IS NOT NULL
ORDER BY Priority");
            query.AddInputParameter("Goods", goods);
            query.AddInputParameter("Date", date.Date);
            QueryResult result = query.SelectRow();

            if (result != null)
                {
                cell = new KeyValuePair<long, string>(Convert.ToInt64(result["NomenclatureCell"]), result["Description"].ToString());
                palett = Convert.ToInt64(result["GoodsCode"]);

                return true;
                }

            cell = new KeyValuePair<long, string>();
            palett = 0;

            return false;
            }
        #endregion
        }
    }