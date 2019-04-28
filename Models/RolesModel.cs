using System.Collections.Generic;


namespace Models
{
    public class RolesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RoutesModel> Routes { get; set; }
    }
}
