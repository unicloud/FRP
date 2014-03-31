using System.Windows.Controls;
using UniCloud.Presentation.Service.Part.Part;

namespace UniCloud.Presentation.Part.ManageSCN
{
    public partial class AuditOrganizations
    {
        private readonly ScnDTO _scn;
        public AuditOrganizations(ScnDTO scn)
        {
            InitializeComponent();
            _scn = scn;
        }

        private void ButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
                switch (button.Content.ToString())
                {
                    case "机身系统室审核":
                        _scn.ScnStatus = 1; break;
                    case "航材计划室审核":
                        _scn.ScnStatus = 2; break;
                    case "机务工程部审核":
                        _scn.ScnStatus = 3; break;
                    case "生效":
                        _scn.ScnStatus = 4; break;
                }
            Close();
        }
    }
}
