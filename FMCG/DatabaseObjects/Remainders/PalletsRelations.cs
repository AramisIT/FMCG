﻿using System;
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

        public override DatabaseObject[] GetObjectsOfMotions()
            {
            return new DatabaseObject[] { new AcceptanceOfGoods(), new Inventory(), new Moving() };
            }

        public override string AddMotions(DatabaseObject databaseObject)
            {
            if (databaseObject is AcceptanceOfGoods)
                {
                var item = databaseObject as AcceptanceOfGoods;

                AddMotion(Pallet, item.NomenclatureCode);
                AddMotion(PreviousPallet, item.PreviousPalletCode);
                AddMotion(Quantity, string.Empty);

                SetExceptionsValues("MarkForDeleting", 1);
                SetExceptionsValues(item.NomenclatureState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
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

                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.FinalCodeOfPreviousPallet, 0);
                SetExceptionsValues("MarkForDeleting", 1);

                StartNewMotionsCollection();

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.StartCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty, true);

                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.StartCodeOfPreviousPallet, 0);
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
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.FinalCodeOfPreviousPallet, 0);

                StartNewMotionsCollection();

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.StartCodeOfPreviousPallet);
                AddMotion(Quantity, string.Empty, true);

                SetExceptionsValues("MarkForDeleting", 1);
                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.StartCodeOfPreviousPallet, 0);

                return item.RowDate.ColumnName;
                }

            return null;
            }
        }
    }
