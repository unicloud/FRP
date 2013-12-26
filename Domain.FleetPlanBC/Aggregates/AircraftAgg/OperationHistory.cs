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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public string AircraftReg { get; private set; }

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
        public Guid AircraftID { get; private set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesID { get; private set; }

        /// <summary>
        ///     实际引进方式
        /// </summary>
        public Guid ImportCategoryID { get; private set; }

        /// <summary>
        ///     实际退出方式
        /// </summary>
        public Guid? ExportCategoryID { get; private set; }


        #endregion

        #region 导航属性



        #endregion

        #region 操作



        #endregion
    }
}
