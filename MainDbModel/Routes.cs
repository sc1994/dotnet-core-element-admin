// =============系统自动生成=============
// 时间：2019/5/6 17:47
// 备注：表字段对应的数据模型。请勿在此文件中变动代码。
// =============系统自动生成=============
// ReSharper disable InconsistentNaming

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace .MainDb
{
    /// <summary></summary>
    [Table("Routes")]
    public class RoutesModel
    {
        /// <summary>设置true则固定再tag试图中不可删除</summary>
        public int AffixInt { get; set; }

        /// <summary>如果设置为false，则不会在breadcrumb面包屑中显示</summary>
        public int BreadcrumbInt { get; set; }

        /// <summary>文件位置</summary>
        public string Component { get; set; }

        /// <summary>当设置 true 的时候该路由不会再侧边栏出现</summary>
        public int HiddenInt { get; set; }

        /// <summary>设置该路由的图标</summary>
        public string Icon { get; set; }

        /// <summary>主键</summary>
        public int Id { get; set; }

        /// <summary>名称</summary>
        public string Name { get; set; }

        /// <summary>父级Id</summary>
        public int ParentId { get; set; }

        /// <summary>url路径</summary>
        public string Path { get; set; }

        /// <summary>设置了面包屑的位置，当设置 noredirect 的时候该路由在面包屑导航中不可被点击</summary>
        public string Redirect { get; set; }

        /// <summary>设置该路由在侧边栏和面包屑中展示的名字</summary>
        public string Title { get; set; }
    }
}
