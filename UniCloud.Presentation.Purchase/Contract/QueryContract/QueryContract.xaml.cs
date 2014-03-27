#region 命名空间

using System.Collections;
using System.ComponentModel.Composition;
using System.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof(QueryContract))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class QueryContract
    {
        public QueryContract()
        {
            InitializeComponent();
            //DragDropManager.AddDragOverHandler(FoldersTreeView, OnItemDragOver);
            //DragDropManager.AddDropHandler(FoldersTreeView, OnDrop);
        }

        [Import(typeof(QueryContractVM))]
        public QueryContractVM ViewModel
        {
            get { return DataContext as QueryContractVM; }
            set { DataContext = value; }
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (data == null) return;
            if (e.Effects != DragDropEffects.None)
            {
                var destinationItem = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

                if (_destinationItems != null)
                {
                    int dropIndex = dropDetails != null && dropDetails.DropIndex >= _destinationItems.Count ? _destinationItems.Count : dropDetails != null && dropDetails.DropIndex < 0 ? 0 : dropDetails.DropIndex;
                    _destinationItems.Insert(dropIndex, data);
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
            var position = GetPosition(item, e.GetPosition(item));
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
                dropDetails.CurrentDropPosition = position;
            }

            e.Handled = true;
        }



        private DropPosition GetPosition(RadTreeViewItem item, Point point)
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
    }
}