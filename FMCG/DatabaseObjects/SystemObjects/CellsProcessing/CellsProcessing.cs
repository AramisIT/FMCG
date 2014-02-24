using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aramis.Attributes;
using Aramis.Core;
using Aramis.Enums;
using Catalogs;

namespace SystemObjects
    {
    public class CellsProcessing : SystemObject
        {

        [DataField(Description = "Начальный этаж", ShowInList = true)]
        public int StartFloor
            {
            get
                {
                return z_StartFloor;
                }
            set
                {
                if (z_StartFloor == value)
                    {
                    return;
                    }
                z_StartFloor = value;
                NotifyPropertyChanged("Floor");
                }
            }
        private int z_StartFloor;

        [DataField(Description = "Конечный этаж", ShowInList = true)]
        public int FinishFloor
            {
            get
                {
                return z_FinishFloor;
                }
            set
                {
                if (z_FinishFloor == value)
                    {
                    return;
                    }
                z_FinishFloor = value;
                NotifyPropertyChanged("FinishFloor");
                }
            }
        private int z_FinishFloor;

        [DataField(Description = "Начальный ряд", ShowInList = true)]
        public int StartRow
            {
            get
                {
                return z_StartRow;
                }
            set
                {
                if (z_StartRow == value)
                    {
                    return;
                    }
                z_StartRow = value;
                NotifyPropertyChanged("StartRow");
                }
            }
        private int z_StartRow;

        [DataField(Description = "Конечный ряд", ShowInList = true)]
        public int FinishRow
            {
            get
                {
                return z_FinishRow;
                }
            set
                {
                if (z_FinishRow == value)
                    {
                    return;
                    }
                z_FinishRow = value;
                NotifyPropertyChanged("FinishRow");
                }
            }
        private int z_FinishRow;

        [DataField(Description = "Начальный стеллаж", ShowInList = true)]
        public int StartRack
            {
            get
                {
                return z_StartRack;
                }
            set
                {
                if (z_StartRack == value)
                    {
                    return;
                    }
                z_StartRack = value;
                NotifyPropertyChanged("StartRack");
                }
            }
        private int z_StartRack;

        [DataField(Description = "Конечный стеллаж", ShowInList = true)]
        public int FinishRack
            {
            get
                {
                return z_FinishRack;
                }
            set
                {
                if (z_FinishRack == value)
                    {
                    return;
                    }
                z_FinishRack = value;
                NotifyPropertyChanged("FinishRack");
                }
            }
        private int z_FinishRack;

        [DataField(Description = "Начальный ярус", ShowInList = true)]
        public int StartStorey
            {
            get
                {
                return z_StartStorey;
                }
            set
                {
                if (z_StartStorey == value)
                    {
                    return;
                    }
                z_StartStorey = value;
                NotifyPropertyChanged("StartStorey");
                }
            }
        private int z_StartStorey;

        [DataField(Description = "Конечный ярус", ShowInList = true)]
        public int FinishStorey
            {
            get
                {
                return z_FinishStorey;
                }
            set
                {
                if (z_FinishStorey == value)
                    {
                    return;
                    }
                z_FinishStorey = value;
                NotifyPropertyChanged("FinishStorey");
                }
            }
        private int z_FinishStorey;

        [DataField(Description = "Начальный позиция", ShowInList = true)]
        public int StartPosition
            {
            get
                {
                return z_StartPosition;
                }
            set
                {
                if (z_StartPosition == value)
                    {
                    return;
                    }
                z_StartPosition = value;
                NotifyPropertyChanged("StartPosition");
                }
            }
        private int z_StartPosition;

        [DataField(Description = "Конечная позиция", ShowInList = true)]
        public int FinishPosition
            {
            get
                {
                return z_FinishPosition;
                }
            set
                {
                if (z_FinishPosition == value)
                    {
                    return;
                    }
                z_FinishPosition = value;
                NotifyPropertyChanged("FinishPosition");
                }
            }
        private int z_FinishPosition;

        [DataField(Description = "Префикс наименования", ShowInList = true, Size = 3)]
        public string Prefix
            {
            get
                {
                return z_Prefix;
                }
            set
                {
                if (z_Prefix == value)
                    {
                    return;
                    }
                z_Prefix = value;
                NotifyPropertyChanged("Prefix");
                }
            }
        private string z_Prefix = string.Empty;

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

        [DataField(Description = "Родитель", ShowInList = true, CatalogValue = CatalogValueKinds.OnlyGroups)]
        public Cells ParentOfCell
            {
            get
                {
                return (Cells)GetValueForObjectProperty("ParentOfCell");
                }
            set
                {
                SetValueForObjectProperty("ParentOfCell", value);
                }
            }
        }
    }
