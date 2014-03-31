#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TechnicalSolutionFactory
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System;
#endregion

namespace UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg
{
    /// <summary>
    /// TechnicalSolution工厂。
    /// </summary>
    public static class TechnicalSolutionFactory
    {
        /// <summary>
        /// 创建TechnicalSolution。
        /// </summary>
        ///  <returns>TechnicalSolution</returns>
        public static TechnicalSolution CreateTechnicalSolution()
        {
            var technicalSolution = new TechnicalSolution
            {
            };
            technicalSolution.GenerateNewIdentity();
            return technicalSolution;
        }

        /// <summary>
        /// 创建技术解决方案
        /// </summary>
        /// <param name="fiNumber">功能标识号</param>
        /// <param name="position">位置</param>
        /// <param name="tsNumber">技术解决方案编号</param>
        /// <returns></returns>
        public static TechnicalSolution CreateTechnicalSolution(string fiNumber,string position,string tsNumber)
        {
            var technicalSolution = new TechnicalSolution
            {
            };
            technicalSolution.GenerateNewIdentity();
            technicalSolution.SetFiNumber(fiNumber);
            technicalSolution.SetPosition(position);
            technicalSolution.SetTsNumber(tsNumber);
            return technicalSolution;
        }
    }
}
