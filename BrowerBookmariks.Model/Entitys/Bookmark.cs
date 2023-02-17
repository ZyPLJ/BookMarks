using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowerBookmariks.Model.Entitys
{
    /// <summary>
    /// 书签类
    /// 用于解析浏览器书签
    /// 默认通过id排序
    /// </summary>
    public class Bookmark
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 书签名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 当前分级
        /// </summary>
        public int Children { get; set; }
        /// <summary>
        /// 当前分级的目录id
        /// </summary>
        public int classificationid { get; set; }
        /// <summary>
        /// 导航属性
        /// </summary>
        public Classification? classification { get; set; }
    }
}
