using FFImageLoading.Forms.Platform;
using Foundation;
using LightForms;
using TickTest.iOS.Services;
using TickTest.Services;
using Rg.Plugins.Popup;
using UIKit;

namespace TickTest.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatformInitializer
    {

        public LocalNotificationImplementation LocalNotification { get; private set; }

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Popup.Init();
            CachedImageRenderer.Init();
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new TickTest.App());
            LocalNotification?.OnLaunching(options);
            return base.FinishedLaunching(app, options);
        }

        public async void RegisterTypes(ICrossContainer container)
        {
            container.Register<ILocalNotification, LocalNotificationImplementation>(FetchTarget.Singleton);
            LocalNotification = container.Create<ILocalNotification>() as LocalNotificationImplementation;
            await LocalNotification.Init();
        }
    }
}