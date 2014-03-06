#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/25 16:38:49
// 文件名：CompareScnVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/25 16:38:49
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(CompareScnVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CompareScnVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly FilterDescriptor _descriptor = new FilterDescriptor("CSCNumber", FilterOperator.IsEqualTo, string.Empty);
        [Import]
        public CompareScn CurrentCompareScn;

        [ImportingConstructor]
        public CompareScnVm(IRegionManager regionManager, IPartService service)
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
            ImportMscnListCommand = new DelegateCommand<object>(OnImportMscnList);
            SaveMscnListCommand = new DelegateCommand<object>(OnSaveMscnList);
            CompareMscnListCommand = new DelegateCommand<object>(OnCompareMscnList);
            SelectAircraftCommand = new DelegateCommand<object>(OnSelectAircraft);
            CompareAircraftScnCommand = new DelegateCommand<object>(OnCompareAircraftScn);
            // 创建并注册CollectionView
            Scns = new QueryableDataServiceCollectionView<ScnDTO>(_context, _context.Scns);

            Scns.LoadedData += (o, e) =>
                               {
                                   if (_currentTabName.Equals("Same", StringComparison.OrdinalIgnoreCase))
                                   {
                                       _loadScn = true;
                                       GenerateResult();
                                   }
                                   else
                                   {
                                       GenerateAircraftScn();
                                   }
                               };
            AirBusScns = _service.CreateCollection(_context.AirBusScns);
            AirBusScns.FilterDescriptors.Add(_descriptor);
            AirBusScns.LoadedData += (o, e) =>
                                     {
                                         _loadAirBusScn = true;
                                         GenerateResult();
                                     };
            _service.RegisterCollectionView(AirBusScns);
        }

        #endregion

        #region 数据

        #region 公共属性

        private string _currentTabName = string.Empty;
        #region 批次号
        private string _cscNumber;
        public string CscNumber
        {
            get { return _cscNumber; }
            set
            {
                _cscNumber = value;
                RaisePropertyChanged("CscNumber");
            }
        }
        #endregion

        #region SCN/MSCN
        /// <summary>
        ///     SCN/MSCN集合
        /// </summary>
        public QueryableDataServiceCollectionView<ScnDTO> Scns { get; set; }
        private bool _loadScn;
        /// <summary>
        ///     选中的SCN/MSCN
        /// </summary>
        private ScnDTO _scn;
        public ScnDTO Scn
        {
            get { return _scn; }
            set
            {
                if (_scn != value)
                {
                    _scn = value;
                    RaisePropertyChanged(() => Scn);
                }
            }
        }
        #endregion

        #region AirBusScn
        /// <summary>
        ///     AirBusScn集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirBusScnDTO> AirBusScns { get; set; }
        private bool _loadAirBusScn;

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
            // 将CollectionView的AutoLoad属性设为True
        }

        #endregion

        #endregion

        #region 操作

        #region 导入SCN/MSCN清单
        /// <summary>
        ///     导入SCN/MSCN清单
        /// </summary>
        public DelegateCommand<object> ImportMscnListCommand { get; set; }
        private readonly List<AirBusMscn> _airBusMscns = new List<AirBusMscn>();
        protected void OnImportMscnList(object obj)
        {
            if (string.IsNullOrEmpty(CscNumber))
            {
                MessageAlert("请输入批次号！");
                return;
            }
            var openFileDialog = new OpenFileDialog { Filter = "可用文档|*.csv" };
            if (openFileDialog.ShowDialog() == true)
            {
                using (var reader = openFileDialog.File.OpenText())
                {
                    while (!reader.EndOfStream)
                    {
                        var msg = reader.ReadLine();
                        if (msg.Contains("CSC"))
                        {
                            var results = msg.Split(',');
                            _airBusMscns.Add(new AirBusMscn
                                            {
                                                CSCNumber = CscNumber,
                                                ScnNumber = results[1],
                                                Title = results[2],
                                                Status = results[3],
                                                ModNumber = results[4]
                                            });
                        }
                    }
                    MessageAlert("导入成功。");
                }
            }
        }

        #endregion

        #region 保存SCN/MSCN清单
        /// <summary>
        ///     保存SCN/MSCN清单
        /// </summary>
        public DelegateCommand<object> SaveMscnListCommand { get; set; }
        protected void OnSaveMscnList(object obj)
        {
            _airBusMscns.ForEach(p => AirBusScns.AddNew(new AirBusScnDTO
            {
                Id = RandomHelper.Next(),
                CSCNumber = p.CSCNumber,
                ModNumber = p.ModNumber,
                ScnNumber = p.ScnNumber,
                Title = p.Title

            }));
            AirBusScns.SubmitChanges();
        }

        #endregion

        #region 同批次对比SCN/MSCN清单
        /// <summary>
        ///     同批次对比SCN/MSCN清单
        /// </summary>
        public DelegateCommand<object> CompareMscnListCommand { get; set; }
        protected void OnCompareMscnList(object obj)
        {
            if (string.IsNullOrEmpty(CscNumber))
            {
                MessageAlert("请输入批次号！");
                return;
            }

            Scns.FilterDescriptors.Add(_descriptor);
            _currentTabName = "Same";
            _loadScn = false;
            _loadAirBusScn = false;
            IsBusy = true;
            _descriptor.Value = CscNumber;
            Scns.Load(true);
            AirBusScns.Load(true);
        }

        #endregion

        #region 生成对比结果

        private List<CompareMscn> _compareMscns;
        public List<CompareMscn> CompareMscns
        {
            get { return _compareMscns; }
            set
            {
                _compareMscns = value;
                RaisePropertyChanged(() => CompareMscns);
            }
        }
        private void GenerateResult()
        {
            if (_loadScn && _loadAirBusScn)
            {
                var tempCompareMscns = new List<CompareMscn>();
                var tempSysScns = Scns.ToList();
                var tempAirBusScns = new List<AirBusScnDTO>();
                var importAirBusScns = new List<AirBusMscn>();
                _airBusMscns.ForEach(importAirBusScns.Add);
                AirBusScns.ToList().ForEach(p =>
                {
                    var temp = importAirBusScns.FirstOrDefault(
                            t => t.CSCNumber.Equals(p.CSCNumber, StringComparison.OrdinalIgnoreCase) &&
                                t.ScnNumber.Equals(p.ScnNumber, StringComparison.OrdinalIgnoreCase));
                    if (temp != null)
                    {
                        tempAirBusScns.Add(new AirBusScnDTO
                        {
                            CSCNumber = temp.CSCNumber,
                            ScnNumber = temp.ScnNumber,
                            Title = temp.Title,
                            ModNumber = temp.ModNumber
                        });
                        importAirBusScns.Remove(temp);
                    }
                    else
                    {
                        tempAirBusScns.Add(new AirBusScnDTO
                        {
                            CSCNumber = p.CSCNumber,
                            ScnNumber = p.ScnNumber,
                            Title = p.Title,
                            ModNumber = p.ModNumber
                        });
                    }
                });
                importAirBusScns.ForEach(p => tempAirBusScns.Add(new AirBusScnDTO
                                                                 {
                                                                     CSCNumber = p.CSCNumber,
                                                                     ScnNumber = p.ScnNumber,
                                                                     Title = p.Title,
                                                                     ModNumber = p.ModNumber
                                                                 }));
                tempSysScns.ForEach(p =>
                                    {
                                        var temp = tempAirBusScns.FirstOrDefault(
                                                t => t.CSCNumber.Equals(p.CSCNumber, StringComparison.OrdinalIgnoreCase) &&
                                                    t.ScnNumber.Equals(p.ScnNumber, StringComparison.OrdinalIgnoreCase));
                                        if (temp != null)
                                        {
                                            //tempCompareMscns.Add(new CompareMscn
                                            //                 {
                                            //                     SysModNo = p.ModNumber,
                                            //                     SysMscnNo = p.ScnNumber,
                                            //                     SysTitle = p.Title,
                                            //                     AirBusModNo = temp.ModNumber,
                                            //                     AirBusMscnNo = temp.ScnNumber,
                                            //                     AirBusTitle = temp.Title
                                            //                 });
                                            tempAirBusScns.Remove(temp);
                                        }
                                        else
                                        {
                                            tempCompareMscns.Add(new CompareMscn
                                            {
                                                SysModNo = p.ModNumber,
                                                SysMscnNo = p.ScnNumber,
                                                SysTitle = p.Title
                                            });
                                        }
                                    });
                tempAirBusScns.ForEach(p => tempCompareMscns.Add(new CompareMscn
                                                             {
                                                                 AirBusModNo = p.ModNumber,
                                                                 AirBusMscnNo = p.ScnNumber,
                                                                 AirBusTitle = p.Title
                                                             }));
                CompareMscns = tempCompareMscns;
                IsBusy = false;
            }
        }
        #endregion

        #region 选择飞机
        private readonly List<ContractAircraftDTO> _aircrafts = new List<ContractAircraftDTO>();
        /// <summary>
        ///     选择飞机
        /// </summary>
        public DelegateCommand<object> SelectAircraftCommand { get; set; }
        protected void OnSelectAircraft(object obj)
        {
            var aircrafts = new SelectAircrafts();
            aircrafts.ViewModel.InitData(_aircrafts);
            aircrafts.ShowDialog();
        }
        #endregion

        #region 不同飞机对比SCN/MSCN清单
        /// <summary>
        ///     不同飞机对比SCN/MSCN清单
        /// </summary>
        public DelegateCommand<object> CompareAircraftScnCommand { get; set; }
        protected void OnCompareAircraftScn(object obj)
        {
            if (_aircrafts.Count == 0)
            {
                MessageAlert("请选择飞机！");
                return;
            }
            _currentTabName = "Different";
            Scns.FilterDescriptors.Clear();
            Scns.Load(true);
        }

        private readonly List<GridViewDataColumn> _extendColumns = new List<GridViewDataColumn>();
        private List<AircraftScn> _compareAircraftScns;
        public List<AircraftScn> CompareAircraftScns
        {
            get { return _compareAircraftScns; }
            set
            {
                _compareAircraftScns = value;
                RaisePropertyChanged(() => CompareAircraftScns);
            }
        }
        private void GenerateAircraftScn()
        {
            CurrentCompareScn.AircraftScnList.Columns.RemoveItems(_extendColumns);
            _extendColumns.Clear();
            int i = 0;
            _aircrafts.ForEach(p =>
                               {
                                   i++;
                                   if (i > 5)
                                   {
                                       return;
                                   }
                                   var tempColumn = new GridViewDataColumn
                                                    {
                                                        Header = p.RankNumber,
                                                        DataMemberBinding = new Binding("Aircraft" + i),

                                                    };
                                   _extendColumns.Add(tempColumn);
                                   CurrentCompareScn.AircraftScnList.Columns.Add(tempColumn);
                               });
            var tempAircraftScns = new List<AircraftScn>();
            Scns.ToList().ForEach(p =>
                                  {
                                      var aircraftScn = new AircraftScn
                                                        {
                                                            ModNumber = p.ModNumber,
                                                            ScnNumber = p.ScnNumber,
                                                            Title = p.Title
                                                        };
                                      p.ApplicableAircrafts.ToList().ForEach(t =>
                                                                             {
                                                                                 var aircraft = _aircrafts.FirstOrDefault(o => o.Id == t.ContractAircraftId);
                                                                                 if (aircraft != null)
                                                                                 {
                                                                                     int index = _aircrafts.IndexOf(aircraft);
                                                                                     switch (index)
                                                                                     {
                                                                                         case 0:
                                                                                             aircraftScn.Aircraft1 = "Y";break;
                                                                                         case 1:
                                                                                              aircraftScn.Aircraft2 = "Y";break;
                                                                                         case 2:
                                                                                              aircraftScn.Aircraft3 = "Y";break;
                                                                                         case 3:
                                                                                              aircraftScn.Aircraft4 = "Y";break;
                                                                                         case 4:
                                                                                              aircraftScn.Aircraft5 = "Y";break;
                                                                                     }
                                                                                 }
                                                                             });
                                      tempAircraftScns.Add(aircraftScn);
                                  });
            CompareAircraftScns = tempAircraftScns;
        }
        #endregion
        #endregion
    }

    public class AirBusMscn
    {
        public string CSCNumber;
        public string ScnNumber;
        public string Title;
        public string Status;
        public string ModNumber;
    }

    public class CompareMscn
    {
        public string SysMscnNo { get; set; }
        public string SysTitle { get; set; }
        public string SysStatus { get; set; }
        public string SysModNo { get; set; }
        public string AirBusMscnNo { get; set; }
        public string AirBusTitle { get; set; }
        public string AirBusStatus { get; set; }
        public string AirBusModNo { get; set; }
    }

    public class AircraftScn
    {
        public string CSCNumber { get; set; }
        public string ScnNumber { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string ModNumber { get; set; }
        public string Aircraft1 { get; set; }
        public string Aircraft2 { get; set; }
        public string Aircraft3 { get; set; }
        public string Aircraft4 { get; set; }
        public string Aircraft5 { get; set; }
    }
}
