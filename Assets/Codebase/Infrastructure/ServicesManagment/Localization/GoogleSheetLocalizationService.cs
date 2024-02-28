using Assets.SimpleLocalization.Scripts;
using GamePush;
using System;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Localization
{
    public class GoogleSheetLocalizationService : ILocalizationService
    {
        // Keys as they are in Google Sheets
        private const string RussianLanguageKey = "Russian";
        private const string EnglishLanguageKey = "English";

        private const string LanguageNotFoundMessage = "Language not found! Default language was set.";

        public GoogleSheetLocalizationService()
        {
            LocalizationManager.Read();
        }

        public string LocalizeTextByKey(string key)
        {
            return LocalizationManager.Localize(key);
        }

        public void SetLanguage(Language language)
        {
            string languageKey = RussianLanguageKey;

            switch (language)
            {
                case Language.English:
                    languageKey = EnglishLanguageKey;
                    break;
                case Language.Russian:
                    languageKey = RussianLanguageKey;
                    break;
                default:
                    throw new ArgumentException(LanguageNotFoundMessage);
            }

            LocalizationManager.Language = languageKey;
        }
    }
}
