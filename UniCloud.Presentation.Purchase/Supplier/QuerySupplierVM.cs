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
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    /// <summary>
    ///     查询供应商信息
    /// </summary>
    [Export(typeof (QuerySupplierVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QuerySupplierVM : EditViewModelBase
    {
        private FilterDescriptor _linkManFilter; //查找联系人配置。
        private PurchaseData _purchaseData;
        private FilterDescriptor _supplierFilter; //查找供应商配置。

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QuerySupplierVM()
        {
            InitialSupplierCompany(); //初始化合作公司。
            InitialSupplier(); //初始化联系人。
            InitialLinkMan(); //初始化联系人。
            InitialSupplierCompanyAcMaterial(); //初始化合作公司飞机物料
            InitialSupplierCompanyBFEMaterial(); //初始化合作公司BFE物料
            InitialSupplierCompanyEngineMaterial(); //初始化合作公司发动机物料
        }

        #region SupplierCompany相关信息

        private SupplierCompanyDTO _selectedSupplierCompany;

        /// <summary>
        ///     选择合作公司。
        /// </summary>
        public SupplierCompanyDTO SelSupplierCompany
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
                    LoadBFEMaterialByCompany(value);
                    LoadEngineMaterialByCompany(value);
                    RaisePropertyChanged(() => SelSupplierCompany);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyDTO> SupplierCompanysView { get; set; }

        /// <summary>
        ///     初始化合作公司信息。
        /// </summary>
        private void InitialSupplierCompany()
        {
            SupplierCompanysView = Service.CreateCollection(_purchaseData.SupplierCompanys);
            Service.RegisterCollectionView(SupplierCompanysView); //注册查询集合。
            SupplierCompanysView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompany = e.Entities.Cast<SupplierCompanyDTO>().FirstOrDefault();
                };
        }

        #endregion

        #region LinkMan相关信息

        private LinkmanDTO _selectedLinkMan;

        /// <summary>
        ///     选择联系人。
        /// </summary>
        public LinkmanDTO SelLinkMan
        {
            get { return _selectedLinkMan; }
            set
            {
                _selectedLinkMan = value;
                RaisePropertyChanged(() => SelLinkMan);
            }
        }

        /// <summary>
        ///     获取某供应商公司下所有联系人。
        /// </summary>
        public QueryableDataServiceCollectionView<LinkmanDTO> LinkmansView { get; set; }

        /// <summary>
        ///     初始化联系人。
        /// </summary>
        private void InitialLinkMan()
        {
            LinkmansView = Service.CreateCollection(_purchaseData.Linkmans);
            Service.RegisterCollectionView(LinkmansView); //注册查询集合。
            _linkManFilter = new FilterDescriptor("SourceId", FilterOperator.IsEqualTo, Guid.Empty);
            LinkmansView.FilterDescriptors.Add(_linkManFilter);
            LinkmansView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelLinkMan = e.Entities.Cast<LinkmanDTO>().FirstOrDefault();
                };
            LinkmansView.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "CurrentAddItem")
                    {
                        if (LinkmansView.CurrentAddItem is LinkmanDTO)
                        {
                            (LinkmansView.CurrentAddItem as LinkmanDTO).SourceId = SelSupplierCompany.LinkManId;
                            (LinkmansView.CurrentAddItem as LinkmanDTO).LinkmanId = RandomHelper.Next();
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
            if (!LinkmansView.AutoLoad)
            {
                LinkmansView.AutoLoad = true;
            }
            else
            {
                LinkmansView.Load(true);
            }
        }

        #endregion

        #region Supplier相关信息

        private SupplierDTO _selectedSupplier;

        /// <summary>
        ///     选择供应商。
        /// </summary>
        public SupplierDTO SelSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                if (_selectedSupplier != value)
                {
                    _selectedSupplier = value;
                    RaisePropertyChanged(() => SelSupplier);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> SuppliersView { get; set; }

        /// <summary>
        ///     初始化供应商。
        /// </summary>
        private void InitialSupplier()
        {
            SuppliersView = Service.CreateCollection(_purchaseData.Suppliers);
            Service.RegisterCollectionView(SuppliersView); //注册查询集合。
            _supplierFilter = new FilterDescriptor("SuppierCompanyId", FilterOperator.IsEqualTo, -1);
            SuppliersView.FilterDescriptors.Add(_supplierFilter);
            SuppliersView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplier = e.Entities.Cast<SupplierDTO>().FirstOrDefault();
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
            if (!SuppliersView.AutoLoad)
            {
                SuppliersView.AutoLoad = true;
            }
            else
            {
                SuppliersView.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyAcMaterial相关信息

        private FilterDescriptor _acMeterialFilter; //查找合作公司飞机物料。
        private SupplierCompanyAcMaterialDTO _selectedSupplierCompanyAcMaterial;

        /// <summary>
        ///     选择合作公司飞机物料。
        /// </summary>
        public SupplierCompanyAcMaterialDTO SelSupplierCompanyAcMaterial
        {
            get { return _selectedSupplierCompanyAcMaterial; }
            set
            {
                if (_selectedSupplierCompanyAcMaterial != value)
                {
                    _selectedSupplierCompanyAcMaterial = value;
                    RaisePropertyChanged(() => SelSupplierCompanyAcMaterial);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司飞机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> SupplierCompanyAcMaterialsView { get; set; }

        /// <summary>
        ///     初始化合作公司飞机物料信息。
        /// </summary>
        private void InitialSupplierCompanyAcMaterial()
        {
            SupplierCompanyAcMaterialsView = Service.CreateCollection(_purchaseData.SupplierCompanyAcMaterials);
            Service.RegisterCollectionView(SupplierCompanyAcMaterialsView); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _acMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyAcMaterialsView.FilterDescriptors.Add(_acMeterialFilter);
            SupplierCompanyAcMaterialsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompanyAcMaterial = e.Entities.Cast<SupplierCompanyAcMaterialDTO>().FirstOrDefault();
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
            if (!SupplierCompanyAcMaterialsView.AutoLoad)
            {
                SupplierCompanyAcMaterialsView.AutoLoad = true;
            }
            else
            {
                SupplierCompanyAcMaterialsView.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyEngineMaterial相关信息

        private FilterDescriptor _engienMeterialFilter; //查找合作公司发动机物料。
        private SupplierCompanyEngineMaterialDTO _selectedSupplierCompanyEngineMaterial;

        /// <summary>
        ///     选择合作公司发动机物料。
        /// </summary>
        public SupplierCompanyEngineMaterialDTO SelSupplierCompanyEngineMaterial
        {
            get { return _selectedSupplierCompanyEngineMaterial; }
            set
            {
                if (_selectedSupplierCompanyEngineMaterial != value)
                {
                    _selectedSupplierCompanyEngineMaterial = value;
                    RaisePropertyChanged(() => SelSupplierCompanyEngineMaterial);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> SupplierCompanyEngineMaterialsView { get; set; }

        /// <summary>
        ///     初始化合作公司发动机物料信息。
        /// </summary>
        private void InitialSupplierCompanyEngineMaterial()
        {
            SupplierCompanyEngineMaterialsView = Service.CreateCollection(_purchaseData.SupplierCompanyEngineMaterials);
            Service.RegisterCollectionView(SupplierCompanyEngineMaterialsView); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _engienMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyEngineMaterialsView.FilterDescriptors.Add(_engienMeterialFilter);
            SupplierCompanyEngineMaterialsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompanyEngineMaterial =
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
            if (!SupplierCompanyEngineMaterialsView.AutoLoad)
            {
                SupplierCompanyEngineMaterialsView.AutoLoad = true;
            }
            else
            {
                SupplierCompanyEngineMaterialsView.Load(true);
            }
        }

        #endregion

        #region SupplierCompanyBFEMaterial相关信息

        private FilterDescriptor _bfeMeterialFilter; //查找合作公司Bfe物料。
        private SupplierCompanyBFEMaterialDTO _selectedSupplierCompanyBFEMaterial;

        /// <summary>
        ///     选择合作公司BFE物料。
        /// </summary>
        public SupplierCompanyBFEMaterialDTO SelSupplierCompanyBFEMaterial
        {
            get { return _selectedSupplierCompanyBFEMaterial; }
            set
            {
                if (_selectedSupplierCompanyBFEMaterial != value)
                {
                    _selectedSupplierCompanyBFEMaterial = value;
                    RaisePropertyChanged(() => SelSupplierCompanyBFEMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司BFE物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> SupplierCompanyBFEMaterialsView { get; set; }

        /// <summary>
        ///     初始化合作公司BFE物料信息。
        /// </summary>
        private void InitialSupplierCompanyBFEMaterial()
        {
            SupplierCompanyBFEMaterialsView = Service.CreateCollection(_purchaseData.SupplierCompanyBFEMaterials);
            Service.RegisterCollectionView(SupplierCompanyBFEMaterialsView); //注册查询集合。
            //根据合作公司Id，查询飞机物料
            _bfeMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyBFEMaterialsView.FilterDescriptors.Add(_bfeMeterialFilter);
            SupplierCompanyBFEMaterialsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompanyBFEMaterial = e.Entities.Cast<SupplierCompanyBFEMaterialDTO>().FirstOrDefault();
                };
        }

        /// <summary>
        ///     根据合作公司Id，加载合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        private void LoadBFEMaterialByCompany(SupplierCompanyDTO supplierCompany)
        {
            if (supplierCompany == null) return;
            _bfeMeterialFilter.Value = supplierCompany.SupplierCompanyId;
            if (!SupplierCompanyBFEMaterialsView.AutoLoad)
            {
                SupplierCompanyBFEMaterialsView.AutoLoad = true;
            }
            else
            {
                SupplierCompanyBFEMaterialsView.Load(true);
            }
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            if (!SupplierCompanysView.AutoLoad)
            {
                SupplierCompanysView.AutoLoad = true; //加载数据。
            }
            else
            {
                SupplierCompanysView.Load(true);
            }
        }

        /// <summary>
        ///     创建服务。
        /// </summary>
        /// <returns></returns>
        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
        }

        #endregion
    }
}