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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Calendar;
using ViewModelBase = UniCloud.Presentation.MVVM.ViewModelBase;

#endregion

namespace UniCloud.Presentation.Portal.Manager
{
    [Export(typeof(ManagerPortalVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManagerPortalVm : ViewModelBase
    {
        public ManagerPortal CurrentManagerPortal
        {
            get { return ServiceLocator.Current.GetInstance<ManagerPortal>(); }
        }

        public override void LoadData()
        {
            //throw new System.NotImplementedException();
        }

        public ManagerPortalVm()
        {
            InitAircraftCostData();
            InitStructureDate();
            InitCalendarData();
            InitProjectData();
        }

        #region 飞机引进成本
        private Size _zoom = new Size(1, 1);//滚动条的对应
        public Size Zoom
        {
            get { return _zoom; }
            set
            {
                if (Zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged("Zoom");
                }
            }
        }

        private Point _panOffset; //滚动条的滑动
        public Point PanOffset
        {
            get { return _panOffset; }
            set
            {
                if (PanOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged("PanOffset");
                }
            }
        }

        private List<AircraftCost> _aircraftCosts;
        public List<AircraftCost> AircraftCosts
        {
            get { return _aircraftCosts; }
            set
            {
                if (_aircraftCosts != value)
                {
                    _aircraftCosts = value;
                    RaisePropertyChanged("AircraftCosts");
                }
            }
        }
        private void InitAircraftCostData()
        {
            AircraftCosts = new List<AircraftCost>
                          {
                              new AircraftCost{DateTime =new DateTime(2011,1,1),Purchase=20000000,Lease=10000000,Maintain = 200000},
                              new AircraftCost{DateTime =new DateTime(2012,1,1),Purchase=40000000,Lease=20000000 ,Maintain = 200000},
                              new AircraftCost{DateTime =new DateTime(2013,1,1),Purchase=50000000,Lease=40000000 ,Maintain = 250000},
                              new AircraftCost{DateTime =new DateTime(2014,1,1),Purchase=90000000,Lease=60000000 ,Maintain = 300000},
                          };
        }
        #endregion

        #region 机队分析
        private IEnumerable<FleetAircraftTypeComposition> _fleetAircraftTypeCollection;
        /// <summary>
        ///     机型饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftTypeCollection
        {
            get { return _fleetAircraftTypeCollection; }
            set
            {
                if (Equals(_fleetAircraftTypeCollection, value)) return;
                _fleetAircraftTypeCollection = value;
                RaisePropertyChanged(() => FleetAircraftTypeCollection);
            }
        }

        private IEnumerable<FleetAircraftTypeComposition> _fleetAircraftImportTypeCollection;
        /// <summary>
        ///     机型饼图的数据集合（指定时间点）
        /// </summary>
        public IEnumerable<FleetAircraftTypeComposition> FleetAircraftImportTypeCollection
        {
            get { return _fleetAircraftImportTypeCollection; }
            set
            {
                if (Equals(_fleetAircraftImportTypeCollection, value)) return;
                _fleetAircraftImportTypeCollection = value;
                RaisePropertyChanged(() => FleetAircraftImportTypeCollection);
            }
        }
        private void InitStructureDate()
        {
            var types = new List<FleetAircraftTypeComposition>
                        {
                             new FleetAircraftTypeComposition
                            {
                                AircraftRegional="A319",
                                AirNum=16,
                                AirTt="16架，占23%",
                                Color = "#FF339933"
                            },
                             new FleetAircraftTypeComposition
                            {
                                AircraftRegional="A320",
                                AirNum=31,
                                AirTt="31架，占44%",
                                Color = "#FF8CBF26"
                            },
                            new FleetAircraftTypeComposition
                            {
                                AircraftRegional="A321",
                                AirNum=19,
                                AirTt="19架，占27%",
                                Color = "#FFF09609"
                            },
                            new FleetAircraftTypeComposition
                            {
                                AircraftRegional="A330-200",
                                AirNum=4,
                                AirTt="4架，占6%",
                                Color = "#FFE671B8"
                            },
                        };
            FleetAircraftTypeCollection = types;

            types = new List<FleetAircraftTypeComposition>
                        {
                             new FleetAircraftTypeComposition
                            {
                                AircraftRegional="购买",
                                AirNum=24,
                                AirTt="24架，占46%",
                                Color = "#FF339933"
                            },
                            new FleetAircraftTypeComposition
                            {
                                AircraftRegional="租赁",
                                AirNum=28,
                                AirTt="28架，占54%",
                                Color = "#FF8CBF26"
                            },
                        };
            FleetAircraftImportTypeCollection = types;
        }
        #endregion

        #region 日程安排
        EventsCollection _calendars;
        public EventsCollection Calendars
        {
            get
            {
                return _calendars;
            }
            set
            {
                if (_calendars != value)
                {
                    _calendars = value;
                    RaisePropertyChanged("Calendars");
                }
            }
        }

        private void InitCalendarData()
        {
            Calendars = new EventsCollection
                        {
                            new Event
                            {
                                Day = 2,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 11.15 AM",
                                Important=true
                            },
                            new Event
                            {
                                Day = 2,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 4.00 PM",
                            },
                            new Event
                            {
                                Day = 2,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 1.00 PM"
                            },

                            new Event
                            {
                                Day = 3,
                                Title = "What's new in WCF 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Grace Becerra; Start Time - 10.00 AM"
                            },
                            new Event
                            {
                                Day = 3,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 11.15 AM"
                            },
                            new Event
                            {
                                Day = 3,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 4.00 PM"
                            },

                            new Event
                            {
                                Day = 4,
                                Title = "What's new in WCF 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Grace Becerra; Start Time - 10.00 AM"
                            },
                            new Event
                            {
                                Day = 4,
                                Title = "Multimedia in Silverlight 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Jeremy Boatner; Start Time - 12.00 PM"
                            },

                            new Event
                            {
                                Day = 5,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 11.15 AM"
                            },
                            new Event
                            {
                                Day = 5,
                                Title = "Multimedia in Silverlight 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Jeremy Boatner; Start Time - 2.00 PM"
                            },
                            new Event
                            {
                                Day = 5,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 4.00 PM"
                            },

                            new Event
                            {
                                Day = 8,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 9:45 AM"
                            },
                            new Event
                            {
                                Day = 8,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 2.00 PM"
                            },
                            new Event
                            {
                                Day = 8,
                                Title = "Multimedia in Silverlight 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Jeremy Boatner; Start Time - 4.00 PM"
                            },

                            new Event
                            {
                                Day = 11,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 11.15 AM"
                            },
                            new Event
                            {
                                Day = 11,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 4.00 PM"
                            },

                            new Event
                            {
                                Day = 14,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 11.45 PM"
                            },

                            new Event
                            {
                                Day = 14,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 1.15 PM",
                                Important = true
                            },
                            new Event
                            {
                                Day = 14,
                                Title = "Multimedia in Silverlight 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Jeremy Boatner; Start Time - 3.00 PM"
                            },


                            new Event
                            {
                                Day = 15,
                                Title = " Windows Phone 7 Development",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Brenda Smith; Start Time - 12:00 AM"
                            },
                            new Event
                            {
                                Day = 15,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 1.00 PM"
                            },

                            new Event
                            {
                                Day = 17,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 11.15 AM"
                            },
                            new Event
                            {
                                Day = 17,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 1:30 PM"
                            },
                            new Event
                            {
                                Day = 17,
                                Title = "What's new in WCF 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Grace Becerra; Start Time - 3.00 PM"
                            },

                            new Event
                            {
                                Day = 24,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 12.00 AM"
                            },
                            new Event
                            {
                                Day = 24,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 2.15 PM"
                            },
                            new Event
                            {
                                Day = 24,
                                Title = " Windows Phone 7 Development",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Brenda Smith; Start Time - 5:00 PM"
                            },

                            new Event
                            {
                                Day = 25,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 10.00 AM",
                                Important = true
                            },
                            new Event
                            {
                                Day = 25,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 1.00 PM"
                            },
                            new Event
                            {
                                Day = 25,
                                Title = " Windows Phone 7 Development",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Brenda Smith; Start Time - 3:00 PM"
                            },

                            new Event
                            {
                                Day = 27,
                                Title = "What's new in WCF 4",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Grace Becerra; Start Time - 10.00 AM"
                            },
                            new Event
                            {
                                Day = 27,
                                Title = "Blend For Silverlight Developers",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Tom Black; Start Time - 2.00 PM"
                            },

                            new Event
                            {
                                Day = 28,
                                Title = "Integrating WPF and WCF",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Wildermuth; Start Time - 1.00 PM"
                            },
                            new Event
                            {
                                Day = 28,
                                Title = "The Future Of Web Development",
                                Company = "Telerik Inc - Boston, USA",
                                Description = "Speaker: Tom Black; Start Time - 2:00 PM"
                            },
                            new Event
                            {
                                Day = 28,
                                Title = " Windows Phone 7 Development",
                                Company = "Telerik Inc - Texas, USA",
                                Description = "Speaker: Brenda Smith; Start Time - 4:00 PM"
                            },
                        };
            EventDayTemplateSelector.Events = Calendars;
        }

        public void CalendarSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = (CurrentManagerPortal.Resources["EventsView"] as CollectionViewSource);
            if (c != null)
            {
                c.View.Refresh();
                CurrentManagerPortal.EmptyContent.Visibility = c.View.IsEmpty ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        #endregion

        #region 项目进度
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
        private void InitProjectData()
        {
            CountryRevenues = new List<CountryRevenue>
                              {
                                  new CountryRevenue{Country = "项目1",Actual=97,Target=100,Color="#FFCCCCCC"},
                                  new CountryRevenue{Country = "项目2",Actual=80,Target=100,Color="#FFCCCCCC"},
                                  new CountryRevenue{Country = "项目3",Actual=70,Target=100,Color="#FFF90202"},
                              };
        }
        #endregion
    }
    #region 飞机成本
    public class AircraftCost
    {
        public string Aircraft { get; set; } //飞机相关的名称
        public DateTime DateTime { get; set; } //时间点
        public decimal Purchase { get; set; } //采购
        public decimal Lease { get; set; } //租赁
        public decimal Maintain { get; set; } //维修
        public string AircraftColor { get; set; } //飞机数的颜色
        public string SeatColor { get; set; } //座位数的颜色
        public string LoadColor { get; set; } //商载量的颜色
    }
    #endregion

    #region 机队结构
    public class FleetAircraftTypeComposition
    {
        public string AircraftRegional { get; set; }
        public decimal AirNum { get; set; }
        public string AirTt { get; set; }
        public string Color { get; set; }
    }
    #endregion

    #region 项目进度
    public class CountryRevenue
    {
        public string Country { get; set; }

        public double Actual { get; set; }

        public double Target { get; set; }

        public string Color { get; set; }
    }
    #endregion

    #region 日程安排
    public class Event : INotifyPropertyChanged
    {
        public Event()
        {
            _date = DateTime.Now;
        }

        public int Day
        {
            get
            {
                return Date.Day;
            }
            set
            {
                DateTime today = DateTime.Now;
                Date = new DateTime(today.Year, today.Month, value);
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                if (_date != value)
                {
                    _date = value;
                    FormatedDate = value.ToString("dddd, MMMM dd");
                    OnPropertyChanged("Date");
                    OnPropertyChanged("Day");
                }
            }
        }

        private string _formatedDate;
        public string FormatedDate
        {
            get
            {
                return _formatedDate;
            }
            protected set
            {
                if (_formatedDate != value)
                {
                    _formatedDate = value;
                    OnPropertyChanged("FormatedDate");
                }
            }
        }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string _company;
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                if (_company != value)
                {
                    _company = value;
                    OnPropertyChanged("Company");
                }
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private bool _important;
        public bool Important
        {
            get { return _important; }
            set
            {
                if (_important != value)
                {
                    _important = value;
                    OnPropertyChanged("Important");
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class EventsCollection : ObservableCollection<Event>
    {
    }

    public class EventDayTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var content = item as CalendarButtonContent;

            //Some days are special.
            if (EventsCollection.Any(e => content != null && e.Date == content.Date))
            {
                if (EventsCollection.Any(e => content != null && e.Date == content.Date && e.Important))
                {
                    return ImportantTemplate;
                }
                return EventTemplate;
            }

            return DefaultTemplate;
        }

        public static EventsCollection Events;
        public EventsCollection EventsCollection
        {
            get { return Events; }
            set { }
        }
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate ImportantTemplate { get; set; }
    }

    #endregion
}
