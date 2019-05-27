﻿using System;
using Newtonsoft.Json;

namespace ElementAdmin.Infrastructure
{
    public static class ConvertHelper
    {
        public static string ToJson<T>(this T that, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(that, formatting);
        }

        public static T ToObjectByJson<T>(this string that)
        {
            return JsonConvert.DeserializeObject<T>(that);
        }

        public static long ToLong(this Guid that)
        {
            var buffer = that.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }
}
