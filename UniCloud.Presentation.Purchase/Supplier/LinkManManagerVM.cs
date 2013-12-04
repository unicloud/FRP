using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

namespace UniCloud.Presentation.Purchase.Supplier
{
    [Export(typeof(LinkManManagerVM))]
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
           Service.RegisterCollectionView(SupplierCompanysView); //注册查询集合。
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
               _selectedLinkMan = value;
               RaisePropertyChanged(() => SelLinkMan);
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
           Service.RegisterCollectionView(LinkmansView); //注册查询集合。
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
               if (e.PropertyName == "CurrentAddItem")
               {
                   if (LinkmansView.CurrentAddItem is LinkmanDTO)
                   {
                       (LinkmansView.CurrentAddItem as LinkmanDTO).SourceId = SelSupplierCompany.LinkManId;
                       (LinkmansView.CurrentAddItem as LinkmanDTO).LinkmanId = RandomHelper.Next();
                   }
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
