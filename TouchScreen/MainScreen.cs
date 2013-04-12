using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;
using AtosFMCG.TouchScreen.Screens.Base;

namespace AtosFMCG.TouchScreen
    {
    /// <summary>Основний так-скрін</summary>
    public partial class MainScreen : Form
        {
        /// <summary>Основний так-скрін</summary>
        public MainScreen()
            {
            InitializeComponent();
            loadNextScreen(new StartScreen());
            }

        #region ChangeScreen
        /// <summary>Скрін змінено</summary>
        void screenChanged(object sender, ScreenChangedArgs e)
            {
            //Спочатку додаєм новий скрін і аж потім прибираємо старий!!!
            //Для того, щоб не було "моргання"/"блимання" та інших ефектів
            loadNextScreen(e.NextScreen);
            clearCurrentScreen();
            }

        /// <summary>Очистити поточний стрін</summary>
        private void clearCurrentScreen()
            {
            Controls.RemoveAt(0);
            }

        /// <summary>Встановити новий скрін</summary>
        /// <param name="name">Назва скріну</param>
        private void loadNextScreen(BaseScreen name)
            {
            Controls.Add(name);
            name.ScreenChanged += screenChanged;
            Text = name.GetType().Name;
            }
        #endregion
        }
    }
