#region �汾����

// =====================================================
// ��Ȩ���� (C) 2014 UniCloud 
// �����๦�ܸ�����
// 
// ���ߣ���־�� ʱ�䣺2013-11-29��13:11
// ������FRP
// ��Ŀ��Purchase
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// =====================================================

#endregion

#region �����ռ�

using System.ComponentModel.Composition;

#endregion

namespace UniCloud.Presentation.Purchase.Forwarder
{
    [Export]
    public partial class ForwarderManager
    {
        public ForwarderManager()
        {
            InitializeComponent();
        }

        [Import]
        public ForwarderManagerVM ViewModel
        {
            get { return DataContext as ForwarderManagerVM; }
            set { DataContext = value; }
        }
    }
}