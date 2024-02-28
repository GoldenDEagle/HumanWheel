using GamePush;

namespace Assets.Codebase.Infrastructure.ServicesManagment.Localization
{
    /// <summary>
    /// Localization service.
    /// </summary>
    public interface ILocalizationService : IService
    {
        /// <summary>
        /// Sets localization language.
        /// </summary>
        /// <param name="language"></param>
        public void SetLanguage(Language language);

        /// <summary>
        /// Returns string localized by the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string LocalizeTextByKey(string key);
    }
}
