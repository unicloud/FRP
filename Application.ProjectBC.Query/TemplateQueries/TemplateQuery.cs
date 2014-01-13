#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，10:08
// 方案：FRP
// 项目：Application.ProjectBC.Query
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.ProjectBC.Query.TemplateQueries
{
    public class TemplateQuery : ITemplateQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public TemplateQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ITemplateQuery 成员

        public IQueryable<TaskStandardDTO> TaskStandardDTOQuery(QueryBuilder<TaskStandard> query)
        {
            var relatedDocs = _unitOfWork.CreateSet<RelatedDoc>();
            var workGroups = _unitOfWork.CreateSet<WorkGroup>();
            var result = query.ApplyTo(_unitOfWork.CreateSet<TaskStandard>()).Select(t => new TaskStandardDTO
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                OptimisticTime = t.OptimisticTime,
                PessimisticTime = t.PessimisticTime,
                NormalTime = t.NormalTime,
                SourceGuid = t.SourceGuid,
                IsCustom = t.IsCustom,
                TaskType = (int) t.TaskType,
                WorkGroupId = t.WorkGroupId,
                TaskCases = t.TaskCases.Select(tc => new TaskCaseDTO
                {
                    Id = tc.Id,
                    TaskStandardId = tc.TaskStandardId,
                    RelatedId = tc.RelatedId != null ? tc.RelatedId.Value : 0,
                    Description = tc.Description
                }).ToList(),
                RelatedDocs = relatedDocs.Where(r => r.SourceId == t.SourceGuid).Select(r => new RelatedDocDTO
                {
                    Id = r.Id,
                    SourceId = r.SourceId,
                    DocumentId = r.DocumentId,
                    DocumentName = r.DocumentName
                }).ToList(),
                Members = workGroups.FirstOrDefault(w => w.Id == t.WorkGroupId).Members.Select(m => new MemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    IsManager = m.IsManager,
                    MemberUserId = m.MemberUserId
                }).ToList()
            });
            return result;
        }

        public IQueryable<WorkGroupDTO> WorkGroupDTOQuery(QueryBuilder<WorkGroup> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<WorkGroup>()).Select(w => new WorkGroupDTO
            {
                Id = w.Id,
                Name = w.Name,
                ManagerId = w.ManagerId,
                ManagerName = w.Manager.Name,
                Members = w.Members.Select(m => new MemberDTO
                {
                    Id = m.Id,
                    Name = m.Name,
                    Description = m.Description,
                    IsManager = m.IsManager,
                    MemberUserId = m.MemberUserId
                }).ToList()
            });
            return result;
        }

        public IQueryable<UserDTO> UserDTOQuery(QueryBuilder<User> query)
        {
            var result = query.ApplyTo(_unitOfWork.CreateSet<User>()).Select(u => new UserDTO
            {
                Id = u.Id,
                EmployeeCode = u.EmployeeCode,
                DisplayName = u.DisplayName
            });
            return result;
        }

        #endregion
    }
}