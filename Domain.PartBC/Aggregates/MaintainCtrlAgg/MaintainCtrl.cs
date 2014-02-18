#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：MaintainCtrl
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg
{
    /// <summary>
    /// SnReg聚合根。
    /// MaintainCtrl
    /// 维修控制组
    /// </summary>
    public class MaintainCtrl : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<MaintainCtrlLine> _maintainCtrlLines;

        #endregion

        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal MaintainCtrl()
        {
        }
        #endregion

        #region 属性

        /// <summary>
        /// 控制策略
        /// </summary>
        public int CtrlStrategy
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        /// 维修控制明细
        /// </summary>
        public virtual ICollection<MaintainCtrlLine> MaintainCtrlLines
        {
            get { return _maintainCtrlLines ?? (_maintainCtrlLines = new HashSet<MaintainCtrlLine>()); }
            set { _maintainCtrlLines = new HashSet<MaintainCtrlLine>(value); }
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
