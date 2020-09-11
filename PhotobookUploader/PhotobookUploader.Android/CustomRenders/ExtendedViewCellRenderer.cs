using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PhotobookUploader.Controls;
using PhotobookUploader.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ItemCell),typeof(ExtendedViewCellRenderer))]
namespace PhotobookUploader.Droid.CustomRenders
{
    public class ExtendedViewCellRenderer:ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent,context);
            _selected = false;
            _unselectedBackground = _cellCore.Background;
            return _cellCore;
        }
        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnCellPropertyChanged(sender, e);

            if(e.PropertyName=="IsSelected")
            {
                _selected = !_selected;
                if(_selected)
                {
                    var extendedViewCell = sender as ItemCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    _cellCore.SetBackground(_unselectedBackground);
                }
            }
        }
    }
}