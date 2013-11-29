//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.Project
{
    using Application.ProjectBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 项目管理模块数据类
    /// </summary>
    public class ProjectData
    {
        private readonly IProjectAppService _flightLogAppService = Container.Current.Resolve<IProjectAppService>();

    }
}