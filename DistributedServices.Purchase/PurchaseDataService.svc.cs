#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，15:11
// 方案：FRP
// 项目：DistributedServices.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Services;
using System.Data.Services.Common;

#endregion

namespace UniCloud.DistributedServices.Purchase
{
    public class PurchaseDataService : DataService<PurchaseData>
    {
        /// <summary>
        ///     初始化服务端策略
        /// </summary>
        /// <param name="config">数据服务配置</param>
        public static void InitializeService(DataServiceConfiguration config)
        {
            #region 实体集访问控制

            config.SetEntitySetAccessRule("*", EntitySetRights.All);

            #endregion

            #region 服务操作访问控制

            // config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);

            #endregion

            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V3;
            config.UseVerboseErrors = true;
        }

        #region 服务操作

        #endregion
    }
}