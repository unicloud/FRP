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
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

namespace UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg
{
    /// <summary>
    /// PnReg聚合根。
    /// Dependency非聚合根
    /// Dependency
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
        /// 依赖项件号
        /// </summary>
        public string Pn
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 装机控制Id
        /// </summary>
        public int InstallControllerId
        {
            get;
            internal set;
        }

        /// <summary>
        /// 依赖项附件Id
        /// </summary>
        public int DependencyPnId
        {
            get;
            private set;
        }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置依赖项
        /// </summary>
        /// <param name="dependencyPnReg">依赖项附件</param>
        public void SetPnReg(PnReg dependencyPnReg)
        {
            if (dependencyPnReg == null || dependencyPnReg.IsTransient())
            {
                throw new ArgumentException("依赖项附件参数为空！");
            }

            Pn = dependencyPnReg.Pn;
            DependencyPnId = dependencyPnReg.Id;
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
