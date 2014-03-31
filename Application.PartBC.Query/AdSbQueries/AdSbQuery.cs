#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.AdSbQueries
{
    /// <summary>
    /// AdSb查询
    /// </summary>
    public class AdSbQuery : IAdSbQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public AdSbQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// AdSb查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>AdSbDTO集合</returns>
        public IQueryable<AdSbDTO> AdSbDTOQuery(QueryBuilder<AdSb> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AdSb>()).Select(p => new AdSbDTO
            {
                Id = p.Id,
                AircraftSeries = p.AircraftSeries,
                ComplyAircraft = p.ComplyAircraft,
                ComplyClose = p.ComplyClose,
                ComplyDate = p.ComplyDate,
                ComplyFee = p.ComplyFee,
                ComplyFeeCurrency = p.ComplyFeeCurrency,
                ComplyFeeNotes = p.ComplyFeeNotes,
                ComplyNotes = p.ComplyNotes,
                ComplyStatus = p.ComplyStatus,
                FileNo = p.FileNo,
                FileType = p.FileType,
                FileVersion = p.FileVersion
                 
                
            });
        }
    }
}
