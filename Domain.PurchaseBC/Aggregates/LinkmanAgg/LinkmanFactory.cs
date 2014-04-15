#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，16:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.ValueObjects;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg
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
        /// <param name="isDefault"></param>
        /// <param name="telephone">电话</param>
        /// <param name="mobile">手机</param>
        /// <param name="fax">传真</param>
        /// <param name="email">Email</param>
        /// <param name="department"></param>
        /// <param name="address">地址</param>
        /// <param name="sourceId">源Id</param>
        /// <param name="custCode"></param>
        /// <returns></returns>
        public static Linkman CreateLinkman(string name,bool isDefault, string telephone, string mobile, string fax, string email,
            string department,Address address, Guid sourceId, string custCode)
        {
            var linkman = new Linkman
            {
                Name = name,
                IsDefault = isDefault,
                TelePhone = telephone,
                Mobile = mobile,
                Fax = fax,
                Email = email,
                Department = department,
                Address = address,
                CustCode = custCode
            };

            linkman.SetSourceId(sourceId);
            return linkman;
        }

        public static void SetLinkman(Linkman linkman, string name, string department)
        {
            linkman.Name = name;
            linkman.Department = department;
        }
    }
}