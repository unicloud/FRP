#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/20，11:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.UberModel.ValueObjects;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.LinkmanAgg
{
    /// <summary>
    ///     联系人工厂
    /// </summary>
    public static class LinkmanFactory
    {
        /// <summary>
        ///     创建联系人
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="telephone">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="fax">传真</param>
        /// <param name="email">Email</param>
        /// <param name="address">地址</param>
        /// <param name="sourceId">源Id</param>
        /// <returns></returns>
        public static Linkman CreateLinkman(string name, string telephone, string mobile, string fax, string email,
            Address address, Guid sourceId)
        {
            var linkman = new Linkman
            {
                Name = name,
                TelePhone = telephone,
                Mobile = mobile,
                Fax = fax,
                Email = email,
                Address = address
            };

            linkman.SetSourceId(sourceId);
            return linkman;
        }
    }
}