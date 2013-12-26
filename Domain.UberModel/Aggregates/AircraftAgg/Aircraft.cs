#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 文件名：Aircraft.cs
// 程序集：UniCloud.Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机聚合根
    /// </summary>
    public class Aircraft : EntityGuid
    {
        #region 属性

        /// <summary>
        ///     机号
        /// </summary>
        public string RegNumber { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     机型
        /// </summary>
        public virtual AircraftType AircraftType { get; set; }

        #endregion

        #region 操作

        #endregion
    }
}