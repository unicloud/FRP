using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(CompareScn))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public partial class CompareScn 
    {
        public CompareScn()
        {
            InitializeComponent();
        }
        [Import]
        public CompareScnVm ViewModel
        {
            get { return DataContext as CompareScnVm; }
            set { DataContext = value; }
        }
    }
}
