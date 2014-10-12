#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:07:29
// 文件名：Ata
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:07:29
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AtaAgg
{
    public class Ata : EntityInt
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Ata()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 章节
        /// </summary>
        public string ATA { get; internal set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 子章节集合
        /// </summary>
        private  HashSet<Ata> _atas; 
        public virtual ICollection<Ata> ChildAtas
        {
            get { return _atas ?? (_atas = new HashSet<Ata>()); }
            set { _atas = new HashSet<Ata>(value); }
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 父章节外键
        /// </summary>
        public int? ParentId { get; internal set; }

        /// <summary>
        /// 系列外键
        /// </summary>
        public Guid AircraftSeriesId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 父章节
        /// </summary>
        public virtual Ata ParentAta { get; internal set; }

        /// <summary>
        /// 系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; internal set; }
        #endregion

    }
}
