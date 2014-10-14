using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

namespace UniCloud.Presentation.Service.FleetPlan.FleetPlan
{
    public partial class OwnershipHistoryDTO
    {
        internal bool IsOwnershipReadOnly
        {
            get { return Status >= (int) OperationStatus.已审核; }
        }
    }
}
