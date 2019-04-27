using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Models.MainDb
{
    public class UserInfoView : UserInfoModel
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Roles => RolesString.Split(',').ToList();
    }
}
