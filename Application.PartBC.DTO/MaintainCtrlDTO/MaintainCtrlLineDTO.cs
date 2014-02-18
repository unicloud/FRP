#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainCtrlLineDTO
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
#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// MaintainCtrlLine
    /// </summary>
    [DataServiceKey("Id")]
    public class MaintainCtrlLineDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        #endregion

    }
}
