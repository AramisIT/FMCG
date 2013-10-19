using System;
using TouchScreen.Models.Data;

namespace AtosFMCG.TouchScreen.Screens.Base
{
    /// <summary>Стартовий екран</summary>
    public partial class StartScreen : BaseScreen
    {
        /// <summary>Стартовий екран</summary>
        public StartScreen()
        {
            InitializeComponent();
        }

        #region Процеси
        private void AcceptancePlanProcess_Click(object sender, EventArgs e)
        {
            GoToScreen(new SelectAcceptancePlan(new AcceptancePlanData()));
        }
        #endregion
    }
}
