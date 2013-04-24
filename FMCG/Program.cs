using System;
using Aramis.Platform;

namespace AtosFMCG
    {
    public static class Program
        {
        /// <summary>Основна точка входу</summary>
        [STAThread]
        public static void Main(string[] args)
            {
            SystemAramis.SystemStart( args, typeof( AramisMainWindow ) );
            }
        }
    }
