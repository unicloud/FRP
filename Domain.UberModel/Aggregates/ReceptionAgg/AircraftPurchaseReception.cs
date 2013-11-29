#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     购买飞机接收
    /// </summary>
    public class AircraftPurchaseReception : Reception
    {
        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     添加购买飞机接收行
        /// </summary>
        /// <param name="received">接收数量</param>
        /// <returns>购买飞机接收行</returns>
        public AircraftPurchaseReceptionLine AddNewAircraftPurchaseReceptionLine(int received)
        {
            var aircraftPurchaseReceptionLine = new AircraftPurchaseReceptionLine();
            aircraftPurchaseReceptionLine.GenerateNewIdentity();

            aircraftPurchaseReceptionLine.ReceptionId = Id;
            aircraftPurchaseReceptionLine.ReceivedAmount = received;

            ReceptionLines.Add(aircraftPurchaseReceptionLine);

            return aircraftPurchaseReceptionLine;
        }

        #endregion
    }
}