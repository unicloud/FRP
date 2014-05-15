#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 17:33:35
// 文件名：Document
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 17:33:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档聚合根
    /// </summary>
    public class Document : EntityGuid, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Document()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     文档
        /// </summary>
        public byte[] FileStorage { get; internal set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

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
