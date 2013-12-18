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

using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls.DataServices;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        protected EditViewModelBase()
        {
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave); //保存命令。
            AbortCommand = new DelegateCommand<object>(OnAbort, CanAbort); //取消命令。
        }

        protected void OnViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "HasChanges")
            {
                SaveCommand.RaiseCanExecuteChanged();
                AbortCommand.RaiseCanExecuteChanged();
            }
        }

        #region 保存命令

        public DelegateCommand<object> SaveCommand { get; private set; }

        private void OnSave(object sender)
        {
            var collectionView = sender as QueryableDataServiceCollectionViewBase;
            if (!OnSaveExecuting(collectionView))
            {
                return;
            }
            Service.SubmitChanges(collectionView, sm =>
            {
                if (sm.Error == null)
                {
                    MessageAlert("提示", "保存成功。");
                    OnSaveSuccess(collectionView);
                }
                RefreshCommandState();
            });
            RefreshCommandState();
        }

        /// <summary>
        ///     保存成功前执行的操作。
        /// </summary>
        /// <param name="sender"></param>
        protected virtual bool OnSaveExecuting(QueryableDataServiceCollectionViewBase sender)
        {
            return true;
        }

        /// <summary>
        ///     保存成功后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSaveSuccess(QueryableDataServiceCollectionViewBase sender)
        {
        }


        /// <summary>
        ///     保存失败后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSaveFail(QueryableDataServiceCollectionViewBase sender)
        {
        }

        private bool CanSave(object sender)
        {
            var collectionView = sender as QueryableDataServiceCollectionViewBase;
            //提交时，保存按钮不可用
            if (collectionView != null &&collectionView.IsSubmittingChanges)
            {
                return false;
            }
            return collectionView != null && collectionView.HasChanges;
        }

        #endregion

        #region 放弃更改

        public DelegateCommand<object> AbortCommand { get; private set; }

        /// <summary>
        ///     放弃更改执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnAbortExecuting(QueryableDataServiceCollectionViewBase sender)
        {
        }

        private void OnAbort(object sender)
        {
            var collectionView = sender as QueryableDataServiceCollectionViewBase;
            OnAbortExecuting(collectionView); //取消前。
            Service.RejectChanges(collectionView); //取消。
            OnAbortExecuted(collectionView); //取消后。
        }

        /// <summary>
        ///     放弃更改后执行的操作
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnAbortExecuted(QueryableDataServiceCollectionViewBase sender)
        {
        }

        private bool CanAbort(object sender)
        {
            var collectionView = sender as QueryableDataServiceCollectionViewBase;
            //提交时，取消按钮不可用
            if (collectionView != null && collectionView.IsSubmittingChanges)
            {
                return false;
            }
            return collectionView != null && collectionView.HasChanges;
        }

        #endregion

        #region 刷新按钮状态方法
        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        public virtual void RefreshCommandState()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
        }
        #endregion

      
    }
}