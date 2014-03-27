#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/27 9:20:59
// 文件名：RadRadListBoxDragDropBehavior
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
using ParentOfTypeExtensions = Telerik.Windows.Controls.ParentOfTypeExtensions;

#endregion

namespace UniCloud.Presentation.Input
{
    public class RadListBoxDragDropBehavior
    {
        private RadListBox _associatedObject;
        /// <summary>
        /// object that will be associated with the RadListBox instance that enables the behavior
        /// </summary>
        public RadListBox AssociatedObject
        {
            get
            {
                return _associatedObject;
            }
            set
            {
                _associatedObject = value;
            }
        }

        private static Dictionary<RadListBox, RadListBoxDragDropBehavior> instances;

        static RadListBoxDragDropBehavior()
        {
            instances = new Dictionary<RadListBox, RadListBoxDragDropBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            RadListBoxDragDropBehavior behavior = GetAttachedBehavior(obj as RadListBox);

            behavior.AssociatedObject = obj as RadListBox;

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
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(RadListBoxDragDropBehavior),
                new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static RadListBoxDragDropBehavior GetAttachedBehavior(RadListBox RadListBox)
        {
            if (!instances.ContainsKey(RadListBox))
            {
                instances[RadListBox] = new RadListBoxDragDropBehavior();
                instances[RadListBox].AssociatedObject = RadListBox;
            }

            return instances[RadListBox];
        }

        //initializes the behavior by detaching from any existing DragDropManager event handlers and attaching new DragDropManager event handlers
        protected virtual void Initialize()
        {
            this.UnsubscribeFromDragDropEvents();
            this.SubscribeToDragDropEvents();
        }

        //cleans up after disabling the behavior by detaching from the DragDropManager event handlers
        protected virtual void CleanUp()
        {
            this.UnsubscribeFromDragDropEvents();
        }

        //attaching new DragDropManager event handlers
        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.AddDropHandler(this.AssociatedObject, OnDrop);
            DragDropManager.AddDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
            DragDropManager.AddDragOverHandler(this.AssociatedObject, OnDragOver);
        }

        //unsubscribing from the DragDropManager event handlers
        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(this.AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(this.AssociatedObject, OnGiveFeedback);
            DragDropManager.RemoveDropHandler(this.AssociatedObject, OnDrop);
            DragDropManager.RemoveDragDropCompletedHandler(this.AssociatedObject, OnDragDropCompleted);
            DragDropManager.RemoveDragOverHandler(this.AssociatedObject, OnDragOver);

        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            DropIndicationDetails details = new DropIndicationDetails();
            var RadListBoxItem = e.OriginalSource as RadListBoxItem ?? (e.OriginalSource as FrameworkElement).ParentOfType<RadListBoxItem>();

            var item = RadListBoxItem != null ? RadListBoxItem : (sender as RadListBox).SelectedItem;
            details.CurrentDraggedItem = item;

            IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

            dragPayload.SetData("DraggedData", item);
            dragPayload.SetData("DropDetails", details);

            e.Data = dragPayload;

            e.DragVisual = new DragVisual()
            {
                Content = details,
                ContentTemplate = this.AssociatedObject.Resources["DraggedItemTemplate"] as DataTemplate
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
                var collection = (sender as RadListBox).ItemsSource as IList;
                collection.Remove(draggedItem);
            }
        }

        private void OnDrop(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            TreeViewDragDropOptions options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options == null) return;
            var draggedItem = options.DraggedItems.FirstOrDefault();

            if (draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                var collection = (sender as RadListBox).ItemsSource as IList;
                collection.Add(draggedItem);
            }

            e.Handled = true;
        }

        private void OnDragOver(object sender, Telerik.Windows.DragDrop.DragEventArgs e)
        {
            TreeViewDragDropOptions options = DragDropPayloadManager.GetDataFromObject(e.Data, TreeViewDragDropOptions.Key) as TreeViewDragDropOptions;
            if (options == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var draggedItem = options.DraggedItems.FirstOrDefault();
            var itemsType = (this.AssociatedObject.ItemsSource as IList).AsQueryable().ElementType;


            if (draggedItem.GetType() != itemsType)
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                (options.DragVisual as TreeViewDragVisual).IsDropPossible = true;
                options.DropAction = DropAction.Move;
                options.UpdateDragVisual();
            }
            e.Handled = true;
        }
    }
}
