#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/25 10:00:43
// 文件名：GroupAcDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     ConfigGroup
    /// </summary>
    [DataServiceKey("Id")]
    public class GroupAcDTO
    {
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
        ///     构型组Id
        /// </summary>
        public int ConfigGroupId { get; set; }

        #endregion
    }
}