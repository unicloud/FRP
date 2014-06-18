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
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using UniCloud.Presentation.Service;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public abstract class EditViewModelBase : ViewModelBase, IConfirmNavigationRequest
    {
        private readonly IService _service;

        protected EditViewModelBase(IService service)
            : base(service)
        {
            _service = service;
            SaveCommand = new DelegateCommand<object>(OnSave, CanSave);
            AbortCommand = new DelegateCommand<object>(OnAbort, CanAbort);
            if (service != null)
            {
                service.PropertyChanged += (o, e) =>
                {
                    if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
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
            IsBusy = true;
            if (sender is QueryableDataServiceCollectionViewBase)
            {
                var collectionView = sender as QueryableDataServiceCollectionViewBase;
                if (!OnSaveExecuting(collectionView))
                {
                    return;
                }
                _service.SubmitChanges(collectionView, sm =>
                {
                    IsBusy = false;
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(collectionView);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(collectionView);
                        //MyLog.WriteErrLog(null, sm.Error.ToString());
                    }
                    RefreshCommandState();
                });
            }
            else
            {
                OnSaveExecuting(null);
                _service.SubmitChanges(sm =>
                {
                    IsBusy = false;
                    if (sm.Error == null)
                    {
                        MessageAlert("提示", "保存成功。");
                        OnSaveSuccess(sender);
                    }
                    else
                    {
                        MessageAlert("提示", "保存失败，请检查！");
                        OnSaveFail(sender);
                        //MyLog.WriteErrLog(null, sm.Error.ToString());
                    }
                    RefreshCommandState();
                });
            }
        }

        /// <summary>
        ///     保存前执行的操作
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
                IsBusy = true;
                OnAbortExecuting(collectionView); //取消前。
                _service.RejectChanges(collectionView); //取消。
                OnAbortExecuted(collectionView); //取消后。
                IsBusy = false;
            }
            else
            {
                IsBusy = true;
                OnAbortExecuting(sender);
                _service.RejectChanges();
                OnAbortExecuted(sender);
                IsBusy = false;
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

        #region Method

        public void GridViewSelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            var gridView = sender as RadGridView;
            var addedItem = e.AddedItems.FirstOrDefault();
            if (gridView != null && addedItem != null) gridView.ScrollIntoView(e.AddedItems[0]);
        }

        public class MyLog
        {
            public static void WriteErrLog(string strErrMod, string strErrDesc)
            {
                StreamWriter sw;
                string strErrLog = @"F:\Logs\Client_Error_Log" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt";   //获取写日志的路径
                if (File.Exists(strErrLog))
                {
                    FileInfo oFile = new FileInfo(strErrLog);
                    if (oFile.Length > 1024000)
                    {
                        oFile.Delete();
                    }  
                }
                if (File.Exists(strErrLog))
                {
                    sw = File.AppendText(strErrLog);
                }
                else
                {
                    sw = File.CreateText(strErrLog);
                }
                string strDate = "出错时间:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                string strErrMoudle = "出错模块:" + strErrMod;
                string strErrDescOut = "错误原因:" + strErrDesc;
                sw.WriteLine(strDate);
                sw.WriteLine(strErrMoudle);
                sw.WriteLine(strErrDescOut);
                sw.WriteLine("===================================================================");
                sw.Flush();
                sw.Close();
            }
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