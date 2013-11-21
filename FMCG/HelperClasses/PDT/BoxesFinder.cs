using Catalogs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FMCG.HelperClasses.PDT
    {
    class BoxesFinder
        {
        private System.Data.DataTable resultTable;
        private long palletCode;

        public bool BoxesRowAdded
            {
            get
                {
                return BoxesRow != null;
                }
            }

        public DataRow BoxesRow { get; private set; }

        public BoxesFinder(DataTable resultTable, long palletCode)
            {
            this.resultTable = resultTable;
            this.palletCode = palletCode;

            addBoxRow();
            }

        private void addBoxRow()
            {
            var wareRow = resultTable.Rows[0];

            var nomenclatureId = Convert.ToInt64(wareRow["Nomenclature"]);
            var nomenclature = (Nomenclature)new Nomenclature().Read(nomenclatureId);
            if (!nomenclature.BoxType.Empty)
                {
                var planUnitsQuantity = Convert.ToInt32(wareRow["PlanValue"]);
                var factUnitsQuantity = Convert.ToInt32(wareRow["FactValue"]);

                var boxesPlan = (planUnitsQuantity / nomenclature.UnitsQuantityPerPack) + ((planUnitsQuantity % nomenclature.UnitsQuantityPerPack) > 0 ? 1 : 0);
                var boxesFact = (factUnitsQuantity / nomenclature.UnitsQuantityPerPack) + ((factUnitsQuantity % nomenclature.UnitsQuantityPerPack) > 0 ? 1 : 0);

                var boxesRow = resultTable.NewRow();
                boxesRow["Nomenclature"] = nomenclature.BoxType.Id;
                boxesRow["PlanValue"] = boxesPlan;
                boxesRow["FactValue"] = boxesFact;

                boxesRow["StartCodeOfPreviousPallet"] = 0;
                boxesRow["FinalCodeOfPreviousPallet"] = 0;

                boxesRow["PalletCode"] = palletCode;
                boxesRow["StartCell"] = wareRow["StartCell"];
                boxesRow["FinalCell"] = wareRow["FinalCell"];

                resultTable.Rows.Add(boxesRow);

                BoxesRow = boxesRow;
                }
            }
        }
    }
