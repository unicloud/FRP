#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
// 文件名：IReceptionRepository.cs
// 程序集：UniCloud.Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收仓储接口
    ///     <see cref="UniCloud.Domain.IRepository{Reception}" />
    /// </summary>
    public interface IReceptionRepository : IRepository<Reception>
    {
        /// <summary>
        ///     移除接收行
        /// </summary>
        /// <param name="line">接收行</param>
        void RemoveReceptionLine(ReceptionLine line);

        /// <summary>
        ///     移除交付日程行
        /// </summary>
        /// <param name="schedule">交付日程行</param>
        void RemoveReceptionSchedule(ReceptionSchedule schedule);

        /// <summary>
        ///     移除接收项目
        /// </summary>
        /// <param name="reception">接收项目</param>
        void DeleteReception(Reception reception);
    }
}