#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 10:00:35
// 文件名：OperationHistory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     运营权历史
    /// </summary>
    public class OperationHistory : EntityGuid
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OperationHistory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     注册号
        /// </summary>
        public string RegNumber { get; private set; }

        /// <summary>
        ///     运营日期
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        ///     退出停厂日期
        /// </summary>
        public DateTime? StopDate { get; private set; }

        /// <summary>
        ///     技术接收日期
        /// </summary>
        public DateTime? TechReceiptDate { get; private set; }

        /// <summary>
        ///     接收日期
        /// </summary>
        public DateTime? ReceiptDate { get; private set; }

        /// <summary>
        ///     技术交付日期
        /// </summary>
        public DateTime? TechDeliveryDate { get; private set; }

        /// <summary>
        ///     起租日期
        /// </summary>
        public DateTime? OnHireDate { get; private set; }

        /// <summary>
        ///     退出日期
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        ///     说明
        /// </summary>
        public string Note { get; private set; }


        #endregion

        #region 外键属性


        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; internal set; }

        /// <summary>
        ///    运营权人外键
        /// </summary>
        public Guid AirlinesId { get; private set; }

        /// <summary>
        ///     实际引进方式
        /// </summary>
        public Guid ImportCategoryId { get; private set; }

        /// <summary>
        ///     实际退出方式
        /// </summary>
        public Guid? ExportCategoryId { get; private set; }


        #endregion

        #region 导航属性

        /// <summary>
        ///     运营权人
        /// </summary>
        public virtual Airlines Airlines { get; set; }

        /// <summary>
        ///     实际引进方式
        /// </summary>
        public virtual ActionCategory ImportCategory { get; set; }

        /// <summary>
        ///     实际退出方式
        /// </summary>
        public virtual ActionCategory ExportCategory { get; set; }


        #endregion

        #region 操作

        /// <summary>
        ///     设置飞机机号
        /// </summary>
        /// <param name="regNumber">注册号</param>
        public void SetRegNumber(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
            {
                throw new ArgumentException("注册号参数为空！");
            }

            RegNumber = regNumber;
        }

        /// <summary>
        ///     设置运营日期
        /// </summary>
        /// <param name="date">运营日期</param>
        public void SetStartDate(DateTime date)
        {
            StartDate = date;
        }

        /// <summary>
        ///     设置退出停厂日期
        /// </summary>
        /// <param name="date">退出停厂日期</param>
        public void SetStopDate(DateTime? date)
        {
            StopDate = date;
        }

        /// <summary>
        ///     设置技术接收日期
        /// </summary>
        /// <param name="date">技术接收日期</param>
        public void SetTechReceiptDate(DateTime? date)
        {
            TechReceiptDate = date;
        }

        /// <summary>
        ///     设置接收日期
        /// </summary>
        /// <param name="date">接收日期</param>
        public void SetReceiptDate(DateTime? date)
        {
            ReceiptDate = date;
        }

        /// <summary>
        ///     设置技术交付日期
        /// </summary>
        /// <param name="date">技术交付日期</param>
        public void SetTechDeliveryDate(DateTime? date)
        {
            TechDeliveryDate = date;
        }

        /// <summary>
        ///     设置起租日期
        /// </summary>
        /// <param name="date">起租日期</param>
        public void SetOnHireDate(DateTime? date)
        {
            OnHireDate = date;
        }

        /// <summary>
        ///     设置退出日期
        /// </summary>
        /// <param name="date">退出日期</param>
        public void SetEndDate(DateTime? date)
        {
            EndDate = date;
        }

        /// <summary>
        ///     设置备注
        /// </summary>
        /// <param name="note">备注</param>
        public void SetNote(string note)
        {
            //if (string.IsNullOrWhiteSpace(note))
            //{
            //    throw new ArgumentException("备注参数为空！");
            //}
            Note = note;
        }

        /// <summary>
        ///     设置运营权人
        /// </summary>
        /// <param name="airlinesId">运营权人</param>
        public void SetAirlines(Guid airlinesId)
        {
            if (airlinesId == null)
            {
                throw new ArgumentException("运营权人Id参数为空！");
            }

            AirlinesId = airlinesId;
        }

        /// <summary>
        ///     设置实际引进方式
        /// </summary>
        /// <param name="importCategoryId">实际引进方式</param>
        public void SetImportCategory(Guid importCategoryId)
        {
            if (importCategoryId == null)
            {
                throw new ArgumentException("实际引进方式Id参数为空！");
            }

            ImportCategoryId = importCategoryId;
        }

        /// <summary>
        ///     设置实际退出方式
        /// </summary>
        /// <param name="exportCategoryId">实际退出方式</param>
        public void SetExportCategoryID(Guid exportCategoryId)
        {
            if (exportCategoryId == null)
            {
                throw new ArgumentException("实际退出方式Id参数为空！");
            }

            ExportCategoryId = exportCategoryId;
        }

        #endregion
    }
}
