#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/23 16:15:53
// 文件名：FlightLogBCUnitOfWork
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;
using UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.FlightLogBC.UnitOfWork.Mapping
{
    public class FlightLogBCUnitOfWork : BaseContext<FlightLogBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<FlightLog> _flightLogs;


        public IDbSet<FlightLog> FlightLogs
        {
            get { return _flightLogs ?? (_flightLogs = base.Set<FlightLog>()); }
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

                #region FlightLogAgg

                .Add(new FlightLogEntityConfiguration())

                #endregion
                ;
        }

        #endregion
    }
}
