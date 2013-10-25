using System;
using Aramis;
using Aramis.Attributes;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>Користувачі</summary>
    [Catalog(Description = "Користувачі", GUID = "A705F057-D476-46D6-BA96-0D6B111D1F85", AllowEnterByPattern = true, UseDescriptionSpellCheck = true)]
    public class Users : CatalogUsers, ISyncWith1C
        {
        /// <summary>Посилання 1С</summary>
        [DataField(Description = "Посилання 1С", ShowInList = false)]
        public Guid Ref1C
            {
            get
                {
                return z_Ref1C;
                }
            set
                {
                if (z_Ref1C == value)
                    {
                    return;
                    }
                z_Ref1C = value;
                NotifyPropertyChanged("Ref1C");
                }
            }
        private Guid z_Ref1C;

        /// <summary>Електронна адреса</summary>
        [DataField(Description = "Електронна адреса", ShowInList = true)]
        public string Email
            {
            get
                {
                return z_Email;
                }
            set
                {
                if (z_Email != value)
                    {
                    z_Email = value;
                    NotifyPropertyChanged("Email");
                    }
                }
            }
        private string z_Email = string.Empty;

        #region Предопределенные элементы
        private const string CATALOG_NAME = "Roles";

        /// <summary>Менеджер ТСД</summary>
        public static DBObjectRef ManagerOfPDT
            {
            get
                {
                return z_ManagerOfPDT ?? (z_ManagerOfPDT = PredefinedElements.GetPredefinedRef(CATALOG_NAME, "Менеджер ТСД"));
                }
            }
        private static DBObjectRef z_ManagerOfPDT;
        #endregion
        }
    }
