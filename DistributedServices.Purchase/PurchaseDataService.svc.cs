#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，15:11
// 方案：FRP
// 项目：DistributedServices.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.DocumentPathServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.Purchase
{
    public class PurchaseDataService : DataService<PurchaseData>
    {
        /// <summary>
        ///     初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion


            #region 服务操作访问控制

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
            config.SetServiceOperationAccessRule("AddDocPath", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("DelDocPath", ServiceOperationRights.All);
        }

        #region 服务操作

        /// <summary>
        /// 控制生成的服务是否需要缓存
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            HttpCachePolicy cachePolicy = HttpContext.Current.Response.Cache;

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


        #endregion

        #region 文档路径增加、删除
        /// <summary>
        /// 添加文档路径
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLeaf"></param>
        /// <param name="documentId"></param>
        /// <param name="parentId"></param>
        /// <param name="pathSource"></param>
        [WebGet]
        public void AddDocPath(string name, string isLeaf, string documentId, int parentId, int pathSource)
        {
            var documentPathAppService = DefaultContainer.Resolve<IDocumentPathAppService>();
            documentPathAppService.AddDocPath(name, isLeaf, documentId, parentId, pathSource);
        }
        /// <summary>
        /// 删除文档路径
        /// </summary>
        /// <param name="docPathId"></param>
        [WebGet]
        public void DelDocPath(int docPathId)
        {
            var documentPathAppService = DefaultContainer.Resolve<IDocumentPathAppService>();
            documentPathAppService.DelDocPath(docPathId);
        }


        #endregion
    }
}