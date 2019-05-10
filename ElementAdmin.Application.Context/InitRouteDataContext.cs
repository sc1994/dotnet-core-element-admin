using System.Collections.Generic;

namespace ElementAdmin.Domain.Context
{
    public class InitRouteDataContext
    {
        public string Name { get; set; }
        public InitRouteDataMeta Meta { get; set; }
        public string Path { get; set; }
        public string Redirect { get; set; }
        public List<InitRouteDataContext> Children { get; set; }
        public int? Sort { get; set; }
    }

    public class InitRouteDataMeta
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public bool? NoCache { get; set; }
        public string ActiveMenu { get; set; }
    }

}