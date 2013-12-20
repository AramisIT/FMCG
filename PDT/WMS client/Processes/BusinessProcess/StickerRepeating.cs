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

        private const string INVALID_BARCODE_MSG = "Відсканований штрих-код не вірний";

        public StickerRepeating()
            : base(1)
            {
            ToDoCommand = "Повтор етикетки";
            }

        #region Override methods

        public override sealed void DrawControls()
            {
            int top = 42;
            const int delta = 27;

            top += delta;
            MainProcess.CreateLabel("Відскануйте піддон", 10, top, 230,
                MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            currentNomenclatureLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta;
            MainProcess.CreateLabel("Код палети", 10, top, 100,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Regular);

            currentStickerIdTextBox = MainProcess.CreateTextBox(120, top, 80, string.Empty, ControlsStyle.LabelNormal, palletCodeEntered, false);

            top += delta * 3;
            printTaskLabel = MainProcess.CreateLabel(string.Empty, 10, top, 230,
               MobileFontSize.Normal, MobileFontPosition.Left, MobileFontColors.Default, FontStyle.Bold);

            top += delta * 2;
            complateButton = MainProcess.CreateButton("Роздрукувати     (F5)", 5, top, 230, 35, string.Empty, complateProcess);

            updatePrintTask();
            }

        private void palletCodeEntered(object obj, EventArgs e)
            {
            if (string.IsNullOrEmpty(currentStickerIdTextBox.Text.Trim())) return;

            long palletCode = Convert.ToInt64(currentStickerIdTextBox.Text);
            handlePalletCode(new BarcodeData() { StickerId = palletCode });
            }

        public override void OnBarcode(string barcode)
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

        public override void OnHotKey(KeyAction TypeOfAction)
            {
            switch (TypeOfAction)
                {
                case KeyAction.Complate:
                    complateProcess();
                    return;

                case KeyAction.Esc:
                    if (processedPallets.Count == 0 || "Скасувати операцію?".Ask())
                        {
                        exit();
                        }
                    return;
                }
            }

        #endregion

        private void updatePrintTask()
            {
            printTaskLabel.Text = string.Format("Всього друкувати: {0}", processedPallets.Count);

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
            var result = new DataTable();
            result.Columns.Add("Value", typeof(Int64));
            foreach (var palletId in processedPallets.Keys)
                {
                result.Rows.Add(palletId);
                }

            if (!new ServerInteraction().PrintStickers(result))
                {
                CANT_COMPLATE_OPERATION.Warning();
                return;
                }

            exit();
            }
        }
    }