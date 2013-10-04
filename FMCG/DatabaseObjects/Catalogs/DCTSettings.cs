using System.Collections;
using System.Data;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;

namespace Catalogs
    {
    /// <summary>Налаштування ТСД</summary>
    [Catalog(Description = "Налаштування ТСД", GUID = "A2339F64-F7D1-44C5-9E07-2598CC95172C", HierarchicType = HierarchicTypes.None, ShowCodeFieldInForm = false)]
    public class DCTSettings : CatalogTable
        {
        #region Properties
        /// <summary>Дозволений IP</summary>
        [DataField(Description = "Дозволений IP", ShowInList = true)]
        public string AllowIP
            {
            get
                {
                return z_AllowIP;
                }
            set
                {
                if (z_AllowIP == value)
                    {
                    return;
                    }

                z_AllowIP = value;
                NotifyPropertyChanged("AllowIP");
                }
            }
        private string z_AllowIP = string.Empty;
        #endregion

        /// <summary>Дозволені IP адрес пристроїв</summary>
        /// <returns>Список дозволених IP адрес</returns>
        public static ArrayList AllowedIPs()
            {
            const string IP_COLUMN = "AllowedIP";
            string command = string.Format("SELECT DISTINCT RTRIM(AllowIP) {0} FROM {1}", IP_COLUMN, typeof(DCTSettings).Name);
            Query query = DB.NewQuery(command);
            DataTable table = query.SelectToTable();

            if (table == null)
                {
                return new ArrayList();
                }
            
            ArrayList list = new ArrayList();
            foreach (DataRow row in table.Rows)
                {
                list.Add(row[IP_COLUMN].ToString());
                }

            return list;
            }
        }
    }