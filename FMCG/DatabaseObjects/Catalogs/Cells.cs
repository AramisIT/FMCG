using System;
using System.Collections.Generic;
using Aramis;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.DatabaseConnector;
using Aramis.Enums;
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

        [DataField(Description = "Этаж", ShowInList = true)]
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

        [DataField(Description = "Ярус", ShowInList = true)]
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

        [DataField(Description = "Позиция", ShowInList = true)]
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


        }
    }