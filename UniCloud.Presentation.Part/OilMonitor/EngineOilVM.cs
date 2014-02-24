#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/23，15:04
// 方案：FRP
// 项目：Part
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Windows;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.OilMonitor
{
    [Export(typeof (EngineOilVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineOilVM : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public EngineOilVM(IPartService service) : base(service)
        {
            _service = service;
            _context = service.Context;

            Zoom = new Size(3, 1);
            PanOffset = new Point(-10000, 0);

            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            InitializeViewEngineOilDTO();
        }

        #endregion

        #region 数据

        #region 公共属性

        #region Zoom

        private Size _zoom;

        /// <summary>
        ///     Zoom
        /// </summary>
        public Size Zoom
        {
            get { return _zoom; }
            private set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged(() => Zoom);
                }
            }
        }

        #endregion

        #region PanOffset

        private Point _panOffset;

        /// <summary>
        ///     PanOffset
        /// </summary>
        public Point PanOffset
        {
            get { return _panOffset; }
            private set
            {
                if (_panOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged(() => PanOffset);
                }
            }
        }

        #endregion

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
            if (!ViewEngineOilDTO.AutoLoad) ViewEngineOilDTO.AutoLoad = true;
            else ViewEngineOilDTO.Load(true);
        }

        #region 发动机滑油

        private EngineOilDTO _selEngineOilDTO;

        /// <summary>
        ///     发动机滑油集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineOilDTO> ViewEngineOilDTO { get; set; }

        /// <summary>
        ///     选中的发动机滑油
        /// </summary>
        public EngineOilDTO SelEngineOilDTO
        {
            get { return _selEngineOilDTO; }
            private set
            {
                if (_selEngineOilDTO != value)
                {
                    _selEngineOilDTO = value;
                    RaisePropertyChanged(() => SelEngineOilDTO);
                }
            }
        }

        /// <summary>
        ///     初始化发动机滑油集合
        /// </summary>
        private void InitializeViewEngineOilDTO()
        {
            ViewEngineOilDTO = _service.CreateCollection(_context.EngineOils);
            _service.RegisterCollectionView(ViewEngineOilDTO);
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #endregion
    }
}