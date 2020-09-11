using PhotobookUploader.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotobookUploader.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private IServiceProvider container;
        public LoginPage(LoginViewModel viewModel, IServiceProvider container)
        {
            InitializeComponent();
            BindingContext = viewModel;
            this.container = container;
        }
        public async Task NavigateNextPage()
        {

        }
    }
}