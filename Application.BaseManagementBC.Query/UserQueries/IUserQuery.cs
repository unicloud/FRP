#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：IUserQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.Query.UserQueries
{
   /// <summary>
   /// User查询接口
   /// </summary>
   public interface IUserQuery
   {
      /// <summary>
      /// User查询。
      /// </summary>
      /// <param name="query">查询表达式</param>
      ///  <returns>UserDTO集合</returns>
      IQueryable<UserDTO> UsersQuery(QueryBuilder<User> query);
   }
}
