#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/14 17:34:10
// 文件名：IDocumentRepository
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/14 17:34:10
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Document}" />
    /// </summary>
    public interface IDocumentRepository : IRepository<Document>
    {
    }
}
