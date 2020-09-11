using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Service;

namespace PhotobookUploader.ViewModel
{
   public class CreatePhotoAlbumViewModel:BaseViewModel
    {
        private IFotoalbumService service;
        private PB_Fotoalbum _model = new PB_Fotoalbum();
        public PB_Fotoalbum Model { get => _model; set { _model = value; Notify("Model"); } }
        private string _msg;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        public ICommand CreateCommand { get; private set; }
        public ICommand BackCommand { get; private set; }

        public CreatePhotoAlbumViewModel(IServiceProvider container, IFotoalbumService service):base(container)
        {
            this.service = service;
            CreateCommand = new Command(async () => await Create());
            BackCommand = new Command(async () => await Back());
        }

        public async Task Create()
        {
            Model.OprettetDato = DateTime.Now;
            if(Model.Navn.Length > 0 && Model.Beskrivelse.Length > 0)
            {
                try
                {
                    await service.CreateApi(Model);
                    Msg = "Created";
                    Model = new PB_Fotoalbum();
                }
                catch (Exception ex)
                {
                    Msg = ex.Message;
                }
            }
            else
            {
                Msg = "Both fields are required";
            }


        }
        public async Task Back()
        {
            await Navigation.Navigation.PopAsync();
        }
    }
}
