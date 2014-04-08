#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:21
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.ProjectBC.Aggregates.ProjectAgg;
using UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;
using UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork
{
    public class ProjectBCUnitOfWork : BaseContext<ProjectBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ProjectTemp> _projectTemps;
        private IDbSet<Project> _projects;
        private IDbSet<RelatedDoc> _relatedDocs;
        private IDbSet<TaskStandard> _taskStandards;
        private IDbSet<User> _users;
        private IDbSet<WorkGroup> _workGroups;

        public IDbSet<Project> Projects
        {
            get { return _projects ?? (_projects = base.Set<Project>()); }
        }

        public IDbSet<ProjectTemp> ProjectTemps
        {
            get { return _projectTemps ?? (_projectTemps = base.Set<ProjectTemp>()); }
        }

        public IDbSet<RelatedDoc> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = base.Set<RelatedDoc>()); }
        }

        public IDbSet<TaskStandard> TaskStandards
        {
            get { return _taskStandards ?? (_taskStandards = base.Set<TaskStandard>()); }
        }

        public IDbSet<User> Users
        {
            get { return _users ?? (_users = base.Set<User>()); }
        }

        public IDbSet<WorkGroup> WorkGroups
        {
            get { return _workGroups ?? (_workGroups = base.Set<WorkGroup>()); }
        }

        #endregion

        #region DbContext 重载

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除不需要的公约
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 添加通过“TypeConfiguration”类的方式建立的配置
            if (DbConfig.DbUniCloud.Contains("Oracle"))
            {
                OracleConfigurations(modelBuilder);
            }
            else if (DbConfig.DbUniCloud.Contains("Sql"))
            {
                SqlConfigurations(modelBuilder);
            }
        }

        /// <summary>
        ///     Oracle数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void OracleConfigurations(DbModelBuilder modelBuilder)
        {
        }

        /// <summary>
        ///     SqlServer数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SqlConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations

                #region ProjectAgg

                .Add(new ProjectEntityConfiguration())
                .Add(new TaskEntityConfiguration())

                #endregion

                #region ProjectTempAgg

                .Add(new ProjectTempEntityConfiguration())
                .Add(new TaskTempEntityConfiguration())

                #endregion

                #region RelatedDocAgg

                .Add(new RelatedDocEntityConfiguration())

                #endregion

                #region TaskStandardAgg

                .Add(new TaskStandardEntityConfiguration())
                .Add(new TaskCaseEntityConfiguration())

                #endregion

                #region UserAgg

                .Add(new UserEntityConfiguration())

                #endregion

                #region WorkGroupAgg

                .Add(new WorkGroupEntityConfiguration())
                .Add(new MemberEntityConfiguration());

            #endregion
        }

        #endregion
    }
}