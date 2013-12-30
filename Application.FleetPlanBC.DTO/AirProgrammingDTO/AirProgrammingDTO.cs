#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:33:24
// 文件名：AirProgrammingDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 航空公司五年规划
    /// </summary>
    [DataServiceKey("Id")]
    public class AirProgrammingDTO
    {
        #region 私有字段

       List<AirProgrammingLineDTO> _airProgrammingLines;

        #endregion

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     规划名称
        /// </summary>
        public string Name { get;set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }

        /// <summary>
        ///     发文日期
        /// </summary>
        public DateTime? IssuedDate { get;set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get;set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocName { get; set; }
        #endregion

        #region 外键属性
        /// <summary>
        ///     规划期间
        /// </summary>
        public Guid ProgrammingId { get;set; }

        /// <summary>
        ///     文档Id
        /// </summary>
        public Guid? DocumentId { get;set; }

        /// <summary>
        ///   发文单位
        /// </summary>
        public Guid IssuedUnitId { get;set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     航空公司五年规划明细
        /// </summary>
        public virtual List<AirProgrammingLineDTO> AirProgrammingLines
        {
            get { return _airProgrammingLines ?? (_airProgrammingLines = new List<AirProgrammingLineDTO>()); }
            set { _airProgrammingLines = value; }
        }
        #endregion
    }
}
