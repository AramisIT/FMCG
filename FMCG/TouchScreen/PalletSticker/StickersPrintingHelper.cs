using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Aramis.Platform;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;

namespace AtosFMCG.TouchScreen.PalletSticker
    {
    class StickersPrintingHelper
        {
        private const int STICKER_WIDTH = 390;
        private const int STICKER_HEIGHT = 300;

        public StickersPrintingHelper(List<StickerInfo> stickersTasks, string printerName)
            {
            this.stickersTasks = stickersTasks;
            this.printerName = printerName;
            }

        private List<StickerInfo> stickersTasks;
        private string printerName;

        internal bool Print()
            {
            var dataSource = initDataSource();

            var report = createMatrixReport(dataSource);

            return printMatrixReport(report);
            }

        private bool printMatrixReport(MatrixReport matrix)
            {
            var matrixReportPrintHelper = new MatrixReportPrintHelper();

            const bool printLandscape = true;
            const bool showPreview = false;

            try
                {
                matrixReportPrintHelper.CustomPrint(matrix, "Этикетки на паллеты", printLandscape, printerName,
                    STICKER_WIDTH, STICKER_HEIGHT, showPreview);
                }

            catch (Exception exp)
                {
                var message = string.Format("Сбой при печати: {0}", exp.Message);

                return false;
                }

            return true;
            }

        private MatrixReport createMatrixReport(DataTable dataSource)
            {
            string fileName = string.Format(@"{0}\{1}", SystemAramis.APPLICATION_PATH, @"TouchScreen\PalletSticker\StickerAdapter.xml");
            string xmlContent = File.ReadAllText(fileName);
            MatrixAdapter adapter = new MatrixAdapter(new DesktopMatrixReportMainFactory(), System.Xml.Linq.XDocument.Parse(xmlContent).Root, null);

            var sources = new Dictionary<string, DataTable>() { { "barCodeSourceTable", dataSource } }; ;
            var images = new Dictionary<string, MatrixReportImageSource>();
            var reportParameters = new Dictionary<string, object>();

            var matrixReportData = new MatrixReportData()
                {
                    Sources = sources,
                    ReportParameters = reportParameters
                };

            adapter.SetDataSources(matrixReportData, images);

            return adapter.Matrix;
            }

        private DataTable initDataSource()
            {
            var dataSource = new DataTable();
            dataSource.Columns.AddRange(new DataColumn[]
                {
                new DataColumn("Nomenclature", typeof(string)), 
                new DataColumn("BarCode", typeof(string)),
                new DataColumn("PacksCount", typeof(int)), 
                new DataColumn("ReleaseDate", typeof(DateTime)),
                new DataColumn("HalpExpiryDate", typeof(DateTime)),
                new DataColumn("ExpiryDate", typeof(DateTime)),
                new DataColumn("AcceptionDate", typeof(DateTime)),
                new DataColumn("Driver", typeof(string))
                });

            foreach (var stickersTask in stickersTasks)
                {
                for (int i = 0; i < stickersTask.CopiesQuantity; i++)
                    {
                    dataSource.Rows.Add(stickersTask.Nomenclature, stickersTask.Barcode, stickersTask.PacksCount,
                        stickersTask.ReleaseDate, stickersTask.HalpExpiryDate, stickersTask.ExpiryDate, stickersTask.AcceptionDate,
                        stickersTask.Driver);
                    }
                }

            return dataSource;
            }

        }
    }
