using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;
using FMCG.TouchScreen.Controls.Editors;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Едітор цілого числа</summary>
    public partial class NumberEdit : UserControl, IVerticalScroll
        {
        /// <summary>Назад</summary>
        private readonly GoDelegate goBack;
        private readonly FinishFieldEditiong finishFieldEditiong;
        private const int DELAY_BETWEEN_NEXTCLICK = 155;

        /// <summary>Едітор цілого числа</summary>
        /// <param name="value">Початкове значення</param>
        /// <param name="back">Дія при повернені назад</param>
        /// <param name="finish">Дія після завершеня</param>
        public NumberEdit(int value, GoDelegate back, FinishFieldEditiong finish)
            {
            InitializeComponent();
            setInitValue(value);
            goBack = back;
            finishFieldEditiong = finish;
            }

        /// <summary>Встановленя початкового значення</summary>
        /// <param name="value">Початкове значення</param>
        private void setInitValue(int value)
            {
            oldValue.Text = string.Concat("Початкове значення: ", value);
            inputField.Text = value.ToString();
            }

        public void FocusField()
            {

            }

        #region "Калькулятор"
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
                allow = delay.TotalMilliseconds > DELAY_BETWEEN_NEXTCLICK;

                if (!allow)
                    {   
                    Console.Write(string.Empty);
                    }
                }

            timeOfLastClickedButton = new KeyValuePair<object, DateTime>(button, DateTime.Now);
            return allow;
            }

        private void numberButton_Click(object sender, EventArgs e)
            {
            if (allowReclick(sender))
                {
                Button button = (Button) sender;

                if (inputField.Text == 0.ToString())
                    {
                    inputField.Text = button.Text;
                    }
                else
                    {
                    inputField.Text += button.Text;
                    }
                }
            }

        private void clear_Click(object sender, EventArgs e)
            {
            inputField.Text = 0.ToString();
            }

        private void backspace_Click(object sender, EventArgs e)
            {
            if(inputField.Text.Length<2)
                {
                inputField.Text = 0.ToString();
                }
            else
                {
                inputField.Text = inputField.Text.Substring(0, inputField.Text.Length - 1);
                }
            }
        #endregion

        #region NumberIsUpd
        private void inputField_TextChanged(object sender, EventArgs e)
            {
            int value;
            int.TryParse(inputField.Text, out value);
            OnValueIsChanged(new ValueIsChangedArgs<int>(value));
            }
        #endregion

        #region DateIsChanged
        /// <summary>Значення було змінено</summary>
        public event EventHandler<ValueIsChangedArgs<int>> ValueIsChanged;

        private void OnValueIsChanged(ValueIsChangedArgs<int> e)
            {
            EventHandler<ValueIsChangedArgs<int>> handler = ValueIsChanged;

            if (handler != null)
                {
                handler(this, e);
                }
            }
        #endregion

        #region Navigation
        /// <summary>Назад</summary>
        private void goPrev_Click(object sender, EventArgs e)
            {
            goBack();
            }

        /// <summary>Далі</summary>
        private void goNext_Click(object sender, EventArgs e)
            {
            finishFieldEditiong(inputField.Text);
            } 
        #endregion

        private void scrollUp_Click(object sender, EventArgs e)
            {
            if (ScrollUp != null)
                {
                ScrollUp();
                }
            }

        private void scrollDown_Click(object sender, EventArgs e)
            {
            if (ScrollDown != null)
                {
                ScrollDown();
                }
            }

        public event Action ScrollUp;

        public event Action ScrollDown;

        private void NumberEdit_Paint(object sender, PaintEventArgs e)
            {
            this.Paint -= NumberEdit_Paint;
            scrollUp.Visible = ScrollUp != null;
            scrollDown.Visible = ScrollUp != null;
            }
        }
    }
