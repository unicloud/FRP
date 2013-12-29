#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:57:54
// 文件名：Programming
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ProgrammingAgg
{
    /// <summary>
    ///     规划期间聚合根
    /// </summary>
    public class Programming : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Programming()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     规划期间
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     规划开始时间
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        ///     规划结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        #endregion

        #region 外键属性



        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
