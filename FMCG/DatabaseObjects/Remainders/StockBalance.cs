using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aramis.Core;
using Aramis.Core.Reminders;
using Catalogs;
using Documents;

namespace FMCG.DatabaseObjects.Remainders
    {
    class StockBalance : Remainder
        {
        RemainderTableField Nomenclature = RemainderTableField.Group;

        RemainderTableField Cell = RemainderTableField.Group;

        RemainderTableField State = RemainderTableField.Group;

        RemainderTableField Party = RemainderTableField.Group;

        RemainderTableField Code = RemainderTableField.Group;

        RemainderTableField Quantity = RemainderTableField.Number;

        public override DatabaseObject[] GetObjectsOfMotions()
            {
            return new DatabaseObject[] { new AcceptanceOfGoods() };
            }

        public override void AddMotions(DatabaseObject databaseObject)
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
                }
            }
        }
    }
