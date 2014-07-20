#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/27 14:15:58
// 文件名：QueryMaintainCtrlVm
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/27 14:15:58
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Part.ManageItem;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (QueryMaintainCtrlVm))]
    public class QueryMaintainCtrlVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private FilterDescriptor _itemCtrlDescriptor;
        private FilterDescriptor _pnCtrlDescriptor;
        private FilterDescriptor _snDescriptor;

        [ImportingConstructor]
        public QueryMaintainCtrlVm(IPartService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            Items = new QueryableDataServiceCollectionView<ItemDTO>(_context, _context.Items);
            var itemDescriptor = new FilterDescriptor("IsLife", FilterOperator.IsEqualTo, true);
            Items.FilterDescriptors.Add(itemDescriptor);
            Items.LoadedData += (s, e) => { SelItem = Items.FirstOrDefault(); };

            SnRegs = new QueryableDataServiceCollectionView<SnRegDTO>(_context, _context.SnRegs);
            _snDescriptor = new FilterDescriptor("PnRegId", FilterOperator.IsEqualTo, -1);
            SnRegs.FilterDescriptors.Add(_snDescriptor);

            ItemMaintainCtrls = new QueryableDataServiceCollectionView<ItemMaintainCtrlDTO>(_context,
                _context.ItemMaintainCtrls);
            _itemCtrlDescriptor = new FilterDescriptor("ItemId", FilterOperator.IsEqualTo, -1);
            ItemMaintainCtrls.FilterDescriptors.Add(_itemCtrlDescriptor);

            PnMaintainCtrls = new QueryableDataServiceCollectionView<PnMaintainCtrlDTO>(_context,
                _context.PnMaintainCtrls);
            _pnCtrlDescriptor = new FilterDescriptor("PnRegId", FilterOperator.IsEqualTo, -1);
            PnMaintainCtrls.FilterDescriptors.Add(_pnCtrlDescriptor);

            CtrlUnits = new QueryableDataServiceCollectionView<CtrlUnitDTO>(_context, _context.CtrlUnits);
            MaintainWorks = new QueryableDataServiceCollectionView<MaintainWorkDTO>(_context, _context.MaintainWorks);
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
        }

        #region 业务

        #region 附件项集合

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        #endregion

        #region 附件集合

        private List<PnRegDTO> _pnRegs = new List<PnRegDTO>();

        /// <summary>
        ///     附件集合
        /// </summary>
        public List<PnRegDTO> PnRegs
        {
            get { return _pnRegs; }
            set
            {
                if (_pnRegs != value)
                {
                    _pnRegs = value;
                    RaisePropertyChanged(() => PnRegs);
                }
            }
        }

        #endregion

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        #endregion

        #region 项维修控制组集合

        private IEnumerable<ItemVm.CtrlLine> _itemCtrlLines;
        private ItemMaintainCtrlDTO _selItemMaintainCtrl;

        /// <summary>
        ///     项维修控制组集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemMaintainCtrlDTO> ItemMaintainCtrls { get; set; }

        /// <summary>
        ///     选择的项维修控制组
        /// </summary>
        public ItemMaintainCtrlDTO SelItemMaintainCtrl
        {
            get { return _selItemMaintainCtrl; }
            set
            {
                if (_selItemMaintainCtrl != value)
                {
                    _selItemMaintainCtrl = value;
                    _itemCtrlLines = new List<ItemVm.CtrlLine>();
                    if (value != null && value.XmlContent != null)
                    {
                        ItemCtrlLines = ConvertXmlToString(value.XmlContent);
                    }
                    RaisePropertyChanged(() => ItemCtrlLines);
                    RaisePropertyChanged(() => SelItemMaintainCtrl);
                }
            }
        }


        public IEnumerable<ItemVm.CtrlLine> ItemCtrlLines
        {
            get { return _itemCtrlLines; }
            set
            {
                if (!(_itemCtrlLines.Equals(value)))
                {
                    _itemCtrlLines = value;
                    RaisePropertyChanged(() => ItemCtrlLines);
                }
            }
        }

        #endregion

        #region 部件维修控制组集合

        private IEnumerable<ItemVm.CtrlLine> _pnCtrlLines;
        private PnMaintainCtrlDTO _selPnMaintainCtrl;

        /// <summary>
        ///     部件维修控制组集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnMaintainCtrlDTO> PnMaintainCtrls { get; set; }

        /// <summary>
        ///     选择的部件维修控制组
        /// </summary>
        public PnMaintainCtrlDTO SelPnMaintainCtrl
        {
            get { return _selPnMaintainCtrl; }
            set
            {
                if (_selPnMaintainCtrl != value)
                {
                    _selPnMaintainCtrl = value;
                    _pnCtrlLines = new List<ItemVm.CtrlLine>();
                    if (value != null && value.XmlContent != null)
                    {
                        PnCtrlLines = ConvertXmlToString(value.XmlContent);
                    }
                    RaisePropertyChanged(() => PnCtrlLines);
                    RaisePropertyChanged(() => SelPnMaintainCtrl);
                }
            }
        }

        public IEnumerable<ItemVm.CtrlLine> PnCtrlLines
        {
            get { return _pnCtrlLines; }
            set
            {
                if (!(_pnCtrlLines.Equals(value)))
                {
                    _pnCtrlLines = value;
                    RaisePropertyChanged(() => PnCtrlLines);
                }
            }
        }

        #endregion

        #region 选择的附件项

        private ItemDTO _selItem;

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
                    if (value != null)
                    {
                        var path = CreatePnRegQueryPath(value.Id);
                        LoadPnRegs(path);
                        _itemCtrlDescriptor.Value = value.Id;
                        ItemMaintainCtrls.Load(true);
                    }
                    RaisePropertyChanged(() => SelItem);
                }
            }
        }

        #endregion

        #region 选择的附件

        private PnRegDTO _selPnReg;

        /// <summary>
        ///     选择的附件
        /// </summary>
        public PnRegDTO SelPnReg
        {
            get { return _selPnReg; }
            set
            {
                if (_selPnReg != value)
                {
                    _selPnReg = value;
                    if (value != null)
                    {
                        //附件的序号集合
                        _snDescriptor.Value = value.Id;
                        SnRegs.Load(true);
                        _pnCtrlDescriptor.Value = value.Id;
                        PnMaintainCtrls.Load(true);
                    }
                    RaisePropertyChanged(() => SelPnReg);
                }
            }
        }

        #endregion

        #region 创建查询路径

        /// <summary>
        ///     创建查询路径
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private Uri CreatePnRegQueryPath(int itemId)
        {
            return new Uri(string.Format("GetPnRegsByItem?itemId={0}", itemId),
                UriKind.Relative);
        }

        private void LoadPnRegs(Uri path)
        {
            //查询
            _context.BeginExecute<PnRegDTO>(path,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PartData;
                    try
                    {
                        if (context != null)
                        {
                            _pnRegs = new List<PnRegDTO>();
                            _pnRegs = context.EndExecute<PnRegDTO>(result).ToList();
                            SelPnReg = PnRegs.FirstOrDefault();
                            RaisePropertyChanged(() => PnRegs);
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        var response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }

        #endregion

        #endregion

        #endregion

        #region 方法

        private IEnumerable<ItemVm.CtrlLine> ConvertXmlToString(XElement xmlContent)
        {
            if (xmlContent != null)
            {
                var result = new List<ItemVm.CtrlLine>();
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
                        var ctrlLine = new ItemVm.CtrlLine();
                        ctrlLine.Id = RandomHelper.Next();
                        ctrlLine.Description = str;
                        result.Add(ctrlLine);
                    }
                }
                return result;
            }
            return null;
        }

        #endregion

        #endregion
    }
}