using LightForms;
using LightForms.Services;

namespace TickTest.Services
{
    public class Storage<T> : Table<T>
    {
        public Storage(string tablename = "") : base(CrossContainer.Instance.Create<ISerializer>(), CrossContainer.Instance.Create<IDeserializer>(), tablename) { }
    }
}