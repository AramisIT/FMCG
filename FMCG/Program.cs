using Aramis.Platform;
using AramisDesktopUserInterface;
using System;

namespace AtosFMCG
    {
    public static class Program
        {
        /// <summary>Основна точка входу</summary>
        [STAThread]
        public static void Main(string[] args)
            {
            SystemAramis.SystemStart(args, new DesktopUserInterfaceEngine(typeof(AramisMainWindow)));
            }
        }
    }
