namespace WMS_client.Delegates
    {
    public delegate void SelectFromListDelegate(long selectedIndex, string description);
    public delegate void ReadBarcodeDelegate(string barcode);
    public delegate void EnterDataDelegate();
    }