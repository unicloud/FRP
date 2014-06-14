#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/09，14:11
// 文件名：App.xaml.cs
// 程序集：UniCloud.Presentation.Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Media;
using Telerik.Windows.Controls;
using UniCloud.Presentation.Localization;

#endregion

namespace UniCloud.Presentation.Shell
{
    public partial class App
    {
        public App()
        {
            Startup += Application_Startup;
            Exit += Application_Exit;
            UnhandledException += Application_UnhandledException;
            StyleManager.ApplicationTheme = new Windows8Theme();
            LocalizationManager.Manager = new LocalizationManager
            {
                ResourceManager = LocalizationResource.ResourceManager
            };
            SetWindows8Palette();

            InitializeComponent();
        }

        /// <summary>
        ///     设置Windows8主题基本属性
        ///     字体、字号、颜色
        /// </summary>
        private static void SetWindows8Palette()
        {
            #region 缺省

            //Windows8Palette.Palette.FontSizeXS = 10;
            //Windows8Palette.Palette.FontSizeS = 11;
            //Windows8Palette.Palette.FontSize = 12;
            //Windows8Palette.Palette.FontSizeL = 14;
            //Windows8Palette.Palette.FontSizeXL = 16;
            //Windows8Palette.Palette.FontSizeXXL = 19;
            //Windows8Palette.Palette.FontSizeXXXL = 24;
            //Windows8Palette.Palette.FontFamily = new FontFamily("Segoe UI");
            //Windows8Palette.Palette.FontFamilyLight = new FontFamily("Segoe UI Light");
            //Windows8Palette.Palette.FontFamilyStrong = new FontFamily("Segoe UI Semibold");

            #endregion

            #region 自定义

            Windows8Palette.Palette.FontSizeXS = 10;
            Windows8Palette.Palette.FontSizeS = 11;
            Windows8Palette.Palette.FontSize = 13;
            Windows8Palette.Palette.FontSizeL = 14;
            Windows8Palette.Palette.FontSizeXL = 16;
            Windows8Palette.Palette.FontSizeXXL = 19;
            Windows8Palette.Palette.FontSizeXXXL = 24;
            Windows8Palette.Palette.FontFamily = new FontFamily("Microsoft YaHei UI");
            Windows8Palette.Palette.FontFamilyLight = new FontFamily("Microsoft YaHei UI Light");
            Windows8Palette.Palette.FontFamilyStrong = new FontFamily("Microsoft YaHei UI");

            #endregion
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.InitParams != null)
            {
                e.InitParams.ToList().ForEach(p => Current.Resources.Add(p.Key, p.Value));
            }
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        private void Application_Exit(object sender, EventArgs e)
        {
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDOM(e));
                var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                MyLog.WriteErrLog(null, errorMsg);
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                var errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }

        public class MyLog
        {
            public static void WriteErrLog(string strErrMod, string strErrDesc)
            {
                StreamWriter sw;
                string strErrLog =  @"F\Logs\Client_Error_Log" + string.Format("{0:yyyyMMdd}", DateTime.Now) + ".txt";   //获取写日志的路径
                if (File.Exists(strErrLog))
                {
                    FileInfo oFile = new FileInfo(strErrLog);
                    if (oFile.Length > 1024000)
                    {
                        oFile.Delete();
                    }
                }
                if (File.Exists(strErrLog))
                {
                    sw = File.AppendText(strErrLog);
                }
                else
                {
                    sw = File.CreateText(strErrLog);
                }
                string strDate = "出错时间:" + string.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                string strErrMoudle = "出错模块:" + strErrMod;
                string strErrDescOut = "错误原因:" + strErrDesc;
                sw.WriteLine(strDate);
                sw.WriteLine(strErrMoudle);
                sw.WriteLine(strErrDescOut);
                sw.WriteLine("===================================================================");
                sw.Flush();
                sw.Close();
            }
        }

    }
}