//------------------------------------------------------------------------------
//     
//------------------------------------------------------------------------------

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Forwarder
{
    [Export(typeof(ForwarderManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ForwarderManagerVM : EditViewModelBase
    {
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public ForwarderManagerVM(IPurchaseService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialForward();
        }

        #region 加载Forward相关信息
        private ForwarderDTO _selectedForwarder;
        /// <summary>
        ///     选择BFE。
        /// </summary>
        public ForwarderDTO SelectedForwarder
        {
            get { return _selectedForwarder; }
            set
            {
                _selectedForwarder = value;
                RaisePropertyChanged(() => SelectedForwarder);
            }
        }

        private bool _canSelectForward = true;
        //用户能否选择
        public bool CanSelectForward
        {
            get { return _canSelectForward; }
            set
            {
                if (_canSelectForward != value)
                {
                    _canSelectForward = value;
                    RaisePropertyChanged(() => CanSelectForward);
                }
            }
        }

        /// <summary>
        ///     获取所有承运人信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ForwarderDTO> Forwarders { get; set; }

        /// <summary>
        ///     初始化采购信息。
        /// </summary>
        private void InitialForward()
        {
            Forwarders = _service.CreateCollection(_context.Forwarders);
            Forwarders.PageSize = 20;
            _service.RegisterCollectionView(Forwarders); //注册查询集合。
            Forwarders.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                }
            };
            Forwarders.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectForward = !Forwarders.HasChanges;
                }
                if (e.PropertyName.Equals("CurrentAddItem", StringComparison.OrdinalIgnoreCase))
                {
                    if (Forwarders.CurrentAddItem is ForwarderDTO)
                    {
                        (Forwarders.CurrentAddItem as ForwarderDTO).ForwarderId = RandomHelper.Next();
                    }
                }
            };
        }

        #endregion

        /// <summary>
        ///     加载采购数据。
        /// </summary>
        public override void LoadData()
        {
            if (!Forwarders.AutoLoad)
                Forwarders.AutoLoad = true; //加载数据。
            else
                Forwarders.Load(true);
        }
    }
}