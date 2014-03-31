#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 9:32:33

// 文件名：Mod
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.PartBC.Aggregates.ModAgg
{
    /// <summary>
    /// Mod聚合根。
    /// </summary>
    public class Mod : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Mod()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        /// MOD号
        /// </summary>
        public string ModNumber
        {
            get;
            private set;
        }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作
        /// <summary>
        ///     设置MOD号
        /// </summary>
        /// <param name="modNumber">MOD号</param>
        public void SetModNumber(string modNumber)
        {
            if (string.IsNullOrWhiteSpace(modNumber))
            {
                throw new ArgumentException("MOD号参数为空！");
            }

            ModNumber = modNumber;
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
