//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.FleetPlan
{
    using Application.FleetPlanBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 运力规划模块数据类
    /// </summary>
    public class FleetPlanData
    {
        private readonly IFleetPlanAppService _flightLogAppService = Container.Current.Resolve<IFleetPlanAppService>();

    }
}