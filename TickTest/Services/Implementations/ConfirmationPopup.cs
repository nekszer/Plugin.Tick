using Acr.UserDialogs;
using TickTest.Strings;
using System.Threading.Tasks;

namespace TickTest.Services
{
    public class ConfirmationPopup : IConfirmationPopup
    {
        public async Task<bool> Show(string title, string message, string ok = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(ok)) ok = AppResources.OK;
            if (string.IsNullOrEmpty(cancel)) ok = AppResources.Cancel;
            return await UserDialogs.Instance.ConfirmAsync(message, title, ok, cancel);
        }
    }
}
