#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 17:38:25
// 文件名：QueryAdSbVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 17:38:25
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageAdSb
{
    [Export(typeof (QueryAdSbVm))]
    public class QueryAdSbVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;


        [ImportingConstructor]
        public QueryAdSbVm(IPartService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            // 创建并注册CollectionView
            AdSbs = _service.CreateCollection(_context.AdSbs);
            AdSbs.PageSize = 20;
        }

        #endregion

        #region 数据

        #region 公共属性

        #region AdSbSCN/MSCN

        /// <summary>
        ///     AdSb集合
        /// </summary>
        public QueryableDataServiceCollectionView<AdSbDTO> AdSbs { get; set; }

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
            // 将CollectionView的AutoLoad属性设为True
            if (!AdSbs.AutoLoad)
                AdSbs.AutoLoad = true;
            AdSbs.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        #endregion
    }
}