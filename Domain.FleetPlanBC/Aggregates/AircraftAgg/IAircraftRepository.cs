#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 9:35:19
// 文件名：IAircraftRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     实际飞机仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Aircraft}" />
    /// </summary>
    public interface IAircraftRepository : IRepository<Aircraft>
    {
        /// <summary>
        /// 删除飞机
        /// </summary>
        /// <param name="aircraft"></param>
        void DeleteAircraft(Aircraft aircraft);

        /// <summary>
        /// 移除商业数据历史
        /// </summary>
        /// <param name="ab"></param>
        void RemoveAircraftBusiness(AircraftBusiness ab);

        /// <summary>
        /// 移除运营权历史
        /// </summary>
        /// <param name="oh"></param>
        void RemoveOperationHistory(OperationHistory oh);

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="oh"></param>
        void RemoveOwnershipHistory(OwnershipHistory oh);

        /// <summary>
        /// 移除飞机配置历史
        /// </summary>
        /// <param name="ah">飞机配置历史</param>
        void RemoveAcConfigHistory(AcConfigHistory ah);

        /// <summary>
        /// 获取单个运营权历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OperationHistory GetPh(object id);


        /// <summary>
        /// 获取单个的商业数据历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AircraftBusiness GetAb(object id);


        /// <summary>
        /// 获取单个的所有权历史
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        OwnershipHistory GetOh(object id);

    }
}
