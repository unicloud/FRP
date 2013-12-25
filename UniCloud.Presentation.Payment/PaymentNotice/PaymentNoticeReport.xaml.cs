using UniCloud.Presentation.Localization;

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    public partial class PaymentNoticeReport 
    {
        public PaymentNoticeReport()
        {
            InitializeComponent();
            ReportViewer.TextResources=new ReportViewerLocalization();
        }
    }
}
