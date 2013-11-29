#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.MVVM
{
    public static class MessageDialogs
    {
        /// <summary>
        ///     设置提醒对话框
        /// </summary>
        /// <param name="header">对话框标题</param>
        /// <param name="okContent">Ok按钮显示内容</param>
        /// <param name="content">显示内容</param>
        /// <param name="fontSize">字号</param>
        /// <param name="width">对话框宽度</param>
        /// <returns>提醒对话框</returns>
        private static DialogParameters SetAlert(
            string header,
            string okContent,
            string content,
            int fontSize,
            int width)
        {
            var alert = new DialogParameters
                {
                    Header = header,
                    OkButtonContent = okContent,
                    Content = new TextBlock
                        {
                            Text = content,
                            FontFamily = new FontFamily("Microsoft YaHei UI"),
                            FontSize = fontSize,
                            TextWrapping = TextWrapping.Wrap,
                            Width = width,
                        },
                    Closed = null,
                };
            return alert;
        }

        /// <summary>
        ///     设置确认对话框
        /// </summary>
        /// <param name="header">对话框标题</param>
        /// <param name="okContent">Ok按钮显示内容</param>
        /// <param name="cancelContent">Cancel按钮显示内容</param>
        /// <param name="content">显示内容</param>
        /// <param name="fontSize">字号</param>
        /// <param name="width">对话框宽度</param>
        /// <param name="closed">关闭对话框后执行的操作</param>
        /// <returns>确认对话框</returns>
        private static DialogParameters SetConfirm(
            string header,
            string okContent,
            string cancelContent,
            string content,
            int fontSize,
            int width,
            EventHandler<WindowClosedEventArgs> closed)
        {
            var confirm = new DialogParameters
                {
                    Header = header,
                    OkButtonContent = okContent,
                    CancelButtonContent = cancelContent,
                    Content = new TextBlock
                        {
                            Text = content,
                            FontFamily = new FontFamily("Microsoft YaHei UI"),
                            FontSize = fontSize,
                            TextWrapping = TextWrapping.Wrap,
                            Width = width,
                        },
                    Closed = closed,
                };
            return confirm;
        }

        #region 消息通知

        /// <summary>
        ///     消息提醒通知
        /// </summary>
        /// <param name="message">消息</param>
        public static void Alert(string message)
        {
            Alert("提醒", message);
        }

        /// <summary>
        ///     消息提醒通知
        /// </summary>
        /// <param name="title"> </param>
        /// <param name="message">消息</param>
        public static void Alert(string title, string message)
        {
            RadWindow.Alert(SetAlert(title, "确认", message, 13, 250));
        }

        /// <summary>
        ///     弹出子窗体需要用户确认是否执行
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="callback">回调函数</param>
        public static void Confirm(string message, EventHandler<WindowClosedEventArgs> callback)
        {
            Confirm("提醒", message, callback);
        }

        /// <summary>
        ///     弹出子窗体需要用户确认是否执行
        /// </summary>
        /// <param name="title"> </param>
        /// <param name="message">消息</param>
        /// <param name="callback">回调函数</param>
        public static void Confirm(string title, string message, EventHandler<WindowClosedEventArgs> callback)
        {
            RadWindow.Confirm(SetConfirm(title, "确认", "取消", message, 13, 250, callback));
        }

        #endregion
    }
}