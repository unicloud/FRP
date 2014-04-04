#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     购买发动机接收
    /// </summary>
    public class EnginePurchaseReception : Reception
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EnginePurchaseReception()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     添加购买发动机接收行
        /// </summary>
        /// <param name="received">接收数量</param>
        /// <returns>购买发动机接收行</returns>
        public EnginePurchaseReceptionLine AddNewEnginePurchaseReceptionLine(int received)
        {
            var enginePurchaseReceptionLine = new EnginePurchaseReceptionLine();
            enginePurchaseReceptionLine.GenerateNewIdentity();

            enginePurchaseReceptionLine.ReceptionId = Id;
            enginePurchaseReceptionLine.ReceivedAmount = received;

            ReceptionLines.Add(enginePurchaseReceptionLine);

            return enginePurchaseReceptionLine;
        }

        #endregion
    }
}