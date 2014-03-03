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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
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
        private FilterDescriptor _descriptor;

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
            // 创建并注册CollectionView
            Scns = new QueryableDataServiceCollectionView<ScnDTO>(_context, _context.Scns);
            _descriptor = new FilterDescriptor("CSCNumber", FilterOperator.IsEqualTo, string.Empty);
            Scns.FilterDescriptors.Add(_descriptor);
            Scns.LoadedData += (o, e) =>
                               {
                                   _loadScn = true;
                                   GenerateResult();
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
        private List<AirBusMscn> AirBusMscns = new List<AirBusMscn>();
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
                            AirBusMscns.Add(new AirBusMscn
                                            {
                                                CSCNumber = CscNumber,
                                                ScnNumber = results[1],
                                                Title = results[2],
                                                Status = results[3],
                                                ModNumber = results[4]
                                            });
                        }
                    }
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
            AirBusMscns.ForEach(p => AirBusScns.AddNew(new AirBusScnDTO
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

        #region 对比SCN/MSCN清单
        /// <summary>
        ///     对比SCN/MSCN清单
        /// </summary>
        public DelegateCommand<object> CompareMscnListCommand { get; set; }
        protected void OnCompareMscnList(object obj)
        {
            if (string.IsNullOrEmpty(CscNumber))
            {
                MessageAlert("请输入批次号！");
                return;
            }
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
                AirBusMscns.ForEach(importAirBusScns.Add);
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
                                            tempCompareMscns.Add(new CompareMscn
                                                             {
                                                                 SysModNo = p.ModNumber,
                                                                 SysMscnNo = p.ScnNumber,
                                                                 SysTitle = p.Title,
                                                                 AirBusModNo = temp.ModNumber,
                                                                 AirBusMscnNo = temp.ScnNumber,
                                                                 AirBusTitle = temp.Title
                                                             });
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
}
