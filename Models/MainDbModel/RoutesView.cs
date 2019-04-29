using Models.MainDb;
using System;
using System.Collections.Generic;

namespace Models.MainDbModel
{
    public class RoutesView : RoutesModel
    {
        public bool? Hidden => HiddenInt == 1;
        public List<RoutesView> Children { get; set; }
        public MetaView Meta => new MetaView
        {
            Title = Title,
            Affix = AffixInt == 1,
            Breadcrumb = BreadcrumbInt == 1,
            Icon = Icon,
            Roles = Roles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        };
    }

    public class MetaView
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string[] Roles { get; set; }
        public bool? Affix { get; set; }
        public bool? Breadcrumb { get; set; }
    }

    public class RoutesInitView : RoutesModel
    {
        public bool? Hidden { get; set; }
        public List<RoutesInitView> Children { get; set; }
        public MetaView Meta { get; set; }
    }
}
