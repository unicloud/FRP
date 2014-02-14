#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：ScnQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.ScnQueries
{
   /// <summary>
   /// Scn查询
   /// </summary>
   public class ScnQuery: IScnQuery
   {
      private readonly IQueryableUnitOfWork _unitOfWork;
      public ScnQuery(IQueryableUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }

      /// <summary>
      ///     Scn查询。
      /// </summary>
      /// <param name="query">查询表达式。</param>
      /// <returns>ScnDTO集合。</returns>
      public IQueryable<ScnDTO> ScnDTOQuery(
          QueryBuilder<Scn> query)
      {
          return query.ApplyTo(_unitOfWork.CreateSet<Scn>()).Select(a => new ScnDTO
          {
              Id = a.Id,
          });
      }
   }
}
