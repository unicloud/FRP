#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:53:28
// 文件名：ManagerFactory
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

namespace UniCloud.Domain.UberModel.Aggregates.ManagerAgg
{
    /// <summary>
    ///     管理者工厂
    /// </summary>
    public static class ManagerFactory
    {
        /// <summary>
        ///     创建管理者
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="cnShortName">名称</param>
        /// <returns></returns>
        public static Manager CreateManager(Guid id, string cnShortName)
        {
            var manager = new Manager
            {
               CnName = cnShortName,
               CnShortName = cnShortName,
            };
            manager.ChangeCurrentIdentity(id);

            return manager;
        }
    }
}
