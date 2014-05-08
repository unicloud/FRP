#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:41:34
// 文件名：EngineMaintainPlanDetailDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:41:34
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     public class EngineMaintainPlanDetailDTO

    /// </summary>
    [DataServiceKey("Id")]
    public class EngineMaintainPlanDetailDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 发动机
        /// </summary>
        public string EngineNumber { get; set; }
        /// <summary>
        /// 送修日期
        /// </summary>
        public string InMaintainDate { get; set; }
        /// <summary>
        /// 返回日期
        /// </summary>
        public string OutMaintainDate { get; set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public string MaintainLevel { get; set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ChangeLlpNumber { get; set; }
        /// <summary>
        /// TSN/CSN
        /// </summary>
        public string TsnCsn { get; set; }
        /// <summary>
        /// TSR/CSR
        /// </summary>
        public string TsrCsr { get; set; }
        /// <summary>
        /// 非FHA大修费
        /// </summary>
        public decimal NonFhaFee { get; set; }
        /// <summary>
        /// 附件修理费
        /// </summary>
        public decimal PartFee { get; set; }
        /// <summary>
        /// 更换LLP件费
        /// </summary>
        public decimal ChangeLlpFee { get; set; }
        /// <summary>
        /// 关增税
        /// </summary>
        public decimal CustomsTax { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal FreightFee { get; set; }
        /// <summary>
        /// 申报说明
        /// </summary>
        public string Note { get; set; }
        #endregion
    }
}
