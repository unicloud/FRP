#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/17 16:33:16
// 文件名：UserData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/17 16:33:16
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.UserAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class UserData : InitialDataBase
    {
        public UserData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var users = new List<User>
                        {
                            UserFactory.CreateUser("001", "", "", "test1", "1", "", "", "", true),
                            UserFactory.CreateUser("002", "", "", "test2", "1", "", "", "", true),
                            UserFactory.CreateUser("002", "", "", "test3", "1", "", "", "", true),
                        };
            users.ForEach(a => Context.Users.AddOrUpdate(u => u.Id, a));
        }
    }
}
