#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：ScnAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.ScnQueries;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
#endregion

namespace UniCloud.Application.PartBC.ScnServices
{
   /// <summary>
   /// 实现Scn的服务接口。
   ///  用于处理Scn相关信息的服务，供Distributed Services调用。
   /// </summary>
   public class ScnAppService: IScnAppService
   {
      private readonly IScnQuery _scnQuery;
      
      public ScnAppService(IScnQuery scnQuery)
      {
         _scnQuery =scnQuery;
      }
      
      #region ScnDTO
      /// <summary>
      ///     获取所有Scn
      /// </summary>
      /// <returns></returns>
      public IQueryable<ScnDTO> GetScns()
      {
          var queryBuilder =
              new QueryBuilder<Scn>();
          return _scnQuery.ScnDTOQuery(queryBuilder);
      }
      #endregion
      
   }
}
