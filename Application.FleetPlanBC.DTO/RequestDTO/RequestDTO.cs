#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:33:34
// 文件名：RequestDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.RequestDTO
{
    /// <summary>
    /// 申请
    /// </summary>
    [DataServiceKey("Id")]
    public class RequestDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        #endregion
    }
}
