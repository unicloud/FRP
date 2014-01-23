//------------------------------------------------------------------------------
// <copyright file="WebDataService.svc.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace UniCloud.DistributedServices.Part
{
    using System.Data.Services;
    using System.Data.Services.Common;
    using Application.PartBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;


    public class PartDataService : DataService<PartData>
    {
        private readonly IPartAppService _partAppService = Container.Current.Resolve<IPartAppService>();

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
