#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/5 18:55:20
// 文件名：SelectAircraftsVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/5 18:55:20
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(SelectAircraftsVm))]
    public class SelectAircraftsVm : ViewModelBase
    {
        #region 初始化
        public SelectAircrafts SelectAircraftsWindow;
        private ScnDTO _scn;
        public List<ContractAircraftDTO> Aircrafts;
        private readonly FilterDescriptor _descriptor = new FilterDescriptor("CSCNumber", FilterOperator.IsEqualTo, string.Empty);
        public SelectAircraftsVm(SelectAircrafts selectAircraftsWindow, IPartService service)
            : base(service)
        {
            SelectAircraftsWindow = selectAircraftsWindow;
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            #region 飞机
            AircraftQueries = new QueryableDataServiceCollectionView<ContractAircraftDTO>(service.Context, service.Context.ContractAircrafts);
            AircraftQueries.FilterDescriptors.Add(_descriptor);
            AircraftQueries.LoadedData += (e, o) =>
            {
                AircraftList = new ObservableCollection<ContractAircraftDTO>();
                AircraftQueries.ToList().ForEach(AircraftList.Add);
                SelectAircraftList = new ObservableCollection<ContractAircraftDTO>();

                if (_scn != null)
                {
                    _scn.ApplicableAircrafts.ToList().ForEach(p => SelectAircraftList.Add(AircraftList.FirstOrDefault(t => t.Id == p.ContractAircraftId)));
                }
                else
                {
                    Aircrafts.ForEach(p => SelectAircraftList.Add(AircraftList.FirstOrDefault(t => t.Id == p.Id)));
                }
            };
            #endregion
        }

        public void InitData(string cscNumber, List<ContractAircraftDTO> aircrafts)
        {
            _descriptor.Value = cscNumber;
            Aircrafts = aircrafts;
            AircraftQueries.Load(true);
        }

        public void InitData(string cscNumber, ScnDTO scn)
        {
            _scn = scn;
            _descriptor.Value = cscNumber;
            AircraftQueries.Load(true);
        }
        #endregion

        #region 公共属性
        #region 飞机
        public QueryableDataServiceCollectionView<ContractAircraftDTO> AircraftQueries { get; set; }
        private ObservableCollection<ContractAircraftDTO> _aircraftList;
        public ObservableCollection<ContractAircraftDTO> AircraftList
        {
            get
            {
                return _aircraftList;
            }
            set
            {
                _aircraftList = value;
                RaisePropertyChanged(() => AircraftList);
            }
        }
        public ObservableCollection<ContractAircraftDTO> SelectAircraftList { get; set; }
        #endregion
        #endregion

        #region 操作
        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            SelectAircraftsWindow.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            if (_scn != null)
            {
                SelectAircraftList.ToList().ForEach(p =>
                {
                    if (_scn.ApplicableAircrafts.All(t => t.ContractAircraftId != p.Id))
                    {
                        var applicableAircraft = new ApplicableAircraftDTO
                        {
                            Id = RandomHelper.Next(),
                            CompleteDate = DateTime.Now,
                            ContractAircraftId = p.Id,
                        };
                        _scn.ApplicableAircrafts.Add(applicableAircraft);
                    }
                });
                for (int i = _scn.ApplicableAircrafts.Count - 1; i >= 0; i--)
                {
                    var temp = _scn.ApplicableAircrafts[i];
                    if (SelectAircraftList.Count > 0 && SelectAircraftList.All(p => p.Id != temp.ContractAircraftId))
                    {
                        _scn.ApplicableAircrafts.Remove(temp);
                    }
                }
                CaculateApplicableAircraftCost();
            }
            else
            {
                SelectAircraftList.ToList().ForEach(p =>
                {
                    if (Aircrafts.All(t => t.Id != p.Id))
                    {
                        Aircrafts.Add(p);
                    }
                });
                for (int i = Aircrafts.Count - 1; i >= 0; i--)
                {
                    var temp = Aircrafts[i];
                    if (SelectAircraftList.Count > 0 && SelectAircraftList.All(p => p.Id != temp.Id))
                    {
                        Aircrafts.Remove(temp);
                    }
                }
            }
            SelectAircraftsWindow.Close();
        }

        #region 计算适用飞机费用
        private void CaculateApplicableAircraftCost()
        {
            if (_scn.ApplicableAircrafts != null && _scn.ApplicableAircrafts.Count > 0)
            {
                if (_scn.ScnType == 0)
                {
                    var average = _scn.Cost / _scn.ApplicableAircrafts.Count;
                    _scn.ApplicableAircrafts.ToList().ForEach(p => p.Cost = average);
                }
                else
                {
                    var first = _scn.ApplicableAircrafts.First();
                    first.Cost = _scn.Cost;
                    _scn.ApplicableAircrafts.ToList().ForEach(p => { if (p.Id != first.Id) p.Cost = 0; });
                }
            }
        }
        #endregion

        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion
        public override void LoadData()
        {
        }
        #endregion
    }
}
