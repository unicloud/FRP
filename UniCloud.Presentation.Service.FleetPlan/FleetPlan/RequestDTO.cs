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
    public partial class RequestDTO
    {
        internal RequestStatus RequestStatus
        {
            get { return (RequestStatus) Status; }
        }
    }
}
