using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class Picking : BusinessProcess
        {
        private class PickingTaskControls : HideableControlsCollection
            {
            public MobileLabel planPickingCell;
            public MobileLabel pickingWareLine1;
            public MobileLabel pickingWareLine2;
            public MobileLabel productionDate;
            public MobileLabel planedQuantity;
            }

        private class ScanPallet : HideableControlsCollection
            {
            public MobileLabel order;
            }

        private class QuantityEditControls : HideableControlsCollection
            {
            public MobileLabel boxesLabel;
            public MobileLabel unitsLabel;
            public MobileLabel linersLabel;
            public MobileTextBox unitsCountTextBox;
            public MobileTextBox packsCountTextBox;
            public MobileTextBox linersCountTextBox;
            public MobileButton proceedButton;
            }

        public byte linersCount
            {
            get { return (byte)quantityEditControls.linersCountTextBox.GetNumber(9, 0); }
            }

        public int packsCount
            {
            get { return quantityEditControls.packsCountTextBox.GetNumber(); }
            set { quantityEditControls.packsCountTextBox.SetNumber(value); }
            }

        public int unitsCount
            {
            get { return quantityEditControls.unitsCountTextBox.GetNumber(); }
            set { quantityEditControls.unitsCountTextBox.SetNumber(value); }
            }

        private long documentId;
        private PickingTaskControls pickingTask;
        private ScanPallet scanPallet;
        private QuantityEditControls quantityEditControls;
        private BarcodeData pickingTaskData;
        private int currentLineNumber = 0;
        private BarcodeData factPickingData;
        private int sameWareNextTaskLineNumber;
        private int totalUnitsQuantityOnPallet;


        public Picking(long documentId)
            : base(1)
            {
            ToDoCommand = "³��� ������";
            this.documentId = documentId;

            sameWareNextTaskLineNumber = 0;
            if (!startPalletChoosing(0, 0))
                {
                exitProcess();
                }
            }

        private bool startPalletChoosing(long palletId, int _currentLineNumber)
            {
            int predefinedTaskLineNumber = sameWareNextTaskLineNumber;
            quantityEditControls.Hide();
            scanPallet.Show();

            BarcodeData pickingPlan;
            if (!readNextPickingTask(palletId, predefinedTaskLineNumber, _currentLineNumber, out pickingPlan))
                {
                pickingTaskData = new BarcodeData();
                "���� ��'���� � ��������. ������� WiFi �� ��������� ��� (F5)".ShowMessage();
                return false;
                }

            if (pickingPlan.Empty)
                {
                if (palletId > 0)
                    {
                    "³���������� ��������� �� ����������� ��� ������!".Warning();
                    return false;
                    }
                ComplateOperation();
                return true;
                }

            pickingTaskData = pickingPlan;

            setCellDescription(pickingTaskData.Cell);

            int separationPosition = Math.Min(pickingTaskData.Nomenclature.Description.Length - 1, 24);
            pickingTask.pickingWareLine1.Text = pickingTaskData.Nomenclature.Description.Substring(0, separationPosition);
            pickingTask.pickingWareLine2.Text = pickingTaskData.Nomenclature.Description.Substring(separationPosition);

            showParty(pickingTaskData.Party);

            var unitsQuantityStr = pickingTaskData.UnitsRemainder == 0
                ? string.Empty
                : " + " + pickingTaskData.UnitsRemainder.ToString();
            pickingTask.planedQuantity.Text = string.Format("{0} �� {1}{2}", pickingTaskData.FullPacksCount,
                pickingTaskData.UnitsPerBox, unitsQuantityStr);

            return true;
            }

        private void setCellDescription(CatalogItem cell)
            {
            var cellDescription = cell.Id == 0 ? "<?>" : cell.Description;
            pickingTask.planPickingCell.Text = string.Format("������ {0}", cellDescription);
            }

        private void showParty(CatalogItem party)
            {
            pickingTask.productionDate.Text = string.Format("���� �����-�� {0}", party.Id == 0 ? "<?>" : party.Description);
            }

        private void ComplateOperation()
            {
            string errorDescription;
            if (!Program.AramisSystem.ComplateMovement(documentId, false, out errorDescription))
                {
                Warning_CantComplateOperation();
                }

            exitProcess();
            }

        private void exitProcess()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new SelectingProcess();
            }

        private bool readNextPickingTask(long palletId, int predefinedTaskLineNumber, int _currentLineNumber, out BarcodeData pickingPlan)
            {
            pickingPlan = null;
            long stickerId;
            long wareId;
            string wareDescription;
            long cellId;
            string cellDescription;
            long partyId;
            DateTime productionDate;
            int unitsPerBox;
            int unitsToPick;
            int taskLineNumber;

            if (!Program.AramisSystem.GetPickingTask(documentId, palletId, predefinedTaskLineNumber, _currentLineNumber,
                out stickerId,
                out wareId, out wareDescription,
                out cellId, out cellDescription,
                out partyId, out productionDate,
                out unitsPerBox, out unitsToPick,
                out taskLineNumber)) return false;

            currentLineNumber = taskLineNumber;
            pickingPlan = new BarcodeData();
            pickingPlan.Nomenclature.Id = wareId;
            pickingPlan.Nomenclature.Description = wareDescription;
            pickingPlan.Cell.Id = cellId;
            pickingPlan.Cell.Description = cellDescription;
            pickingPlan.UnitsPerBox = unitsPerBox;
            pickingPlan.StickerId = stickerId;
            pickingPlan.TotalUnitsQuantity = unitsToPick;
            pickingPlan.Party.Description = productionDate.ToStandartString();
            pickingPlan.Party.Id = partyId;

            return true;
            }

        public override void DrawControls()
            {
            pickingTask = new PickingTaskControls();

            int top = 42;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            pickingTask.planPickingCell = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            pickingTask.pickingWareLine1 = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS - 7;
            pickingTask.pickingWareLine2 = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            pickingTask.productionDate = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            pickingTask.planedQuantity = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            scanPallet = new ScanPallet();

            scanPallet.order = MainProcess.CreateLabel("³��������� ������", 10, 240, 230,
               MobileFontSize.Large, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            quantityEditControls = new QuantityEditControls();

            top = 205;
            quantityEditControls.boxesLabel = MainProcess.CreateLabel("³������ ��������", 10, top, 150,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            quantityEditControls.packsCountTextBox = MainProcess.CreateTextBox(170, top, 55, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            quantityEditControls.unitsLabel = MainProcess.CreateLabel("³������ �������", 10, top, 150,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);
            quantityEditControls.unitsCountTextBox = MainProcess.CreateTextBox(170, top, 55, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            quantityEditControls.linersLabel = MainProcess.CreateLabel("����� ���������", 10, top, 150,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);
            quantityEditControls.linersCountTextBox = MainProcess.CreateTextBox(170, top, 55, string.Empty, ControlsStyle.LabelNormal, null, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            quantityEditControls.proceedButton = MainProcess.CreateButton("����������              ( F5 )", 10, top, 220, 30, "modelButton", proceed);
            }

        private void proceed()
            {
            factPickingData.TotalUnitsQuantity = unitsCount + packsCount * factPickingData.UnitsPerBox;

            var palletNotEmptyNow = totalUnitsQuantityOnPallet != factPickingData.TotalUnitsQuantity;
            if (palletNotEmptyNow)
                {
                factPickingData.LinersAmount = linersCount;
                }

            pickingTaskData.Tray.CopyFrom(factPickingData.Tray);
            pickingTaskData.Liner.CopyFrom(factPickingData.Liner);
            pickingTaskData.LinersAmount = factPickingData.LinersAmount;

            var resultWriter = new TableMovementWriter(pickingTaskData, factPickingData);
            resultWriter.SetStartCell(factPickingData.Cell);

            int _sameWareNextTaskLineNumber;
            var success = Program.AramisSystem.WritePickingResult(documentId, currentLineNumber, resultWriter.Table, factPickingData.Party.Id, out _sameWareNextTaskLineNumber);
            if (success)
                {
                factPickingData = null;
                this.sameWareNextTaskLineNumber = _sameWareNextTaskLineNumber;
                startPalletChoosing(0, 0);
                }
            else if (lastQueryIsSuccessfull)
                {
                "���� ������� �� ���������".Warning();
                }
            }

        private long lastScannedPalletId;

        protected override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (!scanPallet.Visible) return;
            if (!barcode.IsSticker()) return;

            var barcodeData = barcode.ToBarcodeData();
            if (lastScannedPalletId == barcodeData.StickerId
                && "��������� ��������".Ask())
                {
                if (!new StickersPrinting(barcodeData.StickerId).Print())
                    {
                    Warning_CantComplateOperation();
                    }
                return;
                }

            lastScannedPalletId = barcodeData.StickerId;

            if (!barcodeData.ReadStickerInfo())
                {
                Warning_CantComplateOperation();
                return;
                }

            if (barcodeData.StickerId == pickingTaskData.StickerId)
                {
                factPickingData = pickingTaskData.GetCopy();
                }
            else
                {
                if (pickingTaskData.SameWare(barcodeData, false)
                    &&
                    (pickingTaskData.StickerId == 0
                    || true//string.Format("��������� ����� � ������� {0}", barcodeData.StickerId).Ask()
                    ))
                    {
                    factPickingData = barcodeData;
                    showParty(factPickingData.Party);
                    }
                else
                    {
                    sameWareNextTaskLineNumber = 0;
                    if (startPalletChoosing(barcodeData.StickerId, currentLineNumber))
                        {
                        lastScannedPalletId = 0;
                        OnBarcode(barcode);
                        }
                    return;
                    }
                }

            setCellDescription(barcodeData.Cell);
            factPickingData.Cell.CopyFrom(barcodeData.Cell);
            factPickingData.Party.CopyFrom(barcodeData.Party);

            totalUnitsQuantityOnPallet = barcodeData.TotalUnitsQuantity;
            factPickingData.LinersAmount = barcodeData.LinersAmount;
            factPickingData.Liner.CopyFrom(barcodeData.Liner);
            factPickingData.Tray.CopyFrom(barcodeData.Tray);

            packsCount = pickingTaskData.FullPacksCount;
            unitsCount = pickingTaskData.UnitsRemainder;
            quantityEditControls.packsCountTextBox.Focus();
            quantityEditControls.Show();
            scanPallet.Hide();

            showLinersControls(barcodeData);
            }

        private void showLinersControls(BarcodeData barcodeData)
            {
            var controlsVisible = barcodeData.Liner.Id != Program.Consts.WoodLinerId && barcodeData.LinersAmount > 0;
            quantityEditControls.linersCountTextBox.Visible = controlsVisible;
            quantityEditControls.linersLabel.Visible = controlsVisible;

            quantityEditControls.linersCountTextBox.Text = controlsVisible ? string.Empty : barcodeData.LinersAmount.ToString();
            }

        protected override void OnHotKey(KeyAction key)
            {
            switch (key)
                {
                case KeyAction.Esc:
                    if ("��������� ��������� ��������".Ask())
                        {
                        MainProcess.ClearControls();
                        MainProcess.Process = new SelectingProcess();
                        }
                    break;

                case KeyAction.Complate:
                    if ("��������� ��������� ��������".Ask())
                        {
                        ComplateOperation();
                        }
                    break;

                case KeyAction.F12:
                case KeyAction.Proceed:
                    if (factPickingData != null && factPickingData.StickerId > 0)
                        {
                        proceed();
                        }
                    break;
                }
            }
        }
    }