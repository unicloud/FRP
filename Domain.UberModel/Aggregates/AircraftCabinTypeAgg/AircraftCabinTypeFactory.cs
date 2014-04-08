#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 9:35:52
// 文件名：AircraftCabinTypeFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 9:35:52
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AircraftCabinTypeAgg
{
    /// <summary>
    ///     飞机舱位类型工厂
    /// </summary>
    public static class AircraftCabinTypeFactory
    {
        /// <summary>
        /// 新建座位布局
        /// </summary>
        /// <returns></returns>
        public static AircraftCabinType CreateAircraftCabinType()
        {
            var aircraftCabin = new AircraftCabinType();
            aircraftCabin.GenerateNewIdentity();
            return aircraftCabin;
        }

        public static AircraftCabinType CreateAircraftCabinType(int id,string name)
        {
            var aircraftCabin = new AircraftCabinType();
            aircraftCabin.ChangeCurrentIdentity(id);
            aircraftCabin.Name = name;
            return aircraftCabin;
        }

        /// <summary>
        /// 设置舱位类型属性
        /// </summary>
        /// <param name="aircraftCabinType">舱位类型</param>
        /// <param name="name">名称</param>
        /// <param name="note">备注</param>
        public static void SetAircraftCabinType(AircraftCabinType aircraftCabinType, string name, string note)
        {
            aircraftCabinType.Name = name;
            aircraftCabinType.Note = note;
        }
    }
}
