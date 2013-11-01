using Aramis.DatabaseConnector;
using AtosFMCG.DatabaseObjects.Catalogs;
using Catalogs;

namespace AtosFMCG.HelperClasses
    {
    /// <summary>Переміщувач паллети</summary>
    public static class PalletMover
        {
        //Положення палети зберігається в таблиці FilledCell (на даний момент це довідник):
        //PalletCode - Код паллети
        //PreviousCode - Код попередньої паллети, перед якою ставиться наша паллета

        #region Move
        /// <summary>Встановити паллету в комірку (підставити до іншої паллети)</summary>
        /// <param name="palletCode">Унікальний код палети</param>
        /// <param name="previous">Унікальний код палети до якої підставиться паллета, що встановлюється</param>
        public static void EstablishPalletToCell(long palletCode, long previous = 0)
            {
            //Створити запис (на поточний момент створити елемент довідника)
            PreviousPallets filledCell = new PreviousPallets
                                        {
                                            Description = "Розміщення паллет з ТСД",
                                            PalletCode = palletCode,
                                            PreviousCode = previous
                                        };
            filledCell.Write();

            //Додати рядок в таблицю
            //Query query = DB.NewQuery("");
            //query.AddInputParameter("PalletCode", palletCode);
            //query.AddInputParameter("Previous", previous);
            //query.Execute();
            }

        /// <summary>Перемістити паллету</summary>
        /// <param name="palletCode">Унікальний код паллети</param>
        /// <param name="newPreviousPallet">Унікальний код палети до якої підставиться паллета, що переміщується</param>
        public static void MovePalletToNewPlace(long palletCode, long newPreviousPallet = 0)
            {
            //Просто оновити попередню паллету для нашої паллети
            Query query = DB.NewQuery("UPDATE FilledCell SET PreviousCode=@PreviousCode WHERE PalletCode=@PalletCode");
            query.AddInputParameter("PreviousCode", newPreviousPallet);
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            }

        /// <summary>Прибрати паллету зі скалду</summary>
        /// <param name="palletCode">Унікальний код паллети</param>
        public static void RemovePallet(long palletCode)
            {
            //Видалити рядок (в нашому випадку елемент довідника) з таблиці
            Query query = DB.NewQuery("DELETE FROM FilledCell PalletCode=@PalletCode");
            query.AddInputParameter("PalletCode", palletCode);
            query.Execute();
            } 
        #endregion
        }
    }