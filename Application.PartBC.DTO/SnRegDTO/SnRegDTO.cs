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

        private List<LifeMonitorDTO> _lifeMonitors;
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

        /// <summary>
        /// 当前装机机号
        /// </summary>
        public string RegNumber
        {
            get;
            set;
        }

        /// <summary>
        ///  TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN
        {
            get;
            set;
        }

        /// <summary>
        /// TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR
        {
            get;
            set;
        }

        /// <summary>
        ///  CSN，自装机以来使用循环
        /// </summary>
        public decimal CSN
        {
            get;
            set;
        }

        /// <summary>
        /// CSR，自上一次修理以来使用循环
        /// </summary>
        public decimal CSR
        {
            get;
            set;
        }

        /// <summary>
        ///     序号件状态
        /// </summary>
        public int Status
        {
            get;
            set;
        }

        /// <summary>
        /// 是否寿控件
        /// </summary>
        public bool IsLife
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
        public Guid? AircraftId
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

        /// <summary>
        ///     到寿控制
        /// </summary>
        public virtual List<LifeMonitorDTO> LiftMonitors
        {
            get { return _lifeMonitors ?? (_lifeMonitors = new List<LifeMonitorDTO>()); }
            set { _lifeMonitors = value; }
        }
        #endregion
    }
}
