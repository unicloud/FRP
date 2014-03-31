#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 18:16:34

// 文件名：Dependency
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.PnRegAgg;

namespace UniCloud.Domain.UberModel.Aggregates.TechnicalSolutionAgg
{
    /// <summary>
    /// Dependency聚合根。
    /// </summary>
    public class Dependency : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal Dependency()
        {
        }
        #endregion

        #region 属性

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 附件Id
        /// </summary>
        public int PnRegId
        {
            get;
            private set;
        }

        /// <summary>
        /// 技术解决方案明细外键
        /// </summary>
        public int TsLineId
        {
            get;
            internal set;
        }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置附件
        /// </summary>
        /// <param name="pnReg">附件</param>
        public void SetPnReg(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("附件参数为空！");
            }

            Pn = pnReg.Pn;
            PnRegId = pnReg.Id;
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
