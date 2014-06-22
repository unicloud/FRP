#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:38
// 方案：FRP
// 项目：Infrastructure.Data
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data
{
    /// <summary>
    ///     用于实体框架的数据上下文的抽象基类。
    /// </summary>
    /// <typeparam name="TContext">数据上下文类型。</typeparam>
    public abstract class UniContext<TContext> : DbContext, IQueryableUnitOfWork where TContext : DbContext
    {
        static UniContext()
        {
            Database.SetInitializer<TContext>(null);
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        protected UniContext() : base(Connection, true)
        {
        }

        private static DbConnection Connection
        {
            get
            {
                var modelConfiguration = GetModelConfiguration();
                return modelConfiguration != null ? modelConfiguration.GetDbConnection() : null;
            }
        }

        private static IModelConfiguration GetModelConfiguration()
        {
            var databaseType = ConfigurationManager.AppSettings["DatabaseType"];
            var modelConfigurations = UniContainer.ResolveAll<IModelConfiguration>();
            var modelConfiguration = modelConfigurations.FirstOrDefault(
                configurations =>
                    configurations != null &&
                    configurations.GetDatabaseType()
                        .Equals(databaseType, StringComparison.InvariantCultureIgnoreCase));
            return modelConfiguration;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            var modelConfiguration = GetModelConfiguration();
            if (modelConfiguration != null)
            {
                modelConfiguration.AddConfiguration(modelBuilder);
            }
            base.OnModelCreating(modelBuilder);
        }

        #region IQueryableUnitOfWork 成员

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </summary>
        /// <typeparam name="TEntity">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </returns>
        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </param>
        /// <typeparam name="TEntity">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </typeparam>
        public void Attach<TEntity>(TEntity item) where TEntity : class
        {
            Entry(item).State = EntityState.Unchanged;
        }

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </summary>
        /// <param name="item">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </param>
        /// <typeparam name="TEntity">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </typeparam>
        public void SetModified<TEntity>(TEntity item) where TEntity : class
        {
            Entry(item).State = EntityState.Modified;
        }

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </summary>
        /// <param name="original">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </param>
        /// <param name="current">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </param>
        /// <typeparam name="TEntity">
        ///     <see cref="UniCloud.Infrastructure.Data.IQueryableUnitOfWork" />
        /// </typeparam>
        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class
        {
            Entry(original).CurrentValues.SetValues(current);
        }

        #endregion

        #region IUnitOfWork 成员

        /// <summary>
        ///     <see cref="UniCloud.Domain.IUnitOfWork" />
        /// </summary>
        public void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IUnitOfWork" />
        /// </summary>
        public void CommitAndRefreshChanges()
        {
            bool saveFailed;

            do
            {
                try
                {
                    base.SaveChanges();
                    saveFailed = false;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.ToList()
                        .ForEach(entry => entry.OriginalValues.SetValues(entry.GetDatabaseValues()));
                }
            } while (saveFailed);
        }

        /// <summary>
        ///     <see cref="UniCloud.Domain.IUnitOfWork" />
        /// </summary>
        public void RollbackChanges()
        {
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        #region ISql 成员

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </summary>
        /// <param name="sqlQuery">
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </param>
        /// <param name="parameters">
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </param>
        /// <typeparam name="TEntity">
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </typeparam>
        /// <returns>
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </returns>
        public IQueryable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return Database.SqlQuery<TEntity>(sqlQuery, parameters).AsQueryable<TEntity>();
        }

        /// <summary>
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </summary>
        /// <param name="sqlCommand">
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </param>
        /// <param name="parameters">
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </param>
        /// <returns>
        ///     <see cref="UniCloud.Infrastructure.Data.ISql" />
        /// </returns>
        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion
    }
}