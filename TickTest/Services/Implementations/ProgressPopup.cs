using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace TickTest.Services
{
    public class ProgressPopup : IProgressPopup
    {
        public async void Show()
        {
            var content = new Frame
            {
                BackgroundColor = Color.Black,
                Padding = new Thickness(12),
                WidthRequest = 50,
                HeightRequest = 50,
                CornerRadius = 10,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Content = new ActivityIndicator
                {
                    Color = Color.White,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    WidthRequest = 30,
                    HeightRequest = 30,
                    IsRunning = true
                }
            };

            await PopupNavigation.Instance.PushAsync(new Rg.Plugins.Popup.Pages.PopupPage
            {
                Content = content,
                CloseWhenBackgroundIsClicked = false
            });
        }

        public async void Hide()
        {
            if (PopupNavigation.Instance.PopupStack.Count <= 0) return;
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
}
