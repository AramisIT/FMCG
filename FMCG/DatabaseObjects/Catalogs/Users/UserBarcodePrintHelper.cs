using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using AtosFMCG.TouchScreen.PalletSticker;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;

namespace Catalogs.Helpers
    {
    class UserBarcodePrintHelper
        {
        private MatrixReport matrix;
        private string printerName;

        public UserBarcodePrintHelper(long userId, string userDescription, string printerName)
            {
            var reportParameters = new Dictionary<string, object>() { { "Barcode", "EM." + userId }, { "Name", userDescription } };
            matrix = createReport(reportParameters);

            this.printerName = printerName;
            }

        private MatrixReport createReport(Dictionary<string, object> reportParameters)
            {
            string fileName = string.Format(@"{0}\{1}", SystemAramis.APPLICATION_PATH, @"DatabaseObjects\Catalogs\Users\UserBarcode.xml");
            string xmlContent = File.ReadAllText(fileName);

            MatrixAdapter adapter = new MatrixAdapter(new DesktopMatrixReportMainFactory(), XDocument.Parse(xmlContent).Root, null, true);

            var matrixReportData = new MatrixReportData()
                {
                    Sources = new Dictionary<string, DataTable>(),
                    ReportParameters = reportParameters
                };
            adapter.SetDataSources(matrixReportData, new Dictionary<string, MatrixReportImageSource>());

            return adapter.Matrix;
            }

        internal bool Print()
            {
            var matrixReportPrintHelper = new MatrixReportPrintHelper();

            const bool printLandscape = false;
            const bool showPreview = false;

            try
                {
                matrixReportPrintHelper.CustomPrint(matrix, "Этикетки на паллеты", printLandscape, printerName,
                    StickersPrintingHelper.STICKER_WIDTH, StickersPrintingHelper.STICKER_HEIGHT, showPreview);

                //matrixReportPrintHelper.ShowPreview(matrix, "", printLandscape);
                }

            catch (Exception exp)
                {
                var message = string.Format("Сбой при печати: {0}", exp.Message);

                return false;
                }

            return true;
            }
        }
    }
