#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：PnMaintainCtrlDTO
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
    /// PnMaintainCtrl
    /// </summary>
    [DataServiceKey("Id")]
    public class PnMaintainCtrlDTO
    {
        #region 私有字段

        private List<MaintainCtrlLineDTO> _maintainCtrlLines;

        #endregion


        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 控制策略
        /// </summary>
        public int CtrlStrategy
        {
            get;
            set;
        }

        /// <summary>
        /// 件号
        /// </summary>
        public string Pn
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 附件ID
        /// </summary>
        public int PnRegId
        {
            get;
            set;
        }

        #endregion

        #region 导航属性

        /// <summary>
        ///     维修控制明细
        /// </summary>
        public virtual List<MaintainCtrlLineDTO> MaintainCtrlLines
        {
            get { return _maintainCtrlLines ?? (_maintainCtrlLines = new List<MaintainCtrlLineDTO>()); }
            set { _maintainCtrlLines = value; }
        }

        #endregion
    }
}
