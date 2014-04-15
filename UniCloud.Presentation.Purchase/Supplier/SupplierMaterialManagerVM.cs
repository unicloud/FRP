#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/02，18:12
// 文件名：SupplierMaterialManagerVM.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(SupplierMaterialManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SupplierMaterialManagerVM : EditViewModelBase
    {
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public SupplierMaterialManagerVM(IPurchaseService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialSupplierCompany(); //初始化合作公司
            InitialSupplierCompanyAcMaterial(); //初始化合作公司飞机物料
            InitialSupplierCompanyBfeMaterial(); //初始化合作公司BFE物料
            InitialSupplierCompanyEngineMaterial(); //初始化合作公司发动机物料
            InitialOperatorCommad(); //初始化操作按钮
            InitialAircraftMaterial(); //飞机物料信息初始化
            InitialEngineMaterial(); //发动机物料按钮初始化
            InitialBfeMaterial(); //初始化BFE
            InitialMaterialChild(); //初始化维护物料按钮
        }

        #region SupplierCompany相关信息

        private int _selectedMaterialTab;
        private SupplierCompanyDTO _selectedSupplierCompany;

        /// <summary>
        ///     选择合作公司。
        /// </summary>
        public SupplierCompanyDTO SelectedSupplierCompany
        {
            get { return _selectedSupplierCompany; }
            set
            {
                _selectedSupplierCompany = value;
                SetAcMaterialFilterState(); //重新设置飞机物料
                SetEngineMaterialFilterState(); //重新设置发动机物料
                SetBfeMaterialFilterState(); //重新设置Bfe物料
                LoadAcMaterialByCompany(value);
                LoadBfeMaterialByCompany(value);
                LoadEngineMaterialByCompany(value);
                RefreshMaterialTabState(); //处理物料Tab是否可用
                RaisePropertyChanged(() => SelectedSupplierCompany);
            }
        }

        /// <summary>
        ///     选择的Tab页
        /// </summary>
        public int SelectedMaterialTab
        {
            get { return _selectedMaterialTab; }
            set
            {
                if (_selectedMaterialTab != value)
                {
                    _selectedMaterialTab = value;
                    RaisePropertyChanged(() => SelectedMaterialTab);
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

        #region SupplierCompanyAcMaterial相关信息

        private string _acMaterialNotFilter = string.Empty; //设置飞机物料过滤信息
        private FilterDescriptor _acMeterialFilter; //查找合作公司飞机物料。

        /// <summary>
        ///     飞机物料Tab 是否可见。
        /// </summary>
        public Visibility AcMaterialVisibility
        {
            get
            {
                return SelectedSupplierCompany != null
                       && (SelectedSupplierCompany.AircraftLeaseSupplier
                           || SelectedSupplierCompany.AircraftPurchaseSupplier)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

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
                    DeleteAcMaterialCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedSupplierCompanyAcMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司飞机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyAcMaterialDTO> SupplierCompanyAcMaterials
        {
            get;
            set;
        }

        /// <summary>
        ///     初始化合作公司飞机物料信息。
        /// </summary>
        private void InitialSupplierCompanyAcMaterial()
        {
            SupplierCompanyAcMaterials = _service.CreateCollection(_context.SupplierCompanyAcMaterials);
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
                if (SelectedSupplierCompanyAcMaterial == null)
                    SelectedSupplierCompanyAcMaterial = e.Entities.Cast<SupplierCompanyAcMaterialDTO>().FirstOrDefault();

                if (string.IsNullOrEmpty(_acMaterialNotFilter))
                    SetAcMaterialFilter(); //设置过滤信息
            };

            SupplierCompanyAcMaterials.SubmittedChanges += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SetAcMaterialFilterState();
                MaterialChildView.Close();
                MessageAlert("提示", "保存成功");
            };
        }

        /// <summary>
        ///     设置飞机过滤条件
        /// </summary>
        private void SetAcMaterialFilter()
        {
            _acMaterialNotFilter = string.Empty;
            SupplierCompanyAcMaterials.ToList().ForEach(p => { _acMaterialNotFilter += (p.Name) + ","; });
        }

        /// <summary>
        ///     重新设置飞机物料过滤信息
        /// </summary>
        private void SetAcMaterialFilterState()
        {
            _acMaterialNotFilter = string.Empty;
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
        private string _engineMaterialNotFilter = string.Empty; //设置发动机物料过滤信息
        private SupplierCompanyEngineMaterialDTO _selectedSupplierCompanyEngineMaterial;

        /// <summary>
        ///     发动机物料Tab 是否可见。
        /// </summary>
        public Visibility EngineMaterialVisibility
        {
            get
            {
                return SelectedSupplierCompany != null
                       && (SelectedSupplierCompany.EngineLeaseSupplier
                           || SelectedSupplierCompany.EnginePurchaseSupplier)
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

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
                    DeleteEngineMaterialCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedSupplierCompanyEngineMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyEngineMaterialDTO> SupplierCompanyEngineMaterials
        {
            get;
            set;
        }

        /// <summary>
        ///     初始化合作公司发动机物料信息。
        /// </summary>
        private void InitialSupplierCompanyEngineMaterial()
        {
            SupplierCompanyEngineMaterials = _service.CreateCollection(_context.SupplierCompanyEngineMaterials);
            //根据合作公司Id，查询发动机物料
            _engienMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyEngineMaterials.FilterDescriptors.Add(_engienMeterialFilter);
            SupplierCompanyEngineMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedSupplierCompanyEngineMaterial == null)
                    SelectedSupplierCompanyEngineMaterial =
                        e.Entities.Cast<SupplierCompanyEngineMaterialDTO>().FirstOrDefault();
                if (string.IsNullOrEmpty(_engineMaterialNotFilter))
                    SetEngineMaterialFilter();
            };

            SupplierCompanyEngineMaterials.SubmittedChanges += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SetEngineMaterialFilterState();
                MaterialChildView.Close();
                MessageAlert("提示", "保存成功");
            };
        }

        /// <summary>
        ///     设置发动机过滤条件
        /// </summary>
        private void SetEngineMaterialFilter()
        {
            _engineMaterialNotFilter = string.Empty;
            SupplierCompanyEngineMaterials.ToList().ForEach(p => { _engineMaterialNotFilter += (p.Name) + ","; });
        }

        /// <summary>
        ///     重新设置发动机物料过滤信息
        /// </summary>
        private void SetEngineMaterialFilterState()
        {
            _engineMaterialNotFilter = string.Empty;
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

        private string _bfeMaterialNotFilter = string.Empty; //设置BFE物料过滤信息
        private FilterDescriptor _bfeMeterialFilter; //查找合作公司BFE物料。
        private SupplierCompanyBFEMaterialDTO _selectedSupplierCompanyBfeMaterial;

        /// <summary>
        ///     Bfe Tab 是否可见。
        /// </summary>
        public Visibility BfeMaterialVisibility
        {
            get
            {
                return SelectedSupplierCompany != null
                       && SelectedSupplierCompany.BFEPurchaseSupplier
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }
        }

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
                    DeleteBfeMaterialCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedSupplierCompanyBfeMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商公司BFE物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyBFEMaterialDTO> SupplierCompanyBfeMaterials
        {
            get;
            set;
        }

        /// <summary>
        ///     初始化合作公司BFE物料信息。
        /// </summary>
        private void InitialSupplierCompanyBfeMaterial()
        {
            SupplierCompanyBfeMaterials = _service.CreateCollection(_context.SupplierCompanyBFEMaterials);
            //根据合作公司Id，查询BFE物料
            _bfeMeterialFilter = new FilterDescriptor("SupplierCompanyId", FilterOperator.IsEqualTo, 0);
            SupplierCompanyBfeMaterials.FilterDescriptors.Add(_bfeMeterialFilter);
            SupplierCompanyBfeMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedSupplierCompanyBfeMaterial == null)
                    SelectedSupplierCompanyBfeMaterial =
                        e.Entities.Cast<SupplierCompanyBFEMaterialDTO>().FirstOrDefault();
                if (string.IsNullOrEmpty(_bfeMaterialNotFilter))
                    SetBfeMaterialFilter(); //设置发BFE过滤条件
            };

            SupplierCompanyBfeMaterials.SubmittedChanges += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SetBfeMaterialFilterState();
                MaterialChildView.Close();
                MessageAlert("提示", "保存成功");
            };
        }

        /// <summary>
        ///     设置发BFE过滤条件
        /// </summary>
        private void SetBfeMaterialFilter()
        {
            _bfeMaterialNotFilter = string.Empty;
            SupplierCompanyBfeMaterials.ToList().ForEach(p => { _bfeMaterialNotFilter += (p.Name) + ","; });
        }

        /// <summary>
        ///     重新设置Bfe物料过滤信息
        /// </summary>
        private void SetBfeMaterialFilterState()
        {
            _bfeMaterialNotFilter = string.Empty;
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

        #region 命令处理

        #region 新增飞机物料命令

        public DelegateCommand<object> AddAcMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddAcMaterialExecute(object sender)
        {
            if (SelectedSupplierCompany == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            _type = "飞机物料";
            AddMaterial();
        }


        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddAcMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除飞机物料命令

        public DelegateCommand<object> DeleteAcMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeleteAcMaterialExecute(object sender)
        {
            if (SelectedSupplierCompanyAcMaterial == null)
            {
                MessageAlert("提示", "请选择需要删除的飞机物料");
                return;
            }
            MessageDialogs.Confirm("提示", "确定是否删除该记录？", (o, e) =>
            {
                if (e.DialogResult != true)
                    return;
                SupplierCompanyAcMaterials.Remove(SelectedSupplierCompanyAcMaterial);
                SupplierCompanyAcMaterials.SubmitChanges();
            });
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDeleteAcMaterialExecute(object sender)
        {
            return SelectedSupplierCompanyAcMaterial != null;
        }

        #endregion

        #region 新增发动机物料命令

        public DelegateCommand<object> AddEngineMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddEngineMaterialExecute(object sender)
        {
            if (SelectedSupplierCompany == null)
            {
                MessageAlert("提示", "合作公司不能为空");
            }
            _type = "发动机物料";
            AddMaterial();
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddEngineMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除发动机物料命令

        public DelegateCommand<object> DeleteEngineMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeleteEngineMaterialExecute(object sender)
        {
            if (SelectedSupplierCompanyEngineMaterial == null)
            {
                MessageAlert("提示", "请选择需要删除的发动机物料");
                return;
            }
            MessageDialogs.Confirm("提示", "确定是否删除该记录？", (o, e) =>
            {
                if (e.DialogResult != true)
                    return;
                SupplierCompanyEngineMaterials.Remove(SelectedSupplierCompanyEngineMaterial);
                SupplierCompanyEngineMaterials.SubmitChanges();
            });
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDeleteEngineMaterialExecute(object sender)
        {
            return SelectedSupplierCompanyEngineMaterial != null;
        }

        #endregion

        #region 新增BFE物料命令

        public DelegateCommand<object> AddBfeMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddBfeMaterialExecute(object sender)
        {
            if (SelectedSupplierCompany == null)
            {
                MessageAlert("提示", "合作公司不能为空");
            }
            _type = "BFE物料";
            AddMaterial();
        }


        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddBfeMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除BFE物料命令

        public DelegateCommand<object> DeleteBfeMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeleteBfeMaterialExecute(object sender)
        {
            if (SelectedSupplierCompanyBfeMaterial == null)
            {
                MessageAlert("提示", "请选择需要删除的发动机物料");
                return;
            }
            MessageDialogs.Confirm("提示", "确定是否删除该记录？", (o, e) =>
            {
                if (e.DialogResult != true)
                    return;
                SupplierCompanyBfeMaterials.Remove(SelectedSupplierCompanyBfeMaterial);
                SupplierCompanyBfeMaterials.SubmitChanges();
            });
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDeleteBfeMaterialExecute(object sender)
        {
            return SelectedSupplierCompanyBfeMaterial != null;
        }

        #endregion

        /// <summary>
        ///     初始化操作按钮
        /// </summary>
        private void InitialOperatorCommad()
        {
            //飞机物料按钮
            AddAcMaterialCommand = new DelegateCommand<object>(OnAddAcMaterialExecute, CanAddAcMaterialExecute);
            DeleteAcMaterialCommand = new DelegateCommand<object>(OnDeleteAcMaterialExecute, CanDeleteAcMaterialExecute);
            //发动机按钮
            AddEngineMaterialCommand = new DelegateCommand<object>(OnAddEngineMaterialExecute,
                CanAddEngineMaterialExecute);
            DeleteEngineMaterialCommand = new DelegateCommand<object>(OnDeleteEngineMaterialExecute,
                CanDeleteEngineMaterialExecute);
            //BFE按钮
            AddBfeMaterialCommand = new DelegateCommand<object>(OnAddBfeMaterialExecute, CanAddBfeMaterialExecute);
            DeleteBfeMaterialCommand = new DelegateCommand<object>(OnDeleteBfeMaterialExecute, CanDeleteBfeMaterialExecute);
        }

        #endregion

        #region 子窗体相关

        [Import]
        public MaterialChildView MaterialChildView; //初始化子窗体
        private Visibility _acGridVisibility = Visibility.Collapsed;
        private List<AircraftMaterialDTO> _addingAcMaterial; //需要添加的飞机物料
        private List<BFEMaterialDTO> _addingBfeMaterial; //需要添加的BFe物料
        private List<EngineMaterialDTO> _addingEngineMaterial; //需要添加的发动机物料

        private Visibility _bfeGridVisibility = Visibility.Collapsed;

        private Visibility _engineGridVisibility = Visibility.Collapsed;
        private string _type = "飞机物料"; //操作类型

        #region  加载飞机物料相关信息

        private FilterDescriptor _acMaterialFilter; //查找飞机物料配置。

        /// <summary>
        ///     获取所有飞机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftMaterialDTO> AircraftMaterials { get; set; }

        /// <summary>
        ///     初始化飞机物料信息。
        /// </summary>
        private void InitialAircraftMaterial()
        {
            AircraftMaterials = _service.CreateCollection(_context.AircraftMaterias);
            AircraftMaterials.PageSize = 10;
            _acMaterialFilter = new FilterDescriptor("Name", FilterOperator.IsNotContainedIn, string.Empty);
            AircraftMaterials.FilterDescriptors.Add(_acMaterialFilter);
            AircraftMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (e.Entities.Cast<AircraftMaterialDTO>().FirstOrDefault() != null)
                {
                    _addingAcMaterial.Add(e.Entities.Cast<AircraftMaterialDTO>().FirstOrDefault());
                }
            };
        }

        #endregion

        #region 加载发动机物料相关信息

        private FilterDescriptor _engineMaterialFilter; //查找发动机配置。

        /// <summary>
        ///     获取所有发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<EngineMaterialDTO> EngineMaterials { get; set; }

        /// <summary>
        ///     初始化发动机物料信息。
        /// </summary>
        private void InitialEngineMaterial()
        {
            EngineMaterials = _service.CreateCollection(_context.EngineMaterials);
            EngineMaterials.PageSize = 10;
            EngineMaterials.PageSize = 20;
            _engineMaterialFilter = new FilterDescriptor("Name", FilterOperator.IsNotContainedIn, string.Empty);
            EngineMaterials.FilterDescriptors.Add(_engineMaterialFilter);
            EngineMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (e.Entities.Cast<EngineMaterialDTO>().FirstOrDefault() != null)
                {
                    _addingEngineMaterial.Add(e.Entities.Cast<EngineMaterialDTO>().FirstOrDefault());
                }
            };
        }

        #endregion

        #region 加载BFE物料相关信息

        private FilterDescriptor _bfeMaterialFilter; //查找BFE物料配置。

        /// <summary>
        ///     获取所有发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<BFEMaterialDTO> BfeMaterials { get; set; }

        /// <summary>
        ///     初始化发动机物料信息。
        /// </summary>
        private void InitialBfeMaterial()
        {
            BfeMaterials = _service.CreateCollection(_context.BFEMaterials);
            BfeMaterials.PageSize = 10;
            BfeMaterials.PageSize = 20;
            _bfeMaterialFilter = new FilterDescriptor("Name", FilterOperator.IsNotContainedIn, string.Empty);
            BfeMaterials.FilterDescriptors.Add(_bfeMaterialFilter);
            BfeMaterials.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (e.Entities.Cast<BFEMaterialDTO>().FirstOrDefault() != null)
                {
                    _addingBfeMaterial.Add(e.Entities.Cast<BFEMaterialDTO>().FirstOrDefault());
                }
            };
        }

        #endregion

        #region 属性

        /// <summary>
        ///     bfegrid是否可见。
        /// </summary>
        public Visibility BfeGridVisibility
        {
            get { return _bfeGridVisibility; }
            set
            {
                _bfeGridVisibility = value;
                if (value == Visibility.Visible)
                {
                    AcGridVisibility = Visibility.Collapsed;
                    EngineGridVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(() => BfeGridVisibility);
            }
        }

        /// <summary>
        ///     发动机grid是否可见。
        /// </summary>
        public Visibility EngineGridVisibility
        {
            get { return _engineGridVisibility; }
            set
            {
                _engineGridVisibility = value;
                if (value == Visibility.Visible)
                {
                    BfeGridVisibility = Visibility.Collapsed;
                    AcGridVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(() => EngineGridVisibility);
            }
        }

        /// <summary>
        ///     飞机grid是否可见。
        /// </summary>
        public Visibility AcGridVisibility
        {
            get { return _acGridVisibility; }
            set
            {
                _acGridVisibility = value;
                if (value == Visibility.Visible)
                {
                    BfeGridVisibility = Visibility.Collapsed;
                    EngineGridVisibility = Visibility.Collapsed;
                }
                RaisePropertyChanged(() => AcGridVisibility);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     添加物料
        /// </summary>
        private void AddMaterial()
        {
            if (_type.Equals("飞机物料"))
            {
                SetAcMaterial();
            }
            else if (_type.Equals("发动机物料"))
            {
                SetEngineMaterial();
            }
            else
            {
                SetBfeMaterial();
            }
            MaterialChildView.ShowDialog();
        }

        /// <summary>
        ///     处理飞机物料
        /// </summary>
        private void SetAcMaterial()
        {
            MaterialChildView.Header = "添加飞机物料";
            AcGridVisibility = Visibility.Visible;
            _acMaterialFilter.Value = _acMaterialNotFilter;
            if (!AircraftMaterials.AutoLoad)
            {
                AircraftMaterials.AutoLoad = true;
            }
        }

        /// <summary>
        ///     处理发动机物料
        /// </summary>
        private void SetEngineMaterial()
        {
            MaterialChildView.Header = "添加发动机物料";
            EngineGridVisibility = Visibility.Visible;
            _engineMaterialFilter.Value = _engineMaterialNotFilter;
            if (!EngineMaterials.AutoLoad)
            {
                EngineMaterials.AutoLoad = true;
            }
        }

        /// <summary>
        ///     处理Bfe物料
        /// </summary>
        private void SetBfeMaterial()
        {
            MaterialChildView.Header = "添加BFE物料";
            BfeGridVisibility = Visibility.Visible;
            _bfeMaterialFilter.Value = _bfeMaterialNotFilter;
            if (!BfeMaterials.AutoLoad)
            {
                BfeMaterials.AutoLoad = true;
            }
        }

        #endregion

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            MaterialChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            CommitMaterial();
        }

        /// <summary>
        ///     保存物料
        /// </summary>
        private void CommitMaterial()
        {
            if (_type.Equals("飞机物料"))
            {
                _addingAcMaterial.ForEach(
                    p =>
                    {
                        var acMaterial = new SupplierCompanyAcMaterialDTO
                        {
                            MaterialId = p.AcMaterialId,
                            SupplierCompanyId = SelectedSupplierCompany.SupplierCompanyId,
                            Name = p.Name,
                            SupplierCompanyMaterialId = RandomHelper.Next()
                        };
                        SupplierCompanyAcMaterials.AddNew(acMaterial);
                    });
                SupplierCompanyAcMaterials.SubmitChanges();
            }
            else if (_type.Equals("发动机物料"))
            {
                _addingEngineMaterial.ForEach(
                    p =>
                    {
                        var engineMaterial = new SupplierCompanyEngineMaterialDTO
                        {
                            MaterialId = p.EngineMaterialId,
                            SupplierCompanyId = SelectedSupplierCompany.SupplierCompanyId,
                            Name = p.Name,
                            SupplierCompanyMaterialId = RandomHelper.Next()
                        };
                        SupplierCompanyEngineMaterials.AddNew(engineMaterial);
                    });
                SupplierCompanyEngineMaterials.SubmitChanges();
            }
            else
            {
                _addingBfeMaterial.ForEach(
                    p =>
                    {
                        var bfeMaterial = new SupplierCompanyBFEMaterialDTO
                        {
                            MaterialId = p.BFEMaterialId,
                            SupplierCompanyId = SelectedSupplierCompany.SupplierCompanyId,
                            Name = p.Name,
                            SupplierCompanyMaterialId = RandomHelper.Next()
                        };
                        SupplierCompanyBfeMaterials.AddNew(bfeMaterial);
                    });
                SupplierCompanyBfeMaterials.SubmitChanges();
            }
        }

        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 选中物料命令

        public DelegateCommand<object> SelectMaterialCommand { get; private set; }

        /// <summary>
        ///     执行选择命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSelectMaterialExecute(object sender)
        {
            var radGridView = sender as RadGridView;
            if (radGridView == null) return;
            if (radGridView.Name.Equals("AircraftMaterial"))
            {
                _addingAcMaterial.Clear();
                radGridView.SelectedItems.ToList().ForEach(p => _addingAcMaterial.Add(p as AircraftMaterialDTO));
            }
            else if (radGridView.Name.Equals("EngineMaterial"))
            {
                _addingEngineMaterial.Clear();
                radGridView.SelectedItems.ToList().ForEach(p => _addingEngineMaterial.Add(p as EngineMaterialDTO));
            }
            else
            {
                _addingBfeMaterial.Clear();
                radGridView.SelectedItems.ToList().ForEach(p => _addingBfeMaterial.Add(p as BFEMaterialDTO));
            }
        }

        public bool CanSelectMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion

        /// <summary>
        ///     初始化维护物料命令
        /// </summary>
        private void InitialMaterialChild()
        {
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            SelectMaterialCommand = new DelegateCommand<object>(OnSelectMaterialExecute, CanSelectMaterialExecute);
            _addingAcMaterial = new List<AircraftMaterialDTO>();
            _addingBfeMaterial = new List<BFEMaterialDTO>();
            _addingEngineMaterial = new List<EngineMaterialDTO>();
        }

        #endregion

        /// <summary>
        ///     刷新采购物料状态
        /// </summary>
        private void RefreshMaterialTabState()
        {
            RaisePropertyChanged(() => AcMaterialVisibility);
            RaisePropertyChanged(() => BfeMaterialVisibility);
            RaisePropertyChanged(() => EngineMaterialVisibility);
            //默认选中的物料Tab页
            if (AcMaterialVisibility == Visibility.Visible)
            {
                SelectedMaterialTab = 0;
                return;
            }
            if (EngineMaterialVisibility == Visibility.Visible)
            {
                SelectedMaterialTab = 1;
                return;
            }
            if (BfeMaterialVisibility == Visibility.Visible)
            {
                SelectedMaterialTab = 2;
            }
        }

        #region 重载基类服务

        /// <summary>
        ///     加载数据。
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