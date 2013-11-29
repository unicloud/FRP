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
        /// 初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.AllRead);

            #endregion

            #region 服务操作访问控制

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
        }

        #region 服务操作

        #endregion
    }
}
