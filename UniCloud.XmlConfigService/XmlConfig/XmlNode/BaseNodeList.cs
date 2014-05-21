using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace UniCloud.Fleet.XmlConfigs
{
    public class DateNodeList
    {
        private List<DateNode> _DateNodes;

        private DateTime _BeginTime;
        private int _BeginMonth;
        private int _BeginYear;

        private DateTime _EndTime;
        private int _EndMonth;
        private int _EndYear;
        private int _TotalCount;

        public DateNodeList(DateTime dtBegin, DateTime dtEnd)
        {
            _DateNodes = new List<DateNode>();
            this.BeginTime = dtBegin;
            this.EndTime = dtEnd;
        }

        //开始时间
        public DateTime BeginTime
        {
            set
            {
                _BeginTime = value;
                _BeginYear = _BeginTime.Year;
                _BeginMonth = _BeginTime.Month;
            }
        }

       // 结束时间
        public DateTime EndTime
        {
            set
            {
                _EndTime = value;
                _EndYear = _EndTime.Year;
                _EndMonth = _EndTime.Month;

                _TotalCount = (_EndYear - _BeginYear) * 12 + (_EndMonth - _BeginMonth) + 1;
            }
        }

        public void AddNode(DateNode xmlNode)
        {
            _DateNodes.Add(xmlNode);
        }

        public void GetDataNodeRange(DateTime dtBegin, DateTime dtEnd, out int intBeginIndex, out int intEndIndex)
        {
            intBeginIndex = (dtBegin.Year - _BeginYear) * 12 + (dtBegin.Month - _BeginMonth);
            intEndIndex = (dtEnd.Year - _BeginYear) * 12 + (dtEnd.Month - _BeginMonth);
        }

        public int IndexOf(DateNode xmlNode)
        {
            return _DateNodes.IndexOf(xmlNode);
        }

        public DateNode GetItem(int Index)
        {
            if (Index < 0 || Index >= _TotalCount)
            {
                return null;
            }
            else
            {
                return _DateNodes[Index];
            }
        }

        public int Count()
        {
            return _TotalCount;
        }

    }
}
