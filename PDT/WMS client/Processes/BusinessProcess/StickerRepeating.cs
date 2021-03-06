using System.Drawing;
using System.Linq;
using WMS_client.HelperClasses;
using System.Collections.Generic;
using System.Data;
using System;

namespace WMS_client.Processes
    {
    public class StickerRepeating : BusinessProcess
        {
        private Dictionary<long, bool> processedPallets = new Dictionary<long, bool>();
        private MobileLabel currentNomenclatureLabel;
        private MobileTextBox currentStickerIdTextBox;
        private MobileLabel printTaskLabel;
        private MobileButton complateButton;

        private const string INVALID_BARCODE_MSG = "³����������� �����-��� �� �����";

        public StickerRepeating()
            : base(1)
            {
            ToDoCommand = "������ ��������";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;


            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            MainProcess.CreateLabel("³��������� �����", 10, top, 230,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            currentNomenclatureLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS;
            MainProcess.CreateLabel("��� ������", 10, top, 100,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            currentStickerIdTextBox = MainProcess.CreateTextBox(120, top, 80, string.Empty, ControlsStyle.LabelNormal, palletCodeEntered, false);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS * 3;
            printTaskLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += VERTICAL_DISTANCE_BETWEEN_CONTROLS * 2;
            complateButton = MainProcess.CreateButton("������������     (F4)", 5, top, 230, 35, string.Empty, complateProcess);

            updatePrintTask();
            }

        private void palletCodeEntered(object obj, EventArgs e)
            {
            if (string.IsNullOrEmpty(currentStickerIdTextBox.Text.Trim())) return;

            long palletCode = Convert.ToInt64(currentStickerIdTextBox.Text);
            handlePalletCode(new BarcodeData() { StickerId = palletCode });
            }

        protected override void OnBarcode(string barcode)
            {
            barcode = barcode.Replace("\r\r", "$$");

            if (!barcode.IsSticker()) return;

            handlePalletCode(barcode.ToBarcodeData());
            }

        private void handlePalletCode(BarcodeData barcodeData)
            {
            if (!barcodeData.ReadStickerInfo())
                {
                return;
                }

            if (!processedPallets.ContainsKey(barcodeData.StickerId))
                {
                processedPallets.Add(barcodeData.StickerId, true);
                }

            currentNomenclatureLabel.Text = barcodeData.Nomenclature.Description;
            currentStickerIdTextBox.Text = barcodeData.StickerId.ToString();

            updatePrintTask();
            }

        protected override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Complate:
                    complateProcess();
                    return;

                case KeyAction.Esc:
                    if (processedPallets.Count == 0 || "��������� ��������?".Ask())
                        {
                        exit();
                        }
                    return;
                }
            }

        #endregion

        private void updatePrintTask()
            {
            printTaskLabel.Text = string.Format("������ ���������: {0}", processedPallets.Count);

            if (processedPallets.Count > 0)
                {
                complateButton.Show();
                }
            else
                {
                complateButton.Hide();
                }
            }

        private void exit()
            {
            MainProcess.ClearControls();
            MainProcess.Process = new SelectingProcess();
            }

        private void complateProcess()
            {
            if (!new StickersPrinting(processedPallets.Keys.ToList()).Print())
                {
                Warning_CantComplateOperation();
                return;
                }

            exit();
            }
        }
    }