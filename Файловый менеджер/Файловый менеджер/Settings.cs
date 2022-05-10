using System;

namespace Файловый_менеджер
{
    [Serializable]
    internal class Settings
    {
        private static Settings current;

        public static Settings GetCurrent()
        {
            if (current == null)  current = new Settings();
            return current;
        }
        public static Settings GetCurrent(Settings settings)
        {
            if (current == null)  current = settings;
            return current;
        }

        public string currentFont = "Arial";
        public string currentTheme = "Розовый минимализм";
        public float currentTextSize = 8;
        //public int numberBooksOnPage = 10;
    }
}
