using Newtonsoft.Json;

namespace ElementAdmin.Infrastructure.Common
{
    public class Mapper
    {
        public static T ToMap<T>(object obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    }
}
