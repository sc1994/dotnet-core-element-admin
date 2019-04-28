using System.Collections.Generic;

namespace Models
{
    public class RoutesModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Hidden { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ChildrenItem> Children { get; set; }
    }

  

    public class ChildrenItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Component { get; set; }
    }
}
