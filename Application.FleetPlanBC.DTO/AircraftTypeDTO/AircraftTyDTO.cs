#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/24 9:32:15
// 文件名：AircraftTyDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 机型
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftTyDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属座级
        /// </summary>
        public Guid AircraftCategoryId { get; set; }

        /// <summary>
        /// 座级
        /// </summary>
        public string Regional { get; set; }

        #endregion
    }
}
