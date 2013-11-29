#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：IForwarderAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.ForwarderServices
{
    /// <summary>
    ///     表示用于处理承运人相关信息服务接口。
    /// </summary>
    public interface IForwarderAppService
    {
        /// <summary>
        ///     获取所有承运人。
        /// </summary>
        /// <returns>所有承运人。</returns>
        IQueryable<ForwarderDTO> GetForwarders();

        /// <summary>
        ///     新增承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        void InsertForwarder(ForwarderDTO forwarder);

        /// <summary>
        ///     更新承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        void ModifyForwarder(ForwarderDTO forwarder);


        /// <summary>
        ///     删除承运人。
        /// </summary>
        /// <param name="forwarder">承运人DTO。</param>
        void DeleteForwarder(ForwarderDTO forwarder);
    }
}