using System;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;
using Aramis.DatabaseConnector;
using StorekeeperManagementServer;
using System.Collections.Generic;

namespace AtosFMCG.HelperClasses.PDT
    {
    public partial class SendToTCD : Form
        {
        private readonly StorekeeperManagementServer.AramisTCPServer smServer;

        public SendToTCD(StorekeeperManagementServer.AramisTCPServer server)
            {
            InitializeComponent();
            smServer = server;
            }

        #region Barcode
        private void send_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(message.Text);
            }

        private void sendMe_Click(object sender, EventArgs e)
            {
            Button btn = (Button)sender;
            smServer.PressKeyOnTDC(btn.Text);
            }
        #endregion

        #region Keys
        private void f4_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(115);
            }

        private void f5_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(116);
            }

        private void button86_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(11);
            }
        #endregion

        private void sendNbarcodes_Click(object sender, EventArgs e)
            {
            int count;
            int.TryParse(barcodecCount.Text, out count);

            for (int i = 0; i < count; i++)
                {
                smServer.PressKeyOnTDC(string.Concat(barcodePrefix.Text, i));
                Thread.Sleep(1000);
                }
            }

        private void button3_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(textBox1.Text);
            }

        private void button7_Click(object sender, EventArgs e)
            {
            smServer.PressKeyOnTDC(textBox2.Text);
            }

        private void button8_Click(object sender, EventArgs e)
            {
            var sql = @"
with cellsI as 
(select distinct s.FinalCell, c.Description
	from SubInventoryNomenclatureInfo s 

	join Inventory i on i.Id = s.IdDoc
    join Cells c on c.Id = s.FinalCell
    
	where i.MarkForDeleting = 0 and  s.FinalCell > 0 and i.Date>= '2014-04-16' and c.Row<>3
	--order by c.Description
)

select  cellsI.FinalCell, s.PalletCode, i.[Date], max(s.LineNumber ) ln
	from SubInventoryNomenclatureInfo s 

	join Inventory i on i.Id = s.IdDoc
    join cellsI on cellsI.FinalCell = s.FinalCell 
	where i.MarkForDeleting = 0 and
	i.Date>= '2014-04-16' 
	group by s.PalletCode, i.[Date], cellsI.Description, cellsI.FinalCell
	order by cellsI.Description, i.[Date], ln
";
            var tabl = DB.NewQuery(sql).SelectToTable();
            tabl.Columns.Remove("Date");
            tabl.Columns.Remove("ln");

            var indexesToDelete = new List<int>();
            var dictionary = new HashSet<long>();

            for (int rowIndex = tabl.Rows.Count - 1; rowIndex >= 0; rowIndex--)
                {
                var row = tabl.Rows[rowIndex];
                long palletCode = Convert.ToInt64(row["PalletCode"]);
                if (dictionary.Contains(palletCode))
                    {
                    indexesToDelete.Add(rowIndex);
                    }
                else
                    {
                    dictionary.Add(palletCode);
                    }
                }

            indexesToDelete.ForEach(rowIndex => tabl.Rows.RemoveAt(rowIndex));

            long lastPalletId = 0;
            long currentCell = 0;
            var result = new StringBuilder();
            foreach (DataRow row in tabl.Rows)
                {
                var palletId = Convert.ToInt64(row["PalletCode"]);
                var cell = Convert.ToInt64(row["FinalCell"]);
                long previousPallet;
                if (currentCell == cell)
                    {
                    previousPallet = lastPalletId;
                    }
                else
                    {
                    if (currentCell != 0)
                        {
                        result.AppendLine("--*--");
                        }
                    previousPallet = 0;
                    currentCell = cell;
                    }

                var task = getBarcode(cell, palletId, previousPallet);
                if (!string.IsNullOrEmpty(task))
                    {
                    result.AppendLine(task);
                    lastPalletId = palletId;
                    }
                }

            if (result.Length > 0)
                {
                result.AppendLine("--*--");
                }

            Clipboard.SetText(result.ToString());
            }

        private string getBarcode(long originalCellId, long stickerId, long previousStickerId)
            {
            long nomenclatureId;
            string nomenclatureDescription;
            long trayId;
            long linerId;
            int linersAmount;
            int unitsPerBox;
            long cellId;
            string cellDescription;
            long previousPalletCode;
            DateTime productionDate;
            long partyId;
            int totalUnitsQuantity;
            int traysCount;

            if (!ReceiveMessages.Сommunication.GetPalletBalance(stickerId, out nomenclatureId,
                out nomenclatureDescription,
                out trayId,
                out linerId, out linersAmount,
                out unitsPerBox,
                out cellId, out cellDescription,
                out previousPalletCode,
                out productionDate, out partyId,
                out totalUnitsQuantity,
                out traysCount))
                {
                Trace.WriteLine(string.Format("Error getting Balance {0} {1}", stickerId, previousStickerId));
                }

            if (totalUnitsQuantity == 0) return string.Empty;
            var packCount = unitsPerBox > 0 ? totalUnitsQuantity / unitsPerBox : 0;
            var result = string.Format("{0}*{1}*{2}*{3}*{4}*{5}*{6}*{7}", originalCellId, stickerId, previousStickerId,
                packCount, trayId, linerId, linersAmount, nomenclatureId);

            return result;
            }

        private string[] tasks;
        private int taskIndex;

        private void button9_Click(object sender, EventArgs e)
            {
            tasks = Clipboard.GetText().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            sendTimer.Enabled = true;
            taskIndex = 0;
            }

        private void sendTimer_Tick(object sender, EventArgs e)
            {
            if (pause.Checked) return;

            if (tasks == null || tasks.Length == taskIndex)
                {
                sendTimer.Enabled = false;
                return;
                }

            smServer.PressKeyOnTDC(tasks[taskIndex]);

            taskIndex++;
            }
        }
    }