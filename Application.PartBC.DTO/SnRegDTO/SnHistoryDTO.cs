#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnHistoryDTO
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
    /// SnHistory
    /// </summary>
    [DataServiceKey("Id")]
    public class SnHistoryDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Sn
        {
            get;
            set;
        }

        /// <summary>
        /// 装上时间
        /// </summary>
        public DateTime InstallDate
        {
            get;
            set;
        }

        /// <summary>
        /// 拆下时间
        /// </summary>
        public DateTime? RemoveDate
        {
            get;
            set;
        }

        /// <summary>
        /// FI号
        /// </summary>
        public string FiNumber
        {
            get;
            set;
        }

        /// <summary>
        /// CSN
        /// </summary>
        public string CSN
        {
            get;
            set;
        }

        /// <summary>
        /// CSR
        /// </summary>
        public string CSR
        {
            get;
            set;
        }

        /// <summary>
        /// TSN
        /// </summary>
        public string TSN
        {
            get;
            set;
        }

        /// <summary>
        /// TSR
        /// </summary>
        public string TSR
        {
            get;
            set;
        }

         /// <summary>
        /// 飞机注册号
        /// </summary>
        public string AcReg
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

        /// <summary>
        /// Sn外键
        /// </summary>
        public int SnRegId
        {
            get;
            set;
        }
        #endregion

    }
}
