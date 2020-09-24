using LightForms.Services;

namespace TickTest.Services
{
    public class ServiceHandler : IServiceHandler
    {
        public const string ServicesNameSpace = "TickTest.Services";
        string IServiceHandler.ServicesNameSpace
        {
            get => ServicesNameSpace;
            set { }
        }
    }
}