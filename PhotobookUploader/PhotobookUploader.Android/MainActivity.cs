using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Microsoft.Extensions.DependencyInjection;
using Service;
using System.Linq;
using PhotobookUploader.ViewModel;
using PhotobookUploader.View;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Reflection;

namespace PhotobookUploader.Droid
{
    [Activity(Label = "PhotobookUploader", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            IServiceCollection services = new ServiceCollection();
            services = ContainerService.AddModels(services);
            services = ContainerService.AddServices(services);
            services = ContainerService.AddDataAcces(services);
            services = AddViewModels(services);
            services = AddViews(services);
            services.AddTransient(typeof(Service.Interface.IMobileFeature), typeof(AndroidSpecific.MobileFeature));
            //services.AddTransient<App>();
            var provider = services.BuildServiceProvider();
            LoadApplication(new App((LoginPage)provider.GetRequiredService(typeof(LoginPage)),(Home)provider.GetRequiredService(typeof(Home)),(ILoginStateService)provider.GetRequiredService(typeof(ILoginStateService))));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private static IServiceCollection AddViewModels(IServiceCollection services)
        {

            var viewmodelList = Assembly.Load(nameof(PhotobookUploader)).GetTypes().Where(x => x.Namespace.Contains("ViewModel") && !x.IsNested).ToList();

            foreach (var e in viewmodelList)
            {
                services.AddTransient(e);
            }
            return services;

        }
        private static IServiceCollection AddViews(IServiceCollection services)
        {

            var viewList = Assembly.Load(nameof(PhotobookUploader)).GetTypes().Where(x => x.Namespace.Contains("View") && !x.IsNested).ToList();
            foreach (var e in viewList)
            {
                services.AddTransient(e);
            }
            return services;

        }

    }
}