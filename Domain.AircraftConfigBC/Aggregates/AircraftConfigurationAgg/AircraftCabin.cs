#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 11:39:33
// 文件名：AircraftCabin
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 11:39:33
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     舱位聚合根
    /// </summary>
    public class AircraftCabin : EntityInt, IValidatableObject
    {
          #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftCabin()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 舱位类型
        /// </summary>
        public AircraftCabinType AircraftCabinType { get; internal set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatNumber { get; internal set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; internal set; }
        #endregion

        #region 外键属性
        /// <summary>
        /// 飞机配置
        /// </summary>
        public int AircraftConfiguratonId { get; internal set; }
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
