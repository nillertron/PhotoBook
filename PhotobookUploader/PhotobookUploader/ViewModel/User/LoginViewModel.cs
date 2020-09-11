using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhotobookUploader.View;
using Service;
using Xamarin.Forms;
using PhotobookUploader.Navigation;
namespace PhotobookUploader.ViewModel
{
    public class LoginViewModel:BaseViewModel
    {
        private IBrugerService brugerService;
        private ILoginStateService state;
        private string _email = string.Empty;
        public string Email { get => _email; set { _email = value; Notify("Email"); } }
        private string _password = string.Empty;
        public string Password { get => _password; set { _password = value; Notify("Password"); } }
        private string _msg = string.Empty;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        public ICommand AttemptLoginCommand { get; set; }
        public ICommand CreateUserCommand { get; set; }

        public LoginViewModel(IBrugerService brugerService, ILoginStateService state, IServiceProvider container):base(container)
        {
            this.brugerService = brugerService;
            this.state = state;
            AttemptLoginCommand = new Command(async () =>
            {
                await AttemptLogin();
            });
            CreateUserCommand = new Command(async () =>
            {
                await CreateUser();
            });
        }
        private async Task CreateUser()
        {
            var page = Container.GetService(typeof(CreateUserPage));
            Application.Current.MainPage = (Page)page;
        }
        private async Task AttemptLogin()
        {
            bool succes = false;
            await Task.Run(async () =>
            {
                try
                {
                    await state.Login(Email, Password);
                    succes = true;
                    Application.Current.Properties.Add("UserId", state.user.Id);
                    await Application.Current.SavePropertiesAsync();

                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                    Msg = Ex.Message;
                }
            });
            if(succes)
            {
                var loginpage = (LoginPage)Container.GetService(typeof(LoginPage));
                var newPage = Container.GetService(typeof(Home));
                var nav = new Navigation.Navigation((Page)newPage);
            }

            
        }
        
    }
}
