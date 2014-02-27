﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/25 9:59:07
// 文件名：CaDTO
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
    public class CaDTO
    {
        #region 私有字段

        private List<TsDTO> _technicalSolutions;

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

        /// <summary>
        ///     基本构型组Id
        /// </summary>
        public int? BcGroupId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     Ts集合
        /// </summary>
        public virtual List<TsDTO> TechnicalSolutions
        {
            get { return _technicalSolutions ?? (_technicalSolutions = new List<TsDTO>()); }
            set { _technicalSolutions = value; }
        }

        #endregion
    }
}