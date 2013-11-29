using System;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace UniCloud.Presentation.Input
{
    public abstract class GridViewDoubleClickHelper
    {
        internal void HookEvents(RadGridView grid)
        {
            grid.AddHandler(GridViewCellBase.CellDoubleClickEvent,
                                  new EventHandler<RadRoutedEventArgs>(OnCellDoubleClick), true);
        }

        private void OnCellDoubleClick(object sender, RadRoutedEventArgs args)
        {
            var cell = args.OriginalSource as GridViewCellBase;
            if (cell != null && CanDoubleClick(cell))
            {
                GridViewDoubleClick(cell);
            }
        }

        protected abstract void GridViewDoubleClick(GridViewCellBase cell);

        protected abstract bool CanDoubleClick(GridViewCellBase cell);

    }
}
