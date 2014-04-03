#region NameSpace

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     Apu、Engine的SnReg
    /// </summary>
    [DataServiceKey("Id")]
    public class ApuEngineSnRegDTO
    {
        #region 属性

        private List<SnInstallHistoryDTO> _snInstallHistories;

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     序号
        /// </summary>
        public string Sn { get; set; }

        /// <summary>
        ///     初始安装日期
        /// </summary>
        public DateTime InstallDate { get; set; }

        /// <summary>
        ///     件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        ///     是否停用
        /// </summary>
        public bool IsStop { get; set; }

        /// <summary>
        ///     当前装机机号
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public decimal CSN { get; set; }

        /// <summary>
        ///     CSR，自上一次修理以来使用循环
        /// </summary>
        public decimal CSR { get; set; }

        /// <summary>
        ///     序号件状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     装机历史
        /// </summary>
        public virtual List<SnInstallHistoryDTO> SnInstallHistories
        {
            get { return _snInstallHistories ?? (_snInstallHistories = new List<SnInstallHistoryDTO>()); }
            set { _snInstallHistories = value; }
        }

        #endregion

        #region 外键属性

        /// <summary>
        ///     附件Id
        /// </summary>
        public int PnRegId { get; set; }

        /// <summary>
        ///     当前飞机Id
        /// </summary>
        public Guid? AircraftId { get; set; }

        #endregion
    }
}