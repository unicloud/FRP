#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ContractAircraftDTO
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     ContractAircraft
    /// </summary>
    [DataServiceKey("Id")]
    public class ContractAircraftDTO
    {
        #region 私有字段

        private List<BasicConfigHistoryDTO> _basicConfigHistories;

        private List<SpecialConfigDTO> _specialConfigs;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; set; }

        /// <summary>
        ///     飞机批次号
        /// </summary>
        public string CSCNumber { get; set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     基本构型历史集合
        /// </summary>
        public virtual List<BasicConfigHistoryDTO> BasicConfigHistories
        {
            get { return _basicConfigHistories ?? (_basicConfigHistories = new List<BasicConfigHistoryDTO>()); }
            set { _basicConfigHistories = value; }
        }

        /// <summary>
        ///     特定选型集合
        /// </summary>
        public virtual List<SpecialConfigDTO> SpecialConfigs
        {
            get { return _specialConfigs ?? (_specialConfigs = new List<SpecialConfigDTO>()); }
            set { _specialConfigs = value; }
        }

        #endregion
    }
}