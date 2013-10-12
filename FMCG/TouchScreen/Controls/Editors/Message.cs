using System;
using System.Windows.Forms;
using FMCG.TouchScreen.Controls.Editors;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Повідомлення</summary>
    public partial class Message : UserControl, IVerticalScroll
        {
        /// <summary>Повідомлення</summary>
        /// <param name="fieldName">Заголовок/назва/інформація</param>
        public Message(string fieldName)
            {
            InitializeComponent();
            topic.Text = fieldName;
            }

        /// <summary>Повідомлення</summary>
        /// <param name="fieldName">Заголовок/назва/інформація</param>
        /// <param name="message"> </param>
        public Message(string fieldName, string message)
            {
            InitializeComponent();
            topic.Text = fieldName;
            detailInfo.Text = message;
            }

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

        public event System.Action ScrollUp;

        public event System.Action ScrollDown;

        private void Message_Paint(object sender, PaintEventArgs e)
            {
            this.Paint -= Message_Paint;
            scrollUp.Visible = ScrollUp != null;
            scrollDown.Visible = ScrollUp != null;
            }
        }
    }
