using System;
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
            return new DatabaseObject[] { new AcceptanceOfGoods() };
            }

        public override DataColumn AddMotions(DatabaseObject databaseObject)
            {
            if (databaseObject is AcceptanceOfGoods)
                {
                var item = databaseObject as AcceptanceOfGoods;

                AddMotion(Pallet, item.NomenclatureCode);
                AddMotion(PreviousPallet, item.PreviousPalletCode);
                AddMotion(Quantity, null);

                SetExceptionsValues(item.PreviousPalletCode, 0);

                return item.NomenclatureRowDate;
                }

            return null;
            }
        }
    }
