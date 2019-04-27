using System.Collections.Generic;
using Models.MainDb;

namespace Models.MainDbModel
{
    public class RoutesView : RoutesModel
    {
        public List<RoutesView> Children { get; set; }

        /// <summary>当设置 true 的时候该路由不会再侧边栏出现</summary>
        public bool Hidden => HiddenInt == 1;

        public MetaView Meta => new MetaView
                                {
                                    Roles = Roles.Split(','),
                                    Title = Title,
                                    Breadcrumb = BreadcrumbInt == 1,
                                    Icon = Icon
                                };
    }

    public class MetaView
    {
        public string[] Roles { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }

        /// <summary> 如果设置为false，则不会在breadcrumb面包屑中显示</summary>
        public bool Breadcrumb { get; set; }
    }
}
