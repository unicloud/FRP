using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace UniCloud.Presentation.Input
{
    public class DataPageSerialColumn : GridViewColumn
    {
        public override FrameworkElement CreateCellElement(GridViewCell cell, object dataItem)
        {
            TextBlock textBlock = cell.Content as TextBlock;

            if (textBlock == null)
            {
                textBlock = new TextBlock();
            }

            textBlock.Text = string.Format("{0}", this.DataControl.Items.IndexOf(dataItem) + 1 + DataPage.PageSize * DataPage.PageIndex);
            textBlock.Foreground = new SolidColorBrush(Colors.Black);
            return textBlock;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            if (args.PropertyName == "DataControl")
            {
                if (this.DataControl != null && this.DataControl.Items != null)
                {
                    this.DataControl.Items.CollectionChanged += (s, e) =>
                    {
                        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
                        {
                            this.Refresh();
                        }
                    };
                }
            }
        }
    }

    public static class DataPage
    {
        public static int PageIndex = 0;
        public static int PageSize = 0;
    }
}
