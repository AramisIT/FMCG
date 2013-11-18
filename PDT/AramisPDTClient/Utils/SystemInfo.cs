using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace AramisPDTClient
    {
    class SystemInfo
        {
        private static bool releaseModeChecked;
        private static bool releaseMode;
       
        public static bool ReleaseMode
            {
            get
                {
                if (!releaseModeChecked)
                    {
                    checkIsReleaseMode();
                    }
                return releaseMode;
                }
            }

        private static void checkIsReleaseMode()
            {
            releaseMode = false;
#if !DEBUG
            releaseMode = true;
#endif
            }
        }
    }
