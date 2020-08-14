using PhotobookUploader.View;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class HomeViewModel:BaseViewModel
    {
        public ICommand PicturePageCommand { get; private set; }
        public ICommand CreatePhotoalbumCommand { get; private set; }
        public ICommand SettingsPageCommand { get; private set; }
        public ICommand LogoutCommand { get; private set; }
        public ILoginStateService State { get; set; }

        public HomeViewModel(IServiceProvider container, ILoginStateService state):base(container)
        {
            this.State = state;
            PicturePageCommand = new Command(async() => await PicturePage());
            CreatePhotoalbumCommand = new Command(async () => await PhotoAlbumPage());
            SettingsPageCommand = new Command(async () => await SettingsPage());
            LogoutCommand = new Command(async () => await LogOut());

        }
        private async Task PhotoAlbumPage()
        {
            var page = Container.GetService(typeof(CreatePhotoAlbumPage));
            await Navigation.Navigation.PushAsync((Page)page);
        }
        private async Task PicturePage()
        {
            var page = Container.GetService(typeof(UploadPicture));
            await Navigation.Navigation.PushAsync((Page)page);
        }
        private async Task SettingsPage()
        {
            var page = Container.GetService(typeof(Settings));
            await Navigation.Navigation.PushAsync((Page)page);
        }
        private async Task LogOut()
        {
            bool succes = false;
            try
            {
                await State.Logout();
                Application.Current.Properties.Remove("UserId");
                await Application.Current.SavePropertiesAsync();
                succes = true;
            }
            catch(Exception ex)
            {

            }
            if(succes)
            {
                var mp = (Page)Container.GetService(typeof(View.LoginPage));
                Application.Current.MainPage = mp;
            }

        }
    }
}
