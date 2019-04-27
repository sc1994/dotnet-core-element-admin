
using Newtonsoft.Json;

namespace Utils
{
    public class Mapper
    {
        public static TBase ToBase<TBase>(object extend)
        {
            if (extend == null) return default;
            return JsonConvert.DeserializeObject<TBase>(JsonConvert.SerializeObject(extend));
        }

        public static TExtend ToExtend<TExtend>(object @base)
        {
            if (@base == null) return default;
            return JsonConvert.DeserializeObject<TExtend>(JsonConvert.SerializeObject(@base));
        }
    }
}
