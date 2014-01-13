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

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.ProjectBC.DTO;
using UniCloud.Application.ProjectBC.Query.TemplateQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;

#endregion

namespace UniCloud.Application.ProjectBC.TemplateServices
{
    public class TemplateAppService : ITemplateAppService
    {
        private readonly IProjectTempRepository _projectTempRepository;
        private readonly ITaskStandardRepository _taskStandardRepository;
        private readonly ITemplateQuery _templateQuery;
        private readonly IWorkGroupRepository _workGroupRepository;

        public TemplateAppService(ITemplateQuery templateQuery, IProjectTempRepository projectTempRepository,
            ITaskStandardRepository taskStandardRepository, IWorkGroupRepository workGroupRepository)
        {
            _templateQuery = templateQuery;
            _projectTempRepository = projectTempRepository;
            _taskStandardRepository = taskStandardRepository;
            _workGroupRepository = workGroupRepository;
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

        public IQueryable<UserDTO> GetUsers()
        {
            var query = new QueryBuilder<User>();
            return _templateQuery.UserDTOQuery(query);
        }

        #endregion

        #region TaskStandardDTO

        /// <summary>
        ///     将DTO转为实质的数据实体
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <returns>实质数据实体</returns>
        private static TaskStandard MaterialTaskStandardFromDTO(TaskStandardDTO dto)
        {
            var current = TaskStandardFactory.CreateTaskStandard(dto.Name, dto.Description, dto.OptimisticTime,
                dto.PessimisticTime, dto.NormalTime, dto.IsCustom, (TaskType) dto.TaskType);
            current.ChangeCurrentIdentity(dto.Id);
            return current;
        }

        [Insert(typeof (TaskStandardDTO))]
        public void InsertTaskStandard(TaskStandardDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var current = MaterialTaskStandardFromDTO(dto);

            _taskStandardRepository.Add(current);
        }

        [Update(typeof (TaskStandardDTO))]
        public void UpdateTaskStandard(TaskStandardDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var persist = _taskStandardRepository.Get(dto.Id);
            var current = MaterialTaskStandardFromDTO(dto);

            _taskStandardRepository.Merge(persist, current);
        }

        [Delete(typeof (TaskStandardDTO))]
        public void DeleteTaskStandard(TaskStandardDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var current = _taskStandardRepository.Get(dto.Id);
            if (current != null)
            {
                _taskStandardRepository.Remove(current);
            }
        }

        #endregion

        #region WorkGroupDTO

        /// <summary>
        ///     更新成员数据
        /// </summary>
        /// <param name="dtoMember">成员DTO</param>
        /// <param name="member">成员</param>
        private void UpdateMember(MemberDTO dtoMember, Member member)
        {
            member.UpdateMember(dtoMember.Description);
        }

        /// <summary>
        ///     将DTO转为实质的数据实体
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <returns>实质数据实体</returns>
        private WorkGroup MaterialWorkGroupFromDTO(WorkGroupDTO dto)
        {
            var current = WorkGroupFactory.CreateWorkGroup(dto.Name);
            current.ChangeCurrentIdentity(dto.Id);
            return current;
        }

        /// <summary>
        ///     将DTO转为实质的数据实体
        /// </summary>
        /// <param name="dto">DTO</param>
        /// <returns>实质数据实体</returns>
        private Member MaterialMemberFromDTO(MemberDTO dto)
        {
            var current = WorkGroupFactory.CreateMember(dto.Name, dto.Description, dto.MemberUserId);
            current.ChangeCurrentIdentity(dto.Id);
            return current;
        }

        [Insert(typeof (WorkGroupDTO))]
        public void InsertWorkGroup(WorkGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var current = MaterialWorkGroupFromDTO(dto);
            // 处理工作组成员
            dto.Members.ToList().ForEach(m => current.Members.Add(MaterialMemberFromDTO(m)));

            _workGroupRepository.Add(current);
        }

        [Update(typeof (WorkGroupDTO))]
        public void UpdateWorkGroup(WorkGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var persist = _workGroupRepository.Get(dto.Id);
            var current = MaterialWorkGroupFromDTO(dto);
            _workGroupRepository.Merge(persist, current);
            // 处理工作组成员
            var dtoMembers = dto.Members;
            var members = persist.Members;
            DataHelper.DetailHandle(dtoMembers.ToArray(), members.ToArray(),
                c => c.Id, p => p.Id,
                i => persist.Members.Add(MaterialMemberFromDTO(i)),
                UpdateMember,
                d => _workGroupRepository.RemoveMember(d)
                );
        }

        [Delete(typeof (WorkGroupDTO))]
        public void DeleteWorkGroup(WorkGroupDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("DTO参数为空！");
            }

            var current = _workGroupRepository.Get(dto.Id);
            if (current != null)
            {
                _workGroupRepository.Remove(current);
            }
        }

        #endregion
    }
}