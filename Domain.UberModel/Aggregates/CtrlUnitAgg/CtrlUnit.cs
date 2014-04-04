#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：CtrlUnit
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.UberModel.Aggregates.CtrlUnitAgg
{
    /// <summary>
    /// CtrlUnit聚合根。
    /// 维修控制单位
    /// </summary>
    public class CtrlUnit : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal CtrlUnit()
        {
        }
        #endregion

        #region 属性
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description
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
        ///     设置名称
        /// </summary>
        /// <param name="name">名称</param>
        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("名称参数为空！");
            }

            Name = name;
        }

        /// <summary>
        ///     设置描述
        /// </summary>
        /// <param name="description">描述</param>
        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("描述参数为空！");
            }

            Description = description;
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
