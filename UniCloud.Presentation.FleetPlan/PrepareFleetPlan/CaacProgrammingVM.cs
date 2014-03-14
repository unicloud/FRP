#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 19:09:27
// 文件名：CaacProgrammingVM
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
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(CaacProgrammingVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CaacProgrammingVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public CaacProgrammingVM(IRegionManager regionManager, IFleetPlanService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
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
            CaacProgrammings = _service.CreateCollection(_context.CaacProgrammings, o => o.CaacProgrammingLines);
            _service.RegisterCollectionView(CaacProgrammings);//注册查询集合

            ProgrammingFiles = _service.CreateCollection(_context.ProgrammingFiles);
            ProgrammingFiles.FilterDescriptors.Add(new FilterDescriptor("Type", FilterOperator.IsEqualTo, 1));
            _service.RegisterCollectionView(ProgrammingFiles);//注册查询集合

            Programmings = new QueryableDataServiceCollectionView<ProgrammingDTO>(_context, _context.Programmings);

            AircraftCategories = new QueryableDataServiceCollectionView<AircraftCategoryDTO>(_context,
                _context.AircraftCategories);

            Managers = new QueryableDataServiceCollectionView<ManagerDTO>(_context, _context.Managers);
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
            SaveCaacProgrammingCommand=new DelegateCommand<object>(OnSave,CanCaacProgrammingSave);
            AbortCaacProgrammingCommand=new DelegateCommand<object>(OnAbort,CanCaacProgrammingAbort);
            SaveProgrammingFileCommand = new DelegateCommand<object>(OnSave, CanProgrammingFileSave);
            AbortProgrammingFileCommand = new DelegateCommand<object>(OnAbort, CanProgrammingFileAbort);
            AddDocCommand = new DelegateCommand<object>(OnAddAttach, CanAddDoc);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            CellEditEndCommand=new DelegateCommand<object>(OnCellEditEnd);
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
            if (!CaacProgrammings.AutoLoad)
                CaacProgrammings.AutoLoad = true;
            else
                CaacProgrammings.Load(true);

            if (!ProgrammingFiles.AutoLoad)
                ProgrammingFiles.AutoLoad = true;
            else
                ProgrammingFiles.Load(true);

            Programmings.AutoLoad = true;
            AircraftCategories.AutoLoad = true;
            Managers.AutoLoad = true;
        }

        #region 业务

        #region 民航局公司五年规划集合

        /// <summary>
        ///     民航局公司五年规划集合
        /// </summary>
        public QueryableDataServiceCollectionView<CaacProgrammingDTO> CaacProgrammings { get; set; }

        #endregion

        #region 规划期间集合

        /// <summary>
        ///     规划期间集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingDTO> Programmings { get; set; }

        #endregion

        #region 规划文档集合

        /// <summary>
        ///     规划文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingFileDTO> ProgrammingFiles { get; set; }

        #endregion

        #region 座级集合

        /// <summary>
        ///     座级集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> AircraftCategories { get; set; }

        #endregion

        #region 发文单位集合

        /// <summary>
        ///     发文单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<ManagerDTO> Managers { get; set; }

        #endregion

        #region 选择的规划

        private CaacProgrammingDTO _selCaacProgramming;

        /// <summary>
        ///     选择的规划
        /// </summary>
        public CaacProgrammingDTO SelCaacProgramming
        {
            get { return _selCaacProgramming; }
            private set
            {
                if (_selCaacProgramming != value)
                {
                    _selCaacProgramming = value;
                    RaisePropertyChanged(() => SelCaacProgramming);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划明细

        private CaacProgrammingLineDTO _selCaacProgrammingLine;

        /// <summary>
        ///     选择的规划明细
        /// </summary>
        public CaacProgrammingLineDTO SelCaacProgrammingLine
        {
            get { return _selCaacProgrammingLine; }
            private set
            {
                if (_selCaacProgrammingLine != value)
                {
                    _selCaacProgrammingLine = value;
                    RaisePropertyChanged(() => SelCaacProgrammingLine);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划文档

        private ProgrammingFileDTO _selProgrammingFile;

        /// <summary>
        ///     选择的规划文档
        /// </summary>
        public ProgrammingFileDTO SelProgrammingFile
        {
            get { return _selProgrammingFile; }
            private set
            {
                if (_selProgrammingFile != value)
                {
                    _selProgrammingFile = value;
                    RaisePropertyChanged(() => SelProgrammingFile);
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
            SaveCaacProgrammingCommand.RaiseCanExecuteChanged();
            AbortCaacProgrammingCommand.RaiseCanExecuteChanged();
            SaveProgrammingFileCommand.RaiseCanExecuteChanged();
            AbortProgrammingFileCommand.RaiseCanExecuteChanged();
            AddDocCommand.RaiseCanExecuteChanged();
            RemoveDocCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 保存/取消五年规划更改

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> SaveCaacProgrammingCommand { get; private set; }
        private bool CanCaacProgrammingSave(object obj)
        {
            return CaacProgrammings.HasChanges;
        }

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> AbortCaacProgrammingCommand { get; private set; }
        private bool CanCaacProgrammingAbort(object obj)
        {
            return CaacProgrammings.HasChanges;
        }

        private void OnSave(object sender)
        {
            IsBusy = true;
            if (sender is QueryableDataServiceCollectionViewBase)
            {
                var collectionView = sender as QueryableDataServiceCollectionViewBase;
                if (!OnSaveExecuting(collectionView))
                {
                    return;
                }
                _service.SubmitChanges(collectionView, sm =>
                {
                    IsBusy = false;
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(collectionView);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(collectionView);
                    }
                    RefreshCommandState();
                });
            }
            else
            {
                _service.SubmitChanges(sm =>
                {
                    IsBusy = false;
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(sender);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(sender);
                    }
                    RefreshCommandState();
                });
            }
        }

        private void OnAbort(object sender)
        {
            if (sender is QueryableDataServiceCollectionViewBase)
            {
                var collectionView = sender as QueryableDataServiceCollectionViewBase;
                IsBusy = true;
                OnAbortExecuting(collectionView); //取消前。
                _service.RejectChanges(collectionView); //取消。
                OnAbortExecuted(collectionView); //取消后。
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                OnAbortExecuting(sender);
                _service.RejectChanges();
                OnAbortExecuted(sender);
                IsBusy = false;
            }
        }
        #endregion

        #region 保存/取消规划相关文档更改

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> SaveProgrammingFileCommand { get; private set; }
        private bool CanProgrammingFileSave(object obj)
        {
            return ProgrammingFiles.HasChanges;
        }

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> AbortProgrammingFileCommand { get; private set; }
        private bool CanProgrammingFileAbort(object obj)
        {
            return ProgrammingFiles.HasChanges;
        }

        #endregion

        #region 创建新规划

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var caacProg = new CaacProgrammingDTO
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
            };
            CaacProgrammings.AddNew(caacProg);
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
            if (_selCaacProgramming != null)
            {
                CaacProgrammings.Remove(_selCaacProgramming);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selCaacProgramming != null;
        }

        #endregion

        #region 增加规划行

        /// <summary>
        ///     增加规划行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var caacProgLine = new CaacProgrammingLineDTO
            {
                Id = Guid.NewGuid(),
                Number = 1,
                CaacProgrammingId = SelCaacProgramming.Id,
            };

            SelCaacProgramming.CaacProgrammingLines.Add(caacProgLine);
        }

        private bool CanAddEntity(object obj)
        {
            return _selCaacProgramming != null;
        }

        #endregion

        #region 移除规划行

        /// <summary>
        ///     移除规划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selCaacProgrammingLine != null)
            {
                SelCaacProgramming.CaacProgrammingLines.Remove(_selCaacProgrammingLine);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selCaacProgrammingLine != null;
        }

        #endregion

        #region 添加附件

        protected override bool CanAddAttach(object obj)
        {
            return _selCaacProgramming != null;
        }

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);
            if (sender is Guid)
            {
                SelCaacProgramming.DocumentId = doc.DocumentId;
                SelCaacProgramming.DocName = doc.Name;
            }
            else
            {
                var programmingFile = new ProgrammingFileDTO
                {
                    DocName = doc.Name,
                    DocumentId = doc.DocumentId,
                    CreateDate = DateTime.Now,
                    IssuedDate = DateTime.Now,
                    Type = 1,//1-表示民航规划文档，2--表示川航规划文档
                };
                if (SelCaacProgramming != null)
                    programmingFile.ProgrammingId = SelCaacProgramming.ProgrammingId;

                ProgrammingFiles.AddNew(programmingFile);
            }
        }
        #endregion

        #region 添加规划关联文档

        /// <summary>
        ///     添加规划关联文档
        /// </summary>
        public DelegateCommand<object> AddDocCommand { get; private set; }

        private bool CanAddDoc(object obj)
        {
            return true;
        }

        #endregion

        #region 删除规划关联文档

        /// <summary>
        ///     删除规划关联文档
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {
            if (_selProgrammingFile != null)
            {
                ProgrammingFiles.Remove(SelProgrammingFile);
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selProgrammingFile != null;
        }

        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "ProgrammingNameForCaac"))
                {
                    var value = SelCaacProgramming.ProgrammingId;
                    var programming = Programmings.FirstOrDefault(p => p.Id == value);
                    if (programming != null)
                        SelCaacProgramming.ProgrammingName = programming.Name;
                }
                else if (string.Equals(cell.Column.UniqueName, "ProgrammingNameForFile"))
                {
                    var value = SelProgrammingFile.ProgrammingId;
                    var programming = Programmings.FirstOrDefault(p => p.Id == value);
                    if (programming != null)
                        SelProgrammingFile.ProgrammingName = programming.Name;
                }
            }
        }

        #endregion

        #endregion
    }
}