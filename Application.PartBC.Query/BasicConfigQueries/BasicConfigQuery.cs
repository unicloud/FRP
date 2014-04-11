#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：BasicConfigQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.BasicConfigQueries
{
   /// <summary>
   /// BasicConfig查询
   /// </summary>
   public class BasicConfigQuery: IBasicConfigQuery
   {
      private readonly IQueryableUnitOfWork _unitOfWork;
      public BasicConfigQuery(IQueryableUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }
      
      /// <summary>
      /// BasicConfig查询。
      /// </summary>
      /// <param name="query">查询表达式</param>
      ///  <returns>BasicConfigDTO集合</returns>
      public IQueryable<BasicConfigDTO> BasicConfigDTOQuery(QueryBuilder<BasicConfig> query)
      {
          var items = _unitOfWork.CreateSet<Item>();
         return query.ApplyTo(_unitOfWork.CreateSet<BasicConfig>()).Select(p => new BasicConfigDTO
         {
             Id = p.Id,
             BasicConfigGroupId = p.BasicConfigGroupId,
             FiNumber = items.FirstOrDefault(l=>l.Id==p.ItemId).FiNumber,
             ItemNo = items.FirstOrDefault(l=>l.Id==p.ItemId).ItemNo,
             ItemId = p.ItemId,
             ParentId = p.ParentId,
             RootId = p.RootId,
             Position = p.Position,
             Description = p.Description,
         });
      }
   }
}
