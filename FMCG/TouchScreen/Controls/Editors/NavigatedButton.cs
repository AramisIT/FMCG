using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Enums;
using AtosFMCG.TouchScreen.HelpersClasses;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Навігаційна кнопка</summary>
    public partial class NavigatedButton : Button
        {
        #region Properties
        public const string SPACES_FOR_ICOx32 = "          ";
        /// <summary>Тип шрифта</summary>
        public TypesOfFont TypeOfFont
            {
            get { return z_TypeOfFont; }
            set
                {
                z_TypeOfFont = value;
                Font = HelperClass.GetFontForExtControls(z_TypeOfFont);
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
                    Font = HelperClass.GetFontForExtControls(z_TypeOfFont);
                    }
            };
            }

        public void SetColor() { }

        #region Click
        /// <summary>Одиноки клік (Подія відмінна від Click лиш тим, що забороняє мимовільний дабл-клік)</summary>
        public event EventHandler SingleClick
            {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
                {
                Click += NavigatedButton_Click;
                z_SingleClick = (EventHandler)Delegate.Combine(z_SingleClick, value);
                }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
                {
                Click -= NavigatedButton_Click;
                z_SingleClick = (EventHandler)Delegate.Remove(z_SingleClick, value);
                }
            }
        private EventHandler z_SingleClick;
        /// <summary>К-сть мілісекунд між кліками при яких клік вважається за мимовільний дабл-клік</summary>
        private const int MIN_DELAY_BETWEEN_SINGLECLICK = 155;
        /// <summary>Час останьої натиснутої кнопки</summary>
        private KeyValuePair<object, DateTime> timeOfLastClickedButton;

        /// <summary>Чи дозволений повторний клік кнопки</summary>
        /// <param name="button">Кнопка</param>
        private bool allowReclick(object button)
            {
            //При роботі з тач-скріном можливі мимовільні подвійні натисненя на кнопку
            //Потрібно відслідковувати ці моменти і ігнорувати повторний клік
            bool allow;

            if (timeOfLastClickedButton.Key == null || !timeOfLastClickedButton.Key.Equals(button))
                {
                allow = true;
                }
            else
                {
                TimeSpan delay = DateTime.Now - timeOfLastClickedButton.Value;
                allow = delay.TotalMilliseconds > MIN_DELAY_BETWEEN_SINGLECLICK;
                }

            timeOfLastClickedButton = new KeyValuePair<object, DateTime>(button, DateTime.Now);
            return allow;
            }

        void NavigatedButton_Click(object sender, EventArgs e)
            {
            EventHandler handler = z_SingleClick;

            if (handler != null && allowReclick(sender))
                {
                handler(sender, e);
                }
            }
        #endregion
        }
    }