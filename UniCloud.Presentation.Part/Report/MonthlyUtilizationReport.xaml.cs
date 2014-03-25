using UniCloud.Presentation.Localization;

namespace UniCloud.Presentation.Part.Report
{
    public partial class MonthlyUtilizationReport 
    {
        public MonthlyUtilizationReport()
        {
            InitializeComponent();
            ReportViewer.TextResources = new ReportViewerLocalization();
        }
    }
}
