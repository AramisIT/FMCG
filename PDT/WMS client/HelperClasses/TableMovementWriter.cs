using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.HelperClasses
    {
    class TableMovementWriter
        {
        private BarcodeData finalBarcodeData;
        private BarcodeData startBarcodeData;

        public DataTable Table { get; private set; }

        public TableMovementWriter(BarcodeData startBarcodeData, BarcodeData finalBarcodeData)
            {
            this.startBarcodeData = startBarcodeData;
            this.finalBarcodeData = finalBarcodeData;
            initTable();
            fillTable();
            }

        private void fillTable()
            {
            appendResult(finalBarcodeData.Nomenclature.Id, finalBarcodeData.Nomenclature.Id, startBarcodeData.TotalUnitsQuantity, finalBarcodeData.TotalUnitsQuantity, false);

            if (finalBarcodeData.HasLiners || startBarcodeData.HasLiners)
                {
                appendResult(startBarcodeData.Liner.Id, finalBarcodeData.Liner.Id, startBarcodeData.LinersAmount, finalBarcodeData.LinersAmount, true);
                }

            if (finalBarcodeData.HasTray || startBarcodeData.HasTray)
                {
                appendResult(startBarcodeData.Tray.Id, finalBarcodeData.Tray.Id, startBarcodeData.Tray.Id > 0 ? 1 : 0, finalBarcodeData.Tray.Id > 0 ? 1 : 0, true);
                }
            }

        private void appendResult(long startId, long finalId, long plan, long fact, bool isTare)
            {
            if (startId == finalId)
                {
                appendResultToTable(startId, plan, fact, startBarcodeData.Cell.Id, finalBarcodeData.Cell.Id, isTare);
                return;
                }

            if (startId > 0)
                {
                appendResultToTable(startId, plan, 0, startBarcodeData.Cell.Id, startBarcodeData.Cell.Id, isTare);
                }

            if (finalId > 0)
                {
                appendResultToTable(finalId, 0, fact, finalBarcodeData.Cell.Id, finalBarcodeData.Cell.Id, isTare);
                }
            }

        private void appendResultToTable(long startId, long plan, long fact, long startCell, long finalCell, bool isTare)
            {
            var row = Table.NewRow();

            row["Nomenclature"] = startId;
            row["PlanValue"] = plan;
            row["FactValue"] = fact;

            row["PalletCode"] = finalBarcodeData.StickerId;
            row["StartCodeOfPreviousPallet"] = isTare ? 0L : startBarcodeData.PreviousStickerCode;
            row["FinalCodeOfPreviousPallet"] = isTare ? 0L : finalBarcodeData.PreviousStickerCode;
            row["StartCell"] = startCell;
            row["FinalCell"] = finalCell;

            Table.Rows.Add(row);
            }
        private void initTable()
            {
            Table = new DataTable();
            Table.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("Nomenclature", typeof (long)),
                new DataColumn("PalletCode", typeof (long)),
                new DataColumn("StartCodeOfPreviousPallet", typeof (long)),
                new DataColumn("FinalCodeOfPreviousPallet", typeof (long)),
                new DataColumn("PlanValue", typeof (int)),
                new DataColumn("FactValue", typeof (int)),
                new DataColumn("StartCell", typeof (long)),
                new DataColumn("FinalCell", typeof (long))
                });
            }
        }
    }
