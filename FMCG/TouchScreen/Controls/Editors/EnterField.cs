using System;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;
using FMCG.TouchScreen.Controls.Editors;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Поле для вводу</summary>
    public partial class EnterField : UserControl, IVerticalScroll
        {
        #region Properties
        /// <summary>Введене значення</summary>
        public new string Text
            {
            get { return inputField.Text; }
            set { inputField.Text = value; }
            }
        /// <summary>Заголовок/назва/інформація</summary>
        public string Topic { set { topic.Text = value; } }
        #endregion

        /// <summary>Поле для вводу</summary>
        /// <param name="fieldName">Заголовок/назва/інформація</param>
        /// <param name="value">Введене значення</param>
        public EnterField(string fieldName, string value)
            {
            InitializeComponent();
            Topic = fieldName;
            Text = value;
            oldValue.Text = string.Concat("Початкове значення: ", value);
            }

        private void EnterField_Load(object sender, EventArgs e)
            {
            inputField.Focus();
            inputField.SelectAll();
            }

        private void inputField_TextChanged(object sender, EventArgs e)
            {
            OnFieldValueIsChanged(new ValueIsChangedArgs<string>(inputField.Text));
            }

        /// <summary>Сфокусуватись на полі вводу</summary>
        public void FocusField()
            {
            inputField.Focus();
            inputField.SelectAll();
            }

        #region FieldValueIsChanged
        /// <summary>Значення поля було оновлено</summary>
        public event EventHandler<ValueIsChangedArgs<string>> FieldValueIsChanged;

        private void OnFieldValueIsChanged(ValueIsChangedArgs<string> e)
            {
            EventHandler<ValueIsChangedArgs<string>> handler = FieldValueIsChanged;

            if (handler != null)
                {
                handler(this, e);
                }
            }
        #endregion

        public event Action ScrollUp;

        public event Action ScrollDown;

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

        private void EnterField_Paint(object sender, PaintEventArgs e)
            {
            this.Paint -= EnterField_Paint;
            scrollUp.Visible = ScrollUp != null;
            scrollDown.Visible = ScrollUp != null;
            }
        }
    }
