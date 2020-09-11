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
    public partial class VideoOverview : ContentPage
    {
        public VideoOverview(VideoOverviewViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}