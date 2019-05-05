using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Helpers
{
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

        private const string token_type = "Token_type";
        private const string access_token = "Access_token";
        private const string userASP = "UserASP";
        private const string isRemembered = "IsRemembered";
        private static readonly string stringDefault = string.Empty;
        private static readonly bool booleanDefault = false;
        #endregion

        public static string UserASP
        {
            get
            {
                return AppSettings.GetValueOrDefault(userASP, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(userASP, value);
            }
        }

        public static string Token_type
        {
            get
            {
                return AppSettings.GetValueOrDefault(token_type, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(token_type, value);
            }
        }

        public static string Access_token
        {
            get
            {
                return AppSettings.GetValueOrDefault(access_token, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(access_token, value);
            }
        }

        public static bool IsRemembered
        {
            get
            {
                return AppSettings.GetValueOrDefault(isRemembered, booleanDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(isRemembered, value);
            }
        }
    }
}
