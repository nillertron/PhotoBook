using Model;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace PhotobookUploader.ViewModel
{
    public class UploadPictureViewModel:BaseViewModel
    {
        public ICommand ChosePicCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand UploadCommand { get; private set; }
        private ILoginStateService loginState;
        private IFotoalbumService fotoalbumService;
        public string _path = string.Empty;
        public string Path { get => _path; set { _path = value; Notify("Path"); } }
        public string _msg = string.Empty;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        public int _selectedIndex = 0;
        public int SelectedIndex { get => _selectedIndex; set { _selectedIndex = value; Notify("SelectedIndex"); } }

        public string _beskrivelse = string.Empty;
        public string Beskrivelse { get => _beskrivelse; set { _beskrivelse = value; Notify("Beskrivelse"); } }
        private ObservableCollection<PB_Fotoalbum> _fotoAlbumList = new ObservableCollection<PB_Fotoalbum>();
        public ObservableCollection<PB_Fotoalbum> FotoalbumsListe { get => _fotoAlbumList; set { _fotoAlbumList = value; Notify("FotoalbumsListe"); } }
        private ImageSource _imgSource;
        public ImageSource ImgSource { get => _imgSource; set { _imgSource = value; Notify("ImgSource"); } }
        private MediaFile mediafile;
        private IFotoService service;
        private bool _btnActive = true;
        public bool BtnActive { get => _btnActive; set { _btnActive = value; Notify("BtnActive"); } }
        public UploadPictureViewModel(IServiceProvider container, IFotoService service, IFotoalbumService fotoalbumService, ILoginStateService loginState) : base(container)
        {
            this.service = service;
            this.fotoalbumService = fotoalbumService;
            this.loginState = loginState;
            Task.Run(async () => await GetFotoalbumliste());
            ChosePicCommand = new Command(async () => await UploadPicture());
            UploadCommand = new Command(async () => await Upload());
            TakePhotoCommand = new Command(async () => await TakePhoto());

        }
        private async Task GetFotoalbumliste()
        {
            try
            {
                var list = await fotoalbumService.GetAllForUserApi(loginState.user.Id);
                if (list != null && list.Count > 0)
                {
                    FotoalbumsListe = new ObservableCollection<PB_Fotoalbum>(list);
                    Task.Run(async () =>
                    {
                        await Task.Delay(100);
                        SelectedIndex = 0;
                    });
                }
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }

        }
        private async Task UploadPicture()
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No PickPhoto", " :( No pickphoto available", "Ok");
                return;
            }
            mediafile = await CrossMedia.Current.PickPhotoAsync();
            if (mediafile == null)
                return;
            Path = mediafile.Path;

            ImgSource = ImageSource.FromStream(() =>
            {
                return mediafile.GetStream();
            });
        }
        private async Task Upload()
        {

            Msg = string.Empty;
            if(Beskrivelse == string.Empty)
            {
                Msg = "Write a description";
                return;
            }
            if(FotoalbumsListe.Count == 0)
            {
                Msg = "Create a category before posting pictures";
                return;
            }
            if(mediafile == null)
            {
                Msg = "No photo selected";
                return;
            }
            try
            {
                BtnActive = false;
                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(mediafile.GetStream()), "\"file\"", $"\"{mediafile.Path}\"");
                //bruger ikke content variablen, da det lykkedes med blot at bruge fil lokationen, da det virker med restsharp.
                await service.UploadPhoto(Path,Beskrivelse, FotoalbumsListe[SelectedIndex].Id);
                Msg = "Uploadet";
                Beskrivelse = string.Empty;
                ImgSource = null;

            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
            BtnActive = true;
        }
        private async Task TakePhoto()
        {
            await CrossMedia.Current.Initialize();
            if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No camera", ":( No camera available", "Ok");
                return;
            }
            mediafile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { Directory = "Sample", Name = "myImage.jpg" });
            if (mediafile == null)
                return;
            Path = mediafile.Path;

            ImgSource = ImageSource.FromStream(() =>
            {
                return mediafile.GetStream();
            });

        }
    }
}
