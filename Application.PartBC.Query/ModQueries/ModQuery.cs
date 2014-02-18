#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ModQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ModAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.ModQueries
{
    /// <summary>
    /// Mod查询
    /// </summary>
    public class ModQuery : IModQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public ModQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Mod查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ModDTO集合</returns>
        public IQueryable<ModDTO> ModDTOQuery(QueryBuilder<Mod> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Mod>()).Select(p => new ModDTO
            {
                Id = p.Id,
                ModNumber = p.ModNumber,
            });
        }
    }
}
