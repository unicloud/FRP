#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 11:18:11
// 文件名：IMailAddressRepository
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

namespace UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg
{
    /// <summary>
    ///     邮箱账号仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{MailAddress}" />
    /// </summary>
    public interface IMailAddressRepository : IRepository<MailAddress>
    {
    }
}
