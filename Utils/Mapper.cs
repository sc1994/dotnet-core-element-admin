
using Newtonsoft.Json;

namespace Utils
{
    public class Mapper
    {
        public static TExtend ToExtend<TExtend>(object @base)
        {
            if (@base == default) return default;
            return JsonConvert.DeserializeObject<TExtend>(JsonConvert.SerializeObject(@base));
        }

        public static TCopy Copy<TCopy>(object source)
        {
            if (source == default) return default;
            return JsonConvert.DeserializeObject<TCopy>(JsonConvert.SerializeObject(source));
        }
    }
}
