#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:52:32
// 文件名：AircraftCategoryFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg
{
    /// <summary>
    ///     座级工厂
    /// </summary>
    public static class AircraftCategoryFactory
    {
        /// <summary>
        ///     创建座级
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="category">类型</param>
        /// <param name="regional">座级范围</param>
        /// <returns></returns>
        public static AircraftCategory CreateAircraftCategory(Guid id, string category, string regional)
        {
            var aircraftCategory = new AircraftCategory
            {
                Category = category,
                Regional = regional,
            };
            aircraftCategory.ChangeCurrentIdentity(id);

            return aircraftCategory;
        }
    }
}
