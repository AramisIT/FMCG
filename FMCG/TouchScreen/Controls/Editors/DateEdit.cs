using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Контрол вибору дати</summary>
    public partial class DateEdit : UserControl
        {
        #region Variables
        /// <summary>Шаблон для років</summary>
        private const string YEAR_PATTERN = "{0:0000}";
        /// <summary>Колір не активної кнопки</summary>
        private readonly Color deselectedColor;
        /// <summary>Колір активної кнопки</summary>
        private readonly Color selectedColor;
        /// <summary>Поточна введена дата</summary>
        public DateTime CurrentDate
            {
            get { return z_currentDate; }
            set
                {
                if (z_currentDate != value || z_currentDate == DateTime.MinValue)
                    {
                    //if (z_currentDate != DateTime.MinValue)
                        {
                        OnDateIsChanged(new ValueIsChangedArgs<DateTime>(value));
                        }

                    z_currentDate = value;
                    newValue.Text = string.Concat("Введено дату: ", z_currentDate.ToShortDateString());
                    }
                }
            }
        private DateTime z_currentDate;
        #endregion

        /// <summary>Контрол вибору дати</summary>
        /// <param name="date">Початкова дата</param>
        public DateEdit(DateTime date)
            {
            InitializeComponent();

            deselectedColor = Color.Transparent;
            selectedColor = Color.Bisque;

            setDate(date);
            }

        public void FocusField()
            {

            }

        /// <summary>Очистка вказаної дати</summary>
        private void clearCurrentDate()
            {
            Button dayBtn = findControlByText(CurrentDate.Day.ToString());
            deselectButton(dayBtn);
            Button monthBtn = findControlByTag(CurrentDate.Month.ToString());
            deselectButton(monthBtn);
            Button yearBtn = findControlByText(string.Format(YEAR_PATTERN, CurrentDate.Year));
            deselectButton(yearBtn);
            }

        /// <summary>Введення сьогоднішньї дати</summary>
        private void today_Click(object sender, EventArgs e)
            {
            clearCurrentDate();
            setDate(DateTime.Now.Date);
            }

        #region Find
        /// <summary>Пошук кнопки за текстом</summary>
        /// <param name="text">Текст</param>
        /// <returns>Кнопка</returns>
        private Button findControlByText(string text)
            {
            return (from Control control in Controls select control as Button).FirstOrDefault(btn => btn != null && btn.Text == text);
            }

        /// <summary>Пошук кнопки за тегом</summary>
        /// <param name="text">Текст пошуку</param>
        /// <returns>Кнопка</returns>
        private Button findControlByTag(string text)
            {
            return (from Control control in Controls select control as Button).FirstOrDefault(btn => btn != null && btn.Tag != null && btn.Tag.ToString() == text);
            }
        #endregion

        #region Select
        /// <summary>Виділити кнопку</summary>
        /// <param name="button">Кнопка</param>
        private void selectButton(Button button)
            {
            if (button != null)
                {
                button.BackColor = selectedColor;
                }
            }

        /// <summary>Зняти виділення з кнопки</summary>
        /// <param name="button">Кнопка</param>
        private void deselectButton(Button button)
            {
            if (button != null)
                {
                button.BackColor = deselectedColor;
                }
            }
        #endregion

        #region Set
        /// <summary>Встановити дату</summary>
        /// <param name="date">Дата</param>
        private void setDate(DateTime date)
            {
            CurrentDate = date;
            setYear(CurrentDate.Year);
            setMonth(CurrentDate.Month);
            setDay(CurrentDate.Day);
            }

        private int getCheckedYear(int year)
            {
            return year < 1000 ? DateTime.Now.Year : year;
            }

        /// <summary>Встановити рік</summary>
        /// <param name="year">Рік</param>
        private void setYear(int year)
            {

            year = getCheckedYear(year);

            string yearString = string.Format(YEAR_PATTERN, year);
            Button yearControl = findControlByText(yearString);

            if (yearControl == null)
                {
                if (year <= DateTime.MinValue.Year)
                    {
                    year = DateTime.MinValue.Year + 1;
                    }
                if (year >= DateTime.MaxValue.Year)
                    {
                    year = DateTime.MaxValue.Year - 1;
                    }

                year1.Text = string.Format(YEAR_PATTERN, year - 1);
                year2.Text = string.Format(YEAR_PATTERN, year);
                year3.Text = string.Format(YEAR_PATTERN, year + 1);

                yearButton_Click(year2, new EventArgs());
                }
            else
                {
                selectButton(yearControl);
                }
            }

        /// <summary>Встановити місяць</summary>
        /// <param name="month">Місяць</param>
        private void setMonth(int month)
            {
            Button monthControl = findControlByTag(month.ToString());

            if (monthControl != null)
                {
                selectButton(monthControl);
                buildDayBtn();
                }
            }

        /// <summary>Встановити день</summary>
        /// <param name="day">День</param>
        private void setDay(int day)
            {
            Button dayControl = findControlByText(day.ToString());

            if (dayControl != null)
                {
                selectButton(dayControl);
                }
            }

        /// <summary>Побудувати кнопки днів (в залежності від обраних року і місяця)</summary>
        private void buildDayBtn()
            {
            int maxDay = CurrentDate.Date.EndOfMonth().Day;

            for (int i = 28; i <= 31; i++)
                {
                Control dayControl = findControlByText(i.ToString());
                dayControl.Visible = i <= maxDay;
                }
            }
        #endregion

        #region BtnPressed
        private void dayButton_Click(object sender, EventArgs e)
            {
            Button selectedDay = (Button)sender;

            Button oldDayControl = findControlByText(CurrentDate.Day.ToString());
            deselectButton(oldDayControl);

            Button newDayControl = findControlByText(selectedDay.Text);
            selectButton(newDayControl);

            int day = Convert.ToInt32(selectedDay.Text);
            CurrentDate = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
            }

        private void monthButton_Click(object sender, EventArgs e)
            {
            //month
            Button selectedMonth = (Button)sender;

            Button oldMonthControl = findControlByTag(CurrentDate.Month.ToString());
            deselectButton(oldMonthControl);

            Button newMonthControl = findControlByTag(selectedMonth.Tag.ToString());
            selectButton(newMonthControl);

            int month = Convert.ToInt32(selectedMonth.Tag);

            //day
            int maxDayInMonth = new DateTime(CurrentDate.Year, month, 1).EndOfMonth().Day;

            if (CurrentDate.Day > maxDayInMonth)
                {
                Button currentDay = findControlByText(maxDayInMonth.ToString());
                dayButton_Click(currentDay, new EventArgs());
                }

            //Date
            CurrentDate = new DateTime(CurrentDate.Year, month, CurrentDate.Day);
            buildDayBtn();
            }

        private void yearButton_Click(object sender, EventArgs e)
            {
            Button selectedYear = (Button)sender;

            string yearDes = string.Format(YEAR_PATTERN, getCheckedYear(CurrentDate.Year));
            Button oldYearControl = findControlByText(yearDes);
            deselectButton(oldYearControl);

            Button newYearControl = findControlByText(selectedYear.Text);
            selectButton(newYearControl);

            int year = Convert.ToInt32(selectedYear.Text);

            //day
            int maxDayInMonth = new DateTime(year, CurrentDate.Month, 1).EndOfMonth().Day;

            if (CurrentDate.Day > maxDayInMonth)
                {
                Button currentDay = findControlByText(maxDayInMonth.ToString());
                dayButton_Click(currentDay, new EventArgs());
                }

            CurrentDate = new DateTime(year, CurrentDate.Month, CurrentDate.Day);
            buildDayBtn();
            }

        /// <summary>Зменшити роки (змістити роки вліво)</summary>
        private void prevYears_Click(object sender, EventArgs e)
            {
            string yearDes = string.Format(YEAR_PATTERN, CurrentDate.Year);
            Button currYearBtn = findControlByText(yearDes);
            deselectButton(currYearBtn);
            setYear(CurrentDate.Year - 3);
            }

        /// <summary>Збільшити роки (змістити роки вправо)</summary>
        private void nextYears_Click(object sender, EventArgs e)
            {
            string yearDes = string.Format(YEAR_PATTERN, CurrentDate.Year);
            Button currYearBtn = findControlByText(yearDes);
            deselectButton(currYearBtn);
            setYear(CurrentDate.Year + 3);
            }
        #endregion

        #region DateIsChanged
        /// <summary>Дату змінено</summary>
        public event EventHandler<ValueIsChangedArgs<DateTime>> DateIsChanged;

        private void OnDateIsChanged(ValueIsChangedArgs<DateTime> e)
            {
            EventHandler<ValueIsChangedArgs<DateTime>> handler = DateIsChanged;

            if (handler != null)
                {
                handler(this, e);
                }
            }
        #endregion

        private void button43_Paint(object sender, PaintEventArgs e)
            {
            Button button = sender as Button;

            if (button != null)
                {
                string additionalText = button.Tag.ToString();

                e.Graphics.DrawString(
                    additionalText,
                    button.Font,
                    new SolidBrush(Color.Black),
                    (button.Width - 12 * additionalText.Length) / 2,
                    button.Height - 30);
                }
            }
        }
    }
