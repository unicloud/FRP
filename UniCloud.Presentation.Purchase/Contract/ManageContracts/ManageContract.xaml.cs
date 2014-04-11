#region 命名空间

using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase.DocumentExtension;
using DragDropCompletedEventArgs = Telerik.Windows.DragDrop.DragDropCompletedEventArgs;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.ManageContracts
{
    [Export(typeof(ManageContract))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class ManageContract
    {
        public ManageContract()
        {
            InitializeComponent();

            DragDropManager.AddDragOverHandler(FoldersTreeView, OnItemDragOver);
            DragDropManager.AddDropHandler(FoldersTreeView, OnDrop);
            DragDropManager.AddDragDropCompletedHandler(DocumentListBox, OnDragDropCompleted);

            var timer = new DispatcherTimer { Interval = (new TimeSpan(0, 0, 0, 0, 200)) };
            //设置单击事件
            _doubleClickTimer = timer;
            _doubleClickTimer.Tick += (DoubleClickTimerTick);
        }

        [Import(typeof(ManageContractVm))]
        public ManageContractVm ViewModel
        {
            get { return DataContext as ManageContractVm; }
            set { DataContext = value; }
        }

        #region TreeView拖拽
        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (data == null) return;
            if (e.Effects != DragDropEffects.None)
            {
                var destinationItem = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var realDestinationItem = destinationItem.DataContext as ListBoxDocumentItem;
                var realDraggedData = data as ListBoxDocumentItem;
                if (realDraggedData != null && (realDestinationItem != null && realDestinationItem.DocumentPathId == realDraggedData.DocumentPathId))
                {
                    MessageDialogs.Alert("目标文件夹与源文件夹是同一个文件夹！");
                    _canRemove = false;
                    return;
                }
                var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

                if (_destinationItems != null)
                {
                    foreach (var trigger in _destinationItems)
                    {
                        var temp = trigger as ListBoxDocumentItem;
                        var realData = data as ListBoxDocumentItem;
                        if (realData != null && (temp != null && temp.Name.Equals(realData.Name, StringComparison.OrdinalIgnoreCase)))
                        {
                            MessageDialogs.Alert("目标文件夹的子文件夹中已有同名文件夹！");
                            _canRemove = false;
                            return;
                        }
                    }
                    int dropIndex = dropDetails != null && dropDetails.DropIndex >= _destinationItems.Count ? _destinationItems.Count : dropDetails != null && dropDetails.DropIndex < 0 ? 0 : dropDetails.DropIndex;
                    _destinationItems.Insert(dropIndex, data);
                    _canRemove = true;
                }
            }
        }

        IList _destinationItems;
        private void OnItemDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
            if (item == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var position = GetPosition(e.GetPosition(item));
            if (item.Level == 0 && position != DropPosition.Inside)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var tree = sender as RadTreeView;
            var draggedData = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if ((draggedData == null && dropDetails == null))
            {
                return;
            }
            if (position != DropPosition.Inside)
            {
                e.Effects = DragDropEffects.All;

                if (tree != null)
                    _destinationItems = item.Level > 0 ? (IList)item.ParentItem.ItemsSource : (IList)tree.ItemsSource;
                int index = _destinationItems.IndexOf(item.Item);
                if (dropDetails != null) dropDetails.DropIndex = position == DropPosition.Before ? index : index + 1;
            }
            else
            {
                _destinationItems = (IList)item.ItemsSource;
                const int index = 0;

                if (_destinationItems == null)
                {
                    e.Effects = DragDropEffects.None;
                }
                else
                {
                    e.Effects = DragDropEffects.All;
                    if (dropDetails != null) dropDetails.DropIndex = index;
                }
            }

            if (dropDetails != null)
            {
                dropDetails.CurrentDraggedOverItem = item.Item;
                dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(position);
            }

            e.Handled = true;
        }

        private DropPosition GetPosition(Point point)
        {
            const double treeViewItemHeight = 24;
            if (point.Y < treeViewItemHeight / 4)
            {
                return DropPosition.Before;
            }
            if (point.Y > treeViewItemHeight * 3 / 4)
            {
                return DropPosition.After;
            }

            return DropPosition.Inside;
        }
        #endregion

        #region ListBox拖拽
        private bool _canRemove;
        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            var draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

            if (e.Effects != DragDropEffects.None)
            {
                var listBox = sender as System.Windows.Controls.ListBox;
                if (listBox != null && _canRemove)
                {
                    var collection = listBox.ItemsSource as IList;
                    if (collection != null) collection.Remove(draggedItem);
                }
            }
        }
        #endregion

        #region ListBox双击处理
        /// <summary>
        /// 双击事件定时器
        /// </summary>
        private readonly DispatcherTimer _doubleClickTimer;

        /// <summary>
        /// 是否单击
        /// </summary>
        private bool _isOnceClick;
        private void DoubleClickTimerTick(object sender, EventArgs e)
        {
            _isOnceClick = false;
            _doubleClickTimer.Stop();
        }
        private void StackPanelMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!_isOnceClick)
            {
                _isOnceClick = true;
                _doubleClickTimer.Start();
            }
            else
            {
                ViewModel.ListBoxDoubleClick();
            }
        }
        #endregion

        #region Breadcrumb TreeView 联动
        private void FoldersBreadcrumbCurrentItemChanged(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            FoldersTreeView.BringPathIntoView(FoldersBreadcrumb.Path);
            FoldersTreeView.SelectedItem = FoldersBreadcrumb.CurrentItem;
        }

        private void FoldersTreeViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && FoldersTreeView.ContainerFromItemRecursive(e.AddedItems[0]) != null)
            {
                FoldersBreadcrumb.Path = FoldersTreeView.ContainerFromItemRecursive(e.AddedItems[0]).FullPath;
            }
        }
        #endregion
    }
}