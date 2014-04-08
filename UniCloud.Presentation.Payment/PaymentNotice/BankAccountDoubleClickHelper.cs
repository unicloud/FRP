#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 18:32:54
// 文件名：BankAccountDoubleClickHelper
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 18:32:54
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.ServiceLocation;
using UniCloud.Presentation.Input;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    public class BankAccountDoubleClickHelper : GridViewDoubleClickHelper
    {
        protected override void GridViewDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            var view = ServiceLocator.Current.GetInstance<BankAccountWindow>();
            view.Tag = view.BankAccountList.CurrentItem;
            view.Close();
        }
        protected override bool CanDoubleClick(Telerik.Windows.Controls.GridView.GridViewCellBase cell)
        {
            return true;
        }
    }
}
