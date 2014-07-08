#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 17:59:26
// 文件名：BankAccountWindowVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 17:59:26
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentNotice
{
    [Export(typeof (BankAccountWindowVm))]
    public class BankAccountWindowVm : INotifyPropertyChanged
    {
        private IEnumerable<BankAccountDTO> _bankAccounts;

        public IEnumerable<BankAccountDTO> BankAccounts
        {
            get { return _bankAccounts; }
            set
            {
                _bankAccounts = value;
                OnPropertyChanged("BankAccounts");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void InitBankAccounts(IEnumerable<BankAccountDTO> bankAccounts)
        {
            BankAccounts = bankAccounts;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}