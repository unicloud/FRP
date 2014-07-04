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
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(LinkManManagerVM))]
    public class LinkManManagerVM : EditViewModelBase
    {
        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;
        private FilterDescriptor _linkManFilter; //查找联系人配置。

        [ImportingConstructor]
        public LinkManManagerVM(IPurchaseService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialSupplierCompany(); //初始化合作公司。
            InitialLinkMan(); //初始化联系人。
            InitialLinkManCommand(); //初始化联系人相关命令。
        }

        #region SupplierCompany相关信息

        private SupplierCompanyDTO _selectedSupplierCompany;

        /// <summary>
        ///     选择合作公司。
        /// </summary>
        public SupplierCompanyDTO SelectedSupplierCompany
        {
            get { return _selectedSupplierCompany; }
            set
            {
                if (_selectedSupplierCompany != value)
                {
                    _selectedSupplierCompany = value;
                    LoadLinkManByCompanyId(value);
                    RaisePropertyChanged(() => SelectedSupplierCompany);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierCompanyDTO> SupplierCompanies { get; set; }

        /// <summary>
        ///     初始化合作公司信息。
        /// </summary>
        private void InitialSupplierCompany()
        {
            SupplierCompanies = _service.CreateCollection(_context.SupplierCompanys);
            SupplierCompanies.PageSize = 20;
            SupplierCompanies.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedSupplierCompany = e.Entities.Cast<SupplierCompanyDTO>().FirstOrDefault();
            };
        }

        #endregion

        #region LinkMan相关信息

        private LinkmanDTO _selectedLinkMan;

        /// <summary>
        ///     选择联系人。
        /// </summary>
        public LinkmanDTO SelectedLinkMan
        {
            get { return _selectedLinkMan; }
            set
            {
                if (_selectedLinkMan != value)
                {
                    _selectedLinkMan = value;
                    DeleteLinkManCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectedLinkMan);
                }
            }
        }

        /// <summary>
        ///     获取某供应商公司下所有联系人。
        /// </summary>
        public QueryableDataServiceCollectionView<LinkmanDTO> Linkmen { get; set; }

        /// <summary>
        ///     初始化联系人。
        /// </summary>
        private void InitialLinkMan()
        {
            Linkmen = _service.CreateCollection(_context.Linkmans);
            _linkManFilter = new FilterDescriptor("SourceId", FilterOperator.IsEqualTo, Guid.Empty);
            Linkmen.FilterDescriptors.Add(_linkManFilter);
            Linkmen.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedLinkMan = e.Entities.Cast<LinkmanDTO>().FirstOrDefault();
            };
            Linkmen.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    SaveCommand.RaiseCanExecuteChanged();
                    AbortCommand.RaiseCanExecuteChanged();
                }
                if (e.PropertyName.Equals("IsEditingItem", StringComparison.OrdinalIgnoreCase))
                {
                    RefreshAddAndDelButtonState();
                }
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
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
            if (!Linkmen.AutoLoad)
            {
                Linkmen.AutoLoad = true;
            }
        }

        #endregion

        #region 命令

        #region 新增命令

        public DelegateCommand<object> AddLinkManCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddLinkManExecute(object sender)
        {
            if (SelectedSupplierCompany == null)
            {
                MessageAlert("提示", "合作公司不能为空");
                return;
            }
            var newLiankMan = new LinkmanDTO
            {
                LinkmanId = RandomHelper.Next(),
                SourceId = SelectedSupplierCompany.LinkManId
            };
            Linkmen.AddNewItem(newLiankMan);
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddLinkManExecute(object sender)
        {
            //正在提交时，或者编辑、新增中，新增按钮不可用
            if (Linkmen.IsSubmittingChanges || Linkmen.IsAddingNew
                || Linkmen.IsEditingItem)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 删除命令

        public DelegateCommand<object> DeleteLinkManCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDeleteLinkManExecute(object sender)
        {
            if (SelectedLinkMan == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            Linkmen.Remove(SelectedLinkMan);
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDeleteLinkManExecute(object sender)
        {
            //正在提交时，或者编辑、新增中，删除按钮不可用
            if (Linkmen.IsSubmittingChanges || Linkmen.IsAddingNew
                || Linkmen.IsEditingItem)
            {
                return false;
            }
            return SelectedLinkMan != null;
        }

        #endregion

        /// <summary>
        ///     刷新按钮状态
        /// </summary>
        private void RefreshAddAndDelButtonState()
        {
            AddLinkManCommand.RaiseCanExecuteChanged();
            DeleteLinkManCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        ///     初始化联系人命令
        /// </summary>
        private void InitialLinkManCommand()
        {
            AddLinkManCommand = new DelegateCommand<object>(OnAddLinkManExecute, CanAddLinkManExecute);
            DeleteLinkManCommand = new DelegateCommand<object>(OnDeleteLinkManExecute, CanDeleteLinkManExecute);
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            if (!SupplierCompanies.AutoLoad)
            {
                SupplierCompanies.AutoLoad = true; //加载数据。
            }
            else
            {
                SupplierCompanies.Load(true);
            }
        }

        #endregion
    }
}