#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/25 9:58:06
// 文件名：BcGroupDTO
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
    ///     BasicConfigGroup
    /// </summary>
    [DataServiceKey("Id")]
    public class BcGroupDTO
    {
        #region 私有字段

        private List<CaDTO> _contractAircrafts;
        private List<TsDTO> _technicalSolutions;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     启用日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     基本构型组号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

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


        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public virtual List<CaDTO> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = new List<CaDTO>()); }
            set { _contractAircrafts = value; }
        }

        #endregion
    }
}