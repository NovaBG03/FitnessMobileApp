/// <summary>
/// Стартова точка за приложението
/// </summary>
namespace MobileFitness.App
{
    using Xamarin.Forms;

    /// <summary>
    /// Константи
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// В разработка ли е приложението
        /// </summary>
        public static bool IsDev = true;

        /// <summary>
        /// Цвят на фона
        /// </summary>
        public static Color BackgroundColor = Color.White;

        /// <summary>
        /// Главен цвят за текста
        /// </summary>
        public static Color MainTextColor = Color.White;

        /// <summary>
        /// Цвят на фона на бара
        /// </summary>
        public static Color BarBackgroundColor = Color.FromRgb(58, 153, 215);

        /// <summary>
        /// Цвят на неизбран елемент
        /// </summary>
        public static Color UnselectedTabColor = Color.Black;

        /// <summary>
        /// Цвят на избран елемент
        /// </summary>
        public static Color SelectedTabColor = Color.White;

        /// <summary>
        /// Цвят на фона на поле за текст
        /// </summary>
        public static Color EntryBackgroundColor = Color.White;

        /// <summary>
        /// Височина на икона при вход
        /// </summary>
        public static double LoginIconHeight = 120;
    }
}
