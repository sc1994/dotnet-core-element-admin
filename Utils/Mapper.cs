
using Newtonsoft.Json;

namespace Utils
{
    public class Mapper
    {
        public static TExtend ToExtend<TExtend>(object @base)
        {
            if (@base == null) return default;
            return JsonConvert.DeserializeObject<TExtend>(JsonConvert.SerializeObject(@base));
        }
    }
}
