#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/27 9:20:59
// 文件名：RadSystem.Windows.Controls.ListBoxDragDropBehavior
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/27 9:20:59
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.TreeView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

#endregion

namespace UniCloud.Presentation.Input
{
    public class ListBoxDragDropBehavior
    {
        /// <summary>
        /// object that will be associated with the System.Windows.Controls.ListBox instance that enables the behavior
        /// </summary>
        public System.Windows.Controls.ListBox AssociatedObject { get; set; }

        private static readonly Dictionary<System.Windows.Controls.ListBox, ListBoxDragDropBehavior> Instances;

        static ListBoxDragDropBehavior()
        {
            Instances = new Dictionary<System.Windows.Controls.ListBox, ListBoxDragDropBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            ListBoxDragDropBehavior behavior = GetAttachedBehavior(obj as System.Windows.Controls.ListBox);

            behavior.AssociatedObject = obj as System.Windows.Controls.ListBox;

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

        // Using a DependencyProperty as the backing store for IsEnabled state of the behavior
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(ListBoxDragDropBehavior),
                new PropertyMetadata(OnIsEnabledPropertyChanged));

        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static ListBoxDragDropBehavior GetAttachedBehavior(System.Windows.Controls.ListBox listBox)
        {
            if (!Instances.ContainsKey(listBox))
            {
                Instances[listBox] = new ListBoxDragDropBehavior { AssociatedObject = listBox };
            }

            return Instances[listBox];
        }

        //initializes the behavior by detaching from any existing DragDropManager event handlers and attaching new DragDropManager event handlers
        protected virtual void Initialize()
        {
            UnsubscribeFromDragDropEvents();
            SubscribeToDragDropEvents();
        }

        //cleans up after disabling the behavior by detaching from the DragDropManager event handlers
        protected virtual void CleanUp()
        {
            UnsubscribeFromDragDropEvents();
        }

        //attaching new DragDropManager event handlers
        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDropHandler(AssociatedObject, OnDrop);
            //DragDropManager.AddDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted); //需在其他模块完成自定义逻辑处理
            DragDropManager.AddDragOverHandler(AssociatedObject, OnDragOver);
        }

        //unsubscribing from the DragDropManager event handlers
        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(AssociatedObject, OnDrop);
            //DragDropManager.RemoveDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted); //需在其他模块完成自定义逻辑处理
            DragDropManager.RemoveDragOverHandler(AssociatedObject, OnDragOver);

        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var details = new DropIndicationDetails();
            var listBoxItem = e.OriginalSource as System.Windows.Controls.ListBoxItem ?? (e.OriginalSource as FrameworkElement).ParentOfType<System.Windows.Controls.ListBoxItem>();

            var listBox = sender as System.Windows.Controls.ListBox;
            if (listBox != null)
            {
                var item = listBox.SelectedItem;
                details.CurrentDraggedItem = item;

                IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

                dragPayload.SetData("DraggedData", item);
                dragPayload.SetData("DropDetails", details);

                e.Data = dragPayload;
            }

            e.DragVisual = new DragVisual
                           {
                               Content = details,
                               ContentTemplate = AssociatedObject.Resources["DraggedItemTemplate"] as DataTemplate
                           };
            e.DragVisualOffset = e.RelativeStartPoint;
            e.AllowedEffects = DragDropEffects.All;
        }

        private void OnGiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            e.SetCursor(Cursors.Arrow);
            e.Handled = true;
        }

        private void OnDragDropCompleted(object sender, DragDropCompletedEventArgs e)
        {
            var draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

            if (e.Effects != DragDropEffects.None)
            {
                var listBox = sender as System.Windows.Controls.ListBox;
                if (listBox != null)
                {
                    var collection = listBox.ItemsSource as IList;
                    if (collection != null) collection.Remove(draggedItem);
                }
            }
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options == null) return;
            var draggedItem = options.DraggedItems.FirstOrDefault();

            if (draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                var listBox = sender as System.Windows.Controls.ListBox;
                if (listBox != null)
                {
                    var collection = listBox.ItemsSource as IList;
                    if (collection != null) collection.Add(draggedItem);
                }
            }

            e.Handled = true;
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            var options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var draggedItem = options.DraggedItems.FirstOrDefault();
            var itemsType = (AssociatedObject.ItemsSource as IList).AsQueryable().ElementType;


            if (draggedItem != null && draggedItem.GetType() != itemsType)
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                var treeViewDragVisual = options.DragVisual as TreeViewDragVisual;
                if (treeViewDragVisual != null)
                    treeViewDragVisual.IsDropPossible = true;
                options.DropAction = DropAction.Move;
                options.UpdateDragVisual();
            }
            e.Handled = true;
        }
    }
}
