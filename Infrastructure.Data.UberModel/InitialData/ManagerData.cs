#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 8:37:09
// 文件名：ManagerData
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.UberModel.Aggregates.ManagerAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ManagerData : InitialDataBase
    {
        public ManagerData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var managers = new List<Manager>
            {
                ManagerFactory.CreateManager(Guid.Parse("31A9DE51-C207-4A73-919C-21521F17FEF9"),"民航局"),
                ManagerFactory.CreateManager(Guid.Parse("EDE13A50-2C89-489A-A44E-B8C690D990AB"),"发改委"),
            };
            managers.ForEach(a => Context.Managers.AddOrUpdate(u => u.Id, a));
        }
    }
}
