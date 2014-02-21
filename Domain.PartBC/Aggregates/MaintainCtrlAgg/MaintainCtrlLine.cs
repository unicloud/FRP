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

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    /// MaintainCtrl聚合根。
    /// MaintainCtrlLine
    /// 维修控制明细
    /// </summary>
    public class MaintainCtrlLine : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal MaintainCtrlLine()
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// 基准间隔
        /// </summary>
        public string StandardInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 最大间隔
        /// </summary>
        public string MaxInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 最小间隔
        /// </summary>
        public string MinInterval
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 控制单位Id
        /// </summary>
        public int CtrlUnitId
        {
            get;
            set;
        }

        /// <summary>
        /// 维修工作Id
        /// </summary>
        public int MaintainWorkId
        {
            get;
            set;
        }

        /// <summary>
        /// 维修控制组Id
        /// </summary>
        public int MaintainCtrlId
        {
            get;
            set;
        }
        #endregion

        #region 导航属性

        /// <summary>
        /// 维修控制单位
        /// </summary>
        public CtrlUnit CtrlUnit
        {
            get;
            set;
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
