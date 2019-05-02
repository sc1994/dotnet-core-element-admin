using Models.MainDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Models.MainDbModel
{
    public class RoutesView : RoutesModel
    {
        public bool? Hidden => HiddenInt == 1;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<RoutesView> Children { get; set; }

        private MetaView _meta;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public MetaView Meta
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Title)
                    || AffixInt == 1
                    || BreadcrumbInt == 0
                    || !string.IsNullOrWhiteSpace(Icon))
                {
                    _meta = new MetaView
                    {
                        Title = Title,
                        Affix = AffixInt == 1,
                        Breadcrumb = BreadcrumbInt == 1,
                        Icon = Icon,
                        Roles = _meta?.Roles
                    };
                    return _meta;
                }
                return null;
            }
            set => _meta = value ?? throw new ArgumentNullException(nameof(value));
        }
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
