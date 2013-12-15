#region �汾��Ϣ

// ========================================================================
// ��Ȩ���� (C) 2013 UniCloud 
//�����๦�ܸ�����
// 
// ���ߣ���־�� ʱ�䣺2013/11/07��15:11
// ������FRP
// ��Ŀ��DistributedServices.Purchase
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// ========================================================================

#endregion

#region �����ռ�

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
        ///     ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion


            #region ����������ʿ���

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
            config.SetServiceOperationAccessRule("AddDocPath", ServiceOperationRights.All);
            config.SetServiceOperationAccessRule("DelDocPath", ServiceOperationRights.All);
        }

        #region �������

        /// <summary>
        /// �������ɵķ����Ƿ���Ҫ����
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

        #region �ĵ�·�����ӡ�ɾ��
        /// <summary>
        /// ����ĵ�·��
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
        /// ɾ���ĵ�·��
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