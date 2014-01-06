#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/6 11:01:59
// 文件名：AircraftFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/6 11:01:59
// 修改说明：
// ========================================================================*/
#endregion
using System;

namespace UniCloud.Domain.UberModel.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机工厂
    /// </summary>
    public static class AircraftFactory
    {
        /// <summary>
        ///  创建飞机
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="regNumber">注册号</param>
        /// <param name="serialNumber">序列号</param>
        /// <param name="isOperation">运营状态</param>
        /// <param name="createDate">创建日期</param>
        /// <param name="factoryDate">出厂日期</param>
        /// <param name="importDate">引进日期</param>
        /// <param name="exportDate">注销日期</param>
        /// <param name="seatingCapacity">座位数</param>
        /// <param name="carryingCapacity">商载量（吨）</param>
        /// <param name="supplierId">所有权人外键</param>
        /// <param name="aircraftTypeId">机型外键</param>
        /// <param name="airlinesId">运营权人外键</param>
        /// <param name="importCategoryId">引进方式</param>
        /// <returns></returns>
        public static Aircraft CreateAircraft(Guid id, string regNumber, string serialNumber, bool isOperation, DateTime createDate,
            DateTime? factoryDate, DateTime importDate, DateTime? exportDate, int seatingCapacity, decimal carryingCapacity,
            int? supplierId, Guid aircraftTypeId, Guid airlinesId, Guid importCategoryId)
        {
            var aircraft = new Aircraft
                                   {
                                       CreateDate = createDate,
                                   };
            aircraft.ChangeCurrentIdentity(id);
            aircraft.SetRegNumber(regNumber);
            aircraft.SetSerialNumber(serialNumber);
            aircraft.SetOperation();
            aircraft.SetFactoryDate(factoryDate);
            aircraft.SetImportDate(importDate);
            aircraft.SetExportDate(exportDate);
            aircraft.SetSeatingCapacity(seatingCapacity);
            aircraft.SetCarryingCapacity(carryingCapacity);
            aircraft.SetSupplier(supplierId);
            aircraft.SetAircraftType(aircraftTypeId);
            aircraft.SetAirlines(airlinesId);
            aircraft.SetImportCategory(importCategoryId);
            return aircraft;
        }
    }
}
