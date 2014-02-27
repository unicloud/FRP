#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间



#endregion

namespace UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg
{
    /// <summary>
    /// AirBusScn工厂。
    /// </summary>
    public static class AirBusScnFactory
    {
        /// <summary>
        /// 创建AirBusScn。
        /// </summary>
        ///  <returns>AirBusScn</returns>
        public static AirBusScn CreateAirBusScn()
        {
            var scn = new AirBusScn();
            scn.GenerateNewIdentity();
            return scn;
        }

        /// <summary>
        ///     设置AirBusScn属性
        /// </summary>
        /// <param name="scn">AirBusScn</param>
        /// <param name="title">标题</param>
        /// <param name="cscNumber">批次号</param>
        /// <param name="modNumber">MOD号</param>
        /// <param name="scnNumber">SCN号</param>
        /// <param name="scnStatus">SCN状态</param>
        /// <param name="description">描述</param>
        public static void SetAirBusScn(AirBusScn scn, string title, string cscNumber, string modNumber, string scnNumber,  int scnStatus, string description)
        {
            scn.Title = title;
            scn.SetCscNumber(cscNumber);
            scn.SetModNumber(modNumber);
            scn.SetScnNumber(scnNumber);
            scn.SetDescription(description);
        }
    }
}
