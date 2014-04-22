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

        private static Dictionary<long, CatalogItem> traysDictionary;
        private static Dictionary<long, CatalogItem> linersDictionary;

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

        internal string GetLinerDescription(long id)
            {
            return getItemDescriptionByKey(linersDictionary, id);
            }

        internal string GetTrayDescription(long id)
            {
            return getItemDescriptionByKey(traysDictionary, id);
            }

        private static string getItemDescriptionByKey(Dictionary<long, CatalogItem> itemsDictionary, long id)
            {
            CatalogItem item;
            if ((itemsDictionary ?? new Dictionary<long, CatalogItem>()).TryGetValue(id, out item))
                {
                return item.Description;
                }
            return string.Empty;
            }

        private void initTareLists()
            {
            DataTable tareTable;
            if (!Program.AramisSystem.GetTareTable(out tareTable))
                {
                return;
                }

            var traysDictionary = new Dictionary<long, CatalogItem>();
            var linersDictionary = new Dictionary<long, CatalogItem>();

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
                        if (!traysDictionary.ContainsKey(item.Id))
                            {
                            traysDictionary.Add(item.Id, item);
                            }
                        break;

                    case LINER_TARE__TYPE:
                        if (!linersDictionary.ContainsKey(item.Id))
                            {
                            linersDictionary.Add(item.Id, item);
                            }
                        break;
                    }
                }

            if (traysDictionary.Count > 0)
                {
                Repository.traysDictionary = traysDictionary;
                traysList = traysDictionary.Values.ToList();

                traysList.Insert(0, new CatalogItem() { Description = "без піддону", Id = 0 });
                }

            if (linersDictionary.Count > 0)
                {
                Repository.linersDictionary = linersDictionary;
                linersList = linersDictionary.Values.ToList();

                linersList.Insert(0, new CatalogItem() { Description = "без прокладки", Id = 0 });
                }
            }
        }
    }
