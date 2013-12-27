#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:19:25
// 文件名：Manufacturer
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg
{
    /// <summary>
    ///     制造商聚合根
    /// </summary>
    public class Manufacturer : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Manufacturer()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// 制造商名称
        /// </summary>
        public string Name { get; protected set; }
        
        /// <summary>
        /// 制造商类型 1表示飞机制造商，2表示发动机制造商
        /// </summary>
        public int Type { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        /// Owner外键
        /// </summary>
        public Guid OwnerID { get; private set; }

        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
