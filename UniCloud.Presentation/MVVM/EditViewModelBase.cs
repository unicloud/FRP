#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/15，14:11
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls.DataServices;
using UniCloud.Presentation.Service;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public abstract class EditViewModelBase : ViewModelBase, IConfirmNavigationRequest
    {
        private readonly IService _service;

        protected EditViewModelBase(IService service)
        {
            _service = service;
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            AbortCommand = new DelegateCommand<object>(OnAbort, CanAbort);
            if (_service != null)
            {
                _service.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName == "HasChanges")
                    {
                        SaveCommand.RaiseCanExecuteChanged();
                        AbortCommand.RaiseCanExecuteChanged();
                    }
                };
            }
        }

        #region 保存命令

        public DelegateCommand<object> SaveCommand { get; private set; }

        private void OnSave(object sender)
        {
            if (sender is QueryableDataServiceCollectionViewBase)
            {
                var collectionView = sender as QueryableDataServiceCollectionViewBase;
                if (!OnSaveExecuting(collectionView))
                {
                    return;
                }
                _service.SubmitChanges(collectionView, sm =>
                {
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(collectionView);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(collectionView);
                    }
                    RefreshCommandState();
                });
            }
            else
            {
                _service.SubmitChanges(sm =>
                {
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(sender);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(sender);
                    }
                    RefreshCommandState();
                });
            }
        }

        /// <summary>
        ///     保存成功前执行的操作。
        /// </summary>
        /// <param name="sender"></param>
        protected virtual bool OnSaveExecuting(object sender)
        {
            return true;
        }

        /// <summary>
        ///     保存成功后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSaveSuccess(object sender)
        {
        }

        /// <summary>
        ///     保存失败后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSaveFail(object sender)
        {
        }

        private bool CanSave(object sender)
        {
            return _service != null && _service.HasChanges;
        }

        #endregion

        #region 放弃更改

        public DelegateCommand<object> AbortCommand { get; private set; }

        /// <summary>
        ///     放弃更改执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnAbortExecuting(object sender)
        {
        }

        private void OnAbort(object sender)
        {
            if (sender is QueryableDataServiceCollectionViewBase)
            {
                var collectionView = sender as QueryableDataServiceCollectionViewBase;
                OnAbortExecuting(collectionView); //取消前。
                _service.RejectChanges(collectionView); //取消。
                OnAbortExecuted(collectionView); //取消后。
            }
            else
            {
                OnAbortExecuting(sender);
                _service.RejectChanges();
                OnAbortExecuted(sender);
            }
        }

        /// <summary>
        ///     放弃更改后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnAbortExecuted(object sender)
        {
        }

        private bool CanAbort(object sender)
        {
            return _service != null && _service.HasChanges;
        }

        #endregion

        #region 刷新按钮状态方法

        /// <summary>
        ///     刷新按钮状态
        /// </summary>
        protected virtual void RefreshCommandState()
        {
        }

        #endregion

        #region IConfirmNavigationRequest 成员

        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            if (_service.HasChanges)
            {
                MessageConfirm("还有未保存的更改，继续导航将撤销这些修改，是否继续？", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        _service.RejectChanges();
                        continuationCallback(true);
                    }
                    else
                    {
                        continuationCallback(false);
                    }
                });
            }
            else
            {
                continuationCallback(true);
            }
        }

        #endregion
    }
}