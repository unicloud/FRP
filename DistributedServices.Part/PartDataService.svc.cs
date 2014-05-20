//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.PartBC.AcConfigServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.ItemServices;
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Domain.PartBC.Aggregates;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.Part
{
    using System.Data.Services;
    using System.Data.Services.Common;


    public class PartDataService : DataService<PartData>
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

            config.SetServiceOperationAccessRule("GetItemsByAircraftType", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("QueryAcConfigs", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("GetPnRegsByItem", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        #region 服务操作

        /// <summary>
        ///     控制生成的服务是否需要缓存
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

        #region 机型对应的项集合查询

        [WebGet]
        public List<ItemDTO> GetItemsByAircraftType(string aircraftTypeId)
        {
            Guid id = Guid.Parse(aircraftTypeId);
            var itemService = DefaultContainer.Resolve<IItemAppService>();
            List<ItemDTO> result = itemService.GetItemsByAircraftType(id);
            return result;
        }

        #endregion

        #region 飞机对应的某时刻的功能构型集合查询

        [WebGet]
        public List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, string date)
        {
            var dateTime = DateTime.Parse(date);
            var acConfigService = DefaultContainer.Resolve<IAcConfigAppService>();
            List<AcConfigDTO> result = acConfigService.QueryAcConfigs(contractAircraftId, dateTime);
            return result;
        }

        #endregion

        #region 获取某个项下所带的附件集合(去重)

        [WebGet]
        public List<PnRegDTO> GetPnRegsByItem(int itemId)
        {
            var pnRegService = DefaultContainer.Resolve<IPnRegAppService>();
            List<PnRegDTO> result = pnRegService.GetPnRegsByItem(itemId);
            return result;
        }
        #endregion
    }
}
