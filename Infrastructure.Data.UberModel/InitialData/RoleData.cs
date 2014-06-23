#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：20:12
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.RoleAgg;
using UniCloud.Infrastructure.Data.UberModel.InitialData.InitialBase;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class RoleData : InitialDataBase
    {
        public RoleData(UberModelUnitOfWork context)
            : base(context)
        {
        }


        public override void InitialData()
        {
            new List<Role>
            {
                RoleFactory.CreateRole("系统管理员", true),
            }.ForEach(r => Context.Roles.AddOrUpdate(u => u.Name, r));
        }
    }
}