#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/8 11:21:02
// 文件名：GridViewSelectedItemsBehavior
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/8 11:21:02
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Input
{
    public class GridViewSelectedItemsBehavior : Behavior<RadGridView>
    {
        public INotifyCollectionChanged SelectedItems
        {
            get { return (INotifyCollectionChanged)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems",
            typeof(INotifyCollectionChanged), typeof(GridViewSelectedItemsBehavior), new PropertyMetadata(OnSelectedItemsPropertyChanged));

        private RadGridView GridView
        {
            get
            {
                return AssociatedObject;
            }
        }

        private static void OnSelectedItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behavior = (GridViewSelectedItemsBehavior)d;
            var collection = e.NewValue as INotifyCollectionChanged;
            if (collection != null)
            {
                behavior.UnsubscribeFromEvents();
                behavior.Transfer(behavior.SelectedItems as IList, behavior.GridView.SelectedItems);
                behavior.SubscribeToEvents();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            GridView.SelectedItems.CollectionChanged += GridViewSelectedItemsCollectionChanged;
        }

        private void GridViewSelectedItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnsubscribeFromEvents();
            Transfer(GridView.SelectedItems, SelectedItems as IList);
            SubscribeToEvents();
        }

        public void Transfer(IList source, IList target)
        {
            if (source == null || target == null)
                return;

            target.Clear();

            foreach (var o in source)
            {
                target.Add(o);
            }
        }

        private void SubscribeToEvents()
        {
            GridView.SelectedItems.CollectionChanged += GridViewSelectedItemsCollectionChanged;
        }

        private void UnsubscribeFromEvents()
        {
            GridView.SelectedItems.CollectionChanged -= GridViewSelectedItemsCollectionChanged;
        }
    }  
}
