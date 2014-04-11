#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/07，21:04
// 文件名：InstallControllerQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.InstallControllerQueries
{
    public class InstallControllerQuery : IInstallControllerQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public InstallControllerQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     装机控制查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>装机控制DTO集合。</returns>
        public IQueryable<InstallControllerDTO> InstallControllerDTOQuery(
            QueryBuilder<InstallController> query)
        {
            var pnReg = _unitOfWork.CreateSet<PnReg>();
            return query.ApplyTo(_unitOfWork.CreateSet<InstallController>()).Select(p => new InstallControllerDTO
            {
                Id = p.Id,
                ItemId = p.ItemId,
                ItemNo = p.Item.ItemNo,
                ItemName = p.Item.Name,
                PnRegId = p.PnRegId,
                Pn = p.PnReg.Pn,
                Description = p.PnReg.Description,
                AircraftTypeId = p.AircraftTypeId,
                AircraftTypeName = p.AircraftType.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Dependencies = p.Dependencies.Select(q=>new DependencyDTO
                {
                    Id = q.Id,
                    DependencyPnId = q.DependencyPnId,
                    InstallControllerId = q.DependencyPnId,
                    Pn = q.Pn,
                    Description = pnReg.FirstOrDefault(l=>l.Id==q.DependencyPnId).Description,
                }).ToList(),
            });

        }
    }
}