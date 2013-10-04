using System;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using AtosFMCG.DatabaseObjects.Interfaces;

namespace Catalogs
    {
    /// <summary>Контрагенти</summary>
    [Catalog(Description = "Контрагенти", GUID = "AC76395E-4648-41F2-879D-E37F1CEF2500", DescriptionSize = 100, AllowEnterByPattern = true)]
    public class Contractors : CatalogTable, ISyncWith1C
        {
        #region Fields
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
                if ( z_Ref1C == value )
                    {
                    return;
                    }
                z_Ref1C = value;
                NotifyPropertyChanged("Ref1C");
                }
            }
        private Guid z_Ref1C;

        /// <summary>Скорочена назва</summary>
        [DataField(Description = "Повна назва", Size = 100)]
        public string FullName
            {
            get
                {
                return z_CutName;
                }
            set
                {
                if (z_CutName == value)
                    {
                    return;
                    }

                z_CutName = value;
                NotifyPropertyChanged("CutName");
                }  
            }
        private string z_CutName = string.Empty;

        /// <summary>Місто</summary>
        [DataField(Description = "Місто", ShowInList = true, NotEmpty = false)]
        public Cities City
            {
            get
                {
                return (Cities)GetValueForObjectProperty("City");
                }
            set
                {
                SetValueForObjectProperty("City", value);
                }
            }

        /// <summary>Країна</summary>
        [DataField(Description = "Країна", StorageType = StorageTypes.Local)]
        public string Country
            {
            get { return z_Country; }
            set
                {
                if ( z_Country == value )
                    {
                    return;
                    }
                z_Country = value;
                NotifyPropertyChanged("Country");
                }
            }
        private string z_Country = string.Empty;

        /// <summary>Телефон</summary>
        [DataField(Description = "Телефон", ShowInList = true)]
        public string Phone
            {
            get
                {
                return z_Phone;
                }
            set
                {
                if (z_Phone == value)
                    {
                    return;
                    }

                z_Phone = value;
                NotifyPropertyChanged("Phone");
                }
            }
        private string z_Phone = string.Empty;

        /// <summary>Факс</summary>
        [DataField(Description = "Факс")]
        public string Fax
            {
            get
                {
                return z_Fax;
                }
            set
                {
                if (z_Fax == value)
                    {
                    return;
                    }

                z_Fax = value;
                NotifyPropertyChanged("Fax");
                }
            }
        private string z_Fax = string.Empty;

        /// <summary>/// <summary></summary></summary>
        [DataField(Description = "Вулиця", Size = 20)]
        public string Street
            {
            get
                {
                return z_Street;
                }
            set
                {
                if (z_Street == value)
                    {
                    return;
                    }

                z_Street = value;
                NotifyPropertyChanged("Street");
                }
            }
        private string z_Street = string.Empty;

        /// <summary>Будинок</summary>
        [DataField(Description = "Будинок", Size = 20)]
        public string House
            {
            get
                {
                return z_House;
                }
            set
                {
                if (z_House == value)
                    {
                    return;
                    }

                z_House = value;
                NotifyPropertyChanged("House");
                }
            }
        private string z_House = string.Empty;

        /// <summary>Офіс</summary>
        [DataField(Description = "Офіс", Size = 10)]
        public string Office
            {
            get
                {
                return z_Office;
                }
            set
                {
                if (z_Office == value)
                    {
                    return;
                    }

                z_Office = value;
                NotifyPropertyChanged("Office");
                }
            }
        private string z_Office = string.Empty;

        /// <summary>Сайт</summary>
        [DataField(Description = "Сайт", ShowInList = true)]
        public string WebSite
            {
            get
                {
                return z_WebSite;
                }
            set
                {
                if (z_WebSite == value)
                    {
                    return;
                    }

                z_WebSite = value;
                NotifyPropertyChanged("WebSite");
                }
            }
        private string z_WebSite = string.Empty;
        #endregion

        #region CatalogTable
        protected override void InitItemBeforeShowing()
            {
            base.InitItemBeforeShowing();
            fillCityData();

            ValueOfObjectPropertyChanged += Contractors_ValueOfObjectPropertyChanged;
            }

        void Contractors_ValueOfObjectPropertyChanged(string propertyName)
            {
            switch (propertyName)
                {
                case "City":
                    fillCityData();
                    break;
                }
            }
        #endregion

        #region Filling
        private void fillCityData()
            {
            Country = City.Country.Description;
            }
        #endregion
        }
    }