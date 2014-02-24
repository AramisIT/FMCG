using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Aramis.Platform;
using Catalogs;
using MatrixBuilding;
using ReportView.Configuration;
using ReportView.ReportModel;
using ReportView.Utils;
using RepositoryOfMatrixReportData;
using TableViewInterfaces;

namespace FMCG.Utils.Printing
    {
    abstract class ThermalTransferPrinting
        {
        public const int STICKER_WIDTH = 390;
        public const int STICKER_HEIGHT = 300;

        private string printerName;

        protected ThermalTransferPrinting()
            {
            PrinterName = ThermoPrinters.GetCurrentPrinterName();
            }

        public string PrinterName
            {
            get { return printerName; }
            set
                {
                if (string.IsNullOrEmpty(value)) return;
                printerName = value;
                }
            }

        protected abstract DataTable getDataSource();

        protected virtual void setReportParameters(Dictionary<string, object> reportParameters) { }

        /// <summary>
        /// xml file must be located in \XML\Adapters\
        /// </summary>
        /// <returns></returns>
        protected abstract string getShortFileNameOfAdapter();

        private MatrixReport createMatrixReport(MatrixReportData matrixReportData)
            {
            string fileName = string.Format(@"{0}\XML\Adapters\{1}", SystemAramis.APPLICATION_PATH,
                getShortFileNameOfAdapter());
            
            if (!File.Exists(fileName))
                {
                string.Format(@"File ""{0}"" not found! Printing canceled.", getShortFileNameOfAdapter()).NotifyToUser();
                return null;
                }

            string xmlContent = File.ReadAllText(fileName);
            MatrixAdapter adapter = new MatrixAdapter(new DesktopMatrixReportMainFactory(), XDocument.Parse(xmlContent).Root, null, true);

            var images = new Dictionary<string, MatrixReportImageSource>();
            adapter.SetDataSources(matrixReportData, images);

            return adapter.Matrix;
            }

        private bool printMatrixReport(MatrixReport matrix)
            {
            var matrixReportPrintHelper = new MatrixReportPrintHelper();

            const bool printLandscape = true;
            const bool showPreview = false;

            try
                {
                matrixReportPrintHelper.CustomPrint(matrix, "Этикетки на паллеты", printLandscape, PrinterName,
                    STICKER_WIDTH, STICKER_HEIGHT, showPreview);

                //matrixReportPrintHelper.ShowPreview(matrix, "" ,false);
                }

            catch (Exception exp)
                {
                var message = string.Format("Сбой при печати: {0}", exp.Message);

                return false;
                }

            return true;
            }


        public bool Print()
            {
            var sources = new Dictionary<string, DataTable>(new IgnoreCaseStringEqualityComparer());
            var dataSource = getDataSource();
            if (dataSource != null && dataSource.Columns.Count > 0)
                {
                sources.Add("SourceTable", dataSource);
                }

            var reportParameters = new Dictionary<string, object>(new IgnoreCaseStringEqualityComparer());
            setReportParameters(reportParameters);

            var emptyParameters = reportParameters.Count == 0;
            var emptyTable = sources.Count == 0 || dataSource.Rows.Count == 0;

            var noAnyDataForPrinting = emptyParameters && emptyTable;
            if (noAnyDataForPrinting)
                {
                return true;
                }

            var matrixReportData = new MatrixReportData()
            {
                Sources = sources,
                ReportParameters = reportParameters
            };

            var report = createMatrixReport(matrixReportData);
            if (report == null) return false;

            return printMatrixReport(report);
            }

        }
    }
