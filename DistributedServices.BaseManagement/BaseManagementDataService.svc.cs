//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.BaseManagement
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Web;

    public class BaseManagementDataService : DataService<BaseManagementData>
    {

        /// <summary>
        /// ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.All);
            config.UseVerboseErrors = true;

            #endregion

            #region ����������ʿ���

            config.SetServiceOperationAccessRule("GetFunctionItemsWithHierarchy", ServiceOperationRights.All);

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
        public List<FunctionItemDTO> GetFunctionItemsWithHierarchy()
        {
            var functionItemAppService = DefaultContainer.Resolve<IFunctionItemAppService>();
            return functionItemAppService.GetFunctionItemsWithHierarchy().ToList();
        }
        #endregion

    }
}
