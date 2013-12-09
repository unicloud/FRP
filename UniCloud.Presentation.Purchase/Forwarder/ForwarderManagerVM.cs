//------------------------------------------------------------------------------
//     
//------------------------------------------------------------------------------

#region 命名空间

using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Forwarder
{
    [Export(typeof (ForwarderManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ForwarderManagerVM : EditViewModelBase
    {
        private PurchaseData _purchaseData;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public ForwarderManagerVM()
        {
            InitialForward();
        }

        #region 加载Forward相关信息

        private bool _canSelectForward = true;
        private ForwarderDTO _selectedForwarder;

        /// <summary>
        ///     选择BFE。
        /// </summary>
        public ForwarderDTO SelForwarder
        {
            get { return _selectedForwarder; }
            set
            {
                _selectedForwarder = value;
                RaisePropertyChanged(() => SelForwarder);
            }
        }

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
        public QueryableDataServiceCollectionView<ForwarderDTO> ForwardersView { get; set; }

        /// <summary>
        ///     初始化采购信息。
        /// </summary>
        private void InitialForward()
        {
            ForwardersView = Service.CreateCollection(_purchaseData.Forwarders);
            Service.RegisterCollectionView(ForwardersView); //注册查询集合。
            ForwardersView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelForwarder==null)
                {
                    SelForwarder = e.Entities.Cast<ForwarderDTO>().FirstOrDefault();
                }
            };
            ForwardersView.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "HasChanges")
                {
                    CanSelectForward = !ForwardersView.HasChanges;
                }
                if (e.PropertyName == "CurrentAddItem")
                {
                    if (ForwardersView.CurrentAddItem is ForwarderDTO)
                    {
                        (ForwardersView.CurrentAddItem as ForwarderDTO).ForwarderId = RandomHelper.Next();
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
            ForwardersView.AutoLoad = true; //加载数据。
        }

        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
        }
    }
}