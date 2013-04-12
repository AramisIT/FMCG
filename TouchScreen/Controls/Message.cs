using System.Windows.Forms;

namespace AtosFMCG.TouchScreen.Controls
    {
    /// <summary>Поле для вводу</summary>
    public partial class Message : UserControl
        {
        /// <summary>Поле для вводу</summary>
        /// <param name="fieldName">Заголовок/назва/інформація</param>
        public Message(string fieldName)
            {
            InitializeComponent();
            topic.Text = fieldName;
            }
        }
    }
