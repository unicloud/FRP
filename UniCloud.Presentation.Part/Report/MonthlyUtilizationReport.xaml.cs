using System.ComponentModel.Composition;
using UniCloud.Presentation.Localization;

namespace UniCloud.Presentation.Part.Report
{
    [Export]
    public partial class MonthlyUtilizationReport 
    {
        public MonthlyUtilizationReport()
        {
            InitializeComponent();
            ReportViewer.TextResources = new ReportViewerLocalization();
        }
    }
}
