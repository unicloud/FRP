#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 9:08:13
// 文件名：InSeatAssemblyQueryVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export(typeof (InSeatAssemblyQueryVm))]
    public class InSeatAssemblyQueryVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public InSeatAssemblyQueryVm(IPartService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            ContractAircrafts = new QueryableDataServiceCollectionView<ContractAircraftDTO>(_context,
                _context.ContractAircrafts);
            ContractAircrafts.FilterDescriptors.Add(new FilterDescriptor("SerialNumber", FilterOperator.IsNotEqualTo,
                null));

            QueryCommand = new DelegateCommand<object>(OnQuery);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 合同飞机集合

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

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
            ContractAircrafts.Load(true);
        }

        #region 业务

        #region 界面所选合同飞机

        private ContractAircraftDTO _selContractAircraft;


        /// <summary>
        ///     界面所选合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return _selContractAircraft; }
            set
            {
                if (_selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    RaisePropertyChanged(() => _selContractAircraft);
                }
            }
        }

        #endregion

        #region 界面所选日期

        private DateTime _queryDate = DateTime.Now;


        /// <summary>
        ///     界面所选日期
        /// </summary>
        public DateTime QueryDate
        {
            get { return _queryDate; }
            set
            {
                if (_queryDate != value)
                {
                    _queryDate = value;
                    RaisePropertyChanged(() => QueryDate);
                }
            }
        }

        #endregion

        #region 功能构型集合

        private readonly List<AcConfigDTO> _curLeftAcConfigs = new List<AcConfigDTO>();
        private readonly List<AcConfigDTO> _curRightAcConfigs = new List<AcConfigDTO>();
        private List<AcConfigDTO> _leftViewAcConfigs = new List<AcConfigDTO>();

        private List<AcConfigDTO> _rightViewAcConfigs = new List<AcConfigDTO>();

        /// <summary>
        ///     左边功能构型集合
        /// </summary>
        public List<AcConfigDTO> LeftViewAcConfigs
        {
            get { return _leftViewAcConfigs; }
            set
            {
                if (_leftViewAcConfigs != value)
                {
                    _leftViewAcConfigs = value;
                    RaisePropertyChanged(() => LeftViewAcConfigs);
                }
            }
        }

        /// <summary>
        ///     右边功能构型集合
        /// </summary>
        public List<AcConfigDTO> RightViewAcConfigs
        {
            get { return _rightViewAcConfigs; }
            set
            {
                if (_rightViewAcConfigs != value)
                {
                    _rightViewAcConfigs = value;
                    RaisePropertyChanged(() => RightViewAcConfigs);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        /// <summary>
        ///     创建查询路径
        /// </summary>
        /// <param name="contractAircraftId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private Uri CreateAcConfigQueryPath(int contractAircraftId, string date)
        {
            return new Uri(string.Format("QueryAcConfigs?contractAircraftId={0}&date='{1}'", contractAircraftId, date),
                UriKind.Relative);
        }

        private void LoadAcConfigs(Uri path)
        {
            //查询
            _context.BeginExecute<AcConfigDTO>(path,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PartData;
                    try
                    {
                        if (context != null)
                        {
                            //if (_loadForLeft)
                            //{
                            //    _curLeftAcConfigs = new List<AcConfigDTO>();
                            //    LeftViewAcConfigs = new List<AcConfigDTO>();
                            //    _curLeftAcConfigs = context.EndExecute<AcConfigDTO>(result).ToList();
                            //    //重组飞机功能构型
                            //    _curLeftAcConfigs.ToList().ForEach(p =>
                            //    {
                            //        p.Color = "Blue";
                            //        GenerateLeftAcConfigStructure(p);
                            //    });

                            //    //得到需要界面展示的功能构型集合
                            //    List<AcConfigDTO> acs = _curLeftAcConfigs.Where(p => p.ParentId == null).ToList();
                            //    LeftViewAcConfigs = acs;
                            //}
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        var response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }

        #region 重组成有层次结构的构型

        public void GenerateLeftAcConfigStructure(AcConfigDTO acConfig)
        {
            acConfig.SubAcConfigs.Clear();
            var temp =
                _curLeftAcConfigs.Where(p => p.ParentId == acConfig.Id).ToList().OrderBy(p => p.Position);
            acConfig.SubAcConfigs.AddRange(temp);
            foreach (var subItem in acConfig.SubAcConfigs)
            {
                GenerateLeftAcConfigStructure(subItem);
            }
        }

        public void GenerateRightAcConfigStructure(AcConfigDTO acConfig)
        {
            acConfig.SubAcConfigs.Clear();
            var temp =
                _curRightAcConfigs.Where(p => p.ParentId == acConfig.Id).ToList().OrderBy(p => p.Position);
            acConfig.SubAcConfigs.AddRange(temp);
            foreach (var subItem in acConfig.SubAcConfigs)
            {
                GenerateRightAcConfigStructure(subItem);
            }
        }

        #endregion

        #region 比较构型差异

        /// <summary>
        ///     比较构型差异
        /// </summary>
        public DelegateCommand<object> QueryCommand { get; private set; }

        private void OnQuery(object obj)
        {
            if (_curLeftAcConfigs != null && _curRightAcConfigs != null)
            {
                _curLeftAcConfigs.ForEach(p =>
                {
                    if (!_curRightAcConfigs.Any(l => l.ItemId == p.ItemId && l.Position == p.Position))
                        p.Color = "Green";
                });
                _curRightAcConfigs.ForEach(p =>
                {
                    if (!_curLeftAcConfigs.Any(l => l.ItemId == p.ItemId && l.Position == p.Position))
                        p.Color = "Red";
                });
            }
        }

        #endregion

        #endregion
    }
}