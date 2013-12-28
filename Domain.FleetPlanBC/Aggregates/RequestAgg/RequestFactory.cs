#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:30:10
// 文件名：RequestFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;

namespace UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg
{
    /// <summary>
    ///     申请工厂
    /// </summary>
    public static class RequestFactory
    {
        /// <summary>
        ///     创建申请
        /// </summary>
        /// <returns>申请</returns>
        public static Request CreateRequest(DateTime? submitDate)
        {
            var request = new Request
            {
                CreateDate = DateTime.Now,
                SubmitDate = submitDate,
            };

            return request;
        }
    }
}
