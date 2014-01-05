#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/1 9:50:22
// 文件名：IStaticLoad
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;

#endregion

namespace UniCloud.Application.FleetPlanBC
{
    /// <summary>
    ///     静态数据加载接口
    /// </summary>
    public interface IStaticLoad
    {

        /// <summary>
        /// 设置刷新活动类型集合
        /// </summary>
        void RefreshActionCategory();

        /// <summary>
        /// 设置刷新飞机系列集合
        /// </summary>
        void RefreshAcType();

        /// <summary>
        /// 设置刷新机型集合
        /// </summary>
        void RefreshAircraftType();

        /// <summary>
        /// 设置刷新座级集合
        /// </summary>
        void RefreshAircraftCategory();

        /// <summary>
        /// 设置刷新航空公司集合
        /// </summary>
        void RefreshAirlines();

        /// <summary>
        /// 设置刷新发动机型号集合
        /// </summary>
        void RefreshEngineType();

        /// <summary>
        /// 设置刷新管理者集合
        /// </summary>
        void RefreshManager();

        /// <summary>
        /// 设置刷新制造商集合
        /// </summary>
        void RefreshManufacturer();

        /// <summary>
        /// 设置刷新规划期间集合
        /// </summary>
        void RefreshProgramming();

        /// <summary>
        /// 设置刷新供应商集合
        /// </summary>
        void RefreshSupplier();

        /// <summary>
        ///     获取活动类型静态集合
        /// </summary>
        /// <returns>活动类型静态集合</returns>
        IQueryable<ActionCategoryDTO> GetActionCategories();

        /// <summary>
        ///     获取飞机系列静态集合
        /// </summary>
        /// <returns>飞机系列静态集合</returns>
        IQueryable<AcTypeDTO> GetAcTypes();

        /// <summary>
        ///     获取机型静态集合
        /// </summary>
        /// <returns>机型静态集合</returns>
        IQueryable<AircraftTypeDTO> GetAircraftTypes();

        /// <summary>
        ///     获取座级静态集合
        /// </summary>
        /// <returns>座级静态集合</returns>
        IQueryable<AircraftCategoryDTO> GetAircraftCategories();

        /// <summary>
        ///     获取航空公司静态集合
        /// </summary>
        /// <returns>航空公司静态集合</returns>
        IQueryable<AirlinesDTO> GetAirlineses();

        /// <summary>
        ///     获取发动机型号静态集合
        /// </summary>
        /// <returns>发动机型号静态集合</returns>
        IQueryable<EngineTypeDTO> GetEngineTypes();

        /// <summary>
        ///     获取管理者静态集合
        /// </summary>
        /// <returns>管理者静态集合</returns>
        IQueryable<ManagerDTO> GetManagers();

        /// <summary>
        ///     获取制造商静态集合
        /// </summary>
        /// <returns>制造商静态集合</returns>
        IQueryable<ManufacturerDTO> GetManufacturers();

        /// <summary>
        ///     获取规划期间静态集合
        /// </summary>
        /// <returns>规划期间静态集合</returns>
        IQueryable<ProgrammingDTO> GetProgrammings();

        /// <summary>
        ///     获取供应商静态集合
        /// </summary>
        /// <returns>供应商静态集合</returns>
        IQueryable<SupplierDTO> GetSuppliers();

    }
}
