#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，13:11
// 文件名：SupplierRoleManagerVM.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof (SupplierRoleManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SupplierRoleManagerVM : EditViewModelBase
    {
        private PurchaseData _purchaseData;
        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public SupplierRoleManagerVM()
        {
            InitialSupplierCompany(); //初始化合作公司。
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
            Service.RegisterCollectionView(SupplierCompanysView); //注册查询集合。
            SupplierCompanysView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelSupplierCompany==null)
                    {
                        SelSupplierCompany = e.Entities.Cast<SupplierCompanyDTO>().FirstOrDefault();
                    }
                };

            SupplierCompanysView.PropertyChanged += OnViewPropertyChanged;
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