#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/25 9:59:22
// 文件名：ConfigGroupDTO
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
    ///     构型组
    /// </summary>
    [DataServiceKey("Id")]
    public class ConfigGroupDTO
    {
        #region 私有字段

        private List<GroupAcDTO> _groupAcs;
        private List<TsDTO> _technicalSolutions;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     构型组号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        ///     构型组名
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        #endregion

        #region 外键属性

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
        ///     组内飞机集合
        /// </summary>
        public virtual List<GroupAcDTO> GroupAcs
        {
            get { return _groupAcs ?? (_groupAcs = new List<GroupAcDTO>()); }
            set { _groupAcs = value; }
        }

        #endregion
    }
}