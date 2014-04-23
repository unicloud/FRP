#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 13:43:50
// 文件名：ProgrammingFileFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingFileAgg
{
    /// <summary>
    ///     规划文档工厂
    /// </summary>
    public static class ProgrammingFileFactory
    {
        /// <summary>
        /// 创建规划文档
        /// </summary>
        /// <param name="issuedUnit">发文单位</param>
        /// <param name="issuedDate">发文日期</param>
        /// <param name="docNumber">发文文号</param>
        /// <param name="documentId">文档外键</param>
        /// <param name="docName">文档名称</param>
        /// <param name="programming">规划期间</param>
        /// <param name="type">规划文档类型</param>
        /// <returns>规划文档</returns>
        public static ProgrammingFile CreateProgrammingFile(IssuedUnit issuedUnit,DateTime? issuedDate,string docNumber,Guid documentId,string docName,Programming programming,int type)
        {
            var programmingFile = new ProgrammingFile
            {
                CreateDate = DateTime.Now,
                Type=type,
            };

            programmingFile.GenerateNewIdentity();
            programmingFile.SetDocNumber(docNumber);
            programmingFile.SetDocument(documentId,docName);
            programmingFile.SetIssuedDate(issuedDate);
            programmingFile.SetIssuedUnit(issuedUnit);
            programmingFile.SetProgramming(programming);
            return programmingFile;
        }
    }
}
