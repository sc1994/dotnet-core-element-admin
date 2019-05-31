using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ElementAdmin.Infrastructure;

namespace ElementAdmin.Application.Model.Tools
{
    public class StressTestModel
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string ConnectedId { get; set; }
        public StressTestKeyValue[] Headers { get; set; }
        public StressTestKeyValue[] Body { get; set; }
        public string BodyJson { get; set; }
        public string DynamicJson { get; set; }
        public Dictionary<string, object>[] Dynamic { get; set; }
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
            Dynamic = DynamicJson.ToObjectByJson<Dictionary<string, object>[]>();

            var regex = @"<#\w+/>";
            var match = Regex.Match(Url, regex);
            if (match.Success)
            {
                if (string.IsNullOrWhiteSpace(DynamicJson))
                {
                    VerifyMessage = "设置了动态参数，但是没有传入动态参数";
                    return false;
                }

                foreach (var item in match.Groups)
                {
                    var key = item.ToString().Replace("<#", "").Replace("/>", "");
                    if (Dynamic.Any(x => !x.ContainsKey(key))) // 任何不包含key
                    {
                        VerifyMessage = "设置了动态参数，但是部分动态参数没有在json中声明";
                        return false;
                    }
                }
            }
            if (Headers?.Any() ?? false)
            {
                
            }

            return true;
        }
    }


    public class StressTestKeyValue
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}