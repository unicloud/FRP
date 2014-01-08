#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/8 9:51:51
// 文件名：ManagerPortalVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/8 9:51:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Media;
using UniCloud.Presentation.MVVM;

#endregion

namespace UniCloud.Presentation.Portal.Manager
{
    [Export(typeof(ManagerPortalVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerPortalVm : ViewModelBase
    {
        List<CountryRevenue> _countryRevenues;
        public List<CountryRevenue> CountryRevenues
        {
            get
            {
                return _countryRevenues;
            }
            set
            {
                if (_countryRevenues != value)
                {
                    _countryRevenues = value;
                    RaisePropertyChanged("CountryRevenues");
                }
            }
        }
        public override void LoadData()
        {
            //throw new System.NotImplementedException();
        }

        public ManagerPortalVm()
        {
            InitData();
        }

        private void InitData()
        {
            CountryRevenues = new List<CountryRevenue>
                              {
                                  new CountryRevenue{Country = "CN",Actual=97,Target=100,Color="#FFCCCCCC"},
                                  new CountryRevenue{Country = "USA",Actual=80,Target=100,Color="#FFCCCCCC"},
                                   new CountryRevenue{Country = "Jp",Actual=70,Target=100,Color="#FFF90202"},
                              };
        }

        public class CountryRevenue
        {
            public string Country { get; set; }

            public double Actual { get; set; }

            public double Target { get; set; }

            public string Color { get; set; }
        }
    }
}
