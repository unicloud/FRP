#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AnnualAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AnnualQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AnnualServices
{
    /// <summary>
    ///     实现计划年度服务接口。
    ///     用于处理计划年度相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class AnnualAppService : ContextBoundObject, IAnnualAppService
    {
        private readonly IAnnualQuery _annualQuery;
        private readonly IAnnualRepository _annualRepository;

        public AnnualAppService(IAnnualQuery annualQuery, IAnnualRepository annualRepository)
        {
            _annualQuery = annualQuery;
            _annualRepository = annualRepository;
        }

        #region AnnualDTO

        /// <summary>
        ///     获取所有计划年度
        /// </summary>
        /// <returns></returns>
        public IQueryable<AnnualDTO> GetAnnuals()
        {
            var queryBuilder =
                new QueryBuilder<Annual>();
            return _annualQuery.AnnualDTOQuery(queryBuilder);
        }

        public IQueryable<PlanYearDTO> GetPlanYears()
        {
            var queryBuilder =
                new QueryBuilder<Annual>();
            return _annualQuery.PlanYearDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增计划年度。
        /// </summary>
        /// <param name="dto">计划年度DTO。</param>
        [Insert(typeof (AnnualDTO))]
        public void InsertAnnual(AnnualDTO dto)
        {
            var newAnnual = new Annual();
            _annualRepository.Add(newAnnual);
        }

        /// <summary>
        ///     更新计划年度。
        /// </summary>
        /// <param name="dto">计划年度DTO。</param>
        [Update(typeof (AnnualDTO))]
        public void ModifyAnnual(AnnualDTO dto)
        {
            //获取需要更新的对象
            Annual updateAnnual = _annualRepository.Get(dto.Id);

            if (updateAnnual != null)
            {
                //更新主表：
                updateAnnual.SetIsOpen(dto.IsOpen);
            }
            _annualRepository.Modify(updateAnnual);
        }

        /// <summary>
        ///     删除计划年度。
        /// </summary>
        /// <param name="dto">计划年度DTO。</param>
        [Delete(typeof (AnnualDTO))]
        public void DeleteAnnual(AnnualDTO dto)
        {
            Annual delAnnual = _annualRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delAnnual != null)
            {
                _annualRepository.Remove(delAnnual); //删除航空公司五年规划。
            }
        }

        #endregion
    }
}