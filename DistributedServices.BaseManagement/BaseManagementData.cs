//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.BaseManagement
{
    using Application.BaseManagementBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 基础管理模块数据类
    /// </summary>
    public class BaseManagementData
    {
        private readonly IBaseManagementAppService _flightLogAppService = Container.Current.Resolve<IBaseManagementAppService>();

    }
}