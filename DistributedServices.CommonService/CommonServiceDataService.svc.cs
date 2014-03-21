#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/02，18:12
// 文件名：CommonServiceDataService.svc.cs
// 程序集：UniCloud.DistributedServices.CommonService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.CommonServiceBC.DocumentServices;
using UniCloud.Application.CommonServiceBC.DocumnetSearch;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.CommonService
{
    public class CommonServiceDataService : DataService<CommonServiceData>
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

            config.SetServiceOperationAccessRule("SearchDocument", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("GetSingleDocument", ServiceOperationRights.All);
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
        public List<DocumentDTO> SearchDocument(string keyword, string documentType)
        {
            var searchDocument = DefaultContainer.Resolve<IDocumentSearchAppService>();
            return searchDocument.Search(keyword, documentType);
        }

        [WebGet]
        public DocumentDTO GetSingleDocument(string documentId)
        {
            var documentService = DefaultContainer.Resolve<IDocumentAppService>();
            return documentService.GetSingleDocument(Guid.Parse(documentId));
        }
        #endregion
    }
}