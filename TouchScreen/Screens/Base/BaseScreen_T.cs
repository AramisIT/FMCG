using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens.Base
{
    /// <summary>Базовий скрін</summary>
    public abstract class BaseScreen<T> : BaseScreen where T : BaseData
    {
        /// <summary>Данні</summary>
        public T ScreenData { get { return (T)Data; } set { Data = value; } }
        /// <summary>Данні</summary>
        public BaseData Data { get; private set; }

        /// <summary>Базовий скрін</summary>
        protected BaseScreen()
        {
        }

        /// <summary>Базовий скрін</summary>
        /// <param name="data">Данні</param>
        protected BaseScreen(T data)
        {
            ScreenData = data;
        }

        #region Virtual&Abstract
        /// <summary>Ініціалізація</summary>
        protected abstract void initScreen();
        #endregion
    }
}
