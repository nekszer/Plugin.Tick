using LightForms;
using LightForms.Services;
using LightForms.Xaml;
using TickTest.Services;
using TickTest.Strings;
using TickTest.ViewModels;
using TickTest.Views;

namespace TickTest
{
    public partial class App : LightFormsApplication
    {
        #region Initialize
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
#if DEBUG
            EnableDebugRainbows(true);
#endif
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            InitializeComponent();
            TranslateExtension.Init(AppResources.ResourceManager);
            SetServiceHandler(new ServiceHandler());
            CrossContainer.Instance.Create<INotificationHandler>().Init();
            Initialize<MainPage, MainViewModel>(AppRoutes.Main);
        }
        #endregion

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnSleep();
        }

        protected override void Routes(IRoutingService routingservice)
        {
            // set routes 
            // routingservice.Route<View,ViewModel>("/routename");
        }

        protected override void RegisterTypes(ICrossContainer container)
        {
            base.RegisterTypes(container);
            container.Register<ISerializer, JsonConverter>();
            container.Register<IDeserializer, JsonConverter>();
            container.Register<IProgressPopup, ProgressPopup>();
            container.Register<IToastPopup, ToastPopup>();
            container.Register<IConfirmationPopup, ConfirmationPopup>();
            container.Register<IStorageManager<Settings>, SettingsManager<Settings>>();
            container.Register<INotificationHandler, NotificationHandler>();
            var settings = container.Create<IStorageManager<Settings>>();
            var mode = AppMode.Development;
            settings.Set(s => s.Mode, mode);
        }

        #region Debutg Rainbows
        void EnableDebugRainbows(bool shouldUseDebugRainbows)
        {
            Resources.Add(new Xamarin.Forms.Style(typeof(Xamarin.Forms.ContentPage))
            {
                ApplyToDerivedTypes = true,
                Setters = {
                new Xamarin.Forms.Setter
                {
                    Property = Xamarin.Forms.DebugRainbows.DebugRainbow.ShowColorsProperty,
                    Value = shouldUseDebugRainbows
                }
            }
            });
        }
        #endregion
    }
}
