#region 版本信息
// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/22，19:01
// 方案：FRP
// 项目：Application.PurchaseBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     购买飞机订单DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class ContractContentDTO
    {
        public int Id { get; set; }

        /// <summary>
        ///     内容标签
        ///     <remarks>
        ///         用“|”分隔
        ///     </remarks>
        /// </summary>
        public string ContentTags { get; set; }

        /// <summary>
        ///     内容文档
        /// </summary>
        public byte[] ContentDoc { get; set; }

    }
}