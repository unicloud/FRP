//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------
namespace UniCloud.DistributedServices.ExposeData
{
    using System.Collections.Generic;
    using System.Data.Services;

    /// <summary>
    ///     表示自定义暴露数据接口。
    /// </summary>
    public interface IExposeData : IUpdatable
    {
        /// <summary>
        ///     本地保存删除集合。
        /// </summary>
        List<object> LocalDeletedCollection { get; }

        /// <summary>
        ///     本地保存修改集合。
        /// </summary>
        List<object> LocalModifiedCollection { get; }

        /// <summary>
        ///     本地保存新增集合。
        /// </summary>
        List<object> LocalNewCollection { get; }
    }
}