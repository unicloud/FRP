#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:13:20
// 文件名：AirProgramming
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     航空公司五年规划聚合根
    /// </summary>
    public class AirProgramming : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AirProgramming()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     规划名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime? CreateDate { get; internal set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime? IssuedDate { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; private set; }



        #endregion

        #region 外键属性
        /// <summary>
        ///     规划期间
        /// </summary>
        public Guid ProgrammingID { get; private set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentID { get; private set; }

        /// <summary>
        ///   发文单位
        /// </summary>
        public Guid IssuedUnitID { get; private set; }

        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
