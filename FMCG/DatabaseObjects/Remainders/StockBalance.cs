using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aramis.Core;
using Aramis.Core.Records;
using Catalogs;
using Documents;
using FMCG.DatabaseObjects.Enums;

namespace FMCG.DatabaseObjects.Remainders
    {
    class StockBalance : BalanceRecord
        {
        MotionsTableField Nomenclature = MotionsTableField.Group;

        MotionsTableField Cell = MotionsTableField.Group;

        MotionsTableField State = MotionsTableField.Group;

        MotionsTableField Party = MotionsTableField.Group;

        MotionsTableField Code = MotionsTableField.Group;

        MotionsTableField Quantity = MotionsTableField.Number;

        public override DatabaseObject[] GetObjectsOfMotions()
            {
            return new DatabaseObject[] { new AcceptanceOfGoods(), new Inventory(), new Moving() };
            }

        public override string AddMotions(DatabaseObject databaseObject)
            {
            if (databaseObject is AcceptanceOfGoods)
                {
                var item = databaseObject as AcceptanceOfGoods;

                AddMotion(Nomenclature, item.Nomenclature);
                AddMotion(Cell, item.NomenclatureCell);
                AddMotion(State, item.NomenclatureState);
                AddMotion(Party, item.NomenclatureParty);
                AddMotion(Code, item.NomenclatureCode);
                AddMotion(Quantity, item.NomenclatureFact);

                SetExceptionsValues(item.NomenclatureState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);

                return item.NomenclatureRowDate.ColumnName;
                }
            else if (databaseObject is Inventory)
                {
                var item = databaseObject as Inventory;

                AddMotion(Nomenclature, item.Nomenclature);
                AddMotion(Cell, item.FinalCell);
                AddMotion(State, item.RowState);
                AddMotion(Party, item.Party);
                AddMotion(Code, item.PalletCode);
                AddMotion(Quantity, item.FactValue);

                SetExceptionsValues(item.FactValue, 0);
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);


                StartNewMotionsCollection();

                AddMotion(Nomenclature, item.Nomenclature);
                AddMotion(Cell, item.StartCell);
                AddMotion(State, item.RowState);
                AddMotion(Party, item.Party);
                AddMotion(Code, item.PalletCode);
                AddMotion(Quantity, item.PlanValue, true);

                SetExceptionsValues(item.PlanValue, 0);
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);

                return item.RowDate.ColumnName;
                }
            else if (databaseObject is Moving)
                {
                var item = databaseObject as Moving;

                AddMotion(Nomenclature, item.Nomenclature);
                AddMotion(Cell, item.FinalCell);
                AddMotion(State, item.RowState);
                AddMotion(Party, item.Party);
                AddMotion(Code, item.PalletCode);
                AddMotion(Quantity, item.FactValue);

                SetExceptionsValues(item.FinalCell, Consts.RedemptionCell.Id);
                SetExceptionsValues(item.FactValue, 0);
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);


                StartNewMotionsCollection();

                AddMotion(Nomenclature, item.Nomenclature);
                AddMotion(Cell, item.StartCell);
                AddMotion(State, item.RowState);
                AddMotion(Party, item.Party);
                AddMotion(Code, item.PalletCode);
                AddMotion(Quantity, item.FactValue, true);

                SetExceptionsValues(item.PlanValue, 0);
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);

                return item.RowDate.ColumnName;
                }

            return null;
            }
        }
    }
