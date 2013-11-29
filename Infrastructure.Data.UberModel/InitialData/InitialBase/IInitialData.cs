#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，17:11
// 文件名：IInitialData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace  UniCloud.Infrastructure.Data.UberModel.InitialData
{
    /// <summary>
    ///     初始化基础数据。
    /// </summary>
    public interface IInitialData
    {
        /// <summary>
        ///     对象。
        /// </summary>
        UberModelUnitOfWork Context { get; }

        /// <summary>
        ///     初始化数据。
        /// </summary>
        void InitialData();
    }
}