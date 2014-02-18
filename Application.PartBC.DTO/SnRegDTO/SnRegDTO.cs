#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegDTO
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
    /// SnReg
    /// </summary>
    [DataServiceKey("Id")]
    public class SnRegDTO
    {
        #region 私有字段

        private List<SnHistoryDTO> _snHistories;

        #endregion

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
        /// 初始安装日期
        /// </summary>
        public DateTime InstallDate
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

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop
        {
            get;
            set;
        }

        #endregion

        #region 外键属性

        /// <summary>
        /// 附件Id
        /// </summary>
        public int PnRegId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前飞机Id
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }

        /// <summary>
        /// 到寿监控Id
        /// </summary>
        public int? LifeMonitorId
        {
            get;
            set;
        }

        #endregion
        
        #region 导航属性

        /// <summary>
        ///     装机历史
        /// </summary>
        public virtual List<SnHistoryDTO> SnHistories
        {
            get { return _snHistories ?? (_snHistories = new List<SnHistoryDTO>()); }
            set { _snHistories = value; }
        }

        #endregion
    }
}
