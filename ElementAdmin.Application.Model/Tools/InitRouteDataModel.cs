using System.Collections.Generic;

namespace ElementAdmin.Application.Model.Tools
{
    public class RouteDataModel
    {
        public string Name { get; set; }
        public RouteDataMetaModel Meta { get; set; }
        public string Path { get; set; }
        public string Redirect { get; set; }
        public RouteDataModel[] Children { get; set; }
        public int? Sort { get; set; }
    }

    public class RouteDataMetaModel
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool? NoCache { get; set; }
        public string ActiveMenu { get; set; }
    }
}
