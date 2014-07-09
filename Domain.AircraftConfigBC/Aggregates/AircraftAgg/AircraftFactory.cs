#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，09:11
// 文件名：AircraftFactory.cs
// 程序集：UniCloud.Domain.AircraftConfigBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg
{
    /// <summary>
    ///     飞机工厂
    /// </summary>
    public static class AircraftFactory
    {
        /// <summary>
        ///     创建新飞机
        /// </summary>
        /// <param name="regNumber">飞机注册号</param>
        /// <param name="serialNumber"></param>
        /// <param name="aircraftTypeId">机型ID</param>
        /// <param name="actionCategoryId">业务活动类型</param>
        /// <param name="airlinesId">航空公司ID</param>
        /// <returns>飞机</returns>
        public static Aircraft CreateAircraft(string regNumber, string serialNumber,
            Guid airlinesId, Guid aircraftTypeId, Guid actionCategoryId)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
            {
                throw new ArgumentException("注册号参数为空！");
            }
            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("序列号参数为空！");
            }

            var aircraft = new Aircraft
            {
                RegNumber = regNumber,
                SerialNumber = serialNumber,
                AirlinesId = airlinesId,
                AircraftTypeId = aircraftTypeId,
                ImportCategoryId = actionCategoryId,
                CreateDate = DateTime.Now
            };
            aircraft.GenerateNewIdentity();

            return aircraft;
        }
    }
}