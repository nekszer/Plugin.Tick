using System.Threading.Tasks;

namespace TickTest.Services
{
    public interface IConfirmationPopup
    {

        Task<bool> Show(string title, string message, string ok = "", string cancel = "");

    }
}
