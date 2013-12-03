#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/16 15:27:57
// 文件名：AnalyseAircraftPriceVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;

namespace UniCloud.Presentation.Purchase.QueryAnalyse
{
    [Export(typeof(AnalyseAircraftPriceVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AnalyseAircraftPriceVm : INotifyPropertyChanged
    {
         private List<FinancialData> _data;
         private List<FinancialData> _data1;
        private Point _panOffset;
        private Size _zoom;

        public AnalyseAircraftPriceVm()
        {
            GenerateData();
            Zoom = new Size(3, 1);
            PanOffset = new Point(-10000, 0);
        }

        public List<FinancialData> Data
        {
            get { return _data; }
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged("Data");
                }
            }
        }

        public List<FinancialData> Data1
        {
            get { return _data1; }
            set
            {
                if (_data1 != value)
                {
                    _data1 = value;
                    OnPropertyChanged("Data1");
                }
            }
        }

        public Point PanOffset
        {
            get { return _panOffset; }
            set
            {
                if (_panOffset != value)
                {
                    _panOffset = value;
                    OnPropertyChanged("PanOffset");
                }
            }
        }

        public Size Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    OnPropertyChanged("Zoom");
                }
            }
        }

        protected void GenerateData()
        {

            var chartData = new List<FinancialData>();
            var ro = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < 500; i++)
            {
                var dataItem = new FinancialData
                                   {
                                       Date = DateTime.Now.AddDays(i),
                                       Close = ro.Next(0, 670),
                                       Volume = ro.Next(0, 15000000)
                                   };

                chartData.Add(dataItem);
            }
            Data = chartData;
            chartData = new List<FinancialData>();
            for (int i = 0; i < 500; i++)
            {
                var dataItem = new FinancialData
                {
                    Date = DateTime.Now.AddDays(i),
                    Close = ro.Next(0, 670),
                    Volume = ro.Next(0, 15000000)
                };

                chartData.Add(dataItem);
            }
            Data1 = chartData;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class FinancialData
    {
        public DateTime Date { get; set; }

        public double Close { get; set; }

        public long Volume { get; set; }
    }
    
}
