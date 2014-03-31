#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/25 10:15:56
// 文件名：TsDTO
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
    ///     TechnicalSolution
    /// </summary>
    [DataServiceKey("Id")]
    public class TsDTO
    {
        #region 私有字段

        private List<TsLineDTO> _tsLines;

        #endregion

        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     FI号
        /// </summary>
        public string FiNumber { get; set; }

        /// <summary>
        ///     TS号
        /// </summary>
        public string TsNumber { get; set; }

        /// <summary>
        ///     位置
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        public string Type { get; set; }
        #endregion

        #region 导航属性

        /// <summary>
        ///     技术解决方案明细
        /// </summary>
        public virtual List<TsLineDTO> TsLines
        {
            get { return _tsLines ?? (_tsLines = new List<TsLineDTO>()); }
            set { _tsLines = value; }
        }

        #endregion
    }
}