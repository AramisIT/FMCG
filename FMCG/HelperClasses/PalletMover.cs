using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using Catalogs;

namespace AtosFMCG.HelperClasses
    {
    /// <summary>���������� �������</summary>
    public static class PalletMover
        {
        //��������� ������ ���������� � ������� FilledCell (�� ����� ������ �� �������):
        //PalletCode - ��� �������
        //PreviousCode - ��� ���������� �������, ����� ���� ��������� ���� �������

        #region Move
        /// <summary>���������� ������� � ������ (��������� �� ���� �������)</summary>
        /// <param name="palletCode">��������� ��� ������</param>
        /// <param name="previous">��������� ��� ������ �� ��� ����������� �������, �� ��������������</param>
        public static void EstablishPalletToCell(long palletCode, long previous = 0)
            {
            //�������� ����� (�� �������� ������ �������� ������� ��������)
            PreviousPallets filledCell = new PreviousPallets
                                        {
                                            Description = "��������� ������ � ���",
                                            PalletCode = palletCode,
                                            PreviousCode = previous
                                        };
            filledCell.Write();

            //������ ����� � �������
            //Query query = DB.NewQuery("");
            //query.AddInputParameter("PalletCode", palletCode);
            //query.AddInputParameter("Previous", previous);
            //query.Execute();
            }

        /// <summary>���������� �������</summary>
        /// <param name="palletCode">��������� ��� �������</param>
        /// <param name="newPreviousPallet">��������� ��� ������ �� ��� ����������� �������, �� �����������</param>
        public static void MovePalletToNewPlace(long palletCode, long newPreviousPallet = 0)
            {
            //������ ������� ��������� ������� ��� ���� �������
            Query query = DB.NewQuery("UPDATE FilledCell SET PreviousCode=@PreviousCode WHERE PalletCode=@PalletCode");
            query.AddInputParameter("PreviousCode", newPreviousPallet);
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            }

        /// <summary>�������� ������� � ������</summary>
        /// <param name="palletCode">��������� ��� �������</param>
        public static void RemovePallet(long palletCode)
            {
            //�������� ����� (� ������ ������� ������� ��������) � �������
            Query query = DB.NewQuery("DELETE FROM FilledCell PalletCode=@PalletCode");
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            } 
        #endregion
        }
    }