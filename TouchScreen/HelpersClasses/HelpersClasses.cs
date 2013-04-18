using System.Drawing;
using System.Text;
using AtosFMCG.TouchScreen.Enums;

namespace AtosFMCG.TouchScreen.HelpersClasses
{
    /// <summary>Допоміжний клас</summary>
    public static class HelperClass
    {
        /// <summary>Шаблон за замовчуванням форматування цілого числа</summary>
        public const string INTEGRAL_NUMBER_PATTERN = "{0:#,0}";
        /// <summary>Шаблон за замовчуванням форматування дрібного числа</summary>
        public const string FRACTIONAL_NUMBER_PATTERN = "{0:#,0.#}";

        /// <summary>Шаблон для форматування дрібного числа</summary>
        /// <param name="NumberOfDecimalPlaces">Кількість знаків після крапки</param>
        /// <returns>Шаблон форматування</returns>
        public static string GetDecimalPattern(int NumberOfDecimalPlaces)
        {
            if (NumberOfDecimalPlaces > 0)
            {
                StringBuilder part = new StringBuilder("{0:#,0.", 10 + NumberOfDecimalPlaces);

                for (int i = 0; i < NumberOfDecimalPlaces; i++)
                {
                    part.Append('#');
                }

                part.Append('}');

                return part.ToString();
            }

            return INTEGRAL_NUMBER_PATTERN;
        }

        /// <summary>Шрифт для Ext контролів</summary>
        public static Font GetFontForExtControls()
        {
            return new Font("Tahoma", 11, FontStyle.Bold);
        }

        /// <summary>Шрифт для Ext контролів</summary>
        public static Font GetFontForExtControls(TypesOfFont type)
            {
            switch (type)
                {
                case TypesOfFont.Small:
                        return new Font("Tahoma", 8);
                    case TypesOfFont.Big:
                        return new Font("Tahoma", 25, FontStyle.Bold);
                    default:
                        return new Font("Tahoma", 11, FontStyle.Bold);
                }
            }
    }
}
