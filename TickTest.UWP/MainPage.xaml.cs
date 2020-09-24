using FFImageLoading.Forms.Platform;
using LightForms;
using TickTest.UWP.Services;
using TickTest.Services;

namespace TickTest.UWP
{
    public sealed partial class MainPage : IPlatformInitializer
    {
        public MainPage()
        {
            this.InitializeComponent();
            CachedImageRenderer.Init();
            LoadApplication(new TickTest.App(this));
        }

        public void RegisterTypes(ICrossContainer container)
        {
            container.Register<ILocalNotification, LocalNotificationImplementation>(FetchTarget.Singleton);
        }
    }
}