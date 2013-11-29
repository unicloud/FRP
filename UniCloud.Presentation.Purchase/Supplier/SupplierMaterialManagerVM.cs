#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.AgentService.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (SupplierMaterialManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SupplierMaterialManagerVM : EditViewModelBase
    {
        private FilterDescriptor _partFilter; //查询部件配置。
        private PurchaseData _purchaseData;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public SupplierMaterialManagerVM()
        {
            InitialSupplierCompanyMaterial(); //初始化供应商物料
            InitialAcType(); //初始化机型
            InitialAcMaterialCommand(); //飞机物料按钮初始化
            InitialEngineMaterialCommand(); //发动机物料按钮初始化
            InitialBFEMaterialCommand();//初始化BFE
            InitialPart(); //初始化部件信息
        }

        #region SupplierCompanyMaterialDTO相关信息

        private SupplierCompanyMaterialDTO _selectedSupplierCompanyMaterial;

        /// <summary>
        ///     选择供应商物料。
        /// </summary>
        public SupplierCompanyMaterialDTO SelSupplierCompanyMaterial
        {
            get { return _selectedSupplierCompanyMaterial; }
            set
            {
                if (_selectedSupplierCompanyMaterial != value)
                {
                    _selectedSupplierCompanyMaterial = value;
                    //根据选择的供应商获取相关信息
                    RaisePropertyChanged(() => SelSupplierCompanyMaterial);
                }
            }
        }

        /// <summary>
        ///     获取所有供应商物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyMaterialDTO> SupplierCompanyMaterialsView { get; set; }

        /// <summary>
        ///     初始化合作公司信息。
        /// </summary>
        private void InitialSupplierCompanyMaterial()
        {
            SupplierCompanyMaterialsView = Service.CreateCollection(_purchaseData.SupplierCompanyMaterials);
            Service.RegisterCollectionView(SupplierCompanyMaterialsView); //注册查询集合。
            SupplierCompanyMaterialsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompanyMaterial = e.Entities.Cast<SupplierCompanyMaterialDTO>().FirstOrDefault();
                };
        }

        #endregion

        #region 机型信息

        public IEnumerable<AcTypeDTO> AcTypes
        {
            get { return SupplierService.AcTypes; }
        }

        /// <summary>
        ///     初始化机型数据
        /// </summary>
        private void InitialAcType()
        {
            if (SupplierService.AcTypes == null)
                SupplierService.LoadAcType();
        }

        #endregion

        #region 部件相关信息

        /// <summary>
        ///     获取部件信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PartDTO> PartView { get; set; }

        /// <summary>
        ///     初始化发动机物料。
        /// </summary>
        private void InitialPart()
        {
            PartView = Service.CreateCollection(_purchaseData.Parts);
            _partFilter = new FilterDescriptor("Name", FilterOperator.Contains, string.Empty);
            PartView.FilterDescriptors.Add(_partFilter);
            PartView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                    }
                };
        }

        /// <summary>
        ///     根据搜索的条件查询部件相关信息。
        /// </summary>
        /// <param name="condition">供应商公司</param>
        private void LoadPartByCondition(string condition)
        {
            if (string.IsNullOrEmpty(condition)) return;
            _partFilter.Value = condition;
            PartView.AutoLoad = false;
            PartView.AutoLoad = true;
        }

        #endregion

        #region 飞机物料相关信息

        #region 新增命令

        public DelegateCommand<object> AddAcMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddAcMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelSupplierCompanyMaterial != null)
            {
                SelAcMaterial = CreateAcMaterial();
                SelSupplierCompanyMaterial.AircraftMaterials.Add(SelAcMaterial);
            }
        }

        /// <summary>
        /// 创建飞机物料
        /// </summary>
        /// <returns></returns>
        private AircraftMaterialDTO CreateAcMaterial()
        {

            var defaultTypeId = AcTypes != null && AcTypes.Any(p => p.Name.Contains("A320"))
                                   ? AcTypes.First(p => p.Name.Contains("A320")).AcTypeId
                                   : Guid.Empty;
            return new AircraftMaterialDTO
            {
                AcMaterialId = RandomHelper.Next(),
                AircraftTypeId = defaultTypeId
            };
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

        #region 删除命令

        public DelegateCommand<object> DelAcMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelAcMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelAcMaterial == null)
            {
                MessageAlert("提示", "请选选择需要删除的记录");
                return;
            }
            SelSupplierCompanyMaterial.AircraftMaterials.Remove(SelAcMaterial);
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelAcMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        private AircraftMaterialDTO _selectedAcMaterial;

        /// <summary>
        ///     选择飞机供应商物料。
        /// </summary>
        public AircraftMaterialDTO SelAcMaterial
        {
            get { return _selectedAcMaterial; }
            set
            {
                if (_selectedAcMaterial != value)
                {
                    _selectedAcMaterial = value;
                    //根据选择的供应商获取相关信息
                    RaisePropertyChanged(() => SelAcMaterial);
                }
            }
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialAcMaterialCommand()
        {
            AddAcMaterialCommand = new DelegateCommand<object>(OnAddAcMaterialExecute, CanAddAcMaterialExecute);
            DelAcMaterialCommand=new DelegateCommand<object>(OnDelAcMaterialExecute,CanDelAcMaterialExecute);
        }


        #endregion

        #region 发动机物料相关信息

        #region 新增命令

        public DelegateCommand<object> AddEngineMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddEngineMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelSupplierCompanyMaterial != null)
            {
                SelEngineMaterial = CreateEngineMaterial();
                SelSupplierCompanyMaterial.EngineMaterials.Add(SelEngineMaterial);
            }
        }

        /// <summary>
        /// 创建发动机物料
        /// </summary>
        /// <returns></returns>
        private EngineMaterialDTO CreateEngineMaterial()
        {

            return new EngineMaterialDTO
            {
                EngineMaterialId = RandomHelper.Next(),
            };
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

        #region 删除命令

        public DelegateCommand<object> DelEngineMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelEngineMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelEngineMaterial == null)
            {
                MessageAlert("提示", "请选选择需要删除的记录");
                return;
            }
            SelSupplierCompanyMaterial.EngineMaterials.Remove(SelEngineMaterial);
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelEngineMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        private EngineMaterialDTO _selectedEngineMaterial;

        /// <summary>
        ///     选择发动机供应商物料。
        /// </summary>
        public EngineMaterialDTO SelEngineMaterial
        {
            get { return _selectedEngineMaterial; }
            set
            {
                if (_selectedEngineMaterial != value)
                {
                    _selectedEngineMaterial = value;
                    //根据选择的供应商获取相关信息
                    RaisePropertyChanged(() => SelEngineMaterial);
                }
            }
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialEngineMaterialCommand()
        {
            AddEngineMaterialCommand = new DelegateCommand<object>(OnAddEngineMaterialExecute, CanAddEngineMaterialExecute);
            DelEngineMaterialCommand = new DelegateCommand<object>(OnDelEngineMaterialExecute, CanDelEngineMaterialExecute);
        }


        #endregion

        #region BFE物料相关信息

        #region 新增命令

        public DelegateCommand<object> AddBFEMaterialCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddBFEMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelSupplierCompanyMaterial != null)
            {
                SelBFEMaterial = CreateBFEMaterial();
                SelSupplierCompanyMaterial.BFEMaterials.Add(SelBFEMaterial);
            }
        }

        /// <summary>
        /// 创建BFE物料
        /// </summary>
        /// <returns></returns>
        private BFEMaterialDTO CreateBFEMaterial()
        {

            return new BFEMaterialDTO
            {
                BFEMaterialId = RandomHelper.Next(),
            };
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddBFEMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 删除命令

        public DelegateCommand<object> DelBFEMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelBFEMaterialExecute(object sender)
        {
            if (SelSupplierCompanyMaterial == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            if (SelBFEMaterial == null)
            {
                MessageAlert("提示", "请选选择需要删除的记录");
                return;
            }
            SelSupplierCompanyMaterial.BFEMaterials.Remove(SelBFEMaterial);
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelBFEMaterialExecute(object sender)
        {
            return true;
        }

        #endregion

        private BFEMaterialDTO _selectedBFEMaterial;

        /// <summary>
        ///     选择BFE供应商物料。
        /// </summary>
        public BFEMaterialDTO SelBFEMaterial
        {
            get { return _selectedBFEMaterial; }
            set
            {
                if (_selectedBFEMaterial != value)
                {
                    _selectedBFEMaterial = value;
                    //根据选择的供应商获取相关信息
                    RaisePropertyChanged(() => SelBFEMaterial);
                }
            }
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialBFEMaterialCommand()
        {
            AddBFEMaterialCommand = new DelegateCommand<object>(OnAddBFEMaterialExecute, CanAddBFEMaterialExecute);
            DelBFEMaterialCommand = new DelegateCommand<object>(OnDelBFEMaterialExecute, CanDelBFEMaterialExecute);
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载数据。
        /// </summary>
        public override void LoadData()
        {
            SupplierCompanyMaterialsView.AutoLoad = true; //加载数据。
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

        /// <summary>
        ///     按钮控制。
        /// </summary>
        protected override void RefreshButtonState()
        {
        }

        #endregion
    }
}