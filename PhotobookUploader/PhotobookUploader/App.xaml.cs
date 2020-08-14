using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotobookUploader.View;
using PhotobookUploader.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Service;

namespace PhotobookUploader
{
    public partial class App : Application
    {
        public App(LoginPage loginPage, Home home, ILoginStateService loginState)
        {
            InitializeComponent();

            if (App.Current.Properties.ContainsKey("UserId"))
            {
                loginState.GetUserFromId((int)App.Current.Properties["UserId"]);
                new Navigation.Navigation(home);
            }
            else
            {
                MainPage = loginPage;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
