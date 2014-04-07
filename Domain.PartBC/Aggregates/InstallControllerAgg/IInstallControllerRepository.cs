#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 20:17:05
// 文件名：IInstallControllerRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间



#endregion

namespace UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg
{
    /// <summary>
    ///     装机控制仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{InstallController}" />
    /// </summary>
    public interface IInstallControllerRepository : IRepository<InstallController>
    {
        /// <summary>
        ///     删除装机控制
        /// </summary>
        /// <param name="installController">装机控制</param>
        void DeleteInstallController(InstallController installController);

        /// <summary>
        ///     移除依赖项
        /// </summary>
        /// <param name="dependency">依赖项</param>
        void RemoveDependency(Dependency dependency);
    }
}