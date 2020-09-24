using Acr.UserDialogs;

namespace TickTest.Services
{
    public class ToastPopup : IToastPopup
    {
        public void Show(string text)
        {
            UserDialogs.Instance.Toast(text);
        }
    }
}