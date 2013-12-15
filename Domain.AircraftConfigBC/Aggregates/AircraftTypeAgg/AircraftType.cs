#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：Domain.AircraftConfigBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg
{
    /// <summary>
    ///     机型聚合根
    /// </summary>
    public class AircraftType : EntityGuid
    {
        #region 私有字段

        private HashSet<Aircraft> _aircrafts;

        #endregion

        #region 属性

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     飞机集合
        /// </summary>
        public virtual ICollection<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = new HashSet<Aircraft>()); }
            set { _aircrafts = new HashSet<Aircraft>(value); }
        }

        #endregion

        #region 操作

        #endregion
    }
}