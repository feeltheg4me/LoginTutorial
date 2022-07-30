// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LoginTutorial.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string LastUserNameSettings = "last_user_name_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string LastUsedUserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastUserNameSettings, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastUserNameSettings, value);
            }
        }

    }
}
