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
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Data.OData.Query.SemanticAst;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof(QueryMaintainCtrlVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryMaintainCtrlVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly PartData _context;
        private FilterDescriptor _snDescriptor;

        [ImportingConstructor]
        public QueryMaintainCtrlVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            Items.LoadedData += (s, e) =>
            {
                SelItem = Items.FirstOrDefault();
            };

            SnRegs = new QueryableDataServiceCollectionView<SnRegDTO>(_context, _context.SnRegs);
            _snDescriptor = new FilterDescriptor("PnRegId", FilterOperator.IsEqualTo, -1);
            SnRegs.FilterDescriptors.Add(_snDescriptor);

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
            private set
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
                        //TODO:带出附件项的维修控制组及明细
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
                        //TODO:带出附件的维修控制组及明细
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
                        QueryOperationResponse response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }

        #endregion
        #endregion

        #endregion

        #endregion
    }
}
