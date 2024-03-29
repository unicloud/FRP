﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/12，09:11
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
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.MVVM
{
    /// <summary>
    ///     ViewModel基类
    /// </summary>
    public abstract class ViewModelBase : NotificationObject, INavigationAware, ILoadData
    {
        [Import] public DocViewer docViewer;
        [Import] public DocViewerVM docViewerVM;
        public ListDocuments listDocuments;
        protected Action<DocumentDTO> windowClosed;

        #region ctor

        protected ViewModelBase(IService service)
        {
            AddAttachCommand = new DelegateCommand<object>(OnAddAttach, CanAddAttach);
            AddLocalAttachCommand = new DelegateCommand<object>(OnAddLocalAttach, CanAddLocalAttach);
            AddWordCommand = new DelegateCommand<object>(OnAddWord, CanAddWord);
            AddExcelCommand = new DelegateCommand<object>(OnAddExcel, CanAddExcel);
            AddPdfCommand = new DelegateCommand<object>(OnAddPdf, CanAddPdf);
            ViewAttachCommand = new DelegateCommand<object>(OnViewAttach);
            ExcelExportCommand = new DelegateCommand<object>(OnExcelExport);
            WordExportCommand = new DelegateCommand<object>(OnWordExport);
            ChartExportCommand = new DelegateCommand<object>(OnChartExport);
            ChartDataExportCommand = new DelegateCommand<object>(OnChartDataExport);
            if (service != null)
            {
                service.PropertyChanged += (o, e) =>
                {
                    if (!e.PropertyName.Equals("IsBusy", StringComparison.OrdinalIgnoreCase)) return;
                    IsBusy = service.IsBusy;
                    BusyContent = string.Empty;
                };
            }
            DocTypeIds = new int[0];
        }

        #endregion

        #region IDisposable Implemented

        /// <summary>
        ///     释放内存
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        ///     释放内存的时候触发
        /// </summary>
        /// <param name="disposing">是否正在释放内存</param>
        protected virtual void Dispose(bool disposing)
        {
        }

        protected virtual void SetIsBusy()
        {
        }

        #endregion

        #region Protected Properties

        private string _busyContent = "";
        private bool _isBusy;

        /// <summary>
        ///     界面是否繁忙
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public string BusyContent
        {
            get { return _busyContent; }
            set
            {
                _busyContent = value;
                RaisePropertyChanged(() => BusyContent);
            }
        }

        #endregion

        #region ViewModel

        #region 服务器添加附件

        public DelegateCommand<object> AddAttachCommand { get; set; }

        protected int[] DocTypeIds { get; set; }

        /// <summary>
        ///     服务器添加附件
        /// </summary>
        /// <param name="sender">命令参数</param>
        protected void OnAddAttach(object sender)
        {
            listDocuments = ServiceLocator.Current.GetInstance<ListDocuments>();
            if ((sender is Guid) && (Guid) sender != Guid.Empty)
            {
                MessageConfirm("附件已存在，继续操作将有可能替换当前附件。是否继续？", (o, e) =>
                {
                    if (e.DialogResult == null || e.DialogResult != true) return;
                    listDocuments.ViewModel.InitData(d => WindowClosed(d, sender), DocTypeIds);
                    listDocuments.ShowDialog();
                });
            }
            else
            {
                listDocuments.ViewModel.InitData(d => WindowClosed(d, sender), DocTypeIds);
                listDocuments.ShowDialog();
            }
        }

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected virtual void WindowClosed(DocumentDTO doc, object sender)
        {
            docViewer.Close();
        }

        protected virtual bool CanAddAttach(object obj)
        {
            return true;
        }

        #endregion

        #region 本地添加附件

        public DelegateCommand<object> AddLocalAttachCommand { get; set; }

        /// <summary>
        ///     本地添加附件
        /// </summary>
        /// <param name="sender">命令参数</param>
        protected void OnAddLocalAttach(object sender)
        {
            var openFileDialog = new OpenFileDialog {Filter = "可用文档|*.docx;*.pdf"};
            if (openFileDialog.ShowDialog() != true) return;
            listDocuments = ServiceLocator.Current.GetInstance<ListDocuments>();
            listDocuments.Close();

            docViewer.ShowDialog();
            docViewerVM.InitDocument(openFileDialog.File, windowClosed, DocTypeIds);
        }


        protected virtual bool CanAddLocalAttach(object obj)
        {
            return true;
        }

        #endregion

        #region 添加Word文档

        /// <summary>
        ///     添加Word文档
        /// </summary>
        public DelegateCommand<object> AddWordCommand { get; private set; }

        private void OnAddWord(object obj)
        {
            var openFileDialog = new OpenFileDialog {Filter = "WORD文档|*.docx"};
            if (openFileDialog.ShowDialog() != true) return;
            listDocuments = ServiceLocator.Current.GetInstance<ListDocuments>();
            listDocuments.Close();

            docViewer.ShowDialog();
            docViewerVM.InitDocument(openFileDialog.File, windowClosed, DocTypeIds);
        }

        private bool CanAddWord(object obj)
        {
            return true;
        }

        #endregion

        #region 添加Excel文档

        /// <summary>
        ///     添加Excel文档
        /// </summary>
        public DelegateCommand<object> AddExcelCommand { get; private set; }

        private void OnAddExcel(object obj)
        {
            var openFileDialog = new OpenFileDialog {Filter = "EXCEL文档|*.xlsx"};
            if (openFileDialog.ShowDialog() != true) return;
            listDocuments = ServiceLocator.Current.GetInstance<ListDocuments>();
            listDocuments.Close();

            docViewer.ShowDialog();
            docViewerVM.InitDocument(openFileDialog.File, windowClosed, DocTypeIds);
        }

        private bool CanAddExcel(object obj)
        {
            return true;
        }

        #endregion

        #region 添加Pdf文档

        /// <summary>
        ///     添加Pdf文档
        /// </summary>
        public DelegateCommand<object> AddPdfCommand { get; private set; }

        private void OnAddPdf(object obj)
        {
            var openFileDialog = new OpenFileDialog {Filter = "PDF文档|*.pdf"};
            if (openFileDialog.ShowDialog() != true) return;
            listDocuments = ServiceLocator.Current.GetInstance<ListDocuments>();
            listDocuments.Close();

            docViewer.ShowDialog();
            docViewerVM.InitDocument(openFileDialog.File, windowClosed, DocTypeIds);
        }

        private bool CanAddPdf(object obj)
        {
            return true;
        }

        #endregion

        #region 查看附件

        public DelegateCommand<object> ViewAttachCommand { get; set; }

        /// <summary>
        ///     查看附件
        /// </summary>
        /// <param name="sender">查看附件命令的参数</param>
        protected void OnViewAttach(object sender)
        {
            if (sender is Guid)
            {
                docViewer.ShowDialog();
                docViewerVM.InitDocument((Guid) sender);
            }
            else
            {
                throw new ArgumentException("查看附件应包含文档的GUID参数！");
            }
        }

        #endregion

        #region 导出Excel

        public DelegateCommand<object> ExcelExportCommand { get; set; }

        private void OnExcelExport(object sender)
        {
            var grid = sender as RadGridView;
            if (grid != null)
            {
                grid.ExportToExcel();
            }
        }

        #endregion

        #region 导出Word

        public DelegateCommand<object> WordExportCommand { get; set; }

        private void OnWordExport(object sender)
        {
            var grid = sender as RadGridView;
            if (grid != null)
            {
                grid.ExportToWord();
            }
        }

        #endregion

        #region 导出图表

        public DelegateCommand<object> ChartExportCommand { get; set; }

        /// <summary>
        ///     导出图标
        /// </summary>
        /// <param name="sender">导出对象</param>
        protected virtual void OnChartExport(object sender)
        {
        }

        #endregion

        #region 图表数据导出Excel

        public DelegateCommand<object> ChartDataExportCommand { get; set; }

        /// <summary>
        ///     导出图标数据
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnChartDataExport(object sender)
        {
        }

        #endregion

        #endregion

        #region 消息窗口

        /// <summary>
        ///     消息提醒通知
        /// </summary>
        /// <param name="message">消息</param>
        public void MessageAlert(string message)
        {
            MessageDialogs.Alert(message);
        }

        /// <summary>
        ///     消息提醒通知
        /// </summary>
        /// <param name="title"> </param>
        /// <param name="message">消息</param>
        public void MessageAlert(string title, string message)
        {
            MessageDialogs.Alert(title, message);
        }

        /// <summary>
        ///     弹出子窗体需要用户确认是否执行
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="callback">回调函数</param>
        public void MessageConfirm(string message, EventHandler<WindowClosedEventArgs> callback)
        {
            MessageDialogs.Confirm(message, callback);
        }

        /// <summary>
        ///     弹出子窗体需要用户确认是否执行
        /// </summary>
        /// <param name="title"> </param>
        /// <param name="message">消息</param>
        /// <param name="callback">回调函数</param>
        public void MessageConfirm(string title, string message, EventHandler<WindowClosedEventArgs> callback)
        {
            MessageDialogs.Confirm(title, message, callback);
        }

        #endregion

        #region INavigationAware Implemented

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            LoadData();
        }

        #endregion

        #region ILoadData Implemented

        /// <summary>
        ///     加载数据。
        /// </summary>
        public abstract void LoadData();

        #endregion
    }
}