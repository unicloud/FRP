//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

namespace UniCloud.DistributedServices.Part
{
    using Application.PartBC.Services;
    using InstanceProviders;
    using Microsoft.Practices.Unity;

    /// <summary>
    /// 管理门户模块数据类
    /// </summary>
    public class PartData
    {
        private readonly IPartAppService _partAppService = Container.Current.Resolve<IPartAppService>();

    }
}