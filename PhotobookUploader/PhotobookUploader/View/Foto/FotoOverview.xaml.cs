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
    public partial class FotoOverview : ContentPage
    {
        public FotoOverview(FotoOverviewViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}