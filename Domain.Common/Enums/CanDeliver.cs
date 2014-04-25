#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/23 23:11:51
// 文件名：CanDeliver
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
    /// 能否执行交付操作
    /// 可交付存在两种情形，一种是无需申请的，一种是申请已批复且批准的
    /// 1、可交付
    /// 2、交付中
    /// 3、已交付
    /// 4、未申请
    /// 5、未批复
    /// 6、未批准
    /// </summary>
    public enum CanDeliver
    {
        [Description("1、可交付")]
        可交付 = 0,

        [Description("2、交付中")]
        交付中 = 1,

        [Description("3、已交付")]
        已交付 = 2,

        [Description("4、未申请")]
        未申请 = 3,

        [Description("5、未批复")]
        未批复 = 4,

        [Description("6、未批准")]
        未批准 = 5,
    }
}
