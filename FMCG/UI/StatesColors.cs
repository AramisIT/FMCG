using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AtosFMCG.Enums;

namespace FMCG.UI
    {
    class StatesColors
        {
        public static readonly Color Planed = Color.White;

        public static readonly Color Processing = "#fdf580".ToSystemDrawingColor();

        public static readonly Color Performed = "#9bffa0".ToSystemDrawingColor();

        public static readonly Color Canceled = "#cbcbcb".ToSystemDrawingColor();

        public static readonly Color Completed = "#9aaafd".ToSystemDrawingColor();
        }
    }
