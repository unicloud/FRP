#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/16，16:12
// 文件名：IMaintainInvoiceAppService.cs
// 程序集：UniCloud.Application.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Infrastructure.Crosscutting.Caching;

#endregion

namespace UniCloud.Application.PaymentBC.PaymentScheduleServices
{
    /// <summary>
    ///     表示用于付款计划相关信息服务
    /// </summary>
    public interface IPaymentScheduleAppService
    {       
        #region 所有付款计划

        /// <summary>
        ///     查询所有付款计划
        /// </summary>
        /// <returns></returns>
        IQueryable<PaymentScheduleDTO> GetPaymentSchedules();

        #endregion

        #region 飞机付款计划

        /// <summary>
        ///     获取所有飞机付款计划
        /// </summary>
        /// <returns>所有飞机付款计划</returns>
       [Caching(CachingMethod.Get, typeof(AcPaymentScheduleDTO))]
        IQueryable<AcPaymentScheduleDTO> GetAcPaymentSchedules();

        /// <summary>
        ///     新增飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule">飞机付款计划DTO。</param>
       [Caching(CachingMethod.Put, typeof(AcPaymentScheduleDTO))]
       void InsertAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule);
       /// <summary>
        ///     修改飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule">飞机付款计划DTO。</param>
        [Caching(CachingMethod.Put, typeof(AcPaymentScheduleDTO))]
        void ModifyAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule);

        /// <summary>
        ///     删除飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule">飞机付款计划DTO。</param>
        [Caching(CachingMethod.Remove, typeof(AcPaymentScheduleDTO))]
        void DeleteAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule);

        #endregion

        #region 发动机付款计划

        /// <summary>
        ///     获取所有发动机付款计划
        /// </summary>
        /// <returns>所有发动机付款计划</returns>
        IQueryable<EnginePaymentScheduleDTO> GetEnginePaymentSchedules();

        /// <summary>
        ///     新增发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule">发动机付款计划DTO。</param>
        void InsertEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule);

        /// <summary>
        ///     修改发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule">发动机付款计划DTO。</param>
        void ModifyEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule);

        /// <summary>
        ///     删除发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule">发动机付款计划DTO。</param>
        void DeleteEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule);


        #endregion

        #region 标准付款计划

        /// <summary>
        ///     获取所有标准付款计划
        /// </summary>
        /// <returns>所有标准付款计划</returns>
        IQueryable<StandardPaymentScheduleDTO> GetStandardPaymentSchedules();

        /// <summary>
        ///     新增标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        void InsertStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule);

        /// <summary>
        ///     修改标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        void ModifyStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule);

        /// <summary>
        ///     删除标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        void DeleteStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule);


        #endregion

    }
}
