#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，18:11
// 文件名：ForwarderFactory.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.Common.ValueObjects;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg
{
    /// <summary>
    ///     承运人工厂
    /// </summary>
    public static class ForwarderFactory
    {
        /// <summary>
        ///     创建对象。
        /// </summary>
        /// <param name="name">名称。</param>
        /// <param name="tel">电话。</param>
        /// <param name="fax">传真。</param>
        /// <param name="attn">联系人。</param>
        /// <param name="email">邮件。</param>
        /// <param name="addr">地址。</param>
        /// <returns></returns>
        public static Forwarder Create(string name, string tel, string fax, string attn, string email, string addr)
        {
            return new Forwarder
            {
                CnName = name,
                Tel = tel,
                Fax = fax,
                Attn = attn,
                Email = email,
                Address = new Address(null, null, addr, null)
            };
        }
    }
}