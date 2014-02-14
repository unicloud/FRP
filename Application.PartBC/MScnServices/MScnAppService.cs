#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：MScnAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.MScnQueries;
using UniCloud.Domain.PartBC.Aggregates.MScnAgg;
#endregion

namespace UniCloud.Application.PartBC.MScnServices
{
   /// <summary>
   /// 实现MScn的服务接口。
   ///  用于处理MScn相关信息的服务，供Distributed Services调用。
   /// </summary>
   public class MScnAppService: IMScnAppService
   {
      private readonly IMScnQuery _mScnQuery;
      
      public MScnAppService(IMScnQuery mScnQuery)
      {
         _mScnQuery = mScnQuery;
      }
      
      #region MScnDTO
      #endregion
      
   }
}
