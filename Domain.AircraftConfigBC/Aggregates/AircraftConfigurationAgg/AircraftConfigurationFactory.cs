#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:52
// 文件名：AircraftConfigurationFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:52
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg
{
    /// <summary>
    ///     飞机配置工厂
    /// </summary>
    public static class AircraftConfigurationFactory
    {
        /// <summary>
        /// 新建飞机配置
        /// </summary>
        /// <returns></returns>
        public static AircraftConfiguration CreateAircraftConfiguration()
        {
            var aircraftConfiguration = new AircraftConfiguration();
            aircraftConfiguration.GenerateNewIdentity();
            return aircraftConfiguration;
        }

        /// <summary>
        /// 设置飞机配置属性
        /// </summary>
        /// <param name="aircraftConfiguration">飞机配置</param>
        /// <param name="configCode">配置代码</param>
        /// <param name="aircraftSeriesId">系列</param>
        /// <param name="aircraftTypeId">机型</param>
        /// <param name="bew">基本空重</param>
        /// <param name="bw">基本重量</param>
        /// <param name="bwf">基本重量重心</param>
        /// <param name="bwi">基本重量指数</param>
        /// <param name="description">描述</param>
        /// <param name="mcc">最大商载</param>
        /// <param name="mlw">最大着陆重量</param>
        /// <param name="mmfw">最大可用燃油</param>
        /// <param name="mtow">最大起飞重量</param>
        /// <param name="mtw">最大滑行重量</param>
        /// <param name="mzfw">最大零燃油重量</param>
        /// <param name="fileName">座位布局图名字</param>
        /// <param name="fileContent">座位布局图</param>
        public static void SetAircraftConfiguration(AircraftConfiguration aircraftConfiguration, string configCode, Guid aircraftSeriesId, Guid aircraftTypeId,
            decimal bew, decimal bw, decimal bwf, decimal bwi, string description, decimal mcc, decimal mlw, decimal mmfw, decimal mtow, decimal mtw, decimal mzfw,
            string fileName, byte[] fileContent)
        {
            aircraftConfiguration.ConfigCode = configCode;
            aircraftConfiguration.AircraftSeriesId = aircraftSeriesId;
            aircraftConfiguration.AircraftTypeId = aircraftTypeId;
            aircraftConfiguration.BEW = bew;
            aircraftConfiguration.BW = bw;
            aircraftConfiguration.BWF = bwf;
            aircraftConfiguration.BWI = bwi;
            aircraftConfiguration.Description = description;
            aircraftConfiguration.MCC = mcc;
            aircraftConfiguration.MLW = mlw;
            aircraftConfiguration.MMFW = mmfw;
            aircraftConfiguration.MTOW = mtow;
            aircraftConfiguration.MTW = mtw;
            aircraftConfiguration.MZFW = mzfw;
            aircraftConfiguration.FileName = fileName;
            aircraftConfiguration.FileContent = fileContent;
        }

        /// <summary>
        /// 新建座位布局
        /// </summary>
        /// <returns></returns>
        public static AircraftCabin CreateAircraftCabin()
        {
            var aircraftCabin = new AircraftCabin();
            aircraftCabin.GenerateNewIdentity();
            return aircraftCabin;
        }

        /// <summary>
        /// 设置座位布局属性
        /// </summary>
        /// <param name="aircraftCabin">座位布局</param>
        /// <param name="aircraftCabinType">舱位类型</param>
        /// <param name="seatNumber">座位数</param>
        /// <param name="note">备注</param>
        public static void SetAircraftCabin(AircraftCabin aircraftCabin, int aircraftCabinType, int seatNumber,string note)
        {
            aircraftCabin.AircraftCabinType = (AircraftCabinType)aircraftCabinType;
            aircraftCabin.SeatNumber = seatNumber;
            aircraftCabin.Note = note;
        }
    }
}
