//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

#region 命名空间

using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Application.BaseManagementBC.OrganizationServices;
using UniCloud.Application.BaseManagementBC.RoleServices;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{
    public class BaseManagementDataService : DataService<BaseManagementData>
    {

        /// <summary>
        /// 初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.UseVerboseErrors = true;

            #endregion

            #region 服务操作访问控制

            config.SetServiceOperationAccessRule("GetFunctionItemsWithHierarchy", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("GetFunctionItemsByUser", ServiceOperationRights.All);
            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        /// <summary>
        ///     控制生成的服务是否需要缓存
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            var cachePolicy = HttpContext.Current.Response.Cache;

            // no-cache是会被缓存的，只不过每次在向客户端（浏览器）提供响应数据时，缓存都要向服务器评估缓存响应的有效性。 
            cachePolicy.SetCacheability(HttpCacheability.NoCache);

            // default cache expire: never 
            //cachePolicy.SetExpires(DateTime.MaxValue);

            // cached output depends on: accept, charset, encoding, and all parameters (like $filter, etc) 
            cachePolicy.VaryByHeaders["Accept"] = true;
            cachePolicy.VaryByHeaders["Accept-Charset"] = true;
            cachePolicy.VaryByHeaders["Accept-Encoding"] = true;
            cachePolicy.VaryByParams["*"] = true;

            cachePolicy.SetValidUntilExpires(true);
        }

        #region 服务操作
        [WebGet]
        public List<FunctionItemDTO> GetFunctionItemsWithHierarchy()
        {
            var functionItemAppService = DefaultContainer.Resolve<IFunctionItemAppService>();
            return functionItemAppService.GetFunctionItemsWithHierarchy().ToList();
        }

        [WebGet]
        public List<FunctionItemDTO> GetFunctionItemsByUser(string userId)
        {
            var userService = DefaultContainer.Resolve<IUserAppService>();
            int id = int.Parse(userId);
            var user = userService.GetUsers().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return null;
            }
            var organizationService = DefaultContainer.Resolve<IOrganizationAppService>();
            var organization = organizationService.GetOrganizations().FirstOrDefault(p => p.Code.Equals(user.OrganizationNo));
            var roleService = DefaultContainer.Resolve<IRoleAppService>();
            var functionItemIds = new List<int>();
            user.UserRoles.ForEach(p => roleService.GetRoles().FirstOrDefault(t => t.Id == p.RoleId).RoleFunctions.ForEach(
                u =>
                {
                    if (!functionItemIds.Contains(u.FunctionItemId))
                    {
                        functionItemIds.Add(u.FunctionItemId);
                    }
                }));
            organization.OrganizationRoles.ForEach(p => roleService.GetRoles().FirstOrDefault(t => t.Id == p.RoleId).RoleFunctions.ForEach(
                u =>
                {
                    if (!functionItemIds.Contains(u.FunctionItemId))
                    {
                        functionItemIds.Add(u.FunctionItemId);
                    }
                }));
            var functionItemService = DefaultContainer.Resolve<IFunctionItemAppService>();
            return functionItemIds.Select(functionItemId => functionItemService.GetFunctionItems().FirstOrDefault(p => p.Id == functionItemId)).ToList();
        }
        #endregion

    }
}
