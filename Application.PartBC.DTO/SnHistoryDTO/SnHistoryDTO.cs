#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 22:28:13
// 文件名：SnHistoryDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     SnHistory
    /// </summary>
    [DataServiceKey("Id")]
    public class SnHistoryDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     装机序号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     装机件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime ActionDate { get; set; }

        /// <summary>
        /// 操作类型 拆下/装上/不拆换
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        ///     操作原因
        /// </summary>
        public string ActionReason { get; set; }
        
        /// <summary>
        ///     操作指令号
        /// </summary>
        public string ActionNo { get; set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public int CSN { get; set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; set; }

        /// <summary>
        ///     装机机号
        /// </summary>
        public string RegNumber { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; set; }

        /// <summary>
        ///     Sn外键
        /// </summary>
        public int SnRegId { get; set; }

        /// <summary>
        ///     Pn外键
        /// </summary>
        public int PnRegId { get; set; }
        
        /// <summary>
        ///     拆换记录外键
        /// </summary>
        public int? RemInstRecordId { get; set; }
        #endregion
    }
}