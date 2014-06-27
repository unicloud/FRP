#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 16:15:02
// 文件名：ThresholdAppService
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 16:15:02
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.ThresholdQueries;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;

#endregion

namespace UniCloud.Application.PartBC.ThresholdServices
{
    /// <summary>
    ///     实现Threshold的服务接口。
    ///     用于处理Threshold相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class ThresholdAppService : ContextBoundObject, IThresholdAppService
    {
        private readonly IPnRegRepository _pnRegRepository;
        private readonly IThresholdQuery _thresholdQuery;
        private readonly IThresholdRepository _thresholdRepository;

        public ThresholdAppService(IThresholdQuery thresholdQuery,
            IThresholdRepository thresholdRepository,
            IPnRegRepository pnRegRepository)
        {
            _thresholdQuery = thresholdQuery;
            _thresholdRepository = thresholdRepository;
            _pnRegRepository = pnRegRepository;
        }

        #region ThresholdDTO

        /// <summary>
        ///     获取所有Threshold。
        /// </summary>
        public IQueryable<ThresholdDTO> GetThresholds()
        {
            var queryBuilder =
                new QueryBuilder<Threshold>();
            return _thresholdQuery.ThresholdDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增Threshold。
        /// </summary>
        /// <param name="dto">ThresholdDTO。</param>
        [Insert(typeof (ThresholdDTO))]
        public void InsertThreshold(ThresholdDTO dto)
        {
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
            Threshold newThreshold = ThresholdFactory.CreateThreshold(pnReg, dto.TotalThreshold, dto.IntervalThreshold,
                dto.DeltaIntervalThreshold, dto.Average3Threshold, dto.Average7Threshold);

            _thresholdRepository.Add(newThreshold);
        }

        /// <summary>
        ///     更新Threshold。
        /// </summary>
        /// <param name="dto">ThresholdDTO。</param>
        [Update(typeof (ThresholdDTO))]
        public void ModifyThreshold(ThresholdDTO dto)
        {
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);

            Threshold updateThreshold = _thresholdRepository.Get(dto.Id); //获取需要更新的对象。
            ThresholdFactory.UpdateThreshold(updateThreshold, pnReg, dto.TotalThreshold, dto.IntervalThreshold,
                dto.DeltaIntervalThreshold, dto.Average3Threshold, dto.Average7Threshold);
            //更新。

            _thresholdRepository.Modify(updateThreshold);
        }

        /// <summary>
        ///     删除Threshold。
        /// </summary>
        /// <param name="dto">ThresholdDTO。</param>
        [Delete(typeof (ThresholdDTO))]
        public void DeleteThreshold(ThresholdDTO dto)
        {
            Threshold delThreshold = _thresholdRepository.Get(dto.Id); //获取需要删除的对象。
            _thresholdRepository.Remove(delThreshold); //删除Threshold。
        }

        #endregion
    }
}