#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:27:10
// 文件名：ApprovalDocFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion


namespace UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg
{
    /// <summary>
    ///     批文工厂
    /// </summary>
    public static class ApprovalDocFactory
    {
        /// <summary>
        ///     创建批文文档
        /// </summary>
        /// <returns>批文</returns>
        public static ApprovalDoc CreateApprovalDoc()
        {
            var approvalDoc = new ApprovalDoc
            {
            };

            return approvalDoc;
        }
    }
}
