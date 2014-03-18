using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;
using Catalogs;
using FMCG.DatabaseObjects.Enums;
using FMCG.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace Documents
    {
    /// <summary>Переміщення</summary>
    [Document(Description = "Переміщення", GUID = "015CC1EA-D666-431E-9D08-510395C78E4C", NumberType = NumberType.Int64, NumberIsReadonly = false, NumberUniquenessPeriod = NumberUniquenessPeriods.Year)]
    public class Moving : DocumentTable
        {
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

        [DataField(Description = "План отбора", ShowInList = true, AllowOpenItem = true)]
        public ShipmentPlan PickingPlan
            {
            get
                {
                return (ShipmentPlan)GetValueForObjectProperty("PickingPlan");
                }
            set
                {
                SetValueForObjectProperty("PickingPlan", value);
                }
            }

        #region StorageType = StorageTypes.Local
        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        /// <summary>Номер накладної</summary>
        [DataField(Description = "Номер накладної", ShowInList = true, StorageType = StorageTypes.Local)]
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

        [DataField(Description = "Дата накладної", ShowInList = true, StorageType = StorageTypes.Local)]
        public string IncomeDate
            {
            get
                {
                return z_IncomeDate;
                }
            set
                {
                if (z_IncomeDate == value)
                    {
                    return;
                    }

                z_IncomeDate = value;
                NotifyPropertyChanged("IncomeDate");
                }
            }
        private string z_IncomeDate = string.Empty;
        #endregion

        [Table(Columns = "Nomenclature,PalletCode,RowState,RowDate, Employee, StartCodeOfPreviousPallet, FinalCodeOfPreviousPallet, PlanValue,FactValue,StartCell,FinalCell,Party", AllowFiltering = true)]
        [DataField(Description = "Номенклатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        [SubTableField(Description = "Номенклатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        [SubTableField(Description = "Код вантажу", PropertyType = typeof(long))]
        public DataColumn PalletCode { get; set; }

        [SubTableField(Description = "Стан рядка", PropertyType = typeof(RowsStates))]
        public DataColumn RowState { get; set; }

        [SubTableField(Description = "Дата рядка", PropertyType = typeof(DateTime))]
        public DataColumn RowDate { get; set; }

        [SubTableField(Description = "Виконавець", PropertyType = typeof(Users))]
        public DataColumn Employee { get; set; }

        [SubTableField(Description = "Початковий код попередньої паллети", PropertyType = typeof(long))]
        public DataColumn StartCodeOfPreviousPallet { get; set; }

        [SubTableField(Description = "Кінцевий код попередньої паллети", PropertyType = typeof(long))]
        public DataColumn FinalCodeOfPreviousPallet { get; set; }

        [SubTableField(Description = "План", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn PlanValue { get; set; }

        [SubTableField(Description = "Факт", PropertyType = typeof(decimal), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn FactValue { get; set; }

        [SubTableField(Description = "Початкова комірка", PropertyType = typeof(Cells))]
        public DataColumn StartCell { get; set; }

        [SubTableField(Description = "Кінцева комірка", PropertyType = typeof(Cells))]
        public DataColumn FinalCell { get; set; }

        [SubTableField(Description = "Партія", PropertyType = typeof(Parties))]
        public DataColumn Party { get; set; }

        public override Func<DataRow, Color> GetFuncGetRowColor()
            {
            return row =>
                {
                    return row.GetDocumentColor();
                };
            }
        
        }
    }