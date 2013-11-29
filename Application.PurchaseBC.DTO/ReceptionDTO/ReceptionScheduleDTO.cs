#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 13:46:32
// 文件名：ReceptionScheduleDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 交付日程
    /// </summary>
    [DataServiceKey("ReceptionScheduleId")]
    public partial class ReceptionScheduleDTO 
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ReceptionScheduleId { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// 重要性级别
        /// </summary>
        public string Importance
        {
            get;
            set;
        }

        public DateTime Start
        {
            get;
            set;
        }
        public DateTime End
        {
            get;
            set;
        }

        /// <summary>
        /// 是否全天事件
        /// </summary>
        public bool IsAllDayEvent
        {
            get;
            set;
        }

        /// <summary>
        /// 分组信息
        /// </summary>
        public string Group
        {
            get;
            set;
        }

        /// <summary>
        /// 进度
        /// </summary>
        public string Tempo
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Location
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UniqueId
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            get;
            set;
        }

        #region 外键

        /// <summary>
        /// 接收项目外键
        /// </summary>
        public int ReceptionId { get; set; }

        #endregion
    }
}
