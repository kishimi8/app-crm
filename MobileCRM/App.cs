﻿using System;
using System.Threading.Tasks;
using MobileCRM.Extensions;
using MobileCRM.Localization;
using MobileCRM.Pages;
using MobileCRM.Services;
using Xamarin.Forms;
using MobileCRM.Pages.Splash;
using MobileCRM.ViewModels.Splash;

namespace MobileCRM
{
    public class App : Application
    {
        public App()
        {
            if (Device.OS != TargetPlatform.WinPhone)
                TextResources.Culture = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();

            MainPage = new RootPage();
        }

        static readonly Lazy<AuthenticationService> _LazyAuthenticationService = new Lazy<AuthenticationService>(() => new AuthenticationService());

        static AuthenticationService _AuthenticationService { get { return _LazyAuthenticationService.Value; } }

        public static async Task<bool> Authenticate()
        {
            try
            {
                return await _AuthenticationService.Authenticate();
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(typeof(App));
                return false;
            }
        }

        public static async Task<bool> Logout()
        {
            try
            {
                return await _AuthenticationService.Logout();
            }
            catch (Exception ex)
            {
                ex.WriteFormattedMessageToDebugConsole(typeof(App));
                return false;
            }
        }

        public static bool IsAuthenticated
        {
            get { return _AuthenticationService.IsAuthenticated; }
        }
    }
}
