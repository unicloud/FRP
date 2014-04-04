#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：MaintainCtrlLine
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
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    ///     MaintainCtrl聚合根。
    ///     维修控制明细
    /// </summary>
    public class MaintainCtrlLine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal MaintainCtrlLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     基准间隔
        /// </summary>
        public int StandardInterval { get; private set; }

        /// <summary>
        ///     最大间隔
        /// </summary>
        public int MaxInterval { get; private set; }

        /// <summary>
        ///     最小间隔
        /// </summary>
        public int MinInterval { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     控制单位Id
        /// </summary>
        public int CtrlUnitId { get; private set; }

        /// <summary>
        ///     维修工作Id
        /// </summary>
        public int MaintainWorkId { get; private set; }

        /// <summary>
        ///     维修控制组Id
        /// </summary>
        public int MaintainCtrlId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     维修控制单位
        /// </summary>
        public CtrlUnit CtrlUnit { get; set; }

        /// <summary>
        ///     维修工作
        /// </summary>
        public MaintainWork MaintainWork { get; set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置基准间隔
        /// </summary>
        /// <param name="standardInterval">基准间隔</param>
        public void SetStandardInterval(int standardInterval)
        {
            if (standardInterval == 0)
            {
                throw new ArgumentException("基准间隔参数为空！");
            }

            StandardInterval = standardInterval;
        }

        /// <summary>
        ///     设置最大间隔
        /// </summary>
        /// <param name="maxInterval">最大间隔</param>
        public void SetMaxInterval(int maxInterval)
        {
            if (maxInterval == 0)
            {
                throw new ArgumentException("最大间隔参数为空！");
            }

            MaxInterval = maxInterval;
        }

        /// <summary>
        ///     设置最小间隔
        /// </summary>
        /// <param name="minInterval">最小间隔</param>
        public void SetMinInterval(int minInterval)
        {
            if (minInterval == 0)
            {
                throw new ArgumentException("最小间隔参数为空！");
            }

            MinInterval = minInterval;
        }

        /// <summary>
        ///     设置维修控制单位
        /// </summary>
        /// <param name="ctrlUnit">维修控制单位</param>
        public void SetCtrlUnit(CtrlUnit ctrlUnit)
        {
            if (ctrlUnit == null || ctrlUnit.IsTransient())
            {
                throw new ArgumentException("维修控制单位参数为空！");
            }

            CtrlUnit = ctrlUnit;
            CtrlUnitId = ctrlUnit.Id;
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

            MaintainWorkId = maintainWork.Id;
            MaintainWork = maintainWork;
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