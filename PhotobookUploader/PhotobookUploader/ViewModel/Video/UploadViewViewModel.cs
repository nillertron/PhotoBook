using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class UploadViewViewModel:BaseViewModel
    {
        public ICommand SelectCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        public ICommand TakeVideoCommand { get; private set; }

        private IVideoService service;
        private IFotoalbumService fotoalbumService;
        private ILoginStateService loginState;
        private string _msg = string.Empty;
        public string Msg { get => _msg; set { _msg = value;Notify("Msg"); } }
        private string _videoName = string.Empty;
        public string VideoName { get => _videoName; set { _videoName = value; Notify("VideoName"); } }
        private string _beskrivelse = string.Empty;
        public string Beskrivelse { get => _beskrivelse; set { _beskrivelse = value; Notify("Beskrivelse"); } }
        private ObservableCollection<PB_Fotoalbum> _category = new ObservableCollection<PB_Fotoalbum>();
        public ObservableCollection<PB_Fotoalbum> Category { get => _category; set { _category = value; Notify("Category"); } }
        private int _selectedIndex;
        public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; Notify("SelectedIndex"); } }
        private MediaFile mediaFile;
        private bool _btnActive = true;
        public bool BtnActive { get => _btnActive; set { _btnActive = value; Notify("BtnActive"); } }
        public UploadViewViewModel(IServiceProvider container, IVideoService service, IFotoalbumService fotoalbumService, ILoginStateService loginstate):base(container)
        {
            SelectCommand = new Command(async () => await BrowseVideos());
            UploadCommand = new Command(async () => await UploadVideo());
            TakeVideoCommand = new Command(async () => await TakeVideo());

            this.service = service;
            this.fotoalbumService = fotoalbumService;
            this.loginState = loginstate;
            Task.Run(async () => await GetCategories());
        }
        private async Task GetCategories()
        {
            try
            {
                var list = await fotoalbumService.GetAllForUserApi(loginState.user.Id);
                Category = new ObservableCollection<PB_Fotoalbum>(list);
                if (Category.Count > 0)
                    SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }

        }
        private async Task BrowseVideos()
        {
            await CrossMedia.Current.Initialize();
            if(!CrossMedia.Current.IsPickVideoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Video", "Not supported :(", "Ok");
                return;
            }
            mediaFile = await CrossMedia.Current.PickVideoAsync();
            if (mediaFile == null)
                return;
            VideoName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mediaFile.Path);

        }
        private async Task TakeVideo()
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Video", "Not supported :(", "Ok");
                return;
            }
            var options = new StoreVideoOptions { Directory = "Sample", Name = "myVideo" };
            mediaFile = await CrossMedia.Current.TakeVideoAsync(options);
            if (mediaFile == null)
                return;
            VideoName = mediaFile.Path;


        }
        private async Task UploadVideo()
        {
            Msg = string.Empty;
            
            if (mediaFile == null)
            {
                Msg = "No file selected";
                return;
            }
            else if(Beskrivelse == string.Empty)
            {
                Msg = "Description is empty";
                return;
            }
            else if(Category.Count == 0)
            {
                Msg = "No categories available, go create one :)";
                    return;
            }
            try
            {
                BtnActive = false;
                await service.UploadVideo(VideoName, Beskrivelse, Category[SelectedIndex].Id);
                Beskrivelse = string.Empty;
                mediaFile = null;
                VideoName = string.Empty;
                Msg = "Uploadet";
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
            BtnActive = true;
        }
    }
}
