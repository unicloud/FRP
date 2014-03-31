#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/27 10:04:15
// 文件名：TreeViewDragDropBehavior
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/27 10:04:15
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

#endregion

namespace UniCloud.Presentation.Input
{
    public class TreeViewDragDropBehavior
    {
        public double TreeViewItemHeight { get; set; }

        /// <summary>
        /// AssociatedObject Property
        /// </summary>
        public RadTreeView AssociatedObject { get; set; }

        private static readonly Dictionary<RadTreeView, TreeViewDragDropBehavior> Instances;

        static TreeViewDragDropBehavior()
        {
            Instances = new Dictionary<RadTreeView, TreeViewDragDropBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            TreeViewDragDropBehavior behavior = GetAttachedBehavior(obj as RadTreeView);

            behavior.AssociatedObject = obj as RadTreeView;

            if (value)
            {
                behavior.Initialize();
            }
            else
            {
                behavior.CleanUp();
            }
            obj.SetValue(IsEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(TreeViewDragDropBehavior),
                new PropertyMetadata(OnIsEnabledPropertyChanged));

        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            //SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static TreeViewDragDropBehavior GetAttachedBehavior(RadTreeView gridview)
        {
            if (!Instances.ContainsKey(gridview))
            {
                Instances[gridview] = new TreeViewDragDropBehavior { AssociatedObject = gridview };
            }

            return Instances[gridview];
        }

        protected virtual void Initialize()
        {
            DragDropManager.AddDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted);
            DragDropManager.AddDropHandler(AssociatedObject, OnDrop);
            TreeViewItemHeight = 24.0;

            AssociatedObject.ItemPrepared += AssociatedObjectItemPrepared;
        }

        protected virtual void CleanUp()
        {
            DragDropManager.RemoveDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            DragDropManager.RemoveDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted);
            DragDropManager.RemoveDropHandler(AssociatedObject, OnDrop);
        }

        void AssociatedObjectItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
        {
            DragDropManager.RemoveDragOverHandler(e.PreparedItem, OnItemDragOver);
            DragDropManager.AddDragOverHandler(e.PreparedItem, OnItemDragOver);
        }

        private IList _sourceItems;
        private IList _destinationItems;
        private object _sourceItem;
        private bool _isTreeSource;

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var treeViewItem = e.OriginalSource as RadTreeViewItem ?? (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
            var data = treeViewItem != null ? treeViewItem.Item : (sender as RadTreeView).SelectedItem;

            var payload = DragDropPayloadManager.GeneratePayload(null);

            var dropDetails = new DropIndicationDetails { CurrentDraggedItem = data };

            var visual = new DragVisual
                         {
                             Content = dropDetails,
                             //ContentTemplate = data is CategoryViewModel ? AssociatedObject.Resources["CategoryDragTemplate"] as DataTemplate : AssociatedObject.Resources["ProductDragTemplate"] as DataTemplate
                         };

            payload.SetData("DraggedData", data);
            payload.SetData("DropDetails", dropDetails);

            e.DragVisual = visual;
            e.DragVisualOffset = e.RelativeStartPoint;
            e.Data = payload;
            e.AllowedEffects = DragDropEffects.All;

            FrameworkElement sourceItem = e.OriginalSource as RadTreeViewItem ?? (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
            if (sourceItem == null)
            {
                _sourceItems = (IList)AssociatedObject.ItemsSource;
            }
            else
            {
                _sourceItems = (sourceItem as RadTreeViewItem).ParentItem != null ? (IList)(sourceItem as RadTreeViewItem).ParentItem.ItemsSource : (IList)AssociatedObject.ItemsSource;
            }

            _sourceItem = sourceItem.DataContext;
            _destinationItems = AssociatedObject.ItemsSource as IList;
            _isTreeSource = true;
        }

        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            if (e.Effects != DragDropEffects.None && _isTreeSource)
            {
                var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

                _sourceItems.Remove(data);
            }
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            if (_isTreeSource)
            {
                var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

                _sourceItems.Remove(data);
            }

            if (e.Effects != DragDropEffects.None)
            {
                var destinationItem = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
                var data = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

                if (_destinationItems != null)
                {
                    int dropIndex = dropDetails.DropIndex >= _destinationItems.Count ? _destinationItems.Count :
                                                                                                              dropDetails.DropIndex < 0 ? 0 : dropDetails.DropIndex;

                    _destinationItems.Insert(dropIndex, data);
                }
            }

            if (_isTreeSource)
            {
                _isTreeSource = false;
                _sourceItem = null;
                _sourceItems = null;
                _destinationItems = null;
            }
        }

        private void OnItemDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var draggedData = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            var item = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();

            var position = GetPosition(item, e.GetPosition(item));
            if (position != DropPosition.Inside)
            {
                e.Effects = DragDropEffects.All;

                _destinationItems = item.Level > 0 ? (IList)item.ParentItem.ItemsSource : (IList)AssociatedObject.ItemsSource;
                //(IList)item.ParentItem.ItemsSource != null ? (IList)item.ParentItem.ItemsSource : (IList)this.AssociatedObject.ItemsSource;
                int index = _destinationItems.IndexOf(item.Item);
                dropDetails.DropIndex = position == DropPosition.Before ? index : index + 1;
            }
            else
            {
                _destinationItems = (IList)item.ItemsSource;
                int index = 0;

                if (_destinationItems == null)
                {
                    e.Effects = DragDropEffects.None;
                }
                else
                {
                    e.Effects = DragDropEffects.All;
                    dropDetails.DropIndex = index;
                }
            }

            dropDetails.CurrentDraggedOverItem = item.Item;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(position);

            if (_isTreeSource && IsChildOfSource(item))
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private bool IsChildOfSource(FrameworkElement item)
        {
            var currentItem = item;

            while (currentItem != null)
            {
                if ((currentItem as RadTreeViewItem).Item == _sourceItem)
                {
                    return true;
                }

                currentItem = currentItem.ParentOfType<RadTreeViewItem>();
            }

            return false;
        }

        private DropPosition GetPosition(RadTreeViewItem item, Point point)
        {
            if (point.Y < TreeViewItemHeight / 4)
            {
                return DropPosition.Before;
            }
            else if (point.Y > TreeViewItemHeight * 3 / 4)
            {
                return DropPosition.After;
            }

            return DropPosition.Inside;
        }
    }
}
