using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    [Description("Последовательность паллет")]
    class PalletsRelations : BalanceRecord
        {
        [Description("Код паллеты")]
        MotionsTableField Pallet = MotionsTableField.Group;

        [Description("Код предыдущей паллеты")]
        MotionsTableField PreviousPallet = MotionsTableField.Group;

        [Description("Количество")]
        MotionsTableField Quantity = MotionsTableField.Number;

        public override IDatabaseObject[] GetObjectsOfMotions()
            {
            return new DatabaseObject[] { new AcceptanceOfGoods(), new Inventory(), new Moving() };
            }

        public override string AddMotions(IDatabaseObject databaseObject)
            {
            if (databaseObject is AcceptanceOfGoods)
                {
                var item = databaseObject as AcceptanceOfGoods;

                AddMotion(Pallet, item.NomenclatureCode);
                AddMotion(PreviousPallet, item.PreviousPalletCode);
                AddMotion(Quantity, string.Empty);

                SetExceptionsValues("MarkForDeleting", 1);
                SetExceptionsValues("NomenclatureCell.AccessToAllWares", 1);
                SetRequirementValue(item.NomenclatureState, RowsStates.Completed);
                SetExceptionsValues(item.PreviousPalletCode, 0);
                SetExceptionsValues(item.IsTare, 1);

                return item.NomenclatureRowDate.ColumnName;
                }
            else if (databaseObject is Inventory)
                {
                var item = databaseObject as Inventory;

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.FinalCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty);

                SetRequirementValue(item.RowState, RowsStates.Completed);
                SetExceptionsValues(item.FinalCodeOfPreviousPallet, 0);
                SetExceptionsValues("FinalCell.AccessToAllWares", 1);
                SetExceptionsValues("MarkForDeleting", 1);

                StartNewMotionsCollection();

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.StartCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty, true);

                SetRequirementValue(item.RowState, RowsStates.Completed);
                SetExceptionsValues(item.StartCodeOfPreviousPallet, 0);
                SetExceptionsValues("StartCell.AccessToAllWares", 1);
                SetExceptionsValues("MarkForDeleting", 1);

                return item.RowDate.ColumnName;
                }
            else if (databaseObject is Moving)
                {
                var item = databaseObject as Moving;

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.FinalCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty);

                SetExceptionsValues("MarkForDeleting", 1);
                SetExceptionsValues("FinalCell.AccessToAllWares", 1);
                SetRequirementValue(item.RowState, RowsStates.Completed);
                SetExceptionsValues(item.FinalCodeOfPreviousPallet, 0);

                StartNewMotionsCollection();

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.StartCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty, true);

                SetExceptionsValues("MarkForDeleting", 1);
                SetExceptionsValues("StartCell.AccessToAllWares", 1);
                SetRequirementValue(item.RowState, RowsStates.Completed);
                SetExceptionsValues(item.StartCodeOfPreviousPallet, 0);

                return item.RowDate.ColumnName;
                }

            return null;
            }
        }
    }
