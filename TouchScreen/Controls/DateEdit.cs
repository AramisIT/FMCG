using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;

namespace AtosFMCG.TouchScreen.Controls
    {
    public partial class DateEdit : UserControl
        {
        #region Variables
        private readonly Color deselectedColor;
        private readonly Color selectedColor;
        
        public DateTime CurrentDate
            {
            get { return z_currentDate; }
            set
                {
                if (z_currentDate != value)
                    {
                    if(z_currentDate != DateTime.MinValue)
                        {
                        OnDateIsChanged(new DateIsChangedArgs(value));
                        }

                    z_currentDate = value;
                    newValue.Text = string.Concat("Введено дату: ", z_currentDate.ToShortDateString());
                    }
                }
            }
        private DateTime z_currentDate; 
        #endregion

        public DateEdit(DateTime date)
            {
            InitializeComponent();

            deselectedColor = Color.Transparent;
            selectedColor = Color.Bisque;

            setDate(date);
            }
        #region Find

        private Button findControlByText(string text)
            {
            return (from Control control in Controls select control as Button).FirstOrDefault(btn => btn != null && btn.Text == text);
            }

        private Button findControlByTag(string text)
            {
            return (from Control control in Controls select control as Button).FirstOrDefault(btn => btn != null && btn.Tag!=null && btn.Tag.ToString() == text);
            } 
        #endregion

        #region Select
        private void selectControl(Button button)
            {
            if (button!=null)
                {
                button.BackColor = selectedColor;
                }
            }

        private void deselectControl(Button button)
            {
            if (button != null)
                {
                button.BackColor = deselectedColor;
                }
            } 
        #endregion

        #region Set
        private void setDate(DateTime date)
            {
            CurrentDate = date;
            setYear(CurrentDate.Year);
            setMonth(CurrentDate.Month);
            setDay(CurrentDate.Day);
            }

        private void setYear(int year)
            {
            Button yearControl = findControlByText(year.ToString());

            if (yearControl == null)
                {
                year1.Text = (year - 1).ToString();
                year2.Text = year.ToString();
                year3.Text = (year + 1).ToString();

                yearButton_Click(year2, new EventArgs());
                }
            else
                {
                selectControl(yearControl);
                }
            }

        private void setMonth(int month)
            {
            Button monthControl = findControlByTag(month.ToString());

            if(monthControl!=null)
                {
                selectControl(monthControl);
                buildDayBtn();
                }
            }

        private void setDay(int day)
            {
            Button dayControl = findControlByText(day.ToString());

            if (dayControl != null)
                {
                selectControl(dayControl);
                }
            }

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
            Button selectedDay = (Button) sender;

            Button oldDayControl = findControlByText(CurrentDate.Day.ToString());
            deselectControl(oldDayControl);

            Button newDayControl = findControlByText(selectedDay.Text);
            selectControl(newDayControl);

            int day = Convert.ToInt32(selectedDay.Text);
            CurrentDate = new DateTime(CurrentDate.Year, CurrentDate.Month, day);
            }

        private void monthButton_Click(object sender, EventArgs e)
            {
            //month
            Button selectedMonth = (Button) sender;

            Button oldMonthControl = findControlByTag(CurrentDate.Month.ToString());
            deselectControl(oldMonthControl);

            Button newMonthControl = findControlByTag(selectedMonth.Tag.ToString());
            selectControl(newMonthControl);

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

            Button oldYearControl = findControlByText(CurrentDate.Year.ToString());
            deselectControl(oldYearControl);

            Button newYearControl = findControlByText(selectedYear.Text);
            selectControl(newYearControl);

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

        private void prevYears_Click(object sender, EventArgs e)
            {
            setYear(CurrentDate.Year - 3);
            }

        private void nextYears_Click(object sender, EventArgs e)
            {
            setYear(CurrentDate.Year + 3);
            }
        #endregion

        #region DateIsChanged
        public event EventHandler<DateIsChangedArgs> DateIsChanged;

        private void OnDateIsChanged(DateIsChangedArgs e)
            {
            EventHandler<DateIsChangedArgs> handler = DateIsChanged;

            if(handler!=null)
                {
                handler(this, e);
                }
            }
        #endregion
        }
    }
