#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 13:55:41
// 文件名：AircraftConfigUnitOfWork
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 13:55:41
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork
{
    public class AircraftConfigBCUnitOfWork : BaseContext<AircraftConfigBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftSeries> _aircraftSeries;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Airlines> _airlineses;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Supplier> _suppliers;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = base.Set<ActionCategory>()); }
        }

        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = base.Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = base.Set<AircraftCategory>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = base.Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = base.Set<AircraftType>()); }
        }

        public IDbSet<Airlines> Airlineses
        {
            get { return _airlineses ?? (_airlineses = base.Set<Airlines>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = base.Set<Manufacturer>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = base.Set<Supplier>()); }
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

                #region ActionCategoryAgg

                .Add(new ActionCategoryEntityConfiguration())

                #endregion

                #region AircraftSeriesAgg

                .Add(new AircraftSeriesEntityConfiguration())

                #endregion

                #region AircraftCategoryAgg

                .Add(new AircraftCategoryEntityConfiguration())

                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())
                .Add(new AircraftLicenseEntityConfiguration())

                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())

                #endregion

                #region AirlinesAgg

                .Add(new AirlinesEntityConfiguration())

                #endregion

                #region ManufacturerAgg

                .Add(new ManufacturerEntityConfiguration())

                #endregion

                #region SupplierAgg

                .Add(new SupplierEntityConfiguration())

                #endregion


                ;
        }

        #endregion
    }
}
