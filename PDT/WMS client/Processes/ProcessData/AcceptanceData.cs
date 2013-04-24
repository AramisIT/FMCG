using System.Collections.Generic;

namespace WMS_client.Processes
    {
    public class AcceptanceData : IProcessData
        {
        /// <summary>�����</summary>
        public KeyValuePair<long, string> Goods { get; set; }
        /// <summary>��������� �������</summary>
        /// <summary>�������������� � ������?</summary>
        public bool IsCell { get; set; }
        /// <summary>Id ���������� ������ (������)</summary>
        public PlaningData<long> Place { get; set; }
        /// <summary>������</summary>
        public KeyValuePair<long, string> Cell { get; set; }
        /// <summary>���� ������������</summary>
        public string Date { get; set; }
        /// <summary>��������� �������</summary>
        public string Topic { get; set; }
        /// <summary>�������� �����������</summary>
        public string UserBarcode { get; set; }
        /// <summary>Id ������</summary>
        public long Car { get; set; }
        /// <summary>Id ����</summary>
        public long Party { get; set; }
        /// <summary>Id ��������� ��������</summary>
        public long ConsignmentNote { get; set; }
        /// <summary>�-��� �����</summary>
        public double BoxCount { get; set; }
        /// <summary>�-��� �������</summary>
        public double BottleCount { get; set; }
        /// <summary>��������� ������������� ������� ������</summary>
        public bool PermitInstallPalletManually { get; set; }

        public AcceptanceData()
            {
            Topic = "���������";
            }
        }
    }