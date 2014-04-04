#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 10:14:27
// 文件名：IAirProgrammingRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion


namespace UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     航空公司五年规划仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{AirProgramming}" />
    /// </summary>
    public interface IAirProgrammingRepository : IRepository<AirProgramming>
    {
        /// <summary>
        /// 删除航空公司五年规划
        /// </summary>
        /// <param name="airProgramming"></param>
        void DeleteAirProgramming(AirProgramming airProgramming);

        /// <summary>
        /// 移除规划行
        /// </summary>
        /// <param name="line"></param>
        void RemoveAirProgrammingLine(AirProgrammingLine line);
    }
}
