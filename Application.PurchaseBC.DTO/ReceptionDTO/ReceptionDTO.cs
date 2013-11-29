#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/11 16:52:42
// 文件名：ReceptionDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCloud.Application.PurchaseBC.DTO
{

    public partial class ReceptionDTO
    {
        public ReceptionDTO()
        {
            ReceptionSchedules = new List<ReceptionScheduleDTO>();
            Doucments=new List<string>();
        }

        #region 属性
        //接机编号
        public string ReceptionNumber { get; set; }
        // 描述
        public string Description { get; set; }
        //交付起始时间 
        public DateTime StartDate { get; set; }
        //交付截止时间
        public DateTime EndDate { get; set; }
        //创建日期
        public DateTime CreateDate { get; set; }
        //是否关闭
        public bool IsClosed { get; set; }
        //关闭日期
        public DateTime CloseDate { get; set; }

        //供应商
        public string SupplierName { get; set; }

        #endregion

        #region 外键属性

        //供应商外键
        public string SupplierId { get; set; }

        #endregion


        #region 导航属性

        /// <summary>
        ///     交付日程
        /// </summary>
        public List<ReceptionScheduleDTO> ReceptionSchedules { get; set; }

        /// <summary>
        ///  交付相关文档
        /// </summary>
        public List<string> Doucments { get; set; }

        #endregion
    }
}
