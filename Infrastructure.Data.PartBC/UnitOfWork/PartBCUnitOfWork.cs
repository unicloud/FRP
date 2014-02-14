#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:34:55
// 文件名：PartBCUnitOfWork
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.PartBC.Aggregates.MScnAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork
{
    public class PartBCUnitOfWork : BaseContext<PartBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<Scn> _scns;
        private IDbSet<MScn> _mscns;

        public IDbSet<Scn> Scns
        {
            get { return _scns ?? (_scns = base.Set<Scn>()); }
        }

        public IDbSet<MScn> MScns
        {
            get { return _mscns ?? (_mscns = base.Set<MScn>()); }
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

                #region Scn

                .Add(new ScnEntityConfiguration())
                .Add(new ScnLineEntityConfiguration())
                #endregion

                #region MScn

                .Add(new MScnEntityConfiguration())
                .Add(new MScnLineEntityConfiguration())
                #endregion

                ;
        }

        #endregion
    }
}
