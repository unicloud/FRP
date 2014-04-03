#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ApplicableAircraftDTO
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
    /// ApplicableAircraft
    /// </summary>
    [DataServiceKey("Id")]
    public class ApplicableAircraftDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 合同号+Rank号
        /// </summary>
        public string ApplicableAircraftName
        {
            get; set;
        }

        /// <summary>
        /// 完成日期
        /// </summary>
        public DateTime CompleteDate
        {
            get;
            set;
        }

        /// <summary>
        /// 费用
        /// </summary>
        public decimal Cost
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 合同飞机外键
        /// </summary>
        public int ContractAircraftId
        {
            get;
            set;
        }

        /// <summary>
        /// SCN外键
        /// </summary>
        public int ScnId
        {
            get;
            set;
        }
        #endregion

    }
}
