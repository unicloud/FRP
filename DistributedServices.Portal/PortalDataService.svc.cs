#region �汾����

// =====================================================
// ��Ȩ���� (C) 2014 UniCloud 
// �����๦�ܸ�����
// 
// ���ߣ���־�� ʱ�䣺2013/11/29��13:11
// ������FRP
// ��Ŀ��DistributedServices.Portal
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// =====================================================

#endregion

#region �����ռ�

using System.Data.Services;
using System.Data.Services.Common;

#endregion

namespace UniCloud.DistributedServices.Portal
{
    public class PortalDataService : DataService<PortalData>
    {
        /// <summary>
        ///     ��ʼ������˲���
        /// </summary>
        /// <param name="config">���ݷ�������</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region ʵ�弯���ʿ���

            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);

            #endregion

            #region ����������ʿ���

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        #region �������

        #endregion
    }
}