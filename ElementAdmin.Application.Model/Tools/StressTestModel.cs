using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElementAdmin.Infrastructure;

namespace ElementAdmin.Application.Model.Tools
{
    public class StressTestModel
    {
        private readonly string _regex = @"<#\w+/>";

        /// <summary>
        /// 记录获取动态参数的下标，当不是混淆取值的时候，这个值生效
        /// </summary>
        private int _dynamicIndex = -1;

        /// <summary>
        /// 混淆动态值（意味着将错乱使用动态参数的值，如果你的动态值是互相依赖的则不要使用这个属性）
        /// </summary>
        /// <value></value>
        public bool MixDynamic { get; set; } = false;

        /// <summary>
        /// Url
        /// </summary>
        /// <value></value>
        public string Url { get; set; }

        /// <summary>
        /// 获取Url(如果有参数，将会随机生成)
        /// </summary>
        /// <returns></returns>
        public string GetUrl()
        {
            return GetString(Url);
        }

        /// <summary>
        /// 方法（get,post,put）
        /// </summary>
        /// <value></value>
        public string Method { get; set; }

        /// <summary>
        /// 链接Id（唯一标识符）
        /// </summary>
        /// <value></value>
        public string ConnectedId { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        /// <value></value>
        public StressTestKeyValue[] Headers { get; set; }

        /// <summary>
        /// 获取Header(如果有参数，将会随机生成)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetHeaders()
        {
            return GetKeyValue(Headers);
        }

        /// <summary>
        /// 键值对的body内容
        /// </summary>
        /// <value></value>
        public StressTestKeyValue[] Body { get; set; }

        /// <summary>
        /// 获取键值对的Body(如果有参数，将会随机生成)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> GetBody()
        {
            return GetKeyValue(Body);
        }

        /// <summary>
        /// json格式的body内容
        /// </summary>
        /// <value></value>
        public string BodyJson { get; set; }

        /// <summary>
        /// 获取json合适的body(如果有参数，将会随机生成)
        /// </summary>
        /// <returns></returns>
        public string GetBodyJson()
        {
            BodyJson = BodyJson.Replace("\\n", "").Replace("\\", "").Trim('\"');
            return GetString(BodyJson);
        }

        /// <summary>
        /// json格式的动态参数
        /// </summary>
        /// <value></value>
        public string DynamicJson { get; set; }

        /// <summary>
        /// 序列化之后的动态参数
        /// </summary>
        /// <value></value>
        public Dictionary<string, object>[] DynamicObject { get; private set; }

        /// <summary>
        /// 线程数
        /// </summary>
        /// <value></value>
        public int Thread { get; set; } = 10;

        /// <summary>
        /// 间隔时间
        /// </summary>
        /// <value></value>
        public int Delay { get; set; } = 1000;

        /// <summary>
        /// 循环数
        /// </summary>
        /// <value></value>
        public int Cycle { get; set; } = 10;

        /// <summary>
        /// 验证消息
        /// </summary>
        /// <value></value>
        public string VerifyMessage { get; set; }

        /// <summary>
        /// 验证开始压测参数
        /// </summary>
        /// <returns></returns>
        public bool VerifyStart()
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                VerifyMessage = "Url不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(Method))
            {
                VerifyMessage = "Method不能为空";
                return false;
            }
            if (string.IsNullOrWhiteSpace(ConnectedId))
            {
                VerifyMessage = "ConnectedId不能为空";
                return false;
            }
            if (!new[] { "get", "post", "put" }.Contains(Method))
            {
                VerifyMessage = "Method值错误";
                return false;
            }
            if (Method == "post" || Method == "put")
            {
                if (string.IsNullOrWhiteSpace(BodyJson) && (Body?.Length ?? 0) < 1)
                {
                    VerifyMessage = "post/put必须填充body";
                    return false;
                }
            }
            DynamicJson = DynamicJson.Replace("\\n", "").Replace("\\", "").Trim('\"');
            DynamicObject = DynamicJson.ToObjectByJson<Dictionary<string, object>[]>();

            var match = Regex.Match(Url, _regex);
            if (match.Success)
            {
                if (string.IsNullOrWhiteSpace(DynamicJson))
                {
                    VerifyMessage = "Url中设置动态参数，但是没有传入动态参数";
                    return false;
                }

                foreach (var item in match.Groups)
                {
                    var key = item.ToString().Replace("<#", "").Replace("/>", "");
                    if (!DynamicObject.All(x => x.ContainsKey(key))) // 任何不包含key
                    {
                        VerifyMessage = "Url中设置动态参数，但是部分动态参数没有在json中声明";
                        return false;
                    }
                }
            }

            if (!VerifyKeyValue(Headers, nameof(Headers)))
                return false;
            if (!VerifyKeyValue(Body, nameof(Body)))
                return false;

            return true;
        }

        private bool VerifyKeyValue(StressTestKeyValue[] keyValue, string nameof)
        {
            Match match;

            if (keyValue?.Any() ?? false)
            {
                foreach (var item in keyValue)
                {
                    if (string.IsNullOrWhiteSpace(DynamicJson))
                    {
                        VerifyMessage = nameof + "中设置动态参数，但是没有传入动态参数";
                        return false;
                    }
                    match = Regex.Match(item.Value, _regex);
                    if (match.Success)
                    {
                        var key = match.Value.Replace("<#", "").Replace("/>", "");
                        if (!DynamicObject.All(x => x.ContainsKey(key))) // 任何不包含key
                        {
                            VerifyMessage = nameof + "中设置动态参数，但是部分动态参数没有在json中声明";
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private Dictionary<string, object> GetKeyValue(StressTestKeyValue[] keyValue)
        {
            if (keyValue == null || !keyValue.Any()) return null;
            var result = new Dictionary<string, object>();
            foreach (var item in keyValue)
            {
                var regex = Regex.Match(item.Value, _regex);
                if (regex.Success)
                {
                    var key = item.ToString().Replace("<#", "").Replace("/>", "");
                    if (_dynamicIndex == -1 || MixDynamic)
                    {
                        _dynamicIndex = new Random(Guid.NewGuid().ToInt()).Next(0, DynamicObject.Length);
                    }

                    result.TryAdd(item.Key, item.Value.Replace(item.ToString(), DynamicObject[_dynamicIndex][key].ToString()));
                }
                else
                {
                    result.TryAdd(item.Key, item.Value);
                }
            }
            return result;
        }
        private string GetString(string @sting)
        {
            var regex = Regex.Match(@sting, _regex);
            if (regex.Success)
            {
                var temp = @sting;
                foreach (var item in regex.Groups)
                {
                    var key = item.ToString().Replace("<#", "").Replace("/>", "");
                    if (_dynamicIndex == -1 || MixDynamic)
                    {
                        _dynamicIndex = new Random(Guid.NewGuid().ToInt()).Next(0, DynamicObject.Length);
                    }
                    temp = temp.Replace(item.ToString(), DynamicObject[_dynamicIndex][key].ToString());
                }
                return temp;
            }
            else
            {
                return @sting;
            }
        }
    }


    public class StressTestKeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}