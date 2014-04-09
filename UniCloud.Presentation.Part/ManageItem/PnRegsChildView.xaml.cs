﻿using System.ComponentModel.Composition;

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof(PnRegsChildView))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class PnRegsChildView
    {
        public PnRegsChildView()
        {
            InitializeComponent();
        }

        [Import]
        public ItemControlVm ViewModel
        {
            get { return DataContext as ItemControlVm; }
            set { DataContext = value; }
        }
    }
}