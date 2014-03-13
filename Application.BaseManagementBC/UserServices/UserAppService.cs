#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：UserAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.UserServices
{
   /// <summary>
   /// 实现User的服务接口。
   ///  用于处理User相关信息的服务，供Distributed Services调用。
   /// </summary>
   public class UserAppService: IUserAppService
   {
      private readonly IUserQuery _userQuery;
      
      public UserAppService(IUserQuery userQuery)
      {
         _userQuery =userQuery;
      }
      
      #region UserDTO
      
      /// <summary>
      /// 获取所有User。
      /// </summary>
      public IQueryable<UserDTO> GetUsers()
      {
         var queryBuilder =
            new QueryBuilder<User>();
         return _userQuery.UsersQuery(queryBuilder);
      }
      #endregion
      
   }
}
