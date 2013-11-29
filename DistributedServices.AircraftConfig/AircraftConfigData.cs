//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.AircraftConfig
{
    using Application.AircraftConfigBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 飞机构型模块数据类
    /// </summary>
    public class AircraftConfigData
    {
        private readonly IAircraftConfigAppService _flightLogAppService = Container.Current.Resolve<IAircraftConfigAppService>();

    }
}