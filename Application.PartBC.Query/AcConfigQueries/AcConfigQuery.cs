#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/13，21:04
// 文件名：AcConfigQuery.cs
// 程序集：UniCloud.Application.PartBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PartBC.Query.AcConfigQueries
{
    public class AcConfigQuery : IAcConfigQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public AcConfigQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///     AcConfig查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>AcConfigDTO集合</returns>
        public IQueryable<AcConfigDTO> AcConfigDTOQuery(QueryBuilder<AcConfig> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<AcConfig>()).Select(p => new AcConfigDTO
            {
                Id = p.Id,
                ItemId = p.ItemId,
                ParentId = p.ParentId,
                Description = p.Description,
                Position = p.Position,
                RootId = p.RootId,
            });
        }

        /// <summary>
        ///     构型查询。
        /// </summary>
        /// <param name="contractAircraftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, DateTime date)
        {
            var result = new List<AcConfigDTO>();

            date = date.AddDays(-1);

            //1、依据合同飞机编号查找、日期对应的基本构型历史;再找到基本构型组
            var basicConfigHistories = _unitOfWork.CreateSet<BasicConfigHistory>().ToList();
            basicConfigHistories.ForEach(p =>
            {
                if (p != null && p.EndDate == null)
                    p.SetEndDate(new DateTime(3000, 01, 01));
            });
            var basicConfigHistory =
                basicConfigHistories.OrderBy(l => l.StartDate)
                    .FirstOrDefault(
                        p =>
                            p.ContractAircraftId == contractAircraftId && p.StartDate.CompareTo(date) < 0 &&
                            date.CompareTo(p.EndDate) < 0);
            if (basicConfigHistory != null)
            {
                var basicConfigs =
                    _unitOfWork.CreateSet<BasicConfig>()
                        .Where(p => p.BasicConfigGroupId == basicConfigHistory.BasicConfigGroupId);
                basicConfigs.ToList().ForEach(p =>
                {
                    var item = _unitOfWork.CreateSet<Item>().Find(p.ItemId);
                    var newAcConfig = new AcConfigDTO
                    {
                        Id = p.Id,
                        ItemId = p.ItemId,
                        ItemNo = item.ItemNo,
                        ItemName = item.Name,
                        FiNumber = item.FiNumber,
                        ParentId = p.ParentId,
                        Description = p.Description,
                        Position = p.Position,
                        RootId = p.RootId,
                    };
                    result.Add(newAcConfig);
                });
            }

            var allSpecialConifgs =
                _unitOfWork.CreateSet<SpecialConfig>().ToList();
            allSpecialConifgs.ForEach(p =>
            {
                if (p != null && p.EndDate == null)
                    p.SetEndDate(new DateTime(3000, 01, 01));
            });
            var specialConfigs =
                allSpecialConifgs.Where(
                    p =>
                        p.ContractAircraftId == contractAircraftId && p.StartDate.CompareTo(date) < 0 &&
                        date.CompareTo(p.EndDate) < 0);
            specialConfigs.ToList().ForEach(p =>
            {
                var item = _unitOfWork.CreateSet<Item>().Find(p.ItemId);
                var newAcConfig = new AcConfigDTO
                {
                    Id = p.Id,
                    ItemId = p.ItemId,
                    ItemNo = item.ItemNo,
                    FiNumber = item.FiNumber,
                    ParentId = p.ParentId,
                    Description = p.Description,
                    Position = p.Position,
                    RootId = p.RootId,
                };
                result.Add(newAcConfig);
            });
            return result;
        }
    }
}