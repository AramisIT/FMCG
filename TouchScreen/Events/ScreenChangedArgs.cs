using System;
using AtosFMCG.TouchScreen.Screens.Base;

namespace AtosFMCG.TouchScreen.Events
    {
    /// <summary>���� ������</summary>
    public class ScreenChangedArgs : EventArgs
        {
        /// <summary>��������� ����</summary>
        public BaseScreen NextScreen { get; set; }
        /// <summary>��������� �� ���������</summary>
        public bool IsNext { get; set; }

        /// <summary>���� - "������ ����"</summary>
        /// <param name="nextScreen">��������� ����</param>
        /// <param name="isNext">��������� �� ���������</param>
        public ScreenChangedArgs(BaseScreen nextScreen, bool isNext)
            {
            NextScreen = nextScreen;
            IsNext = isNext;
            }
        }
    }