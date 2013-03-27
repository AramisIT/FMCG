using Aramis.Attributes;
using Catalogs;

namespace Catalogs
    {
    /// <summary>Користувачі</summary>
    [Catalog(Description = "Користувачі", GUID = "A705F057-D476-46D6-BA96-0D6B111D1F85", AllowEnterByPattern = true, UseDescriptionSpellCheck = true)]
    public class Users : CatalogUsers
        {
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
        }
    }
