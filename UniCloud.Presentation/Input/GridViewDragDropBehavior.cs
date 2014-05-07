using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.DragDrop;
using Telerik.Windows.DragDrop.Behaviors;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;
using DragVisual = Telerik.Windows.DragDrop.DragVisual;

namespace UniCloud.Presentation.Input
{
    public class GridViewDragAndDropBehavior
    {
        /// <summary>
        ///     AssociatedObject Property
        /// </summary>
        public RadGridView AssociatedObject { get; set; }

        private static readonly Dictionary<RadGridView, GridViewDragAndDropBehavior> Instances;

        static GridViewDragAndDropBehavior()
        {
            Instances = new Dictionary<RadGridView, GridViewDragAndDropBehavior>();
        }

        public static bool GetIsEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsEnabledProperty);
        }

        public static void SetIsEnabled(DependencyObject obj, bool value)
        {
            GridViewDragAndDropBehavior behavior = GetAttachedBehavior(obj as RadGridView);

            behavior.AssociatedObject = obj as RadGridView;

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
            DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(GridViewDragAndDropBehavior),
                new PropertyMetadata(OnIsEnabledPropertyChanged));


        public static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs e)
        {
            SetIsEnabled(dependencyObject, (bool)e.NewValue);
        }

        private static GridViewDragAndDropBehavior GetAttachedBehavior(RadGridView gridview)
        {
            if (!Instances.ContainsKey(gridview))
            {
                Instances[gridview] = new GridViewDragAndDropBehavior { AssociatedObject = gridview };
            }

            return Instances[gridview];
        }

        //initializes the behavior by detaching from any existing DragDropManager event handlers and attaching new DragDropManager event handlers
        protected virtual void Initialize()
        {
            UnsubscribeFromDragDropEvents();
            SubscribeToDragDropEvents();
        }

        protected virtual void CleanUp()
        {
            UnsubscribeFromDragDropEvents();
        }

        //attaching new DragDropManager event handlers
        private void SubscribeToDragDropEvents()
        {
            DragDropManager.AddDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.AddGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            //DragDropManager.AddDropHandler(AssociatedObject, OnDrop);
            //DragDropManager.AddDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted);
            DragDropManager.AddDragOverHandler(AssociatedObject, OnDragOver);
        }

        //unsubscribing from the DragDropManager event handlers
        private void UnsubscribeFromDragDropEvents()
        {
            DragDropManager.RemoveDragInitializeHandler(AssociatedObject, OnDragInitialize);
            DragDropManager.RemoveGiveFeedbackHandler(AssociatedObject, OnGiveFeedback);
            //DragDropManager.RemoveDropHandler(AssociatedObject, OnDrop);
            //DragDropManager.RemoveDragDropCompletedHandler(AssociatedObject, OnDragDropCompleted);
            DragDropManager.RemoveDragOverHandler(AssociatedObject, OnDragOver);
        }

        private void OnDragInitialize(object sender, DragInitializeEventArgs e)
        {
            var details = new DropIndicationDetails();
            var row = e.OriginalSource as GridViewRow ??
                              (e.OriginalSource as FrameworkElement).ParentOfType<GridViewRow>();

            if (sender != null)
            {
                object item = row != null ? row.Item : (sender as RadGridView).SelectedItem;
                details.CurrentDraggedItem = item;

                IDragPayload dragPayload = DragDropPayloadManager.GeneratePayload(null);

                dragPayload.SetData("DraggedData", item);
                dragPayload.SetData("DropDetails", details);

                e.Data = dragPayload;
            }

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
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");

            if (e.Effects != DragDropEffects.None)
            {
                var gridView = sender as RadGridView;
                if (gridView != null)
                {
                    var collection = gridView.ItemsSource as IList;
                    if (collection != null) collection.Remove(draggedItem);
                }
            }
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            var details = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            if (details == null || draggedItem == null)
            {
                return;
            }

            if (e.Effects != DragDropEffects.None)
            {
                var gridView = sender as RadGridView;
                if (gridView != null)
                {
                    var collection = gridView.ItemsSource as IList;
                    if (collection != null) collection.Add(draggedItem);
                }
            }

            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            var gridView = sender as RadGridView;
            object draggedItem = DragDropPayloadManager.GetDataFromObject(e.Data, "DraggedData");
            if (draggedItem == null || gridView == null) return;
            //Type itemsType = gridView.CurrentItem.GetType();

            //if (draggedItem.GetType() != itemsType)
            //{
            //    e.Effects = DragDropEffects.None;
            //}

            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;
            if (dropDetails == null) return;
            dropDetails.CurrentDraggedOverItem = gridView;
            dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(DropPosition.Inside);

            e.Handled = true;
        }
    }
}