//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.PartBC.ConfigGroupServices;
using UniCloud.Application.PartBC.DTO;
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

            config.SetServiceOperationAccessRule("GetConfigGroups", ServiceOperationRights.All);

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

        #region �������ѯ

        [WebGet]
        public List<ConfigGroupDTO> GetConfigGroups()
        {
            var configGroupService = DefaultContainer.Resolve<IConfigGroupAppService>();
            return configGroupService.GetConfigGroups();
        }

        #endregion
    }
}
