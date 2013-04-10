using System;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>Інвентаризація</summary>
    [Document(Description = "Інвентаризація", GUID = "0E87222E-830D-466A-826F-8ABBC4B36FEE", NumberType = NumberType.Int64, NumberIsReadonly = false)]
    public class Inventory : DocumentTable
        {
        #region Properties
        /// <summary>Стан документу</summary>
        [DataField(Description = "Стан документу", ShowInList = true)]
        public StatesOfDocument State
            {
            get
                {
                return z_State;
                }
            set
                {
                if (z_State == value)
                    {
                    return;
                    }

                z_State = value;
                NotifyPropertyChanged("State");
                }
            }
        private StatesOfDocument z_State;
        /// <summary>Вхідний номер/Номер накладної</summary>
        [DataField(Description = "№ накладної", ShowInList = true, NotEmpty = true)]
        public string IncomeNumber
            {
            get
                {
                return z_IncomeNumber;
                }
            set
                {
                if (z_IncomeNumber == value)
                    {
                    return;
                    }

                z_IncomeNumber = value;
                NotifyPropertyChanged("IncomeNumber");
                }
            }
        private string z_IncomeNumber = string.Empty;

        /// <summary>Тип інветаризації</summary>
        [DataField(Description = "Тип інветаризації", ShowInList = true)]
        public TypesOfInventory TypeOfInventory
            {
            get
                {
                return z_TypeOfInventory;
                }
            set
                {
                if (z_TypeOfInventory == value)
                    {
                    return;
                    }

                z_TypeOfInventory = value;
                NotifyPropertyChanged("TypeOfInventory");
                }
            }
        private TypesOfInventory z_TypeOfInventory;

        #region Local
        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = false, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        /// <summary>Початок періоду</summary>
        [DataField(Description = "Початок періоду", ShowInList = false, StorageType = StorageTypes.Local)]
        public DateTime StartPeriod
            {
            get
                {
                return z_StartPeriod;
                }
            set
                {
                if (z_StartPeriod == value)
                    {
                    return;
                    }

                z_StartPeriod = value;
                NotifyPropertyChanged("StartPeriod");
                }
            }
        private DateTime z_StartPeriod = SystemConfiguration.ServerDateTime.Date;

        /// <summary>Завершення періоду</summary>
        [DataField(Description = "Завершення періоду", ShowInList = false, StorageType = StorageTypes.Local)]
        public DateTime FinishPeriod
            {
            get
                {
                return z_FinishPeriod;
                }
            set
                {
                if (z_FinishPeriod == value)
                    {
                    return;
                    }

                z_FinishPeriod = value;
                NotifyPropertyChanged("FinishPeriod");
                }
            }
        private DateTime z_FinishPeriod = SystemConfiguration.ServerDateTime.Date.AddDays(1);
        #endregion

        #region Таблична частина
        /// <summary>Номенклатура</summary>
        [Table(Columns = "PalletCode,Nomenclature,Measure,PlanValue,FactValue,Party,Cell", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Код вантажа</summary>
        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn PalletCode { get; set; }

        /// <summary>Номенклатура</summary>
        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn Measure { get; set; }

        /// <summary>План</summary>
        [SubTableField(Description = "План", PropertyType = typeof(double))]
        public DataColumn PlanValue { get; set; }

        /// <summary>Факт</summary>
        [SubTableField(Description = "Факт", PropertyType = typeof(double))]
        public DataColumn FactValue { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn Party { get; set; }

        /// <summary>Комірка</summary>
        [SubTableField(Description = "Комірка", PropertyType = typeof(Cells))]
        public DataColumn Cell { get; set; }
        #endregion
        #endregion

        #region DocumentTable
        protected override WritingResult CheckingBeforeWriting()
            {
            if (Responsible.Id != SystemAramis.CurrentUser.Id)
                {
                Responsible = SystemAramis.CurrentUser;
                }

            return base.CheckingBeforeWriting();
            }

        protected override void InitNewBeforeShowing()
            {
            base.InitNewBeforeShowing();
            State = StatesOfDocument.Empty;
            } 
        #endregion

        /// <summary>Сформувати завдання</summary>
        public void CreateTask()
            {
            if(TypeOfInventory == TypesOfInventory.LatestCellsForPeriod)
                {
                if(NomenclatureInfo.Rows.Count>0)
                    {
                    if(!"Всі строки в тиблиці будуть видалені!\r\nПродовжити?".Ask())
                        {
                        return;
                        }

                    NomenclatureInfo.Rows.Clear();
                    }

                //Вибираємо з таблиці 'GoodsMoving' необхідні дані по останнім паллетам в комірці з котрими в обраний період робились хоч якісь операції
                Query query = DB.NewQuery(@"
--DECLARE @StartDate DATETIME2='2013-04-08'
--DECLARE @FinishDate DATETIME2='2013-04-09';

WITH
LastPalletInCell AS (
	SELECT c.PalletCode 
	FROM FilledCell c
	FULL JOIN FilledCell p ON p.PreviousCode=c.PalletCode
	WHERE p.PalletCode IS NULL)
	
SELECT DISTINCT g.UniqueCode,g.Nomenclature,g.MeasureUnit,g.Quantity,g.Cell,n.NomenclatureParty Party
FROM GoodsMoving g
JOIN LastPalletInCell c ON c.PalletCode=g.UniqueCode
LEFT JOIN SubAcceptanceOfGoodsNomenclatureInfo n ON n.NomenclatureCode=g.UniqueCode
WHERE g.WritingDate BETWEEN @StartDate AND @FinishDate");
                query.AddInputParameter("StartDate", StartPeriod);
                query.AddInputParameter("FinishDate", FinishPeriod);
                DataTable table = query.SelectToTable();

                foreach (DataRow row in table.Rows)
                    {
                    DataRow newRow = NomenclatureInfo.GetNewRow(this);
                    newRow[PalletCode] = row[PalletCode.ColumnName];
                    newRow.SetRefValueToRowCell(this, Nomenclature, row[Nomenclature.ColumnName], typeof(Nomenclature));
                    newRow.SetRefValueToRowCell(this, Measure, row[Measure.ColumnName], typeof(Measures));
                    newRow[PlanValue] = row[PlanValue.ColumnName];
                    newRow.SetRefValueToRowCell(this, Cell, row[Cell.ColumnName], typeof(Cells));
                    newRow.SetRefValueToRowCell(this, Party, row[Party.ColumnName], typeof(Party));
                    newRow.AddRowToTable(this);
                    }
                }
            else
                {
                "Завдання формується лише для завдання 'Перевірити останні зачіплені комірки за обраний період'".WarningBox();
                }
            }
        }
    }