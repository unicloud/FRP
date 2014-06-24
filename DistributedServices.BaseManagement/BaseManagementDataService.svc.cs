#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.BaseManagement
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{
    public class BaseManagementDataService : DataService<BaseManagementData>
    {
        /// <summary>
        ///     初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.UseVerboseErrors = true;

            #endregion

            #region 服务操作访问控制

            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);

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
        public List<FunctionItemDTO> GetFunctionItemsWithHierarchy(string userName)
        {
            var functionItemAppService = UniContainer.Resolve<IFunctionItemAppService>();
            return functionItemAppService.GetFunctionItemsWithHierarchy(userName).ToList();
        }

        [WebGet]
        public List<FunctionItemDTO> GetFunctionItemsByUser(string userName)
        {
            var functionItemAppService = UniContainer.Resolve<IFunctionItemAppService>();
            return functionItemAppService.GetFunctionItemsByUser(userName).ToList();
        }

        #endregion
    }
}