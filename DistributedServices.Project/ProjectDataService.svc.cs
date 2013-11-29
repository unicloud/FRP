//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.Project
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Linq;
    using System.ServiceModel.Web;
    using System.Web;
    using Application.ProjectBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    public class ProjectDataService : DataService<ProjectData>
    {
        private readonly IProjectAppService _flightLogAppService = Container.Current.Resolve<IProjectAppService>();

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
