#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/31 16:14:13
// 文件名：DoubleClick
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/31 16:14:13
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

#endregion

namespace UniCloud.Presentation.Input
{
    public delegate void MouseLeftDoubleDownEventHandler(object sender, MouseButtonEventArgs e);
    public delegate void MouseLeftOnceDownEventHandler(object sender, MouseButtonEventArgs e);

    /// <summary>
    /// 定义了双击事件的类
    /// </summary>
    public class DoubleClick
    {
        /// <summary>
        /// 双击事件定时器
        /// </summary>
        private DispatcherTimer _doubleClickTimer;

        /// <summary>
        /// 是否单击
        /// </summary>
        private bool _isOnceClick;

        /// <summary>
        /// 双击事件
        /// </summary>
        public MouseLeftDoubleDownEventHandler MouseLeftDoubleDown;

        /// <summary>
        /// 单击事件
        /// </summary>
        public MouseLeftOnceDownEventHandler MouseLeftOnceDown;

        /// <summary>
        /// 拥有双击事件的UI
        /// </summary>
        private readonly UIElement _owner;

        /// <summary>
        /// 实例化DoubleClick
        /// </summary>
        /// <param name="owner">具有双击事件的UI</param>
        public DoubleClick(UIElement owner)
        {
            _owner = owner;
            BindEvent();
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void BindEvent()
        {
            _owner.MouseLeftButtonDown += (OwnerMouseLeftButtonDown);
            var timer = new DispatcherTimer {Interval = (new TimeSpan(0, 0, 0, 0, 200))};
            //设置单击事件
            _doubleClickTimer = timer;
            _doubleClickTimer.Tick += (DoubleClickTimerTick);
        }

        private void DoubleClickTimerTick(object sender, EventArgs e)
        {
            _isOnceClick = false;
            _doubleClickTimer.Stop();
        }

        private void OwnerMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isOnceClick)
            {
                _isOnceClick = true;
                _doubleClickTimer.Start();
                MouseLeftOnceDown(sender, e);
            }
            else
            {
                MouseLeftDoubleDown(sender, e);
            }
        }
    }
}
