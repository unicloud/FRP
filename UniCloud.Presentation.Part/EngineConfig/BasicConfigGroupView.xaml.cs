using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;
using Telerik.Windows.DragDrop;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Input;
using UniCloud.Presentation.Service.Part.Part;
using DragEventArgs = Telerik.Windows.DragDrop.DragEventArgs;

namespace UniCloud.Presentation.Part.EngineConfig
{
    [Export]
    public partial class BasicConfigGroupView : UserControl
    {
        public BasicConfigGroupView()
        {
            InitializeComponent();
            BasicConfigsTreeView.ExpandAll();
            DragDropManager.AddDragOverHandler(BasicConfigsTreeView, OnDragOver);
            DragDropManager.AddDropHandler(BasicConfigsTreeView, OnDrop);
        }

        [Import(typeof (BasicConfigGroupVm))]
        public BasicConfigGroupVm ViewModel
        {
            get { return DataContext as BasicConfigGroupVm; }
            set { DataContext = value; }
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
                var destinationBasicConfig = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var realDestinationBasicConfig = new BasicConfigDTO();
                if (destinationBasicConfig != null)
                    realDestinationBasicConfig = destinationBasicConfig.DataContext as BasicConfigDTO;
                var collection = (sender as RadTreeView).ItemsSource as ObservableCollection<BasicConfigDTO>;
                var item = draggedItem as ItemDTO;
                if (item != null && collection != null && ViewModel.SelBasicConfigGroup != null)
                {
                    var newBasicConfig = new BasicConfigDTO
                    {
                        Id = RandomHelper.Next(),
                        ItemId = item.Id,
                        ItemNo = item.ItemNo,
                        FiNumber = item.FiNumber,
                        BasicConfigGroupId = ViewModel.SelBasicConfigGroup.Id,
                    };
                    if (realDestinationBasicConfig != null && realDestinationBasicConfig.Id != 0)
                    {
                        newBasicConfig.ParentId = realDestinationBasicConfig.Id;
                        newBasicConfig.RootId = realDestinationBasicConfig.RootId;

                        realDestinationBasicConfig.SubBasicConfigs.Add(newBasicConfig);
                    }
                    else
                    {
                        newBasicConfig.RootId = newBasicConfig.Id;
                        ViewModel.ViewBasicConfigs.Add(newBasicConfig);
                    }
                    ViewModel.BasicConfigs.AddNew(newBasicConfig);
                }
            }

            e.Handled = true;
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            var item = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
            var dropDetails = DragDropPayloadManager.GetDataFromObject(e.Data, "DropDetails") as DropIndicationDetails;

            DropPosition position = GetPosition();
            if (dropDetails != null && item != null)
            {
                var destinationItem = item.Item as BasicConfigDTO;
                if (destinationItem != null)
                    dropDetails.CurrentDraggedOverItem = destinationItem;
                else dropDetails.CurrentDraggedItem = item.Item as RadTreeView;
                dropDetails.CurrentDropPosition = DropIndicationDetails.ConverDropPositionToString(position);
            }
            e.Handled = true;
        }

        private DropPosition GetPosition()
        {
            return DropPosition.After;
        }
    }
}