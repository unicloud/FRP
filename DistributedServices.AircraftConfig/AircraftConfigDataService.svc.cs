//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.AircraftConfig
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Linq;
    using System.ServiceModel.Web;
    using System.Web;
    using Application.AircraftConfigBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    public class AircraftConfigDataService : DataService<AircraftConfigData>
    {
        private readonly IAircraftConfigAppService _flightLogAppService = Container.Current.Resolve<IAircraftConfigAppService>();

        /// <summary>
        /// ��ʼ������˲���
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
