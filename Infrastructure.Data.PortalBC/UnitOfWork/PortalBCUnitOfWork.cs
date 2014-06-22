#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：9:41
// 方案：FRP
// 项目：Infrastructure.Data.PortalBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.PortalBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.PortalBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PortalBC.Aggregates.CAACAircraftTypeAgg;
using UniCloud.Domain.PortalBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.PortalBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PortalBC.UnitOfWork
{
    public class PortalBCUnitOfWork : UniContext<PortalBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<ActionCategory> _actionCategories;
        private IDbSet<AircraftCategory> _aircraftCategories;
        private IDbSet<AircraftSeries> _aircraftSeries;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<CAACAircraftType> _caacAircraftTypes;
        private IDbSet<Manufacturer> _manufacturers;
        private IDbSet<Supplier> _suppliers;

        public IDbSet<ActionCategory> ActionCategories
        {
            get { return _actionCategories ?? (_actionCategories = Set<ActionCategory>()); }
        }

        public IDbSet<AircraftCategory> AircraftCategories
        {
            get { return _aircraftCategories ?? (_aircraftCategories = Set<AircraftCategory>()); }
        }


        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = Set<AircraftSeries>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = Set<AircraftType>()); }
        }

        public IDbSet<CAACAircraftType> CAACAircraftTypes
        {
            get { return _caacAircraftTypes ?? (_caacAircraftTypes = Set<CAACAircraftType>()); }
        }

        public IDbSet<Manufacturer> Manufacturers
        {
            get { return _manufacturers ?? (_manufacturers = Set<Manufacturer>()); }
        }

        public IDbSet<Supplier> Suppliers
        {
            get { return _suppliers ?? (_suppliers = Set<Supplier>()); }
        }

        #endregion
    }
}