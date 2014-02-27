#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainWorkFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg
{
    /// <summary>
    /// MaintainWork工厂。
    /// </summary>
    public static class MaintainWorkFactory
    {
        /// <summary>
        /// 创建MaintainWork。
        /// </summary>
        ///  <returns>MaintainWork</returns>
        public static MaintainWork CreateMaintainWork()
        {
            var maintainWork = new MaintainWork
            {
            };
            maintainWork.GenerateNewIdentity();
            return maintainWork;
        }

        /// <summary>
        /// 创建维修工作
        /// </summary>
        /// <param name="description">描述</param>
        /// <param name="workCode">工作代码</param>
        /// <returns></returns>
        public static MaintainWork CreateMaintainWork(string description, string workCode)
        {
            var maintainWork = new MaintainWork
            {
            };
            maintainWork.GenerateNewIdentity();
            maintainWork.SetDescription(description);
            maintainWork.SetWorkCode(workCode);
            return maintainWork;
        }
    }
}
