using System;
using System.Windows.Forms;
using AtosFMCG.TouchScreen.Events;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Поле для вводу</summary>
    public partial class EnterField : UserControl
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
            OnFieldValueIsChanged(new FieldValueIsChangedArgs(inputField.Text));
            }

        #region FieldValueIsChanged
        /// <summary></summary>
        public event EventHandler<FieldValueIsChangedArgs> FieldValueIsChanged; 

        private void OnFieldValueIsChanged(FieldValueIsChangedArgs e)
            {
            EventHandler<FieldValueIsChangedArgs> handler = FieldValueIsChanged;

            if(handler!=null)
                {
                handler(this, e);
                }
            }
        #endregion
        }
    }
