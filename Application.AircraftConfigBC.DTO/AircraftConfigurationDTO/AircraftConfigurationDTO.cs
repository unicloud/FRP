#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 13:56:35
// 文件名：AircraftConfigurationDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 13:56:35
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Collections.Generic;
using System.Data.Services.Common;

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 飞机配置
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftConfigurationDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 配置代码
        /// </summary>
        public string ConfigCode { get;  set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get;  set; }

        /// <summary>
        /// 飞机最大滑行重量
        /// </summary>
        public decimal MTW { get;  set; }

        /// <summary>
        /// 基本重量
        /// </summary>
        public decimal BW { get;  set; }

        /// <summary>
        /// 基本重量重心
        /// </summary>
        public decimal BWF { get;  set; }

        /// <summary>
        /// 基本重量指数
        /// </summary>
        public decimal BWI { get;  set; }

        /// <summary>
        /// 基本空重
        /// </summary>
        public decimal BEW { get;  set; }

        /// <summary>
        /// 飞机最大起飞重量
        /// </summary>
        public decimal MTOW { get;  set; }

        /// <summary>
        /// 飞机最大着陆重量
        /// </summary>
        public decimal MLW { get;  set; }

        /// <summary>
        /// 飞机最大零燃油重量
        /// </summary>
        public decimal MZFW { get;  set; }

        /// <summary>
        /// 飞机最大可用燃油
        /// </summary>
        public decimal MMFW { get;  set; }

        /// <summary>
        /// 飞机最大商载
        /// </summary>
        public decimal MCC { get;  set; }

        /// <summary>
        /// 座位布局图
        /// </summary>
        public byte[] FileContent { get;  set; }

        /// <summary>
        /// 座位布局图名字
        /// </summary>
        public string FileName { get;  set; }

        private List<AircraftCabinDTO> _aircraftCabins;
        public List<AircraftCabinDTO> AircraftCabins
        {
            get { return _aircraftCabins ?? new List<AircraftCabinDTO>(); }
            set { _aircraftCabins = value; }
        }
        #endregion

        #region 外键属性
        /// <summary>
        /// 机型
        /// </summary>
        public Guid AircraftTypeId { get;  set; }

        /// <summary>
        /// 系列
        /// </summary>
        public Guid AircraftSeriesId { get;  set; }
        #endregion
    }
}
