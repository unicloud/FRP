#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 8:36:03
// 文件名：IAircraftCategoryRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 8:36:03
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCategoryAgg
{
    /// <summary>
    ///     飞机座级仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AircraftCategory}" />
    /// </summary>
    public interface IAircraftCategoryRepository : IRepository<AircraftCategory>
    {
    }
}
