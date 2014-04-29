#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/23 23:10:39
// 文件名：CanRequest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.Common.Enums
{
    /// <summary>
    /// 能否提出申请
    /// 1、可申请
    /// 2、未报计划
    /// 3、已申请
    /// 4、可再次申请
    /// 5、无需申请
    /// </summary>
    public enum CanRequest
    {
        [Description("1、可申请")]
        可申请 = 0,

        [Description("2、未报计划")]
        未报计划 = 1,

        [Description("3、已申请")]
        已申请 = 2,

        [Description("4、可再次申请")]
        可再次申请 = 3,

        [Description("5、已有发改委指标")]
        已有发改委指标 = 4,

        [Description("6、无需申请")]
        无需申请 = 5,

    }
}
