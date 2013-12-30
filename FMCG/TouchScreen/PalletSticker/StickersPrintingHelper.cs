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

namespace AtosFMCG.TouchScreen.PalletSticker
    {
    class StickersPrintingHelper
        {
        public const int STICKER_WIDTH = 390;
        public const int STICKER_HEIGHT = 300;

        public StickersPrintingHelper(List<StickerInfo> stickersTasks, string printerName)
            {
            this.stickersTasks = stickersTasks;
            this.printerName = printerName;
            }

        public StickersPrintingHelper(List<Stickers> stickersTasks, string printerName)
            {
            this.stickersTasks = createStickersTasks(stickersTasks);
            this.printerName = printerName;
            }

        private List<StickerInfo> createStickersTasks(List<Stickers> stickers)
            {
            var result = new List<StickerInfo>();
            stickers.ForEach(sticker =>
            {
                var stickerInfo = new StickerInfo()
                {
                    AcceptionDate = sticker.AcceptionDate,
                    Barcode = sticker.Barcode,
                    Driver = sticker.Driver.Description,
                    ReleaseDate = sticker.ReleaseDate,
                    ExpiryDate = sticker.ExpiryDate,
                    HalpExpiryDate = sticker.HalpExpiryDate,
                    Nomenclature = sticker.Nomenclature.Description,
                    PacksCount = sticker.Quantity,
                    UnitsQuantity = sticker.UnitsQuantity,
                    Id = sticker.Id
                };
                result.Add(stickerInfo);
            });

            return result;
            }
        private List<StickerInfo> stickersTasks;
        private string printerName;

        internal bool Print()
            {
            var dataSource = initDataSource();
            if (dataSource.Rows.Count == 0)
                {
                return true;
                }
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

                //matrixReportPrintHelper.ShowPreview(matrix, "" ,false);
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
            MatrixAdapter adapter = new MatrixAdapter(new DesktopMatrixReportMainFactory(), XDocument.Parse(xmlContent).Root, null, true);

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
                new DataColumn("UnitsQuantity", typeof(int)), 
                new DataColumn("ReleaseDate", typeof(DateTime)),
                new DataColumn("HalpExpiryDate", typeof(DateTime)),
                new DataColumn("ExpiryDate", typeof(DateTime)),
                new DataColumn("AcceptionDate", typeof(DateTime)),
                new DataColumn("Driver", typeof(string)),
                new DataColumn("Id", typeof(long)),
                new DataColumn("barcode", typeof(string))
                });

            foreach (var stickersTask in stickersTasks)
                {
                dataSource.Rows.Add(stickersTask.Nomenclature, stickersTask.Barcode, stickersTask.PacksCount, stickersTask.UnitsQuantity,
                    stickersTask.ReleaseDate, stickersTask.HalpExpiryDate, stickersTask.ExpiryDate, stickersTask.AcceptionDate,
                    stickersTask.Driver, stickersTask.Id, string.Format("S{0}", stickersTask.Id));
                }

            return dataSource;
            }

        }
    }
