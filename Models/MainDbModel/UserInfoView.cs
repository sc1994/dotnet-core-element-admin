using System.Collections.Generic;
using System.Linq;

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
