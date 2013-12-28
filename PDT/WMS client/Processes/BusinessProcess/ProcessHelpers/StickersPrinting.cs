using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client.Processes
    {
    class StickersPrinting
        {
        private DataTable printingTasks;

        public StickersPrinting(long stickerId)
            : this(new List<long>() { stickerId })
        { }

        public StickersPrinting(List<long> stickersIdentifiers)
            {
            printingTasks = new DataTable();
            printingTasks.Columns.Add("Value", typeof(Int64));
            foreach (var palletId in stickersIdentifiers)
                {
                printingTasks.Rows.Add(palletId);
                }
            }

        public bool Print()
            {
            var result = new ServerInteraction().PrintStickers(printingTasks);
            return result;
            }
        }
    }
