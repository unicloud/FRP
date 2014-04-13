﻿using System;
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
    public partial class AircraftConfigView : UserControl
    {
        public AircraftConfigView()
        {
            InitializeComponent();
            SpecialConfigsTreeView.ExpandAll();
            DragDropManager.AddDragOverHandler(SpecialConfigsTreeView, OnDragOver);
            DragDropManager.AddDropHandler(SpecialConfigsTreeView, OnDrop);
        }

        [Import(typeof (AircraftConfigVm))]
        public AircraftConfigVm ViewModel
        {
            get { return DataContext as AircraftConfigVm; }
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
                var destinationSpecialConfig = (e.OriginalSource as FrameworkElement).ParentOfType<RadTreeViewItem>();
                var realDestinationSpecialConfig = new SpecialConfigDTO();
                if (destinationSpecialConfig != null)
                    realDestinationSpecialConfig = destinationSpecialConfig.DataContext as SpecialConfigDTO;
                var collection = (sender as RadTreeView).ItemsSource as ObservableCollection<SpecialConfigDTO>;
                var item = draggedItem as ItemDTO;
                if (item != null && collection != null && ViewModel.SelContractAircraft != null)
                {
                    var newSpecialConfig = new SpecialConfigDTO
                    {
                        Id = RandomHelper.Next(),
                        ItemId = item.Id,
                        ItemNo = item.ItemNo,
                        FiNumber = item.FiNumber,
                        ContractAircraftId = ViewModel.SelContractAircraft.Id,
                        StartDate = DateTime.Now,
                    };
                    if (realDestinationSpecialConfig != null && realDestinationSpecialConfig.Id != 0)
                    {
                        newSpecialConfig.ParentId = realDestinationSpecialConfig.Id;
                        newSpecialConfig.RootId = realDestinationSpecialConfig.RootId;

                        ViewModel.SpecialConfigs.AddNew(newSpecialConfig);

                        //找到ViewSpecialConfigs中的对应节点，直接在节点下添加
                        int index = ViewModel.ViewSpecialConfigs.IndexOf(realDestinationSpecialConfig);
                        ViewModel.ViewSpecialConfigs.Insert(index + 1, newSpecialConfig);
                    }
                    else
                    {
                        newSpecialConfig.RootId = newSpecialConfig.Id;
                        ViewModel.ViewSpecialConfigs.Add(newSpecialConfig);
                        ViewModel.SpecialConfigs.AddNew(newSpecialConfig);
                    }
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
                var destinationItem = item.Item as SpecialConfigDTO;
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