#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/3 14:52:41
// 文件名：ItemVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof (ItemVm))]
    public class ItemVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private FilterDescriptor _pnRegFilter;

        [ImportingConstructor]
        public ItemVm(IPartService service)
            : base(service)
        {
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
            Items = _service.CreateCollection(_context.Items);
            Items.PageSize = 20;
            Items.LoadedData += (o, e) =>
            {
                if (SelItem == null)
                    SelItem = Items.FirstOrDefault();
            };
            _service.RegisterCollectionView(Items);

            ItemMaintainCtrls = _service.CreateCollection(_context.ItemMaintainCtrls);
            _service.RegisterCollectionView(ItemMaintainCtrls);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);

            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);
            _pnRegFilter = new FilterDescriptor("ItemId", FilterOperator.IsEqualTo, -1);
            PnRegs.FilterDescriptors.Add(_pnRegFilter);
            PnRegs.PageSize = 5; //只是用于判断是否能删除附件项，因此不用把项下所有的附件都取出。
            PnRegs.LoadedData += (s, e) => RefreshCommandState();
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddMaintainCtrlCommand = new DelegateCommand<object>(OnAddCtrl, CanAddCtrl);
            RemoveMaintainCtrlCommand = new DelegateCommand<object>(OnRemoveCtrl, CanRemoveCtrl);
            AddCtrlLineCommand = new DelegateCommand<object>(OnAddCtrlLine, CanAddCtrlLine);
            RemoveCtrlLineCommand = new DelegateCommand<object>(OnRemoveCtrlLine, CanRemoveCtrlLine);
            ItemIsLifeChanged = new DelegateCommand<object>(OnChanged);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        /// <summary>
        ///     维修工作集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainWorkDTO> MaintainWorks { get; set; }

        /// <summary>
        ///     附件集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }


        /// <summary>
        ///     维修控制策略
        /// </summary>
        public Dictionary<int, ControlStrategy> ControlStrategies
        {
            get
            {
                return Enum.GetValues(typeof (ControlStrategy))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (ControlStrategy) value);
            }
        }

        /// <summary>
        ///     TSN或CSN
        /// </summary>
        public Dictionary<int, SinceNewType> SinceNewTypes
        {
            get
            {
                return Enum.GetValues(typeof (SinceNewType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (SinceNewType) value);
            }
        }

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
            CtrlUnits.Load(true);
            MaintainWorks.Load(true);

            if (!Items.AutoLoad)
                Items.AutoLoad = true;
            Items.Load(true);

            if (!ItemMaintainCtrls.AutoLoad)
                ItemMaintainCtrls.AutoLoad = true;
            ItemMaintainCtrls.Load(true);
        }

        #region 业务

        #region 附件项

        private ItemDTO _selItem;

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        /// <summary>
        ///     选择的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return _selItem; }
            set
            {
                if (_selItem != value)
                {
                    _selItem = value;
                    ViewItemMaintainCtrls = new ObservableCollection<ItemMaintainCtrlDTO>();
                    if (value != null)
                    {
                        _pnRegFilter.Value = value.Id;
                        PnRegs.Load(true);
                        foreach (var maintainCtrl in ItemMaintainCtrls.SourceCollection.Cast<ItemMaintainCtrlDTO>())
                        {
                            if (maintainCtrl.ItemId == value.Id)
                                ViewItemMaintainCtrls.Add(maintainCtrl);
                        }
                        SelItemMaintainCtrl = ViewItemMaintainCtrls.FirstOrDefault();
                    }
                    RaisePropertyChanged(() => SelItem);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 项维修控制组

        private ItemMaintainCtrlDTO _selItemMaintaiinCtrl;

        private ObservableCollection<ItemMaintainCtrlDTO> _viewItemMaintainCtrls =
            new ObservableCollection<ItemMaintainCtrlDTO>();

        /// <summary>
        ///     项维修控制组
        /// </summary>
        public QueryableDataServiceCollectionView<ItemMaintainCtrlDTO> ItemMaintainCtrls { get; set; }

        /// <summary>
        ///     选中的Item的维修控制组集合
        /// </summary>
        public ObservableCollection<ItemMaintainCtrlDTO> ViewItemMaintainCtrls
        {
            get { return _viewItemMaintainCtrls; }
            set
            {
                if (_viewItemMaintainCtrls != value)
                {
                    _viewItemMaintainCtrls = value;
                    SelItemMaintainCtrl = ViewItemMaintainCtrls.FirstOrDefault();
                    RaisePropertyChanged(() => ViewItemMaintainCtrls);
                }
            }
        }

        /// <summary>
        ///     选中的Item的维修控制组集合
        /// </summary>
        public ItemMaintainCtrlDTO SelItemMaintainCtrl
        {
            get { return _selItemMaintaiinCtrl; }
            set
            {
                if (_selItemMaintaiinCtrl != value)
                {
                    _selItemMaintaiinCtrl = value;
                    _ctrlLines =new List<CtrlLine>();
                    if (value != null && value.XmlContent != null)
                    {
                        CtrlLines = ConvertXmlToString(value.XmlContent);
                    }
                    RaisePropertyChanged(() => CtrlLines);
                    RaisePropertyChanged(() => SelItemMaintainCtrl);
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
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            AddMaintainCtrlCommand.RaiseCanExecuteChanged();
            RemoveMaintainCtrlCommand.RaiseCanExecuteChanged();
            AddCtrlLineCommand.RaiseCanExecuteChanged();
            RemoveCtrlLineCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建附件项

        /// <summary>
        ///     创建附件项
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            SelItem = new ItemDTO
            {
                Id = RandomHelper.Next(),
            };
            Items.AddNew(SelItem);
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 附件项是否寿控属性变化

        /// <summary>
        ///     附件项是否寿控属性变化
        /// </summary>
        public DelegateCommand<object> ItemIsLifeChanged { get; private set; }

        private void OnChanged(object obj)
        {
            var gridView = obj as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "IsLife"))
                {
                    if (SelItem != null && SelItem.IsLife && ViewItemMaintainCtrls.Count != 0)
                    {
                        RefreshCommandState();
                    }
                    else if (SelItem != null && !SelItem.IsLife && ViewItemMaintainCtrls.Count != 0)
                    {
                        const string content = "将此项设置改为非寿控项，将移除已维护的维修控制组，是否继续？";
                        MessageConfirm("确认修改为非寿控项", content, (o, e) =>
                        {
                            if (e.DialogResult == true)
                            {
                                if (SelItem != null && ViewItemMaintainCtrls.Count != 0)
                                {
                                    ViewItemMaintainCtrls.ToList().ForEach(p =>
                                    {
                                        ViewItemMaintainCtrls.Remove(p);
                                        ItemMaintainCtrls.Remove(p);
                                    });
                                    RefreshCommandState();
                                }
                            }
                            else
                            {
                                SelItem.IsLife = true;
                            }
                        });
                    }
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 删除附件项

        /// <summary>
        ///     删除附件项
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (SelItem == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                Items.Remove(SelItem);
                SelItem = Items.FirstOrDefault();
                RefreshCommandState();
            });
        }

        private bool CanRemove(object obj)
        {
            if (SelItem == null)
                return false;
            if (!PnRegs.SourceCollection.Cast<PnRegDTO>().Any())
                return true;
            return false;
        }

        #endregion

        #region 添加维修控制组

        /// <summary>
        ///     添加维修控制组
        /// </summary>
        public DelegateCommand<object> AddMaintainCtrlCommand { get; private set; }

        private void OnAddCtrl(object obj)
        {
            if (SelItem != null)
            {
                var newMaintainCtrl = new ItemMaintainCtrlDTO
                {
                    Id = RandomHelper.Next(),
                    ItemId = SelItem.Id,
                    ItemNo = SelItem.ItemNo,
                };
                ViewItemMaintainCtrls.Add(newMaintainCtrl);
                ItemMaintainCtrls.AddNew(newMaintainCtrl);
            }
        }

        private bool CanAddCtrl(object obj)
        {
            if (SelItem != null && SelItem.IsLife)
                return true;
            return false;
        }

        #endregion

        #region 移除维修控制组

        /// <summary>
        ///     移除维修控制组
        /// </summary>
        public DelegateCommand<object> RemoveMaintainCtrlCommand { get; private set; }

        private void OnRemoveCtrl(object obj)
        {
            if (SelItemMaintainCtrl != null)
            {
                ViewItemMaintainCtrls.Remove(SelItemMaintainCtrl);
                ItemMaintainCtrls.Remove(SelItemMaintainCtrl);
            }
        }

        private bool CanRemoveCtrl(object obj)
        {
            if (SelItemMaintainCtrl != null)
                return true;
            return false;
        }

        #endregion

        #region 增加维修控制明细

        /// <summary>
        ///     增加维修控制明细
        /// </summary>
        public DelegateCommand<object> AddCtrlLineCommand { get; private set; }

        private void OnAddCtrlLine(object obj)
        {
            var xmlNode = SelItemMaintainCtrl.XmlContent ?? new XElement("MaintainCtrl");
            if (FirstVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "StartDate"),
                    new XAttribute("Value", StartDate.Year + "-" + StartDate.Month + "-" + StartDate.Day));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelCtrlUnit.Id));
                var maxNode = new XElement("Max", new XAttribute("Value", Max));
                var minNode = new XElement("Min", new XAttribute("Value", Min));
                detailNode.Add(maxNode);
                detailNode.Add(minNode);
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            else if (SecondVisible == Visibility.Visible || ThirdVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "StartDate"),
                    new XAttribute("Value", StartDate.Year + "-" + StartDate.Month + "-" + StartDate.Day));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelCtrlUnit.Id));
                var standardNode = new XElement("Standard", new XAttribute("Value", Standard));
                detailNode.Add(standardNode);
                if (SecondVisible == Visibility.Visible)
                {
                    var rateNode = new XElement("Rate", new XAttribute("Value", Rate));
                    detailNode.Add(rateNode);
                }
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            else if (FourVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "Action"),
                    new XAttribute("Value", SelMaintainWork.Id));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelCtrlUnit.Id));
                var maxNode = new XElement("Max", new XAttribute("Value", Max));
                var minNode = new XElement("Min", new XAttribute("Value", Min));
                detailNode.Add(maxNode);
                detailNode.Add(minNode);
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            else if (FiveVisible == Visibility.Visible || SixVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "Action"),
                    new XAttribute("Value", SelMaintainWork.Id));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelCtrlUnit.Id));
                var standardNode = new XElement("Standard", new XAttribute("Value", Standard));
                detailNode.Add(standardNode);
                if (FiveVisible == Visibility.Visible)
                {
                    var rateNode = new XElement("Rate", new XAttribute("Value", Rate));
                    detailNode.Add(rateNode);
                }
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            else if (SevenVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "SinceNew"));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelSnType.ToString()));
                var maxNode = new XElement("Max", new XAttribute("Value", Max));
                var minNode = new XElement("Min", new XAttribute("Value", Min));
                detailNode.Add(maxNode);
                detailNode.Add(minNode);
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            else if (EightVisible == Visibility.Visible || NineVisible == Visibility.Visible)
            {
                var node = new XElement("CtrlType", new XAttribute("Name", "SinceNew"));
                var detailNode = new XElement("CtrlDetail", new XAttribute("Type", SelSnType.ToString()));
                var standardNode = new XElement("Standard", new XAttribute("Value", Standard));
                detailNode.Add(standardNode);
                if (EightVisible == Visibility.Visible)
                {
                    var rateNode = new XElement("Rate", new XAttribute("Value", Rate));
                    detailNode.Add(rateNode);
                }
                node.Add(detailNode);
                xmlNode.Add(node);
            }
            SelItemMaintainCtrl.XmlContent = xmlNode;
            CtrlLines = ConvertXmlToString(xmlNode);
            RaisePropertyChanged(() => CtrlLines);
        }

        private bool CanAddCtrlLine(object obj)
        {
            if (SelItemMaintainCtrl == null) return false;
            if (FirstVisible == Visibility.Visible && SelCtrlUnit != null && Max > 0 && Min > 0) return true;
            if (SecondVisible == Visibility.Visible && SelCtrlUnit != null && Standard > 0) return true;
            if (ThirdVisible == Visibility.Visible && SelCtrlUnit != null && Standard > 0) return true;
            if (FourVisible == Visibility.Visible && SelMaintainWork != null && SelCtrlUnit != null && Max > 0 &&
                Min > 0) return true;
            if (FiveVisible == Visibility.Visible && SelMaintainWork != null && SelCtrlUnit != null && Standard > 0)
                return true;
            if (SixVisible == Visibility.Visible && SelMaintainWork != null && SelCtrlUnit != null && Standard > 0)
                return true;
            if (SevenVisible == Visibility.Visible && (int) SelSnType >= 0 && Max > 0 && Min > 0) return true;
            if (EightVisible == Visibility.Visible && (int) SelSnType >= 0 && Standard > 0) return true;
            if (NineVisible == Visibility.Visible && (int) SelSnType >= 0 && Standard > 0) return true;
            return false;
        }

        #endregion

        #region 移除维修控制明细

        /// <summary>
        ///     移除维修控制明细
        /// </summary>
        public DelegateCommand<object> RemoveCtrlLineCommand { get; private set; }

        private void OnRemoveCtrlLine(object obj)
        {
        }

        private bool CanRemoveCtrlLine(object obj)
        {
            return false;
        }

        #endregion

        #endregion

        #region  界面控制属性

        private Visibility _buttonVisible = Visibility.Collapsed;
        private IEnumerable<CtrlLine> _ctrlLines;
        private Visibility _eightVisible = Visibility.Collapsed;
        private Visibility _firstVisible = Visibility.Collapsed;
        private Visibility _fiveVisible = Visibility.Collapsed;
        private Visibility _fourVisible = Visibility.Collapsed;
        private decimal _max;
        private decimal _min;
        private Visibility _nineVisible = Visibility.Collapsed;
        private decimal _rate;
        private Visibility _secondVisible = Visibility.Collapsed;
        private CtrlLine _selCtrlLine;
        private CtrlType _selCtrlType;
        private CtrlUnitDTO _selCtrlUnit;
        private MaintainWorkDTO _selMaintainWork;
        private SinceNewType _selSnType;
        private Visibility _sevenVisible = Visibility.Collapsed;
        private Visibility _sixVisible = Visibility.Collapsed;
        private decimal _standard;
        private DateTime _startDate;
        private Visibility _thirdVisible = Visibility.Collapsed;

        public IEnumerable<CtrlType> CtrlTypes
        {
            get
            {
                var result = new List<CtrlType>
                {
                    new CtrlType {TypeId = 1, Description = "自XX日期起，XX值处于XX与XX之间"},
                    new CtrlType {TypeId = 2, Description = "自XX日期起，XX值达到XX基准值之前（浮动比率为X%）"},
                    new CtrlType {TypeId = 3, Description = "自XX日期起，XX值达到XX基准值之前"},
                    new CtrlType {TypeId = 4, Description = "自上一次XX操作起，XX值处于XX与XX之间"},
                    new CtrlType {TypeId = 5, Description = "自上一次XX操作起，XX值达到XX基准值之前（浮动比率为X%）"},
                    new CtrlType {TypeId = 6, Description = "自上一次XX操作起，XX值达到XX基准值之前"},
                    new CtrlType {TypeId = 7, Description = "TSN或CSN值达到XX与XX之间"},
                    new CtrlType {TypeId = 8, Description = "TSN或CSN值达到XX基准值之前（浮动比率为X%）"},
                    new CtrlType {TypeId = 9, Description = "TSN或CSN值达到XX基准值之前"}
                };
                return result.AsEnumerable();
            }
        }

        public Visibility ButtonVisible
        {
            get { return _buttonVisible; }
            set
            {
                if (_buttonVisible != value)
                {
                    _buttonVisible = value;
                    RaisePropertyChanged(() => ButtonVisible);
                }
            }
        }

        public Visibility FirstVisible
        {
            get { return _firstVisible; }
            set
            {
                if (_firstVisible != value)
                {
                    _firstVisible = value;
                    RaisePropertyChanged(() => FirstVisible);
                }
            }
        }

        public Visibility SecondVisible
        {
            get { return _secondVisible; }
            set
            {
                if (_secondVisible != value)
                {
                    _secondVisible = value;
                    RaisePropertyChanged(() => SecondVisible);
                }
            }
        }

        public Visibility ThirdVisible
        {
            get { return _thirdVisible; }
            set
            {
                if (_thirdVisible != value)
                {
                    _thirdVisible = value;
                    RaisePropertyChanged(() => ThirdVisible);
                }
            }
        }

        public Visibility FourVisible
        {
            get { return _fourVisible; }
            set
            {
                if (_fourVisible != value)
                {
                    _fourVisible = value;
                    RaisePropertyChanged(() => FourVisible);
                }
            }
        }

        public Visibility FiveVisible
        {
            get { return _fiveVisible; }
            set
            {
                if (_fiveVisible != value)
                {
                    _fiveVisible = value;
                    RaisePropertyChanged(() => FiveVisible);
                }
            }
        }

        public Visibility SixVisible
        {
            get { return _sixVisible; }
            set
            {
                if (_sixVisible != value)
                {
                    _sixVisible = value;
                    RaisePropertyChanged(() => SixVisible);
                }
            }
        }

        public Visibility SevenVisible
        {
            get { return _sevenVisible; }
            set
            {
                if (_sevenVisible != value)
                {
                    _sevenVisible = value;
                    RaisePropertyChanged(() => SevenVisible);
                }
            }
        }

        public Visibility EightVisible
        {
            get { return _eightVisible; }
            set
            {
                if (_eightVisible != value)
                {
                    _eightVisible = value;
                    RaisePropertyChanged(() => EightVisible);
                }
            }
        }

        public Visibility NineVisible
        {
            get { return _nineVisible; }
            set
            {
                if (_nineVisible != value)
                {
                    _nineVisible = value;
                    RaisePropertyChanged(() => NineVisible);
                }
            }
        }

        /// <summary>
        ///     选择的控制明细类型
        /// </summary>
        public CtrlType SelCtrlType
        {
            get { return _selCtrlType; }
            set
            {
                if (_selCtrlType != value)
                {
                    _selCtrlType = value;
                    ButtonVisible = Visibility.Collapsed;
                    FirstVisible = Visibility.Collapsed;
                    SecondVisible = Visibility.Collapsed;
                    ThirdVisible = Visibility.Collapsed;
                    FourVisible = Visibility.Collapsed;
                    FiveVisible = Visibility.Collapsed;
                    SixVisible = Visibility.Collapsed;
                    SevenVisible = Visibility.Collapsed;
                    EightVisible = Visibility.Collapsed;
                    NineVisible = Visibility.Collapsed;
                    if (value != null)
                    {
                        ButtonVisible = Visibility.Visible;
                        if (value.TypeId == 1)
                        {
                            FirstVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 2)
                        {
                            SecondVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 3)
                        {
                            ThirdVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 4)
                        {
                            FourVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 5)
                        {
                            FiveVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 6)
                        {
                            SixVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 7)
                        {
                            SevenVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 8)
                        {
                            EightVisible = Visibility.Visible;
                        }
                        else if (value.TypeId == 9)
                        {
                            NineVisible = Visibility.Visible;
                        }
                    }
                    RefreshVisivlities();
                    RaisePropertyChanged(() => SelCtrlType);
                    RefreshCommandState();
                }
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    RaisePropertyChanged(() => StartDate);
                    RefreshCommandState();
                }
            }
        }

        public CtrlUnitDTO SelCtrlUnit
        {
            get { return _selCtrlUnit; }
            set
            {
                if (_selCtrlUnit != value)
                {
                    _selCtrlUnit = value;
                    RaisePropertyChanged(() => SelCtrlUnit);
                    RefreshCommandState();
                }
            }
        }

        public MaintainWorkDTO SelMaintainWork
        {
            get { return _selMaintainWork; }
            set
            {
                if (_selMaintainWork != value)
                {
                    _selMaintainWork = value;
                    RaisePropertyChanged(() => SelMaintainWork);
                    RefreshCommandState();
                }
            }
        }

        public SinceNewType SelSnType
        {
            get { return _selSnType; }
            set
            {
                if (_selSnType != value)
                {
                    _selSnType = value;
                    RaisePropertyChanged(() => SelSnType);
                    RefreshCommandState();
                }
            }
        }

        public decimal Max
        {
            get { return _max; }
            set
            {
                if (_max != value)
                {
                    _max = value;
                    RaisePropertyChanged(() => Max);
                    RefreshCommandState();
                }
            }
        }

        public decimal Min
        {
            get { return _min; }
            set
            {
                if (_min != value)
                {
                    _min = value;
                    RaisePropertyChanged(() => Min);
                    RefreshCommandState();
                }
            }
        }

        public decimal Standard
        {
            get { return _standard; }
            set
            {
                if (_standard != value)
                {
                    _standard = value;
                    RaisePropertyChanged(() => Standard);
                    RefreshCommandState();
                }
            }
        }

        public decimal Rate
        {
            get { return _rate; }
            set
            {
                if (_rate != value)
                {
                    _rate = value;
                    RaisePropertyChanged(() => Rate);
                    RefreshCommandState();
                }
            }
        }

        public IEnumerable<CtrlLine> CtrlLines
        {
            get { return _ctrlLines; }
            set
            {
                if (!(_ctrlLines.Equals(value)))
                {
                    _ctrlLines = value;
                    RaisePropertyChanged(() => CtrlLines);
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     选择的控制明细
        /// </summary>
        public CtrlLine SelCtrlLine
        {
            get { return _selCtrlLine; }
            set
            {
                if (_selCtrlLine != value)
                {
                    _selCtrlLine = value;
                    RaisePropertyChanged(() => SelCtrlLine);
                    RefreshCommandState();
                }
            }
        }

        public void RefreshVisivlities()
        {
            RaisePropertyChanged(() => FirstVisible);
            RaisePropertyChanged(() => SecondVisible);
            RaisePropertyChanged(() => ThirdVisible);
        }

        private IEnumerable<CtrlLine> ConvertXmlToString(XElement xmlContent)
        {
            if (xmlContent != null)
            {
                var result = new List<CtrlLine>();
                foreach (var ctrlType in xmlContent.Descendants("CtrlType"))
                {
                    foreach (var ctrlDetail in ctrlType.Descendants("CtrlDetail"))
                    {
                        string str = null;
                        if (ctrlType.Attribute("Name").Value == "StartDate")
                        {
                            str += "自" + ctrlType.Attribute("Value").Value + "起，当";
                        }
                        else if (ctrlType.Attribute("Name").Value == "Action")
                        {
                            var type = ctrlType;
                            var maintainWork =
                                MaintainWorks.FirstOrDefault(
                                    p => p.Id.ToString(CultureInfo.InvariantCulture) == type.Attribute("Value").Value);
                            if (maintainWork != null)
                                str += "自上次" + maintainWork.WorkCode + "起，当";
                        }

                        //控制单位
                        var detail = ctrlDetail;
                        var ctrlUnit =
                            CtrlUnits.FirstOrDefault(
                                p => p.Id.ToString(CultureInfo.InvariantCulture) == detail.Attribute("Type").Value);
                        if (ctrlUnit != null)
                            str += ctrlUnit.Name + "(" + ctrlUnit.Description + ")";
                        else str += ctrlDetail.Attribute("Type").Value;

                        //间隔
                        if (ctrlDetail.Descendants("Max").Count() != 0 && ctrlDetail.Descendants("Min").Count() != 0)
                        {
                            var max = ctrlDetail.Descendants("Max").First();
                            var min = ctrlDetail.Descendants("Min").First();

                            str += "处于(最小间隔)" + min.Attribute("Value").Value + "和(最大间隔)" + max.Attribute("Value").Value +
                                   "之间";
                        }
                        else if (ctrlDetail.Descendants("Standard").Count() != 0)
                        {
                            var standard = ctrlDetail.Descendants("Standard").First();
                            str += "的基准间隔为" + standard.Attribute("Value").Value;
                        }
                        if (ctrlDetail.Descendants("Rate").Count() != 0)
                        {
                            var rate = ctrlDetail.Descendants("Rate").First();
                            str += "(浮动比率为" + rate.Attribute("Value").Value + ")";
                        }
                        str += "时";
                        var ctrlLine = new CtrlLine {Id = RandomHelper.Next(), Description = str};
                        result.Add(ctrlLine);
                    }
                }
                return result;
            }
            return null;
        }

        public class CtrlLine
        {
            public int Id { get; set; }

            public string Description { get; set; }
        }

        public class CtrlType
        {
            public int TypeId { get; set; }

            public string Description { get; set; }
        }

        #endregion
    }
}