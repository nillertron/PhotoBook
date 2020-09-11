using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class VideoDetailsViewmodel:BaseViewModel
    {
        private PB_Video _model;
        public PB_Video Model { get => _model; set { _model = value; Notify("Model"); } }
        public ICommand EditCommand { get { return new Command(async () => await Edit()); } }
        private IVideoService videoService;
        public VideoDetailsViewmodel(IServiceProvider container, IVideoService videoS):base(container)
        {
            this.videoService = videoS;
        }
        private async Task Edit()
        {
            try
            {
                await videoService.EditVideo(Model);
                await Application.Current.MainPage.DisplayAlert("Edited", "Succes", "Ok");
                

            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

            }
        }
    }
}
