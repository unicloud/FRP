#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:23:21

// 文件名：SnReg
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.UberModel.Aggregates.SnRegAgg
{
    /// <summary>
    /// SnReg聚合根。
    /// </summary>
    public class SnReg : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<SnHistory> _snHistories;

        private HashSet<LifeMonitor> _lifeMonitors;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal SnReg()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            set;
        }

        /// <summary>
        /// 初始安装日期
        /// </summary>
        public DateTime InstallDate
        {
            get;
            set;
        }

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
        {
            get;
            set;
        }

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 附件Id
        /// </summary>
        public int PnRegId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前飞机Id
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        /// 到寿监控集合
        /// </summary>
        public virtual ICollection<LifeMonitor> LifeMonitors
        {
            get { return _lifeMonitors ?? (_lifeMonitors = new HashSet<LifeMonitor>()); }
            set { _lifeMonitors = new HashSet<LifeMonitor>(value); }
        }

        /// <summary>
        /// 装机历史
        /// </summary>
        public virtual ICollection<SnHistory> SnHistories
        {
            get { return _snHistories ?? (_snHistories = new HashSet<SnHistory>()); }
            set { _snHistories = new HashSet<SnHistory>(value); }
        }
        #endregion

        #region 操作

        #endregion

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
