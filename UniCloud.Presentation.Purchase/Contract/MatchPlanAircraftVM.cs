#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:57
// 方案：FRP
// 项目：Purchase
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (MatchPlanAircraftVM))]
    public class MatchPlanAircraftVM : NotificationObject
    {
        #region 声明、初始化

        private Action<PlanAircraftDTO> _winClosed;

        public MatchPlanAircraftVM()
        {
            OkCommand = new DelegateCommand<object>(OnOk, CanOk);
            CancelCommand = new DelegateCommand<object>(OnCancel, CanCancel);
        }

        #endregion

        #region 数据

        #region 公共属性

        #endregion

        #region 加载数据

        public void InitData(Action<PlanAircraftDTO> callback, IEnumerable<PlanAircraftDTO> planAircraftDtos)
        {
            _winClosed = callback;
            ViewPlanAircraftDTO = planAircraftDtos.ToList();
            RaisePropertyChanged(() => ViewPlanAircraftDTO);
        }

        #region 计划飞机

        private PlanAircraftDTO _selPlanAircraftDTO;

        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public List<PlanAircraftDTO> ViewPlanAircraftDTO { get; set; }

        /// <summary>
        ///     选中的计划飞机
        /// </summary>
        public PlanAircraftDTO SelPlanAircraftDTO
        {
            get { return _selPlanAircraftDTO; }
            set
            {
                if (_selPlanAircraftDTO == value) return;
                _selPlanAircraftDTO = value;
                RaisePropertyChanged(() => SelPlanAircraftDTO);
                // 刷新按钮状态
                RefreshCommandState();
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #region 刷新按钮状态

        private void RefreshCommandState()
        {
            OkCommand.RaiseCanExecuteChanged();
            CancelCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #endregion

        #region 确认

        /// <summary>
        ///     确认
        /// </summary>
        public DelegateCommand<object> OkCommand { get; private set; }

        private void OnOk(object obj)
        {
            _winClosed(_selPlanAircraftDTO);
        }

        private bool CanOk(object obj)
        {
            return _selPlanAircraftDTO != null;
        }

        #endregion

        #region 取消

        /// <summary>
        ///     取消
        /// </summary>
        public DelegateCommand<object> CancelCommand { get; private set; }

        private void OnCancel(object obj)
        {
            _winClosed(null);
        }

        private bool CanCancel(object obj)
        {
            return _selPlanAircraftDTO != null;
        }

        #endregion

        #endregion
    }
}