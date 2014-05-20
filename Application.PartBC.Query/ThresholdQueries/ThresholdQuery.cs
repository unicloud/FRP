#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 16:08:22
// 文件名：ThresholdQuery
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 16:08:22
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.ThresholdQueries
{
    /// <summary>
    /// Threshold查询
    /// </summary>
    public class ThresholdQuery : IThresholdQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ThresholdQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Threshold查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ThresholdDTO集合</returns>
        public IQueryable<ThresholdDTO> ThresholdDTOQuery(QueryBuilder<Threshold> query)
        {
            var pnRegs = _unitOfWork.CreateSet<PnReg>();
            return query.ApplyTo(_unitOfWork.CreateSet<Threshold>()).Select(p => new ThresholdDTO
            {
                Id = p.Id,
                Pn = pnRegs.FirstOrDefault(l=>l.Id==p.PnRegId).Pn,
                PnRegId = p.PnRegId,
                TotalThreshold = p.TotalThreshold,
                IntervalThreshold = p.IntervalThreshold,
                DeltaIntervalThreshold = p.DeltaIntervalThreshold,
                Average3Threshold = p.Average3Threshold,
                Average7Threshold = p.Average7Threshold,
            });
        }
    }
}
