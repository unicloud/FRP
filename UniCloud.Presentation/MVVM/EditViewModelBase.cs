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

using Microsoft.Practices.Prism.Commands;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public abstract class EditViewModelBase : ViewModelBase
    {
        protected EditViewModelBase()
        {
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave); //保存命令。
            AbortCommand = new DelegateCommand<object>(OnAbort, CanAbort); //取消命令。
            Service.PropertyChanged += (o, e) =>
            {
                SaveCommand.RaiseCanExecuteChanged();
                AbortCommand.RaiseCanExecuteChanged();
            };
        }

        #region 保存命令

        public DelegateCommand<object> SaveCommand { get; private set; }

        private void OnSave(object sender)
        {
            OnSaveExecuting(sender);
            Service.SubmitChanges(sm =>
            {
                if (sm.Error == null)
                {
                    MessageAlert("提示", "保存成功。");
                    OnSaveSuccess(sender);
                }
                RefreshButtonState();
            });
        }

        /// <summary>
        ///     保存成功前执行的操作。
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnSaveExecuting(object sender)
        {
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
            return Service.HasChanges;
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
            OnAbortExecuting(sender); //取消前。
            Service.RejectChanges(); //取消。
            OnAbortExecuted(sender); //取消后。
            RefreshButtonState();
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
            return Service.HasChanges;
        }

        #endregion

        /// <summary>
        ///     刷新按钮状态
        /// </summary>
        protected abstract void RefreshButtonState();
    }
}