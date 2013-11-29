#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，16:11
// 文件名：ForwardData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region

using UniCloud.Domain.UberModel.Aggregates.ForwarderAgg;
using UniCloud.Domain.UberModel.ValueObjects;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ForwardData : InitialDataBase
    {

        public ForwardData(UberModelUnitOfWork context)
            : base(context)
        {
        }


        /// <summary>
        ///     初始化承运人相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var forwarder = new Forwarder
            {
                Address = new Address(null, null, "四川成都", null),
                Attn = "陈春勇",
                Email = "test@163.com",
                CnName = "川航承运人",
                Fax = "1234567",
                Tel = "1234567",
            };
            Context.Forwarders.Add(forwarder);
        }
    }
}