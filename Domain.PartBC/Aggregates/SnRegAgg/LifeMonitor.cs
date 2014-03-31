#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：LifeMonitor
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    ///     LifeMonitor聚合根。
    ///     到寿监控
    /// </summary>
    public class LifeMonitor : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal LifeMonitor()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     工作代码
        /// </summary>
        public string WorkCode { get; private set; }

        /// <summary>
        ///     预计到寿开始
        /// </summary>
        public DateTime MointorStart { get; private set; }

        /// <summary>
        ///     预计到寿结束
        /// </summary>
        public DateTime MointorEnd { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     维修工作外键
        /// </summary>
        public int MaintainWorkId { get; private set; }

        /// <summary>
        ///     序号外键
        /// </summary>
        public int SnRegId { get; internal set; }

        #endregion

        #region 导航属性

        public MaintainWork MaintainWork { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置到寿期间
        /// </summary>
        /// <param name="start">预计到寿开始</param>
        /// <param name="end">预计到寿期限</param>
        public void SetMointorPeriod(DateTime start,DateTime end)
        {
            MointorStart = start;
            MointorEnd = end;
        }

        /// <summary>
        ///     设置维修工作
        /// </summary>
        /// <param name="maintainWork">维修工作</param>
        public void SetMaintainWork(MaintainWork maintainWork)
        {
            if (maintainWork == null || maintainWork.IsTransient())
            {
                throw new ArgumentException("维修工作参数为空！");
            }

            MaintainWork = maintainWork;
            WorkCode = maintainWork.WorkCode;
            MaintainWorkId = maintainWork.Id;
        }

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