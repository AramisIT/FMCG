using System.Windows.Forms;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Повідомлення</summary>
    public partial class Message : UserControl
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
        }
    }
