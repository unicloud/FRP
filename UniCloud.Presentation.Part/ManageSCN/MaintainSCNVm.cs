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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof (MaintainScnVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaintainScnVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;


        [ImportingConstructor]
        public MaintainScnVm(IPartService service)
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
            AddScnCommand = new DelegateCommand<object>(OnAddScn, CanAddScn);
            RemoveScnCommand = new DelegateCommand<object>(OnRemoveScn, CanRemoveScn);
            AddApplicableAircraftCommand = new DelegateCommand<object>(OnAddApplicableAircraft, CanAddApplicableAircraft);
            RemoveApplicableAircraftCommand = new DelegateCommand<object>(OnRemoveApplicableAircraft,
                CanRemoveApplicableAircraft);
            SubmitScnCommand = new DelegateCommand<object>(OnSubmitScn, CanSubmitScn);
            ReviewScnCommand = new DelegateCommand<object>(OnReviewScn, CanReviewScn);
            // 创建并注册CollectionView
            Scns = _service.CreateCollection(_context.Scns, o => o.ApplicableAircrafts);
            Scns.PageSize = 6;
            Scns.LoadedData += (o, e) =>
            {
                if (Scn == null)
                    Scn = Scns.FirstOrDefault();
            };
            _service.RegisterCollectionView(Scns);
            ContractAircrafts = new QueryableDataServiceCollectionView<ContractAircraftDTO>(_context,
                _context.ContractAircrafts);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     SCN类型
        /// </summary>
        public Dictionary<int, ScnType> Types
        {
            get
            {
                return Enum.GetValues(typeof (ScnType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (ScnType) value);
            }
        }

        /// <summary>
        ///     SCN适用类型
        /// </summary>
        public Dictionary<int, ScnApplicableType> ScnTypes
        {
            get
            {
                return Enum.GetValues(typeof (ScnApplicableType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (ScnApplicableType) value);
            }
        }

        #region SCN/MSCN

        /// <summary>
        ///     选中的SCN/MSCN
        /// </summary>
        private ScnDTO _scn;

        /// <summary>
        ///     SCN/MSCN集合
        /// </summary>
        public QueryableDataServiceCollectionView<ScnDTO> Scns { get; set; }

        public ScnDTO Scn
        {
            get { return _scn; }
            set
            {
                if (_scn != value)
                {
                    _scn = value;
                    if (_scn != null)
                    {
                        SubmitScnCommand.RaiseCanExecuteChanged();
                        ReviewScnCommand.RaiseCanExecuteChanged();
                        ApplicableAircraft = _scn.ApplicableAircrafts.FirstOrDefault();
                    }
                    RaisePropertyChanged(() => Scn);
                }
            }
        }

        #endregion

        #region 合同飞机

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        #endregion

        #region 适用飞机

        /// <summary>
        ///     选中的适用飞机
        /// </summary>
        private ApplicableAircraftDTO _applicableAircraft;

        public ApplicableAircraftDTO ApplicableAircraft
        {
            get { return _applicableAircraft; }
            set
            {
                if (_applicableAircraft != value)
                {
                    _applicableAircraft = value;
                    RaisePropertyChanged(() => ApplicableAircraft);
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
            ContractAircrafts.Load(true);
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
                ReceiveDate = DateTime.Now,
                Type = 0,
                ScnType = 0,
                ScnStatus = 0,
                AuditOrganization = ScnStatus.技术标准室审核.ToString().Replace("审核", string.Empty),
                AuditTime = DateTime.Now
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
                Scn = Scns.FirstOrDefault();
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
            if (Scn == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            if (string.IsNullOrEmpty(Scn.CSCNumber))
            {
                MessageAlert("请输入相应的批次号！");
                return;
            }

            var aircrafts = new SelectAircrafts();
            aircrafts.ViewModel.InitData(Scn.CSCNumber, Scn);
            aircrafts.ShowDialog();
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
            if (ApplicableAircraft == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                Scn.ApplicableAircrafts.Remove(ApplicableAircraft);
                ApplicableAircraft = Scn.ApplicableAircrafts.FirstOrDefault();
                CaculateApplicableAircraftCost();
            });
        }

        protected bool CanRemoveApplicableAircraft(object obj)
        {
            return true;
        }

        #endregion

        #region 提交SCN/MSCN

        /// <summary>
        ///     提交SCN/MSCN
        /// </summary>
        public DelegateCommand<object> SubmitScnCommand { get; private set; }

        protected void OnSubmitScn(object obj)
        {
            if (Scn == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            var auditOrganizations = new AuditOrganizations(Scn)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };
            auditOrganizations.ShowDialog();
        }

        protected bool CanSubmitScn(object obj)
        {
            if (Scn != null)
            {
                if (Scn.ScnStatus == (int) ScnStatus.技术标准室审核)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 审核SCN/MSCN

        /// <summary>
        ///     审核SCN/MSCN
        /// </summary>
        public DelegateCommand<object> ReviewScnCommand { get; private set; }

        protected void OnReviewScn(object obj)
        {
            if (Scn == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            Scn.ScnStatus = (int) ScnStatus.技术标准室审核;
        }

        protected bool CanReviewScn(object obj)
        {
            if (Scn != null)
            {
                if (Scn.ScnStatus > (int) ScnStatus.技术标准室审核 && Scn.ScnStatus < (int) ScnStatus.生效)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 添加附件成功后执行的操作

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
                Scn.ScnDocumentId = doc.DocumentId;
                Scn.ScnDocName = doc.Name;
                if (Scn.ScnDocName!=Scn.ScnNumber+doc.Extension)
                {
                    MessageAlert("SCN编号与文件名不一致！");
                }
                
            }
        }

        #endregion

        #region 计算适用飞机费用

        private void CaculateApplicableAircraftCost()
        {
            if (Scn.ApplicableAircrafts != null && Scn.ApplicableAircrafts.Count > 0)
            {
                if (Scn.ScnType == 0)
                {
                    Scn.ApplicableAircrafts.ToList().ForEach(p => p.Cost = Scn.Cost);
                }
                else
                {
                    var first = Scn.ApplicableAircrafts.First();
                    first.Cost = Scn.Cost;
                    Scn.ApplicableAircrafts.ToList().ForEach(p => { if (p.Id != first.Id) p.Cost = 0; });
                }
            }
        }

        #endregion

        #endregion
    }
}