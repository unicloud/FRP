using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Part;

namespace UniCloud.Presentation.Part.ManageSCN
{
     [Export(typeof(SelectAircrafts))]
    public partial class SelectAircrafts 
    {
        public SelectAircrafts()
        {
            InitializeComponent();
            ViewModel = new SelectAircraftsVm(this, new PartService());
        }

        public SelectAircraftsVm ViewModel
        {
            get { return DataContext as SelectAircraftsVm; }
            set { DataContext = value; }
        }
    }
}
