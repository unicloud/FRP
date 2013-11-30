#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，09:11
// 文件名：AddressConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.ValueObjects;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    internal class AddressConfiguration : ComplexTypeConfiguration<Address>
    {
        public AddressConfiguration()
        {
            Property(p => p.City).HasColumnName("City");
            Property(p => p.ZipCode).HasColumnName("ZipCode");
            Property(p => p.AddressLine1).HasColumnName("AddressLine1");
            Property(p => p.AddressLine2).HasColumnName("AddressLine2");
        }
    }
}