#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，11:01
// 方案：FRP
// 项目：Application.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Application.ProjectBC.Query.TemplateQueries;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;

#endregion

namespace UniCloud.Application.ProjectBC.TemplateServices
{
    public class TemplateAppService : ITemplateAppService
    {
        private readonly ITemplateQuery _templateQuery;

        public TemplateAppService(ITemplateQuery templateQuery)
        {
            _templateQuery = templateQuery;
        }

        #region ITemplateAppService 成员

        public IQueryable<TaskStandardDTO> GetTaskStandards()
        {
            var query = new QueryBuilder<TaskStandard>();
            return _templateQuery.TaskStandardDTOQuery(query);
        }

        public IQueryable<WorkGroupDTO> GetWorkGroups()
        {
            var query = new QueryBuilder<WorkGroup>();
            return _templateQuery.WorkGroupDTOQuery(query);
        }

        #endregion


    }
}