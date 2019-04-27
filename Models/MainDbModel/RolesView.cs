using System.Collections.Generic;
using Models.MainDb;

namespace Models.MainDbModel
{
    public class RolesView : RolesModel
    {
        public List<RoutesView> Routes { get; set; }
    }
}
