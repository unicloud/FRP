#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (SupplierMaterialManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SupplierMaterialManagerVM : EditViewModelBase
    {
        private PurchaseData _purchaseData;
        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public SupplierMaterialManagerVM()
        {
            InitialSupplierCompanyMaterial(); //初始化供应商物料
            InitialOperatorCommad();//初始化操作按钮
            InitialAircraftMaterial(); //飞机物料信息初始化
            InitialEngineMaterial(); //发动机物料按钮初始化
            InitialBFEMaterial(); //初始化BFE
            InitialMaterialChild(); //初始化维护物料按钮

        }


        #region SupplierCompanyMaterial相关信息

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
            SupplierCompanyMaterialsView = Service.CreateCollection(_purchaseData.SupplierCompanyMaterials.Expand(p=>p.AircraftMaterials));
            SupplierCompanyMaterialsView.PageSize = 20;
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

        #region 命令处理

        #region 新增飞机物料命令

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

        public DelegateCommand<object> DelAcMaterialCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelAcMaterialExecute(object sender)
        {
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

        #region 新增发动机物料命令

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
            }
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
        
        #region 新增BFE物料命令

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
            }
            _type = "BFE物料";
            AddMaterial();
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

        #region 删除BFE物料命令

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
            }
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

        /// <summary>
        /// 初始化操作按钮
        /// </summary>
        private void InitialOperatorCommad()
        {  
            //飞机物料按钮
            AddAcMaterialCommand = new DelegateCommand<object>(OnAddAcMaterialExecute, CanAddAcMaterialExecute);
            DelAcMaterialCommand = new DelegateCommand<object>(OnDelAcMaterialExecute, CanDelAcMaterialExecute);
            //发动机按钮
            AddEngineMaterialCommand = new DelegateCommand<object>(OnAddEngineMaterialExecute,CanAddEngineMaterialExecute);
            DelEngineMaterialCommand = new DelegateCommand<object>(OnDelEngineMaterialExecute,CanDelEngineMaterialExecute);
            //BFE按钮
            AddBFEMaterialCommand = new DelegateCommand<object>(OnAddBFEMaterialExecute, CanAddBFEMaterialExecute);
            DelBFEMaterialCommand = new DelegateCommand<object>(OnDelBFEMaterialExecute, CanDelBFEMaterialExecute);

        }

        #endregion

        #region 子窗体相关

        [Import]
        public MaterialChildView MaterialChildView; //初始化子窗体
        private Visibility _acGridVisibility = Visibility.Collapsed;

        private Visibility _bfeGridVisibility = Visibility.Collapsed;

        private Visibility _engineGridVisibility = Visibility.Collapsed;
        private string _type = "飞机物料"; //操作类型
        private List<AircraftMaterialDTO> _addingAcMaterial; //需要添加的飞机物料
        private List<EngineMaterialDTO> _addingEngineMaterial;//需要添加的发动机物料
        private List<BFEMaterialDTO> _addingBfeMaterial;//需要添加的BFe物料

        #region  加载飞机物料相关信息

        /// <summary>
        ///     获取所有飞机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftMaterialDTO> AircraftMaterialsView { get; set; }
        /// <summary>
        ///     初始化飞机物料信息。
        /// </summary>
        private void InitialAircraftMaterial()
        {
            AircraftMaterialsView = Service.CreateCollection(_purchaseData.AircraftMaterias);
            Service.RegisterCollectionView(AircraftMaterialsView); //注册查询集合。
            AircraftMaterialsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if ( e.Entities.Cast<AircraftMaterialDTO>().FirstOrDefault()!=null)
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
        public QueryableDataServiceCollectionView<EngineMaterialDTO> EngineMaterialsView { get; set; }

        /// <summary>
        ///     初始化发动机物料信息。
        /// </summary>
        private void InitialEngineMaterial()
        {
            EngineMaterialsView = Service.CreateCollection(_purchaseData.EngineMaterials);
            EngineMaterialsView.PageSize = 20;
            _engineMaterialFilter = new FilterDescriptor("Name", FilterOperator.IsNotContainedIn, null);
            EngineMaterialsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                }
            };
        }


        #endregion

        #region 加载BFE物料相关信息

        private FilterDescriptor _bfeMaterialFilter; //查找BFE物料配置。

        /// <summary>
        ///     获取所有发动机物料信息。
        /// </summary>
        public QueryableDataServiceCollectionView<BFEMaterialDTO> BFEMaterialsView { get; set; }

        /// <summary>
        ///     初始化发动机物料信息。
        /// </summary>
        private void InitialBFEMaterial()
        {
            BFEMaterialsView = Service.CreateCollection(_purchaseData.BFEMaterials);
            BFEMaterialsView.PageSize = 20;
            _bfeMaterialFilter = new FilterDescriptor("Name", FilterOperator.IsNotContainedIn, null);
            BFEMaterialsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
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
        /// 处理飞机物料
        /// </summary>
        private void SetAcMaterial()
        {
            MaterialChildView.Header = "添加飞机物料";
            AcGridVisibility = Visibility.Visible;
            //设置飞机物料
            var acMaterialNames = new List<string>();
            SelSupplierCompanyMaterial.AircraftMaterials.ToList().ForEach(p => acMaterialNames.Add(p.Name));
            if (!AircraftMaterialsView.AutoLoad)
            {
                AircraftMaterialsView.AutoLoad = true;
            }
        }
        /// <summary>
        /// 处理发动机物料
        /// </summary>
        private void SetEngineMaterial()
        {
            MaterialChildView.Header = "添加发动机物料";
            EngineGridVisibility = Visibility.Visible;
            //设置发动机物料
            var engineMaterialNames = new List<string>();
            SelSupplierCompanyMaterial.EngineMaterials.ToList().ForEach(p => engineMaterialNames.Add(p.Name));
            _engineMaterialFilter.Value = engineMaterialNames;
            if (!EngineMaterialsView.AutoLoad)
            {
                EngineMaterialsView.AutoLoad = true;
            }
        }
        /// <summary>
        /// 处理Bfe物料
        /// </summary>
        private void SetBfeMaterial()
        {
            MaterialChildView.Header = "添加BFE物料";
            BfeGridVisibility = Visibility.Visible;
            //设置BFE物料
            var bfeMaterialNames = new List<string>();
            SelSupplierCompanyMaterial.BFEMaterials.ToList().ForEach(p => bfeMaterialNames.Add(p.Name));
            _bfeMaterialFilter.Value = bfeMaterialNames;
            if (!BFEMaterialsView.AutoLoad)
            {
                BFEMaterialsView.AutoLoad = true;
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
            Service.SubmitChanges(p =>
            {
                MaterialChildView.Close();
            });
        }
        /// <summary>
        /// 保存物料
        /// </summary>
        private void CommitMaterial()
        {
            if (_type.Equals("飞机物料"))
            {
               _addingAcMaterial.ForEach(
                p =>
                {
                    var acMaterial = new SupplierMaterialDTO
                    {
                        MaterialId = p.AcMaterialId,
                        SupplierCompanyId = SelSupplierCompanyMaterial.SupplierCompanyId,
                        Name = p.Name
                    };
                    SelSupplierCompanyMaterial.AircraftMaterials.Add(acMaterial);
                });
            }
            else if (_type.Equals("发动机物料"))
            {
              _addingEngineMaterial.ForEach(
                p =>
                {
                    var acMaterial = new SupplierMaterialDTO
                    {
                        MaterialId = p.PartId,
                        SupplierCompanyId = SelSupplierCompanyMaterial.SupplierCompanyId,
                        Name = p.Name
                    };
                    SelSupplierCompanyMaterial.EngineMaterials.Add(acMaterial);
                });

            }
            else
            {
                _addingBfeMaterial.ForEach(
                  p =>
                  {
                      var acMaterial = new SupplierMaterialDTO
                      {
                          MaterialId = p.PartId,
                          SupplierCompanyId = SelSupplierCompanyMaterial.SupplierCompanyId,
                          Name = p.Name
                      };
                      SelSupplierCompanyMaterial.BFEMaterials.Add(acMaterial);
                  });
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

        public DelegateCommand<object> SelMaterialCommand { get; private set; }

        /// <summary>
        ///     执行选择命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSelMaterialExecute(object sender)
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

        public bool CanSelMaterialExecute(object sender)
        {
            return true;
        }

   
        #endregion
        
        #endregion

        /// <summary>
        /// 初始化维护物料命令
        /// </summary>
        private void InitialMaterialChild()
        {
            CancelCommand=new DelegateCommand<object>(OnCancelExecute,CanCancelExecute);
            CommitCommand=new DelegateCommand<object>(OnCommitExecute,CanCommitExecute);
            SelMaterialCommand = new DelegateCommand<object>(OnSelMaterialExecute, CanSelMaterialExecute);
            _addingAcMaterial = new List<AircraftMaterialDTO>();
            _addingBfeMaterial=new List<BFEMaterialDTO>();
            _addingEngineMaterial = new List<EngineMaterialDTO>();
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