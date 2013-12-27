#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:18:17
// 文件名：IXmlConfigRepository
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg
{
    /// <summary>
    ///     分析数据相关xml仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{XmlConfig}" />
    /// </summary>
    public interface IXmlConfigRepository : IRepository<XmlConfig>
    {
    }
}
