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
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AtaAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;
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
        private IDbSet<CAACAircraftType> _caacAircraftTypes; 
        private IDbSet<Airlines> _airlineses;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Supplier> _suppliers;
        private IDbSet<LicenseType> _licenseTypes;
        private IDbSet<AircraftLicense> _aircraftLicenses;
        private IDbSet<Ata> _atas;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = Set<ActionCategory>()); }
        }

        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = Set<AircraftCategory>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = Set<AircraftType>()); }
        }

        public IDbSet<CAACAircraftType> CaacAircraftTypes
        {
            get { return _caacAircraftTypes ?? (_caacAircraftTypes = Set<CAACAircraftType>()); }
        }
        public IDbSet<Airlines> Airlineses
        {
            get { return _airlineses ?? (_airlineses = Set<Airlines>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = Set<Manufacturer>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = Set<Supplier>()); }
        }

        public IDbSet<LicenseType> LicenseTypes
        {
            get { return _licenseTypes ?? (_licenseTypes = Set<LicenseType>()); }
        }

        public IDbSet<AircraftLicense> AircraftLicenses
        {
            get { return _aircraftLicenses ?? (_aircraftLicenses = Set<AircraftLicense>()); }
        }

        public IDbSet<Ata> Atas
        {
            get { return _atas ?? (_atas = Set<Ata>()); }
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
.Add(new AtaEntityConfiguration())
            #endregion

            #region AircraftCategoryAgg

.Add(new AircraftCategoryEntityConfiguration())

            #endregion

            #region AircraftAgg

.Add(new AircraftEntityConfiguration())

            #endregion

            #region AircraftTypeAgg

.Add(new AircraftTypeEntityConfiguration())
.Add(new CAACAircraftTypeEntityConfiguration())
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

            #region AircraftLicenseAgg
.Add(new LicenseTypeEntityConfiguration())
                .Add(new AircraftLicenseEntityConfiguration())
            #endregion

;
        }

        #endregion
    }
}
