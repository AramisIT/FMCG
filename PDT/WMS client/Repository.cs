using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WMS_client
    {
    public class Repository
        {
        private static List<CatalogItem> traysList;
        private static List<CatalogItem> linersList;

        internal List<CatalogItem> GetTraysList()
            {
            if (traysList == null)
                {
                initTareLists();
                }
            return traysList ?? new List<CatalogItem>();
            }

        internal List<CatalogItem> GetLinersList()
            {
            if (linersList == null)
                {
                initTareLists();
                }
            return linersList ?? new List<CatalogItem>();
            }

        private void initTareLists()
            {
            DataTable tareTable;
            if (!new ServerInteraction().GetTareTable(out tareTable))
                {
                return;
                }


            var trays = new List<CatalogItem>();
            var liners = new List<CatalogItem>();

            foreach (DataRow row in tareTable.Rows)
                {
                var item = new CatalogItem()
                    {
                        Description = row["Description"].ToString(),
                        Id = Convert.ToInt64(row["Id"])
                    };
                if (item.Id == 0)
                    {
                    continue;
                    }

                const int TRAY_TARE_TYPE = 1;
                const int LINER_TARE__TYPE = 2;
                switch (Convert.ToInt32(row["TareType"]))
                    {
                    case TRAY_TARE_TYPE:
                        trays.Add(item);
                        break;

                    case LINER_TARE__TYPE:
                        liners.Add(item);
                        break;
                    }
                }

            if (trays.Count > 0)
                {
                traysList = trays;
                traysList.Insert(0, new CatalogItem() { Description = "без піддону", Id = 0 });
                }

            if (liners.Count > 0)
                {
                linersList = liners;
                linersList.Insert(0, new CatalogItem() { Description = "без прокладки", Id = 0 });
                }
            }
        }
    }
