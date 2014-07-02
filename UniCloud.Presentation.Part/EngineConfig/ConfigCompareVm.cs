#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 18:06:46
// 文件名：ConfigCompareVm
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
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export(typeof (ConfigCompareVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ConfigCompareVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;

        [ImportingConstructor]
        public ConfigCompareVm(IPartService service)
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

            CompareCommand = new DelegateCommand<object>(OnCompare);
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

        /// <summary>
        ///     装机位置
        /// </summary>
        public Dictionary<int, Position> Positions
        {
            get
            {
                return Enum.GetValues(typeof (Position))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (Position) value);
            }
        }

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

        private ContractAircraftDTO _leftContractAircraft;
        private ContractAircraftDTO _rightContractAircraft;


        /// <summary>
        ///     界面左边所选合同飞机
        /// </summary>
        public ContractAircraftDTO LeftContractAircraft
        {
            get { return _leftContractAircraft; }
            set
            {
                if (_leftContractAircraft != value)
                {
                    _leftContractAircraft = value;
                    if (value != null)
                    {
                        LoadLeftAcConfigs();
                    }
                    RaisePropertyChanged(() => LeftContractAircraft);
                }
            }
        }

        /// <summary>
        ///     界面右边所选合同飞机
        /// </summary>
        public ContractAircraftDTO RightContractAircraft
        {
            get { return _rightContractAircraft; }
            set
            {
                if (_rightContractAircraft != value)
                {
                    _rightContractAircraft = value;
                    if (value != null)
                    {
                        LoadRightAcConfigs();
                    }
                    RaisePropertyChanged(() => RightContractAircraft);
                }
            }
        }

        #endregion

        #region 界面所选日期

        private DateTime _leftDate = DateTime.Now;
        private DateTime _rightDate = DateTime.Now;


        /// <summary>
        ///     界面左边所选日期
        /// </summary>
        public DateTime LeftDate
        {
            get { return _leftDate; }
            set
            {
                if (_leftDate != value)
                {
                    _leftDate = value;
                    LoadLeftAcConfigs();
                    RaisePropertyChanged(() => LeftDate);
                }
            }
        }

        /// <summary>
        ///     界面右边所选日期
        /// </summary>
        public DateTime RightDate
        {
            get { return _rightDate; }
            set
            {
                if (_rightDate != value)
                {
                    _rightDate = value;
                    LoadRightAcConfigs();
                    RaisePropertyChanged(() => RightDate);
                }
            }
        }

        #endregion

        #region 功能构型集合

        private List<AcConfigDTO> _curLeftAcConfigs = new List<AcConfigDTO>();
        private List<AcConfigDTO> _curRightAcConfigs = new List<AcConfigDTO>();
        private List<AcConfigDTO> _leftViewAcConfigs = new List<AcConfigDTO>();

        private List<AcConfigDTO> _rightViewAcConfigs = new List<AcConfigDTO>();

        /// <summary>
        ///     左边功能构型集合
        /// </summary>
        public List<AcConfigDTO> LeftViewAcConfigs
        {
            get { return _leftViewAcConfigs; }
            private set
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
            private set
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

        private bool _loadForLeft;
        private bool _loadForRight;

        private void LoadLeftAcConfigs()
        {
            if (LeftContractAircraft != null)
            {
                //加载选择飞机的对应功能构型集合
                var path = CreateAcConfigQueryPath(LeftContractAircraft.Id, LeftDate.ToShortDateString());
                _loadForLeft = true;
                LoadAcConfigs(path);
            }
        }

        private void LoadRightAcConfigs()
        {
            if (RightContractAircraft != null)
            {
                //加载选择飞机的对应功能构型集合
                var path = CreateAcConfigQueryPath(RightContractAircraft.Id, RightDate.ToShortDateString());
                _loadForRight = true;
                LoadAcConfigs(path);
            }
        }

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
                            if (_loadForLeft)
                            {
                                _curLeftAcConfigs = new List<AcConfigDTO>();
                                LeftViewAcConfigs = new List<AcConfigDTO>();
                                _curLeftAcConfigs = context.EndExecute<AcConfigDTO>(result).ToList();
                                //重组飞机功能构型
                                _curLeftAcConfigs.ToList().ForEach(p =>
                                {
                                    p.Color = "Blue";
                                    GenerateLeftAcConfigStructure(p);
                                });

                                //得到需要界面展示的功能构型集合
                                var acs = _curLeftAcConfigs.Where(p => p.ParentId == null).ToList();
                                LeftViewAcConfigs = acs;
                            }
                            if (_loadForRight)
                            {
                                _curRightAcConfigs = new List<AcConfigDTO>();
                                RightViewAcConfigs = new List<AcConfigDTO>();
                                _curRightAcConfigs = context.EndExecute<AcConfigDTO>(result).ToList();
                                //重组飞机功能构型
                                _curRightAcConfigs.ToList().ForEach(p =>
                                {
                                    p.Color = "Blue";
                                    GenerateRightAcConfigStructure(p);
                                });

                                //得到需要界面展示的功能构型集合
                                var acs = _curRightAcConfigs.Where(p => p.ParentId == null).ToList();

                                RightViewAcConfigs = acs;
                            }
                            _loadForLeft = false;
                            _loadForRight = false;
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
        public DelegateCommand<object> CompareCommand { get; private set; }

        private void OnCompare(object obj)
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