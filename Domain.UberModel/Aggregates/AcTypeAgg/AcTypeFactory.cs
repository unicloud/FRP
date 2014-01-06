#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:44:06
// 文件名：AcTypeFactory
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
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AcTypeAgg
{
    /// <summary>
    ///     飞机系列工厂
    /// </summary>
    public static class AcTypeFactory
    {
        /// <summary>
        ///     创建飞机系列
        /// </summary>
        /// <param name="id">飞机系列ID</param>
        /// <param name="name">飞机系列名称</param>
        /// <param name="manufacturerId">制造商</param>
        /// <param name="aircraftCategoryId">座级</param>
        /// <returns></returns>
        public static AcType CreateAcType(Guid id, string name, Guid manufacturerId, Guid aircraftCategoryId)
        {
            var acType = new AcType {Name = name};
            acType.ChangeCurrentIdentity(id);
            acType.ManufacturerId = manufacturerId;
            acType.AircraftCategoryId = aircraftCategoryId;

            return acType;
        }
    }
}
