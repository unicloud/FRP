#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/13 9:43:24
// 文件名：ManageOperationLicenseVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/13 9:43:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManageOperationQualification
{
    [Export(typeof(ManageBusinessLicenseVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageBusinessLicenseVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageBusinessLicenseVm(IRegionManager regionManager, IBaseManagementService service)
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
            AddBusinessLicenseCommand = new DelegateCommand<object>(OnAddBusinessLicense, CanAddBusinessLicense);
            RemoveBusinessLicenseCommand = new DelegateCommand<object>(OnRemoveBusinessLicense, CanRemoveBusinessLicense);
            AddDocumentCommand = new DelegateCommand<object>(AddDocument, CanAddDocument);
            //创建并注册CollectionView
            BusinessLicenses = _service.CreateCollection(_context.BusinessLicenses);
            BusinessLicenses.PageSize = 18;
            _service.RegisterCollectionView(BusinessLicenses);
        }

        #endregion

        #region 数据

        #region 公共属性
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!BusinessLicenses.AutoLoad)
                BusinessLicenses.AutoLoad = true;
        }

        #region 经营证照

        /// <summary>
        ///     经营证照集合
        /// </summary>
        public QueryableDataServiceCollectionView<BusinessLicenseDTO> BusinessLicenses { get; set; }

        private BusinessLicenseDTO _businessLicense;
        public BusinessLicenseDTO BusinessLicense
        {
            get { return _businessLicense; }
            set
            {
                _businessLicense = value;
                if (_businessLicense != null)
                {
                    if (_businessLicense.FileContent != null)
                    {
                        IImageFormatProvider providerByExtension = ImageFormatProviderManager.GetFormatProviderByExtension(
                                Path.GetExtension(_businessLicense.FileName));
                        if (providerByExtension == null)
                        {
                            MessageAlert("不支持文件格式！");
                        }
                        else
                        {
                            Image = providerByExtension.Import(BusinessLicense.FileContent);
                        }
                    }
                    else
                    {
                        Image = null;
                    }
                }
                AddDocumentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => BusinessLicense);
            }
        }
        #endregion

        #region 图片缩放
        public Dictionary<double, string> Percents
        {
            get
            {
                return new Dictionary<double, string>
                         {
                             {0,"适应"},
                             {0.1,"10%"},
                             {0.25,"25%"},
                             {0.5,"50%"},
                             {1,"100%"},
                             {1.5,"150%"},
                             {2,"200%"},
                             {5,"500%"},
                         };
            }
        }

        private double _percent;
        public double Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                RaisePropertyChanged("Percent");
            }
        }

        private RadBitmap _image;
        public RadBitmap Image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged(() => Image);
            }
        }
        #endregion
        #endregion

        #endregion

        #region 操作

        #region 增加经营证照

        /// <summary>
        ///     增加经营证照
        /// </summary>
        public DelegateCommand<object> AddBusinessLicenseCommand { get; set; }

        protected virtual void OnAddBusinessLicense(object obj)
        {
            BusinessLicense = new BusinessLicenseDTO
            {
                Id = RandomHelper.Next(),
                IssuedDate = DateTime.Now,
                ExpireDate = DateTime.Now
            };
            BusinessLicenses.AddNew(BusinessLicense);
            Image = null;
        }

        protected virtual bool CanAddBusinessLicense(object obj)
        {
            return true;
        }

        #endregion

        #region 移除经营证照

        /// <summary>
        ///     移除经营证照
        /// </summary>
        public DelegateCommand<object> RemoveBusinessLicenseCommand { get; private set; }

        protected virtual void OnRemoveBusinessLicense(object obj)
        {
            if (BusinessLicense == null)
            {
                MessageAlert("请选择一条经营证照！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                BusinessLicenses.Remove(BusinessLicense);
            });
        }

        protected virtual bool CanRemoveBusinessLicense(object obj)
        {
            return true;
        }

        #endregion

        #region 打开文档
        public DelegateCommand<object> AddDocumentCommand { get; set; }

        private void AddDocument(object sender)
        {
            try
            {
                var openFileDialog = new OpenFileDialog { Filter = "PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp" };//暂不支持 JPG Files (*.jpg, *.jpeg)|*.jpg;*.jpeg
                if (openFileDialog.ShowDialog() == true)
                {
                    var stream = (Stream)openFileDialog.File.OpenRead();
                    IImageFormatProvider providerByExtension = ImageFormatProviderManager.GetFormatProviderByExtension(openFileDialog.File.Extension);
                    if (providerByExtension == null)
                    {
                        MessageAlert("不支持文件格式！");
                    }
                    else
                    {
                        BusinessLicense.FileName = openFileDialog.File.Name;
                        Image = providerByExtension.Import(stream);
                        BusinessLicense.FileContent = providerByExtension.Export(Image);
                    }
                }
            }
            catch (Exception e)
            {
                MessageAlert(e.Message);
            }
        }

        private bool CanAddDocument(object obj)
        {
            return BusinessLicense != null;
        }
        #endregion
        #endregion
    }
}
