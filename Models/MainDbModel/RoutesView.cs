using System.Collections.Generic;
using Models.MainDb;

namespace Models.MainDbModel
{
    public class RoutesView : RoutesModel
    {
        public List<RoutesView> Children { get; set; }

        /// <summary>当设置 true 的时候该路由不会再侧边栏出现</summary>
        public bool Hidden => HiddenInt == 1;

        /// <summary> 如果设置为false，则不会在breadcrumb面包屑中显示</summary>
        public bool Breadcrumb => BreadcrumbInt == 1;
    }
}
