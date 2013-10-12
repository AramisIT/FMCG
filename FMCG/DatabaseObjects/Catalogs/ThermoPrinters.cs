using System;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using AtosFMCG.DatabaseObjects.Interfaces;
using Catalogs;
using Aramis.Platform;

namespace Catalogs
    {
    [Catalog(Description = "Термо принтеры", GUID = "A0BBC4F1-AE34-476F-ACC5-FCF5BD859F17", HierarchicType = HierarchicTypes.None, DescriptionSize = 100)]
    public class ThermoPrinters : CatalogTable
        {
        [DataField(Description = "Користувач", ShowInList = true)]
        public Users User
            {
            get
                {
                return (Users)GetValueForObjectProperty("User");
                }
            set
                {
                SetValueForObjectProperty("User", value);
                }
            }

        private static string printerName;

        internal static string GetCurrentPrinterName()
            {
            if (string.IsNullOrEmpty(printerName))
                {
                var q = DB.NewQuery("Select top 1 Rtrim(Description) [Description] from ThermoPrinters where [User] = @User and MarkForDeleting = 0");
                q.AddInputParameter("User", SystemAramis.CurrentUserId);
                printerName = Convert.ToString(q.SelectScalar() ?? (object)(string.Empty));
                }

            return printerName;
            }
        }
    }