#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/04，17:12
// 文件名：LinkManManagerVM.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (LinkManManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LinkManManagerVM : EditViewModelBase
    {
        private FilterDescriptor _linkManFilter; //查找联系人配置。
        private PurchaseData _purchaseData;

        [ImportingConstructor]
        public LinkManManagerVM()
        {
            InitialSupplierCompany(); //初始化合作公司。
            InitialLinkMan(); //初始化联系人。
            InitialLinkManCommand();//初始化联系人相关命令。
        }

        #region SupplierCompany相关信息

        private SupplierCompanyDTO _selectedSupplierCompany;

        /// <summary>
        ///     选择合作公司。
        /// </summary>
        public SupplierCompanyDTO SelSupplierCompany
        {
            get { return _selectedSupplierCompany; }
            set
            {
                if (_selectedSupplierCompany != value)
                {
                    _selectedSupplierCompany = value;
                    LoadLinkManByCompanyId(value);
                    RaisePropertyChanged(() => SelSupplierCompany);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyDTO> SupplierCompanysView { get; set; }

        /// <summary>
        ///     初始化合作公司信息。
        /// </summary>
        private void InitialSupplierCompany()
        {
            SupplierCompanysView = Service.CreateCollection(_purchaseData.SupplierCompanys);
            SupplierCompanysView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelSupplierCompany = e.Entities.Cast<SupplierCompanyDTO>().FirstOrDefault();
                };
        }
        #endregion

        #region LinkMan相关信息

        private LinkmanDTO _selectedLinkMan;

        /// <summary>
        ///     选择联系人。
        /// </summary>
        public LinkmanDTO SelLinkMan
        {
            get { return _selectedLinkMan; }
            set
            {
                if (_selectedLinkMan!=value)
                {
                    _selectedLinkMan = value;
                    DelLinkManCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelLinkMan);
                }
            }
        }

        /// <summary>
        ///     获取某供应商公司下所有联系人。
        /// </summary>
        public QueryableDataServiceCollectionView<LinkmanDTO> LinkmansView { get; set; }

        /// <summary>
        ///     初始化联系人。
        /// </summary>
        private void InitialLinkMan()
        {
            LinkmansView = Service.CreateCollection(_purchaseData.Linkmans);
            _linkManFilter = new FilterDescriptor("SourceId", FilterOperator.IsEqualTo, Guid.Empty);
            LinkmansView.FilterDescriptors.Add(_linkManFilter);
            LinkmansView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    SelLinkMan = e.Entities.Cast<LinkmanDTO>().FirstOrDefault();
                };
            LinkmansView.PropertyChanged += (sender, e) =>
                {
                    if (e.PropertyName == "HasChanges")
                    {
                        SaveCommand.RaiseCanExecuteChanged();
                        AbortCommand.RaiseCanExecuteChanged();
                    }
                    if (e.PropertyName=="IsEditingItem")
                    {
                        RefreshAddAndDelButtonState();
                    }
                    if (e.PropertyName == "IsAddingNew")
                    {
                        RefreshAddAndDelButtonState();
                    }
                };
        }

        /// <summary>
        ///     根据合作公司Id，加载联系人。
        /// </summary>
        /// <param name="supplierCompany">供应商</param>
        private void LoadLinkManByCompanyId(SupplierCompanyDTO supplierCompany)
        {
            //加载联系人
            if (supplierCompany == null) return;
            _linkManFilter.Value = supplierCompany.LinkManId;
            if (!LinkmansView.AutoLoad)
            {
                LinkmansView.AutoLoad = true;
            }
        }

        #endregion

        #region 命令

        #region 新增飞机物料命令

        public DelegateCommand<object> AddLinkManCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddLinkManExecute(object sender)
        {
            if (SelSupplierCompany == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            var newLiankMan = new LinkmanDTO
                {
                    LinkmanId = RandomHelper.Next(),
                    SourceId = SelSupplierCompany.LinkManId
                };
            LinkmansView.AddNewItem(newLiankMan);
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddLinkManExecute(object sender)
        {
            //正在提交时，或者编辑、新增中，新增按钮不可用
            if (LinkmansView.IsSubmittingChanges || LinkmansView.IsAddingNew
                || LinkmansView.IsEditingItem)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 删除飞机物料命令

        public DelegateCommand<object> DelLinkManCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelLinkManExecute(object sender)
        {
            if (SelLinkMan==null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            LinkmansView.Remove(SelLinkMan);
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelLinkManExecute(object sender)
        {
            //正在提交时，或者编辑、新增中，删除按钮不可用
            if (LinkmansView.IsSubmittingChanges || LinkmansView.IsAddingNew
                || LinkmansView.IsEditingItem)
            {
                return false;
            }
            return SelLinkMan != null;
        }

        #endregion

        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        private void RefreshAddAndDelButtonState()
        {
           AddLinkManCommand.RaiseCanExecuteChanged();
           DelLinkManCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// 初始化联系人命令
        /// </summary>
        private void InitialLinkManCommand()
        {
            AddLinkManCommand = new DelegateCommand<object>(OnAddLinkManExecute, CanAddLinkManExecute);
            DelLinkManCommand = new DelegateCommand<object>(OnDelLinkManExecute, CanDelLinkManExecute);
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            SupplierCompanysView.AutoLoad = true; //加载数据。
        }

        /// <summary>
        ///     创建服务。
        /// </summary>
        /// <returns></returns>
        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
        }

        #endregion
    }
}