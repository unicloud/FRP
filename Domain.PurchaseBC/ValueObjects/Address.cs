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

namespace UniCloud.Domain.PurchaseBC.ValueObjects
{
    /// <summary>
    ///     地址值对象
    /// </summary>
    public class Address : ValueObject<Address>
    {
        #region 构造函数

        /// <summary>
        ///     用于EF
        /// </summary>
        private Address()
        {
        }

        /// <summary>
        ///     构建地址
        /// </summary>
        /// <param name="city">城市</param>
        /// <param name="zipCode">邮编</param>
        /// <param name="addressLine1">地址一</param>
        /// <param name="addressLine2">地址二</param>
        public Address(string city, string zipCode, string addressLine1, string addressLine2)
        {
            City = city;
            ZipCode = zipCode;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     城市
        /// </summary>
        public string City { get; private set; }

        /// <summary>
        ///     邮编
        /// </summary>
        public string ZipCode { get; private set; }

        /// <summary>
        ///     地址一
        /// </summary>
        public string AddressLine1 { get; private set; }

        /// <summary>
        ///     地址二
        /// </summary>
        public string AddressLine2 { get; private set; }

        #endregion
    }
}