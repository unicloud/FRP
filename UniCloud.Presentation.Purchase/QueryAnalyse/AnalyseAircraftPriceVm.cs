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

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;

#endregion

namespace UniCloud.Presentation.Purchase.QueryAnalyse
{
    [Export(typeof(AnalyseAircraftPriceVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AnalyseAircraftPriceVm : INotifyPropertyChanged
    {
        private List<FinancialData> _data;
        private List<FinancialData> _data1;
        private List<Data> _pieData;
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

        public List<Data> PieData
        {
            get { return _pieData; }
            set
            {
                if (_pieData != value)
                {
                    _pieData = value;
                    OnPropertyChanged("PieData");
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

            for (int i = 0; i < 20; i++)
            {
                var dataItem = new FinancialData
                                   {
                                       Date = DateTime.Now.AddMonths(i),
                                       Close = ro.Next(0, 670),
                                       Volume = ro.Next(0, 15000000)
                                   };

                chartData.Add(dataItem);
            }
            Data = chartData;
            chartData = new List<FinancialData>();
            for (int i = 0; i < 20; i++)
            {
                var dataItem = new FinancialData
                {
                    Date = DateTime.Now.AddMonths(i),
                    Close = ro.Next(0, 670),
                    Volume = ro.Next(0, 15000000)
                };

                chartData.Add(dataItem);
            }
            Data1 = chartData;
            PieData = CreatePieData().ToList();
        }

        private IEnumerable<Data> CreatePieData()
        {
            var pieData = new List<Data>(4)
                          {
                              new Data("Google", 82.35),
                              new Data("Yahoo!", 6.69),
                              new Data("Baidu", 5.12),
                              new Data("Others", 4.71)
                          };

            return pieData;
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

    public class Data
    {
        private DateTime _timeStamp;
        private double _value;
        private string _category;

        public Data(DateTime timeStamp, double value)
        {
            _timeStamp = timeStamp;
            _value = value;
        }

        public Data(string category, double value)
        {
            _category = category;
            _value = value;
        }

        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                _timeStamp = value;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }

}
