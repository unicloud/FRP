#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 11:41:56
// 文件名：AircraftConfiguration
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     飞机配置聚合根
    /// </summary>
    public class AircraftConfiguration : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftConfiguration()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 配置代码
        /// </summary>
        public string ConfigCode { get; protected set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; protected set; }

     
        #endregion

        #region 外键属性
       
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
