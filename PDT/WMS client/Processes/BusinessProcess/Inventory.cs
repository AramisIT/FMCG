using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Inventory : ParusProcess
        {
        private class AcceptancePalletControls : HideableControlsCollection
            {
            public MobileTextBox PacksCountTextBox;
            public MobileTextBox UnitsCountTextBox;
            public MobileTextBox LinersQuantityTextBox;

            public MobileLabel nomenclatureLabel;
            public MobileLabel cellLabel;
            public MobileLabel stickerIdInfoLabel;

            public MobileButton trayButton;
            public MobileButton linerButton;
            public MobileLabel packsLabel;
            public MobileLabel unitsLabel;
            public MobileLabel linersLabel;
            public MobileLabel cellCaptionLabel;
            }

        private class ScanPalletControls : HideableControlsCollection
            {
            public MobileLabel WillLabel;
            }

        /// <summary>
        /// �������������� �������
        /// </summary>
        private AcceptancePalletControls palletEditControls;

        /// <summary>
        /// ����������� ������������� ��������� �������
        /// </summary>
        private ScanPalletControls scanNextPalletControls;

        private BarcodeData currentBarcodeData;
        private long lastStickerId;
        private BarcodeData startBarcodeData;
        private bool cellDefined;
        private long documentId;

        private Dictionary<long, bool> processedPallets = new Dictionary<long, bool>();

        private const string INVALID_BARCODE_MSG = "³����������� �����-��� �� �����";

        public Inventory()
            : base()
            {
            ToDoCommand = "�����������ֲ�";
            checkAcceptanceCache();
            }

        private void checkAcceptanceCache()
            {
            var repository = new Repository();
            repository.GetTraysList();
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            createPalletControls();

            createScanPalletLabelControls();

            startScanNextPallet();
            }

        public override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (scanNextPalletControls.Visible)
                {
                scanNextPalletOnBarcode(barcode);
                }
            else if (palletEditControls.Visible)
                {
                editPalletOnBarcode(barcode);
                }
            }

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            if (palletEditControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Esc:
                        startScanNextPallet();
                        return;

                    case KeyAction.Proceed:
                        if (!cellDefined)
                            {
                            "³��������� ��������� ������ �� ������, ���� ������ �����".ShowMessage();
                            return;
                            }
                        if (saveFact())
                            {
                            processedPallets.Add(currentBarcodeData.StickerId, true);
                            startScanNextPallet();
                            }
                        return;
                    }
                }
            else if (scanNextPalletControls.Visible)
                {
                switch (TypeOfAction)
                    {
                    case KeyAction.Complate:
                        complateProcess();
                        return;

                    case KeyAction.Esc:
                        if ("��������� ��������?".Ask())
                            {
                            MainProcess.ClearControls();
                            MainProcess.Process = new SelectingProcess();
                            }
                        return;
                    }
                }
            }

        #endregion

        private void scanNextPalletOnBarcode(string barcode)
            {
            if (!barcode.IsSticker()) return;

            var barcodeData = barcode.ToBarcodeData();

            if (processedPallets.ContainsKey(barcodeData.StickerId))
                {
                "�� ������ ��� ���� ���������".ShowMessage();
                return;
                }

            if (!barcodeData.ReadStickerInfo()) return;

            startBarcodeData = barcodeData;
            currentBarcodeData = barcodeData.GetCopy();

            ShowControls(palletEditControls);

            updateStickerData();
            }

        private void editPalletOnBarcode(string barcode)
            {
            if (barcode.IsSticker())
                {
                var barcodeData = barcode.ToBarcodeData();
                processedPallets = new Dictionary<long, bool>();

                var samePallet = currentBarcodeData != null && currentBarcodeData.StickerId == barcodeData.StickerId;
                if (samePallet)
                    {
                    return;
                    }

                onAnotherStickerScan(barcodeData);
                }
            else if (barcode.IsCell())
                {
                onCellScan(barcode.ToCell());
                }
            }

        private void onAnotherStickerScan(BarcodeData barcodeData)
            {
            if (barcodeData.StickerId == currentBarcodeData.PreviousStickerCode) return;

            barcodeData.ReadStickerInfo();
            if (!barcodeData.LocatedIdCell)
                {
                showPalletCellNotFountMessage();
                return;
                }

            if (!string.Format(@"��������� � ������ ""{0}"" ���� ������ � {1}", barcodeData.Cell.Description, barcodeData.StickerId).Ask()) return;

            this.currentBarcodeData.Cell.CopyFrom(barcodeData.Cell);
            this.currentBarcodeData.PreviousStickerCode = barcodeData.StickerId;
            notifyCellUpdated();
            }

        private void onCellScan(CatalogItem scannedCell)
            {
            if (scannedCell.Id == startBarcodeData.Cell.Id)
                {
                if (startBarcodeData.PreviousStickerCode == 0)
                    {
                    notifyCellUpdated();
                    }
                else if ("����� ������ ����� � ������?".Ask())
                    {
                    currentBarcodeData.PreviousStickerCode = 0;
                    notifyCellUpdated();
                    }
                }
            else if (string.Format(@"��������� ������ ������ � ������ ""{0}""?", scannedCell.Description).Ask())
                {
                currentBarcodeData.Cell.CopyFrom(scannedCell);
                currentBarcodeData.PreviousStickerCode = 0;
                notifyCellUpdated();
                }
            }

        private void notifyCellUpdated()
            {
            cellDefined = true;
            updateCellDescription();
            palletEditControls.cellLabel.SetFontColor(MobileFontColors.Warning);
            }

        private void startScanNextPallet()
            {
            currentBarcodeData = null;
            ShowControls(scanNextPalletControls);
            palletEditControls.cellLabel.SetFontColor(MobileFontColors.Disable);
            cellDefined = false;
            }

        private void createScanPalletLabelControls()
            {
            scanNextPalletControls = new ScanPalletControls();

            int top = 140;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            scanNextPalletControls.WillLabel = MainProcess.CreateLabel("³��������� ������", 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            }

        private void createPalletControls()
            {
            palletEditControls = new AcceptancePalletControls();
            int top = 42;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.nomenclatureLabel = MainProcess.CreateLabel("<������������>", 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;

            palletEditControls.packsLabel = MainProcess.CreateLabel("��������:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.PacksCountTextBox = MainProcess.CreateTextBox(90, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            palletEditControls.unitsLabel = MainProcess.CreateLabel("+ ��.:", 135, top, 55,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.UnitsCountTextBox = MainProcess.CreateTextBox(195, top, 40, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.stickerIdInfoLabel = MainProcess.CreateLabel(string.Empty, 5, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.trayButton = MainProcess.CreateButton("", 5, top, 230, 35, "modelButton", trayButton_Click);
            updateTrayDescription();

            top += 2 * VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.linersLabel = MainProcess.CreateLabel("ʳ������ ���������:", 5, top, 180,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.LinersQuantityTextBox = MainProcess.CreateTextBox(190, top, 45, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.linerButton = MainProcess.CreateButton(string.Empty, 5, top, 230, 35, "modelButton", linerButton_Click);
            updateLinerButton();

            top += 2 * VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            palletEditControls.cellCaptionLabel = MainProcess.CreateLabel("������:", 5, top, 80,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            palletEditControls.cellLabel = MainProcess.CreateLabel("<?>", 95, top, 140,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            }

        private void complateProcess()
            {
            if (!"��������� ��������?".Ask())
                {
                return;
                }

            string errorDescription;
            if (!new ServerInteraction().ComplateInventory(documentId, false, out errorDescription))
                {
                Warning_CantComplateOperation();
                return;
                }
            MainProcess.Process = new SelectingProcess();
            }

        private void updateLinerButton()
            {
            var liner = (currentBarcodeData ?? new BarcodeData()).Liner ?? new CatalogItem();
            palletEditControls.linerButton.Text = liner.Id > 0 ? liner.Description : "<��� ���������>";
            }

        private void trayButton_Click()
            {
            chooseTray((selectedItem) =>
                {
                    currentBarcodeData.Tray.CopyFrom(selectedItem);
                    updateTrayDescription();
                });
            }

        private void updateTrayDescription()
            {
            var tray = ((currentBarcodeData ?? new BarcodeData()).Tray ?? new CatalogItem());
            palletEditControls.trayButton.Text = tray.Id == 0 ? "��� ������" : tray.Description;
            }

        private void linerButton_Click()
            {
            chooseLiner(liner =>
                {
                    currentBarcodeData.Liner.CopyFrom(liner);
                    updateLinerButton();
                });
            }

        private bool saveFact()
            {
            if (documentId == 0 && !initDocument())
                {
                return false;
                }

            if (currentBarcodeData == null) return true;

            currentBarcodeData.LinersAmount = linersCount;
            currentBarcodeData.TotalUnitsQuantity = unitsCount + packsCount * currentBarcodeData.UnitsPerBox;

            var movementWriter = new TableMovementWriter(startBarcodeData, currentBarcodeData);
            return new ServerInteraction().WriteInventoryResult(documentId, movementWriter.Table);
            }

        private bool initDocument()
            {
            return new ServerInteraction().GetNewInventoryId(0, out documentId);
            }

        public int packsCount
            {
            get { return palletEditControls.PacksCountTextBox.GetNumber(); }
            set { palletEditControls.PacksCountTextBox.SetNumber(value); }
            }

        public int unitsCount
            {
            get { return palletEditControls.UnitsCountTextBox.GetNumber(); }
            set { palletEditControls.UnitsCountTextBox.SetNumber(value); }
            }

        public byte linersCount
            {
            get { return (byte)palletEditControls.LinersQuantityTextBox.GetNumber(); }
            set { palletEditControls.LinersQuantityTextBox.SetNumber(value); }
            }

        private void updateStickerData()
            {
            updateCellDescription();

            palletEditControls.nomenclatureLabel.Text = currentBarcodeData.Nomenclature.Description;
            palletEditControls.trayButton.Text = currentBarcodeData.Tray.Description;

            palletEditControls.stickerIdInfoLabel.Text = string.Format("��� ������: {0}", currentBarcodeData.StickerId);

            if (currentBarcodeData.UnitsPerBox > 0)
                {
                packsCount = currentBarcodeData.FullPacksCount;
                unitsCount = currentBarcodeData.UnitsRemainder;
                }
            else
                {
                packsCount = 0;
                unitsCount = 0;
                }

            linersCount = currentBarcodeData.LinersAmount;
            updateLinerButton();
            updateTrayDescription();
            }

        private void updateCellDescription()
            {
            palletEditControls.cellLabel.Text = (currentBarcodeData.Cell != null && currentBarcodeData.Cell.Id > 0) ? currentBarcodeData.Cell.Description : "<?>";
            }
        }
    }