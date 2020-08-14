using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Model;
using Service;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class CreateUserViewModel:BaseViewModel
    {
        private IBrugerService service;
        private PB_Bruger _model = new PB_Bruger();
        public PB_Bruger Model { get => _model; set { _model = value; Notify("Model"); } }
        public ICommand CreateCommand { get; private set; }
        private string _msg;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        public CreateUserViewModel(IServiceProvider container, IBrugerService service):base(container)
        {
            this.service = service;
            CreateCommand = new Command(async () => await TryCreate());
        }
        public async Task TryCreate()
        {
            try
            {
                await service.CrateUserRestApi(Model);
                Msg = "User created";
                var loginPage = Container.GetService(typeof(View.LoginPage));
                await Task.Delay(1000);
                App.Current.MainPage = (Page)loginPage;
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
        }


    }
}
