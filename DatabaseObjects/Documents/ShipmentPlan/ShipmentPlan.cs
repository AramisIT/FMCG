using System;
using System.Collections.Generic;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using Aramis.Platform;
using Aramis.UI;
using Aramis.UI.WinFormsDevXpress;
using AtosFMCG.DatabaseObjects.Catalogs;
using AtosFMCG.DatabaseObjects.Interfaces;
using AtosFMCG.Enums;

namespace AtosFMCG.DatabaseObjects.Documents
    {
    /// <summary>План відвантаження</summary>
    [Document(Description = "План відвантаження", GUID = "029B0572-E5B5-48CD-9805-1211319A5633", NumberType = NumberType.String, NumberIsReadonly = false)]
    public class ShipmentPlan : DocumentTable, IIncomeOwner
        {
        #region Properties
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

        /// <summary>Тип відвантаження</summary>
        [DataField(Description = "Тип відвантаження", ShowInList = true)]
        public TypesOfShipment TypeOfShipment
            {
            get
                {
                return z_TypeOfShipment;
                }
            set
                {
                if (z_TypeOfShipment == value)
                    {
                    return;
                    }

                z_TypeOfShipment = value;
                NotifyPropertyChanged("TypeOfShipment");
                }
            }
        private TypesOfShipment z_TypeOfShipment;

        /// <summary>Контрагент</summary>
        [DataField(Description = "Контрагент", ShowInList = true, NotEmpty = true, AllowOpenItem = true)]
        public Contractors Contractor
            {
            get
                {
                return (Contractors)GetValueForObjectProperty("Contractor");
                }
            set
                {
                SetValueForObjectProperty("Contractor", value);
                }
            }

        /// <summary>Перевізник</summary>
        [DataField(Description = "Перевізник", ShowInList = true, AllowOpenItem = true)]
        public Contractors Carrier
            {
            get
                {
                return (Contractors)GetValueForObjectProperty("Carrier");
                }
            set
                {
                SetValueForObjectProperty("Carrier", value);
                }
            }

        /// <summary>Водій</summary>
        [DataField(Description = "Водій", ShowInList = true, AllowOpenItem = true)]
        public Drivers Driver
            {
            get
                {
                return (Drivers)GetValueForObjectProperty("Driver");
                }
            set
                {
                SetValueForObjectProperty("Driver", value);
                }
            }

        /// <summary>Машина</summary>
        [DataField(Description = "Машина", ShowInList = true, AllowOpenItem = true)]
        public Cars Car
            {
            get
                {
                return (Cars)GetValueForObjectProperty("Car");
                }
            set
                {
                SetValueForObjectProperty("Car", value);
                }
            }

        /// <summary>Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)</summary>
        [DataField(Description = "Інформація (Відповідальний,останній хто редагував документ + ДатаЧас редагування)", ShowInList = true, StorageType = StorageTypes.Local)]
        public string Info
            {
            get { return string.Concat(Responsible.Description, ' ', Date.ToString()); }
            }

        #region Table Nomeclature
        /// <summary>Номенлатура</summary>
        [Table(Columns = "Nomenclature, Measure, Quantity, Party", ShowLineNumberColumn = true)]
        [DataField(Description = "Номенлатура")]
        public DataTable NomenclatureInfo
            {
            get { return GetSubtable("NomenclatureInfo"); }
            }

        /// <summary>Номенлатура</summary>
        [SubTableField(Description = "Номенлатура", PropertyType = typeof(Nomenclature))]
        public DataColumn Nomenclature { get; set; }

        /// <summary>Од.вим.</summary>
        [SubTableField(Description = "Од.вим.", PropertyType = typeof(Measures))]
        public DataColumn Measure { get; set; }

        /// <summary>К-сть</summary>
        [SubTableField(Description = "К-сть", PropertyType = typeof(double), DecimalPointsNumber = 2, DecimalPointsViewNumber = 2)]
        public DataColumn Quantity { get; set; }

        /// <summary>Партія</summary>
        [SubTableField(Description = "Партія", PropertyType = typeof(Party))]
        public DataColumn Party { get; set; }
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
        #endregion

        #region Movement
        /// <summary>Чи має документ переміщення?</summary>
        public bool HaveDocMovement { get { return getMovementId() != 0; } }
        public event EventHandler MovementDocIsAssigned;

        public void CreateMovement()
            {
            long movementId = getMovementId();
            Movement movement;

            if (movementId == 0)
                {
                WritingResult result = Write();

                if (result == WritingResult.Success)
                    {
                    movement = new Movement { Source = this, State = StatesOfDocument.Planned };
                    movement.AfterWriting += movement_AfterWriting;
                    List<MovementFillingInfo> list = new List<MovementFillingInfo>();

                    foreach (DataRow row in NomenclatureInfo.Rows)
                        {
                        long nomenclature = Convert.ToInt64(row[Nomenclature]);
                        long measure = Convert.ToInt64(row[Measure]);
                        long party = Convert.ToInt64(row[Party]);
                        double count = Convert.ToDouble(row[Quantity]);
                        bool find = false;

                        foreach (MovementFillingInfo info in list)
                            {
                            if(info.Measure == measure && info.Nomenclature == nomenclature && info.Party == party)
                                {
                                info.Count += count;
                                find = true;
                                break;
                                }
                            }

                        if(!find)
                            {
                            list.Add(new MovementFillingInfo
                                         {
                                             Count = count,
                                             Measure = measure,
                                             Nomenclature = nomenclature,
                                             Party = party
                                         });
                            }
                        }

                    fillMovement(movement, list);
                    }
                else
                    {
                    return;
                    }
                }
            else
                {
                movement = new Movement();
                movement.Read(movementId);
                }

            UserInterface.Current.ShowItem(movement);
            }

        private void fillMovement(Movement movement, IEnumerable<MovementFillingInfo> list)
            {
            foreach (MovementFillingInfo info in list)
                {
                fillMovement(movement, info);
                }
            }

        private void fillMovement(Movement movement, MovementFillingInfo info)
            {
            Query query = DB.NewQuery(@"
--DECLARE @Nomenclature BIGINT=1;
--DECLARE @Measure BIGINT=1;
--DECLARE @Party BIGINT=2;
DECLARE @SourceType uniqueidentifier='029B0572-E5B5-48CD-9805-1211319A5633';

WITH
 PalletOrder AS (SELECT f.PalletCode,ROW_NUMBER() OVER(ORDER BY f.CreationDate) PalletOrder FROM FilledCell f)
,AcceptedCode AS (
	SELECT DISTINCT a.NomenclatureCode code 
	FROM SubAcceptanceOfGoodsNomenclatureInfo a 
	WHERE a.NomenclatureParty=@Party 
	
	EXCEPT 
	
	SELECT DISTINCT n.NomenclatureCode
	FROM Movement m
	JOIN SubMovementNomenclatureInfo n ON n.IdDoc=m.Id
	JOIN ShipmentPlan p ON p.Id=m.Source AND m.SourceType=@SourceType
	WHERE m.MarkForDeleting=0 AND n.RowState=0 AND n.NomenclatureParty=@Party AND n.Nomenclature=@Nomenclature AND n.NomenclatureMeasure=@Measure)
,PreparedData AS (
	SELECT
		b.Cell,b.ExpariedDate,b.UniqueCode,b.Quantity,b.MeasureUnit,
		ROW_NUMBER() OVER (PARTITION BY b.Cell ORDER BY p.PalletOrder DESC,b.ExpariedDate DESC) RowNumber
	FROM StockBalance b
	JOIN PalletOrder p ON p.PalletCode=b.UniqueCode
	JOIN AcceptedCode a ON a.code=b.UniqueCode
	WHERE b.Nomenclature=@Nomenclature AND b.MeasureUnit=@Measure AND b.State=4)
	
SELECT *
FROM PreparedData");
            query.AddInputParameter("Nomenclature", info.Nomenclature);
            query.AddInputParameter("Measure", info.Measure);
            query.AddInputParameter("Party", info.Party);
            DataTable table = query.SelectToTable();
            double howIsUsed = 0;

            if (table != null)
                {
                foreach (DataRow row in table.Rows)
                    {
                    double quantity = Convert.ToDouble(row["Quantity"]);
                    howIsUsed += quantity;

                    DataRow newRow = movement.NomenclatureInfo.GetNewRow(movement);
                    newRow[movement.NomenclatureCode] = row["UniqueCode"];
                    newRow.SetRefValueToRowCell(movement, movement.Nomenclature, info.Nomenclature, typeof(Nomenclature));
                    newRow.SetRefValueToRowCell(movement, movement.NomenclatureMeasure, info.Measure, typeof(Measures));
                    newRow.SetRefValueToRowCell(movement, movement.NomenclatureParty, info.Party, typeof(Party));
                    newRow[movement.NomenclatureCount] = quantity;
                    newRow.SetRefValueToRowCell(movement, movement.SourceCell, row["Cell"], typeof(Cells));
                    newRow.SetRefValueToRowCell(movement, movement.DestinationCell, Cells.Buyout.Id, typeof(Cells));
                    newRow[movement.RowState] = StatesOfDocument.Planned;
                    newRow.AddRowToTable(movement);

                    if (howIsUsed >= info.Count)
                        {
                        break;
                        }
                    }
                }

            movement.SetSubtableModified(movement.NomenclatureInfo.TableName);
            }

        void movement_AfterWriting(DatabaseObject item)
            {
            OnMovementDocIsAssigned();
            }

        private long getMovementId()
            {
            Query query = DB.NewQuery(@"
DECLARE @ShipmentPlanType uniqueidentifier='029B0572-E5B5-48CD-9805-1211319A5633'

SELECT TOP 1 Id
FROM Movement
WHERE Source=@Id AND SourceType=@ShipmentPlanType
ORDER BY Date DESC");
            query.AddInputParameter("Id", Id);
            object movementId = query.SelectScalar();

            return movementId == null ? 0 : Convert.ToInt64(movementId);
            } 

        private void OnMovementDocIsAssigned()
            {
            EventHandler handler = MovementDocIsAssigned;

            if(handler!=null)
                {
                handler(this, new EventArgs());
                }
            }
        #endregion
        }
    
    public class MovementFillingInfo
        {
        public long Nomenclature { get; set; }
        public long Measure { get; set; }
        public long Party { get; set; }
        public double Count { get; set; }
        }
    }