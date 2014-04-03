#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:40
// 文件名：AircraftConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg
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
        public string ConfigCode { get; internal set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// 飞机最大滑行重量
        /// </summary>
        public decimal MTW { get; internal set; }

        /// <summary>
        /// 基本重量
        /// </summary>
        public decimal BW { get; internal set; }

        /// <summary>
        /// 基本重量重心
        /// </summary>
        public decimal BWF { get; internal set; }

        /// <summary>
        /// 基本重量指数
        /// </summary>
        public decimal BWI { get; internal set; }

        /// <summary>
        /// 基本空重
        /// </summary>
        public decimal BEW { get; internal set; }

        /// <summary>
        /// 飞机最大起飞重量
        /// </summary>
        public decimal MTOW { get; internal set; }

        /// <summary>
        /// 飞机最大着陆重量
        /// </summary>
        public decimal MLW { get; internal set; }

        /// <summary>
        /// 飞机最大零燃油重量
        /// </summary>
        public decimal MZFW { get; internal set; }

        /// <summary>
        /// 飞机最大可用燃油
        /// </summary>
        public decimal MMFW { get; internal set; }

        /// <summary>
        /// 飞机最大商载
        /// </summary>
        public decimal MCC { get; internal set; }

        /// <summary>
        /// 座位布局图
        /// </summary>
        public byte[] FileContent { get; internal set; }

        /// <summary>
        /// 座位布局图名字
        /// </summary>
        public string FileName { get; internal set; }
        #endregion

        #region 外键属性
        /// <summary>
        /// 机型
        /// </summary>
        public Guid AircraftTypeId { get; internal set; }

        /// <summary>
        /// 系列
        /// </summary>
        public Guid AircraftSeriesId { get; internal set; }
        #endregion

        #region 导航属性
        /// <summary>
        /// 机型
        /// </summary>
        public virtual AircraftType AircraftType { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public virtual AircraftSeries AircraftSeries { get; set; }

        /// <summary>
        ///   座位布局
        /// </summary>
        private HashSet<AircraftCabin> _aircraftCabins;
        public virtual ICollection<AircraftCabin> AircraftCabins
        {
            get { return _aircraftCabins ?? (_aircraftCabins = new HashSet<AircraftCabin>()); }
            set { _aircraftCabins = new HashSet<AircraftCabin>(value); }
        }
        #endregion

        #region 操作
        /// <summary>
        /// 新增舱位
        /// </summary>
        /// <returns></returns>
        public AircraftCabin AddNewAircraftCabin()
        {
            var aircraftCabin = new AircraftCabin
            {
                AircraftConfiguratonId = Id,
            };

            aircraftCabin.GenerateNewIdentity();
            AircraftCabins.Add(aircraftCabin);

            return aircraftCabin;
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
