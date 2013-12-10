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
    ///     租赁发动机接收
    /// </summary>
    public class EngineLeaseReception : Reception
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineLeaseReception()
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
        ///     添加租赁发动机接收行
        /// </summary>
        /// <param name="received">接收数量</param>
        /// <returns>租赁发动机接收行</returns>
        public EngineLeaseReceptionLine AddNewEngineLeaseReceptionLine(int received)
        {
            var engineLeaseReceptionLine = new EngineLeaseReceptionLine();
            engineLeaseReceptionLine.GenerateNewIdentity();

            engineLeaseReceptionLine.ReceptionId = Id;
            engineLeaseReceptionLine.ReceivedAmount = received;

            ReceptionLines.Add(engineLeaseReceptionLine);

            return engineLeaseReceptionLine;
        }

        #endregion
    }
}