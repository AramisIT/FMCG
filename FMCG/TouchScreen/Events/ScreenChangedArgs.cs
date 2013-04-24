using System;
using AtosFMCG.TouchScreen.Screens.Base;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>Скрін змінено</summary>
    public class ScreenChangedArgs : EventArgs
        {
        /// <summary>Наступний скрін</summary>
        public BaseScreen NextScreen { get; set; }
        /// <summary>Наступний чи попередній</summary>
        public bool IsNext { get; set; }

        /// <summary>Подія - "Змінено скрін"</summary>
        /// <param name="nextScreen">Наступний скрін</param>
        /// <param name="isNext">Наступний чи попередній</param>
        public ScreenChangedArgs(BaseScreen nextScreen, bool isNext)
            {
            NextScreen = nextScreen;
            IsNext = isNext;
            }
        }
    }