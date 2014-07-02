#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/27 14:19:18
// 文件名：QueryLifeMonitorVm
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/27 14:19:18
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof (QueryLifeMonitorVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QueryLifeMonitorVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public QueryLifeMonitorVm(IPartService service)
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
            SnRegs = new QueryableDataServiceCollectionView<SnRegDTO>(_context, _context.SnRegs);
            SnRegs.PageSize = 20;
            var snDescriptor = new FilterDescriptor("IsLife", FilterOperator.IsEqualTo, true);
            SnRegs.FilterDescriptors.Add(snDescriptor);
            SnRegs.LoadedData += (s, e) => SelSnReg = SnRegs.FirstOrDefault();
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
            if (!SnRegs.AutoLoad)
                SnRegs.AutoLoad = true;
        }

        #region 业务

        #region 序号件集合

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        #endregion

        #region 选择的序号件

        private SnRegDTO _selSnReg;

        /// <summary>
        ///     选择的序号件
        /// </summary>
        public SnRegDTO SelSnReg
        {
            get { return _selSnReg; }
            set
            {
                if (_selSnReg != value)
                {
                    _selSnReg = value;
                    RaisePropertyChanged(() => SelSnReg);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion
    }
}