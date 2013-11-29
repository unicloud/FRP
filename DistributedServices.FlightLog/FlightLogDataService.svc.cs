//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.FlightLog
{
    using System;
    using System.Collections.Generic;
    using System.Data.Services;
    using System.Data.Services.Common;
    using System.Linq;
    using System.ServiceModel.Web;
    using System.Web;
    using Application.FlightLogBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    public class FlightLogDataService : DataService<FlightLogData>
    {
        private readonly IFlightLogAppService _flightLogAppService = Container.Current.Resolve<IFlightLogAppService>();

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
