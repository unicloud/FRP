#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 10:10:40

// 文件名：AircraftType
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Domain.PartBC.Aggregates.AircraftSeriesAgg;

namespace UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg
{
    /// <summary>
    /// AircraftType聚合根。
    /// 飞机机型
    /// </summary>
    public class AircraftType : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; protected set; }

        #endregion

        #region 外键属性
        /// <summary>
        /// 系列Id
        /// </summary>
        public Guid AircraftSeriesId { get; protected set; }
        #endregion

        #region 导航属性
        public AircraftSeries AircraftSeries { get; set; }
        #endregion

        #region 操作

        #endregion

    }
}

