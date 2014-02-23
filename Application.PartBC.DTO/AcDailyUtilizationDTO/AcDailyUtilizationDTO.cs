#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：AcDailyUtilizationDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
using System.Collections.Generic;
using System.Data.Services.Common;
#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AcDailyUtilization
    /// </summary>
    [DataServiceKey("Id")]
    public class AcDailyUtilizationDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string RegNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 计算日利用率
        /// </summary>
        public decimal CalculatedValue
        {
            get;
            set;
        }

        /// <summary>
        /// 修正日利用率
        /// </summary>
        public decimal AmendValue
        {
            get;
            set;
        }

        /// <summary>
        /// 年度
        /// </summary>
        public int Year
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month
        {
            get;
            set;
        }

        /// <summary>
        /// 是否当前
        /// </summary>
        public bool IsCurrent
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }
        #endregion

    }
}
