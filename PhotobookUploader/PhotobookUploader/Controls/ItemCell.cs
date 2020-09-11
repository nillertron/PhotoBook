using Model;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Forms;

namespace PhotobookUploader.Controls
{
    public class ItemCell : ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create("SelectedBackgroundColorProperty", typeof(Color), typeof(ItemCell), Color.Default);
        public Color SelectedBackgroundColor { get { return (Color)GetValue(SelectedBackgroundColorProperty); } set { SetValue(SelectedBackgroundColorProperty, value); } }
    }
}
