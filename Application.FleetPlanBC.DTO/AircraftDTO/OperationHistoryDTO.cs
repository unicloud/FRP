#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:21:41
// 文件名：OperationHistoryDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 运营权历史
    /// </summary>
    [DataServiceKey("OperationHistoryId")]
    public class OperationHistoryDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid OperationHistoryId { get; set; }

        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        ///     运营日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        ///     退出停厂日期
        /// </summary>
        public DateTime? StopDate { get; set; }

        /// <summary>
        ///     技术接收日期
        /// </summary>
        public DateTime? TechReceiptDate { get; set; }

        /// <summary>
        ///     接收日期
        /// </summary>
        public DateTime? ReceiptDate { get; set; }

        /// <summary>
        ///     技术交付日期
        /// </summary>
        public DateTime? TechDeliveryDate { get; set; }

        /// <summary>
        ///     起租日期
        /// </summary>
        public DateTime? OnHireDate { get; set; }

        /// <summary>
        ///     退出日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportActionType { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportActionName { get; set; }

        /// <summary>
        ///    运营权人
        /// </summary>
        public string AirlinesName { get; set; }

        /// <summary>
        ///    处理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     实际退出方式
        /// </summary>
        public string ExportCategoryName { get; set; }
        #endregion

        #region 外键属性


        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; set; }

        /// <summary>
        ///    运营权人外键
        /// </summary>
        public Guid AirlinesId { get; set; }

        /// <summary>
        ///     实际引进方式
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        /// <summary>
        ///     实际退出方式
        /// </summary>
        public Guid? ExportCategoryId { get; set; }


        #endregion

    }
}
