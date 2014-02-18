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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.PartBC.Aggregates.SnRegAgg
{
    /// <summary>
    /// LifeMonitor聚合根。
    /// 到寿监控
    /// </summary>
    public class LifeMonitor : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal LifeMonitor()
        {
        }
        #endregion

        #region 属性

        /// <summary>
        /// 工作代码
        /// </summary>
        public string WorkCode
        {
            get;
            set;
        }

        /// <summary>
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            set;
        }

        /// <summary>
        /// 预计到寿开始
        /// </summary>
        public DateTime MointorStart
        {
            get;
            set;
        }

        /// <summary>
        /// 预计到寿期限
        /// </summary>
        public string LifeTimeLimit
        {
            get;
            set;
        }
        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

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
