#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/10 16:20:19
// 文件名：ManufacturerDTO
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/10 16:20:19
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

namespace UniCloud.Application.PurchaseBC.DTO
{
    [DataServiceKey("Id")]
    public class ManufacturerDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     制造商中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     制造商英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// 制造商类型 1表示飞机制造商，2表示发动机制造商
        /// </summary>
        public int Type { get; set; }
        #endregion
    }
}
