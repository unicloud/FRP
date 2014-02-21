#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/21 15:56:19
// 文件名：MaintainSCNVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/21 15:56:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainSCNVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainSCNVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;


        [ImportingConstructor]
        public MaintainSCNVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            AddScnCommand = new DelegateCommand<object>(OnAddScn, CanAddScn);
            RemoveScnCommand = new DelegateCommand<object>(OnRemoveScn, CanRemoveScn);
            AddApplicableAircraftCommand = new DelegateCommand<object>(OnAddApplicableAircraft, CanAddApplicableAircraft);
            RemoveApplicableAircraftCommand = new DelegateCommand<object>(OnRemoveApplicableAircraft, CanRemoveApplicableAircraft);
            // 创建并注册CollectionView
            Scns = _service.CreateCollection(_context.Scns, o => o.ApplicableAircrafts);
            _service.RegisterCollectionView(Scns);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     SCN类型
        /// </summary>
        public Array Types
        {
            get { return Enum.GetValues(typeof(ScnType)); }
        }

        /// <summary>
        ///     SCN适用类型
        /// </summary>
        public Array ScnTypes
        {
            get { return Enum.GetValues(typeof(ScnApplicableType)); }
        }

        #region SCN/MSCN

        private ScnDTO _scn;

        /// <summary>
        ///     SCN/MSCN集合
        /// </summary>
        public QueryableDataServiceCollectionView<ScnDTO> Scns { get; set; }

        /// <summary>
        ///     选中的SCN/MSCN
        /// </summary>
        public ScnDTO Scn
        {
            get { return _scn; }
            set
            {
                if (_scn != value)
                {
                    _scn = value;
                    RaisePropertyChanged(() => Scn);
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
            // 将CollectionView的AutoLoad属性设为True
            if (!Scns.AutoLoad)
                Scns.AutoLoad = true;
            Scns.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        #region 创建新SCN/MSCN
        /// <summary>
        ///     创建新SCN/MSCN
        /// </summary>
        public DelegateCommand<object> AddScnCommand { get; set; }

        protected void OnAddScn(object obj)
        {
            Scn = new ScnDTO
            {
                Id = RandomHelper.Next(),
                CheckDate = DateTime.Now,
                TypeString = ScnType.SCN.ToString(),
                ScnTypeString = ScnApplicableType.个体.ToString(),
                ScnStatus = 0,
            };
            Scns.AddNew(Scn);
        }

        protected bool CanAddScn(object obj)
        {
            return true;
        }

        #endregion

        #region 删除SCN/MSCN
        /// <summary>
        ///     删除SCN/MSCN
        /// </summary>
        public DelegateCommand<object> RemoveScnCommand { get; set; }

        protected void OnRemoveScn(object obj)
        {
            if (Scn == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                Scns.Remove(Scn);
            });
        }

        protected bool CanRemoveScn(object obj)
        {
            return true;
        }

        #endregion

        #region 增加适用飞机
        /// <summary>
        ///     增加适用飞机
        /// </summary>
        public DelegateCommand<object> AddApplicableAircraftCommand { get; set; }

        protected void OnAddApplicableAircraft(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}

            //SelectInvoicesWindow = new SelectInvoices();
            //SelectInvoicesWindow.ViewModel.InitData(PaymentNotice);
            //SelectInvoicesWindow.ShowDialog();

            ////var maintainInvoiceLine = new PaymentNoticeLineDTO
            ////{
            ////    PaymentNoticeLineId = RandomHelper.Next(),
            ////};

            ////PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
        }

        protected bool CanAddApplicableAircraft(object obj)
        {
            return true;
        }

        #endregion

        #region 移除适用飞机
        /// <summary>
        ///     移除适用飞机
        /// </summary>
        public DelegateCommand<object> RemoveApplicableAircraftCommand { get; private set; }

        protected void OnRemoveApplicableAircraft(object obj)
        {
            //if (PaymentNoticeLine == null)
            //{
            //    MessageAlert("请选择一条付款通知明细！");
            //    return;
            //}
            //MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            //{
            //    if (arg.DialogResult != true) return;
            //    PaymentNotice.PaymentNoticeLines.Remove(PaymentNoticeLine);
            //});
        }

        protected bool CanRemoveApplicableAircraft(object obj)
        {
            return true;
        }

        #endregion

        #region 提交SCN/MSCN

        protected void OnSubmitInvoice(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}
            //PaymentNotice.Status = (int)PaymentNoticeStatus.待审核;
        }

        protected bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核SCN/MSCN

        protected void OnReviewInvoice(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}
            //PaymentNotice.Status = (int)PaymentNoticeStatus.已审核;
            //PaymentNotice.Reviewer = "admin";
            //PaymentNotice.ReviewDate = DateTime.Now;
        }

        protected bool CanReviewInvoice(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
