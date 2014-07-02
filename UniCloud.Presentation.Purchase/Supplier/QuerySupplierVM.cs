#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/03，14:12
// 文件名：QuerySupplierVM.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    /// <summary>
    ///     查询供应商信息
    /// </summary>
    [Export(typeof (QuerySupplierVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QuerySupplierVM : EditViewModelBase
    {
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;
        private FilterDescriptor _linkManFilter; //查找联系人配置。
        private FilterDescriptor _supplierFilter; //查找供应商配置。

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QuerySupplierVM(IPurchaseService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialSupplierCompany(); //初始化合作公司。
            InitialSupplier(); //初始化联系人。
            InitialLinkMan(); //初始化联系人。
            InitialSupplierCompanyAcMaterial(); //初始化合作公司飞机物料
            InitialSupplierCompanyBfeMaterial(); //初始化合作公司BFE物料
            InitialSupplierCompanyEngineMaterial(); //初始化合作公司发动机物料
        }

        #region SupplierCompany相关信息

        private SupplierCompanyDTO _selectedSupplierCompany;

        /// <summary>
        ///     选择合作公司。
        /// </summary>
        public SupplierCompanyDTO SelectedSupplierCompany
        {
            get { return _selectedSupplierCompany; }
            set
            {
                if (_selectedSupplierCompany != value)
                {
                    _selectedSupplierCompany = value;
                    LoadLinkManByCompany(value);
                    LoadSupplierByCompany(value);
                    LoadAcMaterialByCompany(value);
                    LoadBfeMaterialByCompany(value);
                    LoadEngineMaterialByCompany(value);
                    RaisePropertyChanged(() => SelectedSupplierCompany);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyDTO> SupplierCompanies { get; set; }

        /// <summary>
        ///     初始化合作公司信息。
        /// </summary>
        private void InitialSupplierCompany()
        {
            SupplierCompanies = _service.CreateCollection(_context.SupplierCompanys);
            SupplierCompanies.PageSize = 20;
            _service.RegisterCollectionView(SupplierCompanies); //注册查询集合。
            SupplierCompanies.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplierCompany = e.Entities.Cast<SupplierCompanyDTO>().FirstOrDefault();
            };
        }

        #endregion

        #region LinkMan相关信息

        private LinkmanDTO _selectedLinkMan;

        /// <summary>
        ///     选择联系人。
        /// </summary>
        public LinkmanDTO SelectedLinkMan
        {
            get { return _selectedLinkMan; }
            set
            {
                _selectedLinkMan = value;
                RaisePropertyChanged(() => SelectedLinkMan);
            }
        }

        /// <summary>
        ///     获取某供应商公司下所有联系人。
        /// </summary>
        public QueryableDataServiceCollectionView<LinkmanDTO> Linkmen { get; set; }

        /// <summary>
        ///     初始化联系人。
        /// </summary>
        private void InitialLinkMan()
        {
            Linkmen = _service.CreateCollection(_context.Linkmans);
            _service.RegisterCollectionView(Linkmen); //注册查询集合。
            _linkManFilter = new FilterDescriptor("SourceId", FilterOperator.IsEqualTo, Guid.Empty);
            Linkmen.FilterDescriptors.Add(_linkManFilter);
            Linkmen.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedLinkMan = e.Entities.Cast<LinkmanDTO>().FirstOrDefault();
            };
            Linkmen.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("CurrentAddItem", StringComparison.OrdinalIgnoreCase))
                {
                    if (Linkmen.CurrentAddItem is LinkmanDTO)
                    {
                        (Linkmen.CurrentAddItem as LinkmanDTO).SourceId = SelectedSupplierCompany.LinkManId;
                        (Linkmen.CurrentAddItem as LinkmanDTO).LinkmanId = RandomHelper.Next();
                    }
                }
            };
        }


        /// <summary>
        ///     根据合作公司Id，加载联系人。
        /// </summary>
        /// <param name="supplierCompany">供应商</param>
        private void LoadLinkManByCompany(SupplierCompanyDTO supplierCompany)
        {
            //加载联系人
            if (supplierCompany == null) return;
            _linkManFilter.Value = supplierCompany.LinkManId;
            if (!Linkmen.AutoLoad)
            {
                Linkmen.AutoLoad = true;
            }
            else
            {
                Linkmen.Load(true);
            }
        }

        #endregion

        #region Supplier相关信息

        private SupplierDTO _selectedSupplier;

        /// <summary>
        ///     选择供应商。
        /// </summary>
        public SupplierDTO SelectedSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                if (_selectedSupplier != value)
                {
                    _selectedSupplier = value;
                    RaisePropertyChanged(() => SelectedSupplier);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        /// <summary>
        ///     初始化供应商。
        /// </summary>
        private void InitialSupplier()
        {
            Suppliers = _service.CreateCollection(_context.Suppliers);
            _service.RegisterCollectionView(Suppliers); //注册查询集合。
            _supplierFilter = new FilterDescriptor("SuppierCompanyId", FilterOperator.IsEqualTo, -1);
            Suppliers.FilterDescriptors.Add(_supplierFilter);
            Suppliers.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplier = e.Entities.Cast<SupplierDTO>().FirstOrDefault();
            };
        }

        /// <summary>
        ///     根据合作公司Id，加载联系人。
        /// </summary>
        /// <param name="supplierCompany">供应商</param>
        private void LoadSupplierByCompany(SupplierCompanyDTO supplierCompany)
        {
            //加载供应商
            if (supplierCompany == null) return;
            _supplierFilter.Value = supplierCompany.SupplierCompanyId;
            if (!Suppliers.AutoLoad)
            {
                Suppliers.AutoLoad = true;
            }
            else
            {
                Suppliers.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyAcMaterial相关信息

        private FilterDescriptor _acMeterialFilter; //查找合作公司飞机物料。
        private SupplierCompanyAcMaterialDTO _selectedSupplierCompanyAcMaterial;

        /// <summary>
        ///     选择合作公司飞机物料。
        /// </summary>
        public SupplierCompanyAcMaterialDTO SelectedSupplierCompanyAcMaterial
        {
            get { return _selectedSupplierCompanyAcMaterial; }
            set
            {
                if (_selectedSupplierCompanyAcMaterial != value)
                {
                    _selectedSupplierCompanyAcMaterial = value;
                    RaisePropertyChanged(() => SelectedSupplierCompanyAcMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司飞机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> SupplierCompanyAcMaterials { get; set; }

        /// <summary>
        ///     初始化合作公司飞机物料信息。
        /// </summary>
        private void InitialSupplierCompanyAcMaterial()
        {
            SupplierCompanyAcMaterials = _service.CreateCollection(_context.SupplierCompanyAcMaterials);
            _service.RegisterCollectionView(SupplierCompanyAcMaterials); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _acMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyAcMaterials.FilterDescriptors.Add(_acMeterialFilter);
            SupplierCompanyAcMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplierCompanyAcMaterial = e.Entities.Cast<SupplierCompanyAcMaterialDTO>().FirstOrDefault();
            };
        }

        /// <summary>
        ///     根据合作公司Id，加载合作公司飞机物料。
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        private void LoadAcMaterialByCompany(SupplierCompanyDTO supplierCompany)
        {
            if (supplierCompany == null) return;
            _acMeterialFilter.Value = supplierCompany.SupplierCompanyId;
            if (!SupplierCompanyAcMaterials.AutoLoad)
            {
                SupplierCompanyAcMaterials.AutoLoad = true;
            }
            else
            {
                SupplierCompanyAcMaterials.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyEngineMaterial相关信息

        private FilterDescriptor _engienMeterialFilter; //查找合作公司发动机物料。
        private SupplierCompanyEngineMaterialDTO _selectedSupplierCompanyEngineMaterial;

        /// <summary>
        ///     选择合作公司发动机物料。
        /// </summary>
        public SupplierCompanyEngineMaterialDTO SelectedSupplierCompanyEngineMaterial
        {
            get { return _selectedSupplierCompanyEngineMaterial; }
            set
            {
                if (_selectedSupplierCompanyEngineMaterial != value)
                {
                    _selectedSupplierCompanyEngineMaterial = value;
                    RaisePropertyChanged(() => SelectedSupplierCompanyEngineMaterial);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> SupplierCompanyEngineMaterials {
            get; set; }

        /// <summary>
        ///     初始化合作公司发动机物料信息。
        /// </summary>
        private void InitialSupplierCompanyEngineMaterial()
        {
            SupplierCompanyEngineMaterials = _service.CreateCollection(_context.SupplierCompanyEngineMaterials);
            _service.RegisterCollectionView(SupplierCompanyEngineMaterials); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _engienMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyEngineMaterials.FilterDescriptors.Add(_engienMeterialFilter);
            SupplierCompanyEngineMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplierCompanyEngineMaterial =
                    e.Entities.Cast<SupplierCompanyEngineMaterialDTO>().FirstOrDefault();
            };
        }

        /// <summary>
        ///     根据合作公司Id，加载合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        private void LoadEngineMaterialByCompany(SupplierCompanyDTO supplierCompany)
        {
            if (supplierCompany == null) return;
            _engienMeterialFilter.Value = supplierCompany.SupplierCompanyId;
            if (!SupplierCompanyEngineMaterials.AutoLoad)
            {
                SupplierCompanyEngineMaterials.AutoLoad = true;
            }
            else
            {
                SupplierCompanyEngineMaterials.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyBFEMaterial相关信息

        private FilterDescriptor _bfeMeterialFilter; //查找合作公司Bfe物料。
        private SupplierCompanyBFEMaterialDTO _selectedSupplierCompanyBfeMaterial;

        /// <summary>
        ///     选择合作公司BFE物料。
        /// </summary>
        public SupplierCompanyBFEMaterialDTO SelectedSupplierCompanyBfeMaterial
        {
            get { return _selectedSupplierCompanyBfeMaterial; }
            set
            {
                if (_selectedSupplierCompanyBfeMaterial != value)
                {
                    _selectedSupplierCompanyBfeMaterial = value;
                    RaisePropertyChanged(() => SelectedSupplierCompanyBfeMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司BFE物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> SupplierCompanyBfeMaterials { get; set;
        }

        /// <summary>
        ///     初始化合作公司BFE物料信息。
        /// </summary>
        private void InitialSupplierCompanyBfeMaterial()
        {
            SupplierCompanyBfeMaterials = _service.CreateCollection(_context.SupplierCompanyBFEMaterials);
            _service.RegisterCollectionView(SupplierCompanyBfeMaterials); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _bfeMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyBfeMaterials.FilterDescriptors.Add(_bfeMeterialFilter);
            SupplierCompanyBfeMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplierCompanyBfeMaterial = e.Entities.Cast<SupplierCompanyBFEMaterialDTO>().FirstOrDefault();
            };
        }

        /// <summary>
        ///     根据合作公司Id，加载合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        private void LoadBfeMaterialByCompany(SupplierCompanyDTO supplierCompany)
        {
            if (supplierCompany == null) return;
            _bfeMeterialFilter.Value = supplierCompany.SupplierCompanyId;
            if (!SupplierCompanyBfeMaterials.AutoLoad)
            {
                SupplierCompanyBfeMaterials.AutoLoad = true;
            }
            else
            {
                SupplierCompanyBfeMaterials.Load(true);
            }
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            if (!SupplierCompanies.AutoLoad)
            {
                SupplierCompanies.AutoLoad = true; //加载数据。
            }
            else
            {
                SupplierCompanies.Load(true);
            }
        }

        #endregion
    }
}