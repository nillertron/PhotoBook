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
    public class SettingsViewModel:BaseViewModel
    {
        private PB_Bruger _current;
        public PB_Bruger Current { get => _current; set { _current = value; Notify("Current"); } }
        public ICommand EditCommand { get { return new Command(async () => await Edit()); } }
        private ILoginStateService loginState;
        private string _msg = string.Empty;
        public string Msg { get => _msg; set { _msg = value; Notify("Msg"); } }
        public SettingsViewModel(IServiceProvider container, ILoginStateService loginState):base(container)
        {
            this.loginState = loginState;
            Current = loginState.user;
        }
        public async Task Edit()
        {
            Msg = string.Empty;
            if(Current.EfterNavn == "" || Current.Email == "" || Current.Password == "" || Current.SharedId == "" || Current.Navn == "")
            {
                Msg = "Please fill out all fields";
                return;
            }
            try
            {
                await loginState.EditUser(Current);
                Msg = "Updated";
            }
            catch(Exception ex)
            {
                Msg = ex.Message;
            }
        }
    }
}
