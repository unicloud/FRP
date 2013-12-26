#region 命名空间

using UniCloud.Presentation.Localization;

#endregion

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
