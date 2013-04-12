using System.Drawing;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Enums;
using AtosFMCG.TouchScreen.HelpersClasses;
using AtosFMCG.TouchScreen.Interfaces;

namespace AtosFMCG.TouchScreen.Controls
{
    /// <summary>Навігаційна кнопка</summary>
    public partial class NavigatedButton : Button, IExtControl
    {
        #region Properties
        public const string SPACES_FOR_x32 = "          ";
        /// <summary>Тип шрифта</summary>
        public TypesOfFont TypeOfFont { get { return z_TypeOfFont; }
        set
            {
            if (z_TypeOfFont != value)
                {
                z_TypeOfFont = value;
                Font = HelperClass.GetFontForExtControls(z_TypeOfFont);
                }
            }
        }
        private TypesOfFont z_TypeOfFont;
        /// <summary>Контрол завантажився</summary>
        public bool IsLoaded { get; private set; }
        /// <summary>Иконка</summary>
        public Image Ico
        {
            get { return Image; }
            set
            {
                Image = value;
                if (Image != null)
                {
                    TextAlign = ContentAlignment.MiddleRight;
                    ImageAlign = ContentAlignment.MiddleLeft;
                }
            }
        }
        /// <summary>Колір фону</summary>
        public Color Background
        {
            get { return z_Background; }
            set
            {
                BackColor = value;
                z_Background = value;
            }
        }
        public Color z_Background;
        /// <summary>Чи доступна кнопка</summary>
        public bool IsEnabled
        {
            get { return Enabled; }
            set
            {
                Enabled = value;
                BackColor = Enabled ? z_Background : SystemColors.Control;
            }
        }
        #endregion

        /// <summary>Навігаційна кнопка</summary>
        public NavigatedButton()
        {
            InitializeComponent();
            SizeChanged += (sender, e) =>
            {
                if (!IsLoaded)
                {
                    IsLoaded = true;
                    Font = HelperClass.GetFontForExtControls();
                }
            };
        }

        public void SetColor() { }
    }
}