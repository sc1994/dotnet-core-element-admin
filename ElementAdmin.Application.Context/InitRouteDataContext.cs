using System.Collections.Generic;

namespace ElementAdmin.Domain.Context
{
    public class InitRouteDataContext
    {
        public string name { get; set; }
        public InitRouteDataMeta meta { get; set; }
        public string path { get; set; }
        public string redirect { get; set; }
        public List<InitRouteDataContext> children { get; set; }


    }

    public class InitRouteDataMeta
    {
        public string title { get; set; }
        public string icon { get; set; }
        public bool? noCache { get; set; }
        public string activeMenu { get; set; }
    }

}
