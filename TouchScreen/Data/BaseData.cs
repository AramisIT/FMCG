using System.ComponentModel;

namespace TouchScreen.Models.Data
    {
    /// <summary>Набір даних</summary>
    public abstract class BaseData : INotifyPropertyChanged
        {
        #region Veriables
        public const long EMPTY_ID = 0;
        /// <summary>Чи вірні дані</summary>
        public bool IsValid { get; set; }
        #endregion

        #region PropertyChanged
        /// <summary>Властивість змінено</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>При змінені властивості</summary>
        /// <param name="property">Назва властивості</param>
        protected void OnPropertyChanged(string property)
            {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;

            if (propertyChanged != null)
                {
                propertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        #endregion
        }
    }