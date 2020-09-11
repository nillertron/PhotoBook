using Model;
using PhotobookUploader.View;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class FotoOverviewViewModel:BaseViewModel
    {
        private string _msg;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        private ILoginStateService loginService;
        private ObservableCollection<PB_Foto> _fotoListe = new ObservableCollection<PB_Foto>();
        public ObservableCollection<PB_Foto> FotoListe { get => _fotoListe; set { _fotoListe = value; Notify("FotoListe"); } }
        public ICommand EditPageCommand { get { return new Command(async (e) => await EditPage(e)); } }
        public ICommand DeletePictureCommand { get { return new Command(async (e) => await DeletePicture(e)); } }
        private IFotoService fotoService;
        public FotoOverviewViewModel(IServiceProvider container, ILoginStateService loginservice, IFotoService fotoService):base(container)
        {
            this.loginService = loginservice;
            Task.Run(async () => await Init());
            this.fotoService = fotoService;
        }
        private async Task DeletePicture(object e)
        {
            var delete = await Application.Current.MainPage.DisplayAlert("Delete?", "Cannot be reversed", "Ok", "Cancel");
            if(delete)
            {
                try
                {
                    await fotoService.DeleteFotoAPIAsync((PB_Foto)e);
                    FotoListe.Remove((PB_Foto)e);
                }
                catch(Exception ex)
                {
                    Msg = ex.Message;
                }
            }
            
        }
        private async Task EditPage(object e)
        {
            var obj = e as PB_Foto;
            var page = (FotoDetailsPage)Container.GetService(typeof(FotoDetailsPage));
            page.ViewModel.Selected = obj;
            await Navigation.Navigation.PushAsync(page);
        }
        private async Task Init()
        {
            var list = new List<PB_Foto>();
            foreach(var foto in loginService.user.Fotoalbum)
            {
                list.AddRange(foto.Fotos);
            }
            FotoListe = new ObservableCollection<PB_Foto>(list);
        }
    }
}
