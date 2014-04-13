using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
using AtosFMCG.TouchScreen.PalletSticker;
using Catalogs;

namespace Catalogs
    {
    /// <summary>Комірки зберігання</summary>
    [Catalog(Description = "Комірки зберігання", GUID = "A4BDEF67-D02D-469E-928E-F5BE8FFCA0A1", HierarchicType = HierarchicTypes.GroupsAndElements, ShowCodeFieldInForm = false)]
    public class Cells : CatalogTable
        {
        [DataField(Description = "Тип комірки", ShowInList = true)]
        public TypesOfCell TypeOfCell
            {
            get
                {
                return (TypesOfCell)GetValueForObjectProperty("TypeOfCell");
                }
            set
                {
                SetValueForObjectProperty("TypeOfCell", value);
                }
            }

        [DataField(Description = "Этаж", ShowInList = false, ShowInForm = false)]
        public int Floor
            {
            get
                {
                return z_Floor;
                }
            set
                {
                if (z_Floor == value)
                    {
                    return;
                    }
                z_Floor = value;
                NotifyPropertyChanged("Floor");
                }
            }
        private int z_Floor;

        [DataField(Description = "Ряд", ShowInList = true)]
        public int Row
            {
            get
                {
                return z_Row;
                }
            set
                {
                if (z_Row == value)
                    {
                    return;
                    }
                z_Row = value;
                NotifyPropertyChanged("Row");
                }
            }
        private int z_Row;

        [DataField(Description = "Стеллаж", ShowInList = true)]
        public int Rack
            {
            get
                {
                return z_Rack;
                }
            set
                {
                if (z_Rack == value)
                    {
                    return;
                    }
                z_Rack = value;
                NotifyPropertyChanged("Rack");
                }
            }
        private int z_Rack;

        [DataField(Description = "Ярус", ShowInList = false, ShowInForm = false)]
        public int Storey
            {
            get
                {
                return z_Storey;
                }
            set
                {
                if (z_Storey == value)
                    {
                    return;
                    }
                z_Storey = value;
                NotifyPropertyChanged("Storey");
                }
            }
        private int z_Storey;

        [DataField(Description = "Позиция", ShowInList = false, ShowInForm = false)]
        public int Position
            {
            get
                {
                return z_Position;
                }
            set
                {
                if (z_Position == value)
                    {
                    return;
                    }
                z_Position = value;
                NotifyPropertyChanged("Position");
                }
            }
        private int z_Position;

        [DataField(Description = "Есть доступ ко всем паллетам", ShowInList = true, ShowInForm = true)]
        public bool AccessToAllWares
            {
            get
                {
                return z_AccessToAllWares;
                }
            set
                {
                if (z_AccessToAllWares == value)
                    {
                    return;
                    }
                z_AccessToAllWares = value;
                NotifyPropertyChanged("AccessToAllWares");
                }
            }
        private bool z_AccessToAllWares;

        public string Barcode
            {
            get { return string.Format("C.{0};{1}", Id, Description); }
            }

        public override Dictionary<string, Action<DatabaseObject>> GetActions()
            {
            var result = new Dictionary<string, Action<DatabaseObject>>();

            result.Add("Друкувати", item => new CellsPrintingHelper(new List<Cells>() { item as Cells }).Print());

            result.Add("Оновити найменування", item => (item as Cells).UpdateDescription());

            return result;
            }

        public void UpdateDescription(string prefix = "")
            {
            if (string.IsNullOrEmpty(prefix) && Description.Trim().Length > 0)
                {
                prefix = Description.Substring(0, 1);
                }
            Description = string.Format("{0} {1:D2}-{2:D2}", prefix.Trim(), Row, Rack);
            }
        }
    }