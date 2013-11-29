//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.FlightLog
{
    using Application.FlightLogBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 飞行日志模块数据类
    /// </summary>
    public class FlightLogData
    {
        private readonly IFlightLogAppService _flightLogAppService = Container.Current.Resolve<IFlightLogAppService>();

    }
}