#region �汾����

// =====================================================
// ��Ȩ���� (C) 2014 UniCloud 
// �����๦�ܸ�����
// 
// ���ߣ���־�� ʱ�䣺2013/11/29��13:11
// ������FRP
// ��Ŀ��DistributedServices.BaseManagement
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// =====================================================

#endregion

#region �����ռ�

using System.Collections.Generic;
using System.Data.Services;
using System.Data.Services.Common;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.DistributedServices.BaseManagement
{
    public class BaseManagementDataService : DataService<BaseManagementData>
    {
        /// <summary>
        ///     ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.UseVerboseErrors = true;

            #endregion

            #region ����������ʿ���

            config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        /// <summary>
        ///     �������ɵķ����Ƿ���Ҫ����
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStartProcessingRequest(ProcessRequestArgs args)
        {
            base.OnStartProcessingRequest(args);

            var cachePolicy = HttpContext.Current.Response.Cache;

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

        #region �������

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