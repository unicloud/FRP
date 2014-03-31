#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/27 9:25:51
// 文件名：DropIndicationDetails
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/27 9:25:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Input
{
    public class DropIndicationDetails : ViewModelBase
    {
        private object _currentDraggedItem;
        private string _currentDropPosition;
        private object _currentDraggedOverItem;

        public object CurrentDraggedOverItem
        {
            get
            {
                return _currentDraggedOverItem;
            }
            set
            {
                if (_currentDraggedOverItem != value)
                {
                    _currentDraggedOverItem = value;
                    OnPropertyChanged("CurrentDraggedOverItem");
                }
            }
        }

        public int DropIndex { get; set; }

        public string CurrentDropPosition
        {
            get
            {
                return _currentDropPosition;
            }
            set
            {
                if (_currentDropPosition != value)
                {
                    _currentDropPosition = value;
                    OnPropertyChanged("CurrentDropPosition");
                }
            }
        }

        public object CurrentDraggedItem
        {
            get
            {
                return _currentDraggedItem;
            }
            set
            {
                if (_currentDraggedItem != value)
                {
                    _currentDraggedItem = value;
                    OnPropertyChanged("CurrentDraggedItem");
                }
            }
        }

        public static string ConverDropPositionToString(DropPosition dropPosition)
        {
            switch (dropPosition)
            {
                case DropPosition.After:
                    return "到后面";
                case DropPosition.Before:
                    return "到前面";
                default:
                    return "到里面";
            }
        }
    }
}
