using System;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;

namespace AtosFMCG.TouchScreen.Screens.Base
{
    /// <summary>Базовий скрін</summary>
    public partial class BaseScreen: UserControl 
    { 
        #region Constructors
        /// <summary>Базовий скрін</summary>
        public BaseScreen()
        {
            InitializeComponent();
        }
        #endregion

        #region Virtual
        /// <summary>Дії перед зміною екрану</summary>
        /// <returns>Чи дозволено перхід до наступного екрану</returns>
        protected virtual bool BeforeScreenChanged(bool isNext)
        {
            return DataIsValid();
        }

        /// <summary>Перевірка вірності даних</summary>
        protected virtual bool DataIsValid()
        {
            return true;
        }
        #endregion

        #region Navigation
        /// <summary>Екран змінено</summary>
        public event EventHandler<ScreenChangedArgs> ScreenChanged;

        /// <summary>Перейти на наступний екран</summary>
        /// <param name="newScreen">Назва наступного екрану</param>
        /// <param name="isNext"></param>
        protected void GoToScreen(BaseScreen newScreen, bool isNext = true)
        {
            if (BeforeScreenChanged(isNext) || isNext)
            {
                OnScreenChanged(new ScreenChangedArgs(newScreen, true));
            }
        }

        /// <summary>Екран змінено</summary>
        protected virtual void OnScreenChanged(ScreenChangedArgs e)
        {
            EventHandler<ScreenChangedArgs> handler = ScreenChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        } 
        #endregion
    }
}
