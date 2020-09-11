using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotobookUploader.View;
using PhotobookUploader.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Xamarin.Forms.StyleSheets;
using System.IO;

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

        private void CreateStyle()
        {
            //var buttonStyle = new Style(typeof(Button))
            //{
            //    Setters = {

            //    new Setter { Property = Button.BackgroundColorProperty,Value = Color.FromHex("") },
            //    new Setter {Property = Button.CornerRadiusProperty, Value=10 }
            //}
            //};
            //var pageStyle = new Style(typeof(Page))
            //{
            //    Setters =
            //    {
            //        new Setter { Property = Page.BackgroundColorProperty, Value = Color.Black }
            //    }
            //};

            //Resources = new ResourceDictionary();
            //Resources.Add(buttonStyle);
            //Resources.Add(pageStyle);
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
