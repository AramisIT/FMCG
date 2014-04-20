using System;
using System.Data;
using System.Drawing;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.SystemConfigurations;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.Enums;
using Catalogs;
using FMCG.DatabaseObjects.Enums;
using FMCG.Utils;

namespace Documents
    {
    [Document(Description = "Інвентаризація", GUID = "0E87222E-830D-466A-826F-8ABBC4B36FEE", NumberType = NumberType.Int64, NumberIsReadonly = false, NumberUniquenessPeriod = NumberUniquenessPeriods.Year)]
    public class Inventory : DocumentTable
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

        [DataField(StorageType = StorageTypes.Local, Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = false)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }


        [Table(Columns = "Nomenclature,PalletCode,RowState,RowDate, StartCodeOfPreviousPallet, FinalCodeOfPreviousPallet, PlanValue,FactValue,StartCell,FinalCell,Party", AllowFiltering = true)]
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

        internal void FixWrongRelations()
            {
            var table = DB.NewQuery("SELECT Pallet, PreviousPallet, -Quantity Quantity FROM [dbo].[GetPalletsRelations] ('0001-01-01',0,0) where Quantity<0 order by Quantity").SelectToTable();
            foreach (DataRow row in table.Rows)
                {
                var rowsQuantity = Convert.ToInt32(row["Quantity"]);
                for (int i = 0; i < rowsQuantity; i++)
                    {
                    var newRow = NomenclatureInfo.GetNewRow(this);
                    newRow[PalletCode] = Convert.ToInt64(row["Pallet"]);
                    newRow[FinalCodeOfPreviousPallet] = Convert.ToInt64(row["PreviousPallet"]);
                    newRow[RowState] = RowsStates.Completed;
                    newRow[FinalCell] = Consts.EmptyCell.Id;

                    newRow.AddRowToTable(this);
                    }
                }
            }
        }
    }