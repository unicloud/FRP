#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：MScnQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.MScnAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PartBC.Query.MScnQueries
{
   /// <summary>
   /// MScn查询
   /// </summary>
   public class MScnQuery: IMScnQuery
   {
      private readonly IQueryableUnitOfWork _unitOfWork;
      public MScnQuery(IQueryableUnitOfWork unitOfWork)
      {
         _unitOfWork = unitOfWork;
      }
      
   }
}
