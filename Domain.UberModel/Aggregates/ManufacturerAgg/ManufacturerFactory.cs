#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:53:49
// 文件名：ManufacturerFactory
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

namespace UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg
{
    /// <summary>
    ///     制造商工厂
    /// </summary>
    public static class ManufacturerFactory
    {
        /// <summary>
        ///     创建制造商
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cnShortName">制造商简称</param>
        /// <param name="type">制造商类型</param>
        /// <returns></returns>
        public static Manufacturer CreateManufacturer(Guid id, string cnShortName,int type)
        {
            var manufacturer = new Manufacturer
            {
                CnName = cnShortName,
                CnShortName = cnShortName,
                Type = type,
            };
            manufacturer.ChangeCurrentIdentity(id);

            return manufacturer;
        }
    }
}
