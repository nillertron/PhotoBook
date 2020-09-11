using Model;
using PhotobookUploader.View;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class VideoOverviewViewModel:BaseViewModel
    {
        private ObservableCollection<PB_Video> _videoListe = new ObservableCollection<PB_Video>();
        public ObservableCollection<PB_Video> VideoListe { get => _videoListe; set { _videoListe = value; Notify("VideoListe"); } }
        public ICommand EditCommand { get { return new Command(async (e) => await EditAsync((PB_Video)e)); } }
        public ICommand DeleteCommand { get { return new Command(async (e) => await DeleteAsync((PB_Video)e)); } }
        private IVideoService videoService;
        private ILoginStateService loginState;

        public VideoOverviewViewModel(IServiceProvider container, IVideoService videoService, ILoginStateService loginState) : base(container)
        {
            this.videoService = videoService;
            this.loginState = loginState;
            Init();
        }
        private async Task Init()
        {
            var list = new List<PB_Video>();
            foreach(var v in loginState.user.Fotoalbum)
            {
                list.AddRange(v.Videos);
            }
            VideoListe = new ObservableCollection<PB_Video>(list);
        }
        private async Task EditAsync(PB_Video model)
        {
            var page = (VideoDetailsPage) Container.GetService(typeof(VideoDetailsPage));
            page.ViewModel.Model = model;
            await Navigation.Navigation.PushAsync((Page)page);
        }
        private async Task DeleteAsync(PB_Video model)
        {
            try
            {
                var sure = await Application.Current.MainPage.DisplayAlert("Delete", "Are you sure?", "Yes", "Cancel");
                if(sure)
                await videoService.DeleteVideo(model);
                VideoListe.Remove(model);
                loginState.user.Fotoalbum[model.PB_FotoalbumId].Videos.Remove(model);
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
