#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/11，12:11
// 文件名：MenuItem.cs
// 程序集：UniCloud.Presentation.Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace UniCloud.Presentation.Shell
{
    /// <summary>
    ///     功能菜单
    /// </summary>
    public class MenuItem : INotifyPropertyChanged
    {
        private readonly MenuItemsCollection _items;
        private bool _isChecked;
        private bool _isEnabled = true;

        public MenuItem()
        {
            _items = new MenuItemsCollection(this);
        }

        /// <summary>
        ///     功能名称
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///     导航URI
        /// </summary>
        public string NavUri { get; set; }

        public string GroupName { get; set; }

        public bool IsCheckable { get; set; }

        public bool IsSeparator { get; set; }

        public string ImageUrl { get; set; }

        public bool StaysOpenOnClick { get; set; }

        public MenuItemsCollection Items
        {
            get { return _items; }
        }

        public MenuItem Parent { get; set; }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (value != _isChecked)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");

                    if (!string.IsNullOrEmpty(GroupName))
                    {
                        if (IsChecked)
                        {
                            UncheckOtherItemsInGroup();
                        }
                        else
                        {
                            IsChecked = true;
                        }
                    }
                }
            }
        }

        public Image Image
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl)) return null;

                return new Image
                {
                    Source = new BitmapImage(new Uri(ImageUrl, UriKind.RelativeOrAbsolute)),
                    Stretch = Stretch.None
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void UncheckOtherItemsInGroup()
        {
            var groupItems = Parent.Items.Where(item => item.GroupName == GroupName);
            foreach (var item in groupItems.Where(item => item != this))
            {
                item._isChecked = false;
                item.OnPropertyChanged("IsChecked");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (null != PropertyChanged)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}