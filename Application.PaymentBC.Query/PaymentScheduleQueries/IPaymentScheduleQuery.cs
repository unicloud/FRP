#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/16，16:12
// 文件名：IAcPaymentScheduleQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.PaymentScheduleQueries
{
    public interface IPaymentScheduleQuery
    {
        /// <summary>
        ///     所有付款计划
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>所有付款计划DTO集合</returns>
        IQueryable<PaymentScheduleDTO> PaymentSchedulesQuery(QueryBuilder<PaymentSchedule> query);


        /// <summary>
        ///     飞机付款计划
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>飞机付款计划DTO集合</returns>
        IQueryable<AcPaymentScheduleDTO> AcPaymentSchedulesQuery(
            QueryBuilder<PaymentSchedule> query);

        /// <summary>
        ///     发动机付款计划
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>发动机付款计划DTO集合</returns>
        IQueryable<EnginePaymentScheduleDTO> EnginePaymentSchedulesQuery(
            QueryBuilder<PaymentSchedule> query);

        /// <summary>
        ///     标准付款计划
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>标准付款计划DTO集合</returns>
        IQueryable<StandardPaymentScheduleDTO> StandardPaymentSchedulesQuery(
            QueryBuilder<PaymentSchedule> query);
    }
}