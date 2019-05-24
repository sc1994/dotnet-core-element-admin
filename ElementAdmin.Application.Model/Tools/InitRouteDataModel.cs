using System.Collections.Generic;

namespace ElementAdmin.Application.Model.Tools
{
    public class InitRouteDataModel
    {
        public string Name { get; set; }
        public InitRouteDataMetaModel Meta { get; set; }
        public string Path { get; set; }
        public string Redirect { get; set; }
        public InitRouteDataModel[] Children { get; set; }
        public int? Sort { get; set; }
    }

    public class InitRouteDataMetaModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool? NoCache { get; set; }
        public string ActiveMenu { get; set; }
    }
}
