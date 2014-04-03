#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 11:39:33
// 文件名：AircraftCabinType
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 11:39:33
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg
{
    /// <summary>
    ///     飞机舱位类型聚合根
    /// </summary>
    public class AircraftCabinType : EntityInt, IValidatableObject
    {
          #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftCabinType()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 舱位类型
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; internal set; }
        #endregion

        #region 外键属性
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
