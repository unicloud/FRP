#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：IUserAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.BaseManagementBC.DTO;

#endregion

namespace UniCloud.Application.BaseManagementBC.UserServices
{
   /// <summary>
   /// User的服务接口。
   /// </summary>
   public interface IUserAppService
   {
      /// <summary>
      /// 获取所有User。
      /// </summary>
      IQueryable<UserDTO> GetUsers();
   }
}
