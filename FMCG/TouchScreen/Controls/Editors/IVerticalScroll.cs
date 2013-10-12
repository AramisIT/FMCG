using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMCG.TouchScreen.Controls.Editors
    {
    interface IVerticalScroll
        {
        event Action ScrollUp;
        event Action ScrollDown;
        }
    }
