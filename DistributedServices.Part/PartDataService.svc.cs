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
        ///     ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion

            #region ����������ʿ���

            config.SetServiceOperationAccessRule("GetItemsByAircraftType", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("QueryAcConfigs", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("GetPnRegsByItem", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        #region �������

        /// <summary>
        ///     �������ɵķ����Ƿ���Ҫ����
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            HttpCachePolicy cachePolicy = HttpContext.Current.Response.Cache;

            // no-cache�ǻᱻ����ģ�ֻ����ÿ������ͻ��ˣ���������ṩ��Ӧ����ʱ�����涼Ҫ�����������������Ӧ����Ч�ԡ� 
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

        #region ���Ͷ�Ӧ����ϲ�ѯ

        [WebGet]
        public List<ItemDTO> GetItemsByAircraftType(string aircraftTypeId)
        {
            Guid id = Guid.Parse(aircraftTypeId);
            var itemService = DefaultContainer.Resolve<IItemAppService>();
            List<ItemDTO> result = itemService.GetItemsByAircraftType(id);
            return result;
        }

        #endregion

        #region �ɻ���Ӧ��ĳʱ�̵Ĺ��ܹ��ͼ��ϲ�ѯ

        [WebGet]
        public List<AcConfigDTO> QueryAcConfigs(int contractAircraftId, string date)
        {
            var dateTime = DateTime.Parse(date);
            var acConfigService = DefaultContainer.Resolve<IAcConfigAppService>();
            List<AcConfigDTO> result = acConfigService.QueryAcConfigs(contractAircraftId, dateTime);
            return result;
        }

        #endregion

        #region ��ȡĳ�����������ĸ�������(ȥ��)

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
