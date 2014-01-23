#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/23 16:08:21
// 文件名：ResultHighlighter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/23 16:08:21
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using PanGu;
using PanGu.HighLight;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch.LuceneSearch
{
    public static class ResultHighlighter
    {
        private const String Start = "|~S~|";
        private const String End = "|~E~|";
        private static readonly Highlighter Highlighter;

        static ResultHighlighter()
        {
            var simpleHTMLFormatter = new SimpleHTMLFormatter(Start, End);
            Highlighter = new Highlighter(simpleHTMLFormatter, new Segment()) { FragmentSize = 20 };
        }

        public static string HighlightContent(string keyword, string content)
        {
            return Highlighter.GetBestFragment(keyword, content);
        }
    }
}
