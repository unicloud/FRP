//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.CommonService
{
    using Application.CommonServiceBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 公共服务模块数据类
    /// </summary>
    public class CommonServiceData
    {
        private readonly ICommonServiceAppService _flightLogAppService = Container.Current.Resolve<ICommonServiceAppService>();

    }
}