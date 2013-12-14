#region �汾��Ϣ

// ========================================================================
// ��Ȩ���� (C) 2013 UniCloud 
//�����๦�ܸ�����
// 
// ���ߣ��´��� ʱ�䣺2013/12/02��18:12
// �ļ�����CommonServiceDataService.svc.cs
// ���򼯣�UniCloud.DistributedServices.CommonService
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// ========================================================================

#endregion

#region �����ռ�

using System.Data.Services;
using System.Data.Services.Common;
using System.Web;

#endregion

namespace UniCloud.DistributedServices.CommonService
{
    public class CommonServiceDataService : DataService<CommonServiceData>
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

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

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

        #endregion
    }
}