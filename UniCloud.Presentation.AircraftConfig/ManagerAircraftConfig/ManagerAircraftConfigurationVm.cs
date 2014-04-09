#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 15:16:04
// 文件名：ManagerAircraftConfigurationVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 15:16:04
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using Telerik.Windows.Media.Imaging;
using Telerik.Windows.Media.Imaging.FormatProviders;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.AircraftConfig;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig
{
    [Export(typeof(ManagerAircraftConfigurationVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerAircraftConfigurationVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly AircraftConfigData _context;
        private readonly IRegionManager _regionManager;
        private readonly IAircraftConfigService _service;

        [ImportingConstructor]
        public ManagerAircraftConfigurationVm(IRegionManager regionManager, IAircraftConfigService service)
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
            AddAircraftConfigCommand = new DelegateCommand<object>(OnAddAircraftConfig, CanAddAircraftConfig);
            RemoveAircraftConfigCommand = new DelegateCommand<object>(OnRemoveAircraftConfig, CanRemoveAircraftConfig);
            AddCabinCommand = new DelegateCommand<object>(OnAddCabin, CanAddCabin);
            RemoveCabinCommand = new DelegateCommand<object>(OnRemoveCabin, CanRemoveCabin);
            AddDocumentCommand = new DelegateCommand<object>(AddDocument, CanAddDocument);
            //创建并注册CollectionView
            AircraftTypes = _service.CreateCollection(_context.AircraftTypes);
            AircraftSerieses = _service.CreateCollection(_context.AircraftSeries);
            AircraftConfigurations = _service.CreateCollection(_context.AircraftConfigurations, o => o.AircraftCabins);
            AircraftConfigurations.PageSize = 6;
            _service.RegisterCollectionView(AircraftConfigurations);
            var baseManagementService = ServiceLocator.Current.GetInstance<IBaseManagementService>();
            AircraftCabinTypes = _service.CreateCollection(baseManagementService.Context.AircraftCabinTypes);
        }

        #endregion

        #region 数据

        #region 公共属性
        /// <summary>
        ///    飞机舱位
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCabinTypeDTO> AircraftCabinTypes { get; set; }

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
            if (!AircraftConfigurations.AutoLoad)
                AircraftConfigurations.AutoLoad = true;
            AircraftTypes.Load(true);
            AircraftSerieses.Load(true);
            AircraftCabinTypes.Load(true);
        }

        #region 飞机配置
        public QueryableDataServiceCollectionView<AircraftConfigurationDTO> AircraftConfigurations { get; set; }
        private AircraftConfigurationDTO _aircraftConfiguration;
        public AircraftConfigurationDTO AircraftConfiguration
        {
            get { return _aircraftConfiguration; }
            set
            {
                _aircraftConfiguration = value;
                if (_aircraftConfiguration != null)
                {
                    if (_aircraftConfiguration.FileContent != null)
                    {
                        IImageFormatProvider providerByExtension = ImageFormatProviderManager.GetFormatProviderByExtension(
                                Path.GetExtension(_aircraftConfiguration.FileName));
                        if (providerByExtension == null)
                        {
                            MessageAlert("不支持文件格式！");
                        }
                        else
                        {
                            Image = providerByExtension.Import(AircraftConfiguration.FileContent);
                        }
                    }
                    else
                    {
                        Image = null;
                    }
                }
                AddDocumentCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged(() => AircraftConfiguration);
            }
        }
        #endregion

        #region 舱位
        private AircraftCabinDTO _aircraftCabin;
        public AircraftCabinDTO AircraftCabin
        {
            get { return _aircraftCabin; }
            set
            {
                _aircraftCabin = value;
                RaisePropertyChanged(() => AircraftCabin);
            }
        }
        #endregion

        #region 机型
        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }
        #endregion

        #region 飞机系列
        /// <summary>
        /// 飞机系列
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftSeriesDTO> AircraftSerieses { get; set; }
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
        #region 创建新飞机配置
        /// <summary>
        ///     创建新飞机配置
        /// </summary>
        public DelegateCommand<object> AddAircraftConfigCommand { get; set; }

        protected void OnAddAircraftConfig(object obj)
        {
            AircraftConfiguration = new AircraftConfigurationDTO
            {
                Id = RandomHelper.Next(),
            };
            var aircraftTypeDto = AircraftTypes.FirstOrDefault();
            if (aircraftTypeDto != null)
            { 
                AircraftConfiguration.AircraftTypeId = aircraftTypeDto.AircraftTypeId;
                AircraftConfiguration.AircraftSeriesId = aircraftTypeDto.AircraftSeriesId;
            }
            AircraftConfigurations.AddNew(AircraftConfiguration);
        }

        protected bool CanAddAircraftConfig(object obj)
        {
            return true;
        }

        #endregion

        #region 删除飞机配置
        /// <summary>
        ///     删除飞机配置
        /// </summary>
        public DelegateCommand<object> RemoveAircraftConfigCommand { get; set; }

        protected void OnRemoveAircraftConfig(object obj)
        {
            if (AircraftConfiguration == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AircraftConfigurations.Remove(AircraftConfiguration);
            });
        }

        protected bool CanRemoveAircraftConfig(object obj)
        {
            return true;
        }

        #endregion

        #region 增加舱位
        /// <summary>
        ///     增加舱位
        /// </summary>
        public DelegateCommand<object> AddCabinCommand { get; set; }

        protected void OnAddCabin(object obj)
        {
            if (AircraftConfiguration == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }

            var aircraftCabinTypeDto = AircraftCabinTypes.FirstOrDefault();
            AircraftCabin = new AircraftCabinDTO
                            {
                                Id = RandomHelper.Next(),
                            };
            if (aircraftCabinTypeDto != null)
                AircraftCabin.AircraftCabinTypeId = aircraftCabinTypeDto.Id;
            AircraftConfiguration.AircraftCabins.Add(AircraftCabin);
        }

        protected bool CanAddCabin(object obj)
        {
            return true;
        }

        #endregion

        #region 移除舱位
        /// <summary>
        ///     移除舱位
        /// </summary>
        public DelegateCommand<object> RemoveCabinCommand { get; set; }

        protected void OnRemoveCabin(object obj)
        {
            if (AircraftCabin == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AircraftConfiguration.AircraftCabins.Remove(AircraftCabin);
            });
        }

        protected bool CanRemoveCabin(object obj)
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
                        AircraftConfiguration.FileName = openFileDialog.File.Name;
                        Image = providerByExtension.Import(stream);
                        AircraftConfiguration.FileContent = providerByExtension.Export(Image);
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
            return AircraftConfiguration != null;
        }
        #endregion

        #region Combobox SelectedChanged
        public void SelectedChanged(object comboboxSelectedItem)
        {
            if (comboboxSelectedItem is AircraftTypeDTO)
            {
                var temp = comboboxSelectedItem as AircraftTypeDTO;
                AircraftConfiguration.AircraftSeriesId = temp.AircraftSeriesId;
            }
        }
        #endregion
        #region 重载操作

        #endregion

        #endregion
    }
}
