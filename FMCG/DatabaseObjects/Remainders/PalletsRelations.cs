﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aramis.Core;
using Aramis.Core.Reminders;
using Catalogs;
using Documents;
using FMCG.DatabaseObjects.Enums;

namespace FMCG.DatabaseObjects.Remainders
    {
    class PalletsRelations : Remainder
        {
        RemainderTableField Pallet = RemainderTableField.Group;

        RemainderTableField PreviousPallet = RemainderTableField.Group;

        RemainderTableField Quantity = RemainderTableField.Number;

        public override DatabaseObject[] GetObjectsOfMotions()
            {
            return new DatabaseObject[] { new AcceptanceOfGoods(), new Inventory() };
            }

        public override DataColumn AddMotions(DatabaseObject databaseObject)
            {
            if (databaseObject is AcceptanceOfGoods)
                {
                var item = databaseObject as AcceptanceOfGoods;

                AddMotion(Pallet, item.NomenclatureCode);
                AddMotion(PreviousPallet, item.PreviousPalletCode);
                AddMotion(Quantity, null);

                SetExceptionsValues(item.NomenclatureState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.PreviousPalletCode, 0);
                SetExceptionsValues(item.IsTare, 1);

                return item.NomenclatureRowDate;
                }
            else if (databaseObject is Inventory)
                {
                var item = databaseObject as Inventory;
               
                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.FinalCodeOfPreviousPallet);
                AddMotion(Quantity, null);

                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.FinalCodeOfPreviousPallet, 0);

                StartNewMotionsCollection();

                AddMotion(Pallet, item.PalletCode);
                AddMotion(PreviousPallet, item.StartCodeOfPreviousPallet);
                AddMotion(Quantity, null, true);

                SetExceptionsValues(item.RowState, RowsStates.PlannedAcceptance, RowsStates.PlannedPicking, RowsStates.Canceled, RowsStates.Processing);
                SetExceptionsValues(item.StartCodeOfPreviousPallet, 0);

                return item.RowDate;
                }

            return null;
            }
        }
    }
