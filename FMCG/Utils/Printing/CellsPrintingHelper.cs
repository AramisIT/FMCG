using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using Catalogs;
using FMCG.Utils.Printing;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;

namespace AtosFMCG.TouchScreen.PalletSticker
    {
    class CellsPrintingHelper : ThermalTransferPrinting
        {
        private List<Cells> stickersTasks;

        public CellsPrintingHelper(List<Cells> stickersTasks)
            {
            this.stickersTasks = stickersTasks;
            }

        protected override DataTable getDataSource()
            {
            var result = new DataTable();
            result.Columns.AddRange(new DataColumn[] { new DataColumn("Barcode", typeof(string)), new DataColumn("Name", typeof(string)), });

            stickersTasks.ForEach(cell => result.Rows.Add(cell.Barcode, cell.Description));
            return result;
            }

        protected override string getShortFileNameOfAdapter()
            {
            return "CellBarcode.xml";
            }
        }
    }
