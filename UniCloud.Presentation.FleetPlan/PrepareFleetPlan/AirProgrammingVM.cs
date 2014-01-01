#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 19:08:33
// 文件名：AirProgrammingVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(AirProgrammingVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AirProgrammingVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private FleetPlanData _context;
        private DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public AirProgrammingVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            //AirProgrammings = Service.CreateCollection(_context.AirProgrammings,
            //    (o, p, c) =>
            //    {
            //        foreach (var airProgramming in from object item in o select item as AirProgrammingDTO)
            //        {
            //            airProgramming.AirProgrammingLines.CollectionChanged += c;
            //            airProgramming.AirProgrammingLines.ToList().ForEach(ol => ol.PropertyChanged += p);
            //        }
            //    });
            Service.RegisterCollectionView(AirProgrammings);

            Programmings = new QueryableDataServiceCollectionView<ProgrammingDTO>(_context, _context.Programmings);

            //TODO:缺一个飞机序列集合
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _context = new FleetPlanData(AgentHelper.FleetPlanServiceUri);
            return new FleetPlanService(_context);
        }

        #endregion

        #region 数据

        #region 公共属性

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            AirProgrammings.Load(true);
            Programmings.Load(true);
        }

        #region 业务

        #region 航空公司五年规划集合

        /// <summary>
        ///     航空公司五年规划集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirProgrammingDTO> AirProgrammings { get; set; }

        #endregion

        #region 规划期间集合

        /// <summary>
        ///     规划期间集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingDTO> Programmings { get; set; }

        #endregion

        #region 选择的规划

        private AirProgrammingDTO _selAirProgramming;

        /// <summary>
        /// 选择的规划
        /// </summary>
        public AirProgrammingDTO SelAirProgramming
        {
            get { return this._selAirProgramming; }
            private set
            {
                if (this._selAirProgramming != value)
                {
                    _selAirProgramming = value;
                    this.RaisePropertyChanged(() => this.SelAirProgramming);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划明细

        private AirProgrammingLineDTO _selAirProgrammingLine;

        /// <summary>
        /// 选择的规划明细
        /// </summary>
        public AirProgrammingLineDTO SelAirProgrammingLine
        {
            get { return this._selAirProgrammingLine; }
            private set
            {
                if (this._selAirProgrammingLine != value)
                {
                    _selAirProgrammingLine = value;
                    this.RaisePropertyChanged(() => this.SelAirProgrammingLine);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            AddAttachCommand.RaiseCanExecuteChanged();
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建新规划

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var airProg = new AirProgrammingDTO
            {
                Id = new Guid(),
                CreateDate = DateTime.Now,
            };
            AirProgrammings.AddNew(airProg);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除规划

        /// <summary>
        ///     删除规划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selAirProgramming != null)
            {
                AirProgrammings.Remove(_selAirProgramming);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selAirProgramming != null;
        }

        #endregion

        #region 增加规划行

        /// <summary>
        ///     增加规划行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var airProgLine = new AirProgrammingLineDTO
            {
                Id = new Guid(),
                BuyNum = 1,
                ExportNum = 1,
                LeaseNum = 1,
            };

            SelAirProgramming.AirProgrammingLines.Add(airProgLine);
        }

        private bool CanAddEntity(object obj)
        {
            return _selAirProgramming != null;
        }

        #endregion

        #region 移除规划行

        /// <summary>
        ///     移除规划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selAirProgrammingLine != null)
            {
                SelAirProgramming.AirProgrammingLines.Remove(_selAirProgrammingLine);
                //RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selAirProgrammingLine != null;
        }

        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            DocumentView.ViewModel.InitData(false, _selAirProgramming.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        protected override bool CanAddAttach(object obj)
        {
            return _selAirProgramming != null;
        }
        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                _document = DocumentView.Tag as DocumentDTO;
                SelAirProgramming.DocumentId = _document.DocumentId;
                SelAirProgramming.DocName = _document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (SelAirProgramming == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, _selAirProgramming.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();

        }
        #endregion

        #endregion
    }

}
