﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 16:45:15
// 文件名：EngineType
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.EngineTypeAgg
{
    /// <summary>
    ///     发动机型号聚合根
    /// </summary>
    public class EngineType : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineType()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     发动机型号名称
        /// </summary>
        public string Name { get; protected set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///    制造商
        /// </summary>
        public Guid ManufacturerId { get; protected set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///   制造商
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; }


        #endregion

        #region 操作



        #endregion
    }
}
