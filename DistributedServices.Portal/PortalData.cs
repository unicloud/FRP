//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.Portal
{
    using Application.PortalBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 管理门户模块数据类
    /// </summary>
    public class PortalData
    {
        private readonly IPortalAppService _flightLogAppService = Container.Current.Resolve<IPortalAppService>();

    }
}