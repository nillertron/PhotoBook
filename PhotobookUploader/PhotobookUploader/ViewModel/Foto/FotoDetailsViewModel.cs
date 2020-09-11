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
    public class FotoDetailsViewModel:BaseViewModel
    {
        private PB_Foto _selected;

        public PB_Foto Selected { get => _selected; set { _selected = value; Notify("Selected"); } }
        public ICommand EditCommand { get => new Command(async () => await Edit()); }
        private IFotoService fotoService;
        public FotoDetailsViewModel(IServiceProvider container, IFotoService fotoService):base(container)
        {
            this.fotoService = fotoService;
        }
        private async Task Edit()
        {
            try
            {
                await fotoService.UpdateFotoAPIAsync(Selected);
                await Application.Current.MainPage.DisplayAlert("Updated", "Succesfully", "Ok");
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

            }
        }
        
    }
}
