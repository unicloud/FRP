#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IBasicConfigGroupRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg
{
    /// <summary>
    /// BasicConfigGroup仓储接口。
    /// </summary>
    public interface IBasicConfigGroupRepository : IRepository<BasicConfigGroup>
    {
        /// <summary>
        /// 删除基本构型组
        /// </summary>
        /// <param name="basicConfigGroup"></param>
        void DeleteBasicConfigGroup(BasicConfigGroup basicConfigGroup);

        /// <summary>
        ///     移除基本构型
        /// </summary>
        /// <param name="basicConfig">基本构型</param>
        void RemoveBasicConfig(BasicConfig basicConfig);
    }
}
