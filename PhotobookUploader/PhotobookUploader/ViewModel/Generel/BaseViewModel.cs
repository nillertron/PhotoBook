using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace PhotobookUploader.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected IServiceProvider Container { get; private set; }
        public BaseViewModel(IServiceProvider container)
        {
            Container = container;
        }
        protected void Notify(string component)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(component));
        }
    }
}
