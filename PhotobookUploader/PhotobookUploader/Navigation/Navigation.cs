using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhotobookUploader.Navigation
{
    public class Navigation
    {
        private static NavigationPage navigation;
        public Navigation(Page init)
        {
            navigation = new NavigationPage(init);
             App.Current.MainPage = navigation;
        }
        public static async Task PushAsync(Page p)
        {
            await navigation.PushAsync(p);
            
        }
        public static async Task PopAsync()
        {
            await navigation.PopAsync();
        }

    }
}
