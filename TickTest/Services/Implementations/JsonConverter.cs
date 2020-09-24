using LightForms.Services;
using Newtonsoft.Json;

namespace TickTest.Services
{
    public class JsonConverter : ISerializer, IDeserializer
    {
        public T Deserialize<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }

        public string Serialize<T>(T element)
        {
            return JsonConvert.SerializeObject(element);
        }
    }
}
