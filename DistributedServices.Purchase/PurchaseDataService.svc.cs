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

using System.Data.Services;
using System.Data.Services.Common;

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
        }

        #region �������

        #endregion
    }
}