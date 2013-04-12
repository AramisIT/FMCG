namespace AtosFMCG.TouchScreen.Interfaces
{
    /// <summary>Розширений контрол</summary>
    public interface IExtControl
    {
        /// <summary>Контрол завантажився</summary>
        bool IsLoaded { get; }
        void SetColor();
    }
}
