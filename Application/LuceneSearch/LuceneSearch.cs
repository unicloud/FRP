#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using PanGu;

#endregion

namespace UniCloud.Application.LuceneSearch
{
    /// <summary>
    /// 分词查找
    /// </summary>
    public static class LuceneSearch
    {
        #region 盘古分词搜索
        /// <summary>
        /// 提供搜索的方法
        /// </summary>
        /// <param name="keyword">搜索关键字</param>
        /// <param name="fileType">文档类型</param>
        public static TopDocs PanguQuery(string keyword, string fileType)
        {
            ParallelMultiSearcher multiSearch = IndexManager.GenerateMultiSearcher(fileType);

            if (!multiSearch.GetSearchables().Any())
            {
                return null;
            }
            #region 生成Query语句

            var field = new string[2];
            field[0] = "fileName";
            field[1] = "fileContent";

            var boolQuery = new BooleanQuery();

            if (!string.IsNullOrEmpty(keyword))
            {

                var keywordQuery = new BooleanQuery();
                string queryKeyword = GetKeyWordsSplitBySpace(keyword, new PanGuTokenizer());//对关键字进行分词处理
                #region 查询fileName

                var fileNameParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, field[0], IndexManager.Analyzer);
                fileNameParser.SetDefaultOperator(QueryParser.Operator.AND);
                var fileNameQuery = fileNameParser.Parse(queryKeyword + "~");  // ~ 提供模糊查询
                keywordQuery.Add(fileNameQuery, BooleanClause.Occur.SHOULD);

                #endregion
                #region 查询fileContent

                var fileContentParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_29, field[1], IndexManager.Analyzer);
                fileContentParser.SetDefaultOperator(QueryParser.Operator.AND);
                var fileContentQuery = fileContentParser.Parse(queryKeyword + "~");
                keywordQuery.Add(fileContentQuery, BooleanClause.Occur.SHOULD);

                #endregion
                boolQuery.Add(keywordQuery, BooleanClause.Occur.MUST);
            }
            #endregion

            return multiSearch.Search(boolQuery, null, 1000);
        }

        public static TopDocs Query(string keyword)
        {
            var indexSearcher = IndexManager.GenerateSearcher();
            #region 生成Query语句

            var field = new string[2];
            field[0] = "fileName";
            field[1] = "fileContent";

            var boolQuery = new BooleanQuery();

            //if (!string.IsNullOrEmpty(keyword))
            //{
            var keywordQuery = new BooleanQuery();
            string queryKeyword = GetKeyWordsSplitBySpace(keyword, new PanGuTokenizer());//对关键字进行分词处理
            #region 查询fileName

            var term = new Term(field[0], keyword );
            var fuzzQuery = new FuzzyQuery(term);
            keywordQuery.Add(fuzzQuery, BooleanClause.Occur.SHOULD);
            #endregion
            #region 查询fileContent

            term = new Term(field[1], keyword );
            fuzzQuery = new FuzzyQuery(term);
            keywordQuery.Add(fuzzQuery, BooleanClause.Occur.SHOULD);
            #endregion
            boolQuery.Add(keywordQuery, BooleanClause.Occur.MUST);
            //}
            #endregion

            return indexSearcher.Search(boolQuery, null, 1000);
        }

        /// <summary>
        /// 关键词分词
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="ktTokenizer">采用方式</param>
        /// <returns></returns>
        public static string GetKeyWordsSplitBySpace(string keywords, PanGuTokenizer ktTokenizer)
        {
            var result = new StringBuilder();
            ICollection<WordInfo> words = ktTokenizer.SegmentToWordInfos(keywords);
            foreach (WordInfo word in words)
            {
                if (word == null)
                {
                    continue;
                }
                result.AppendFormat("{0}^{1}.0 ", word.Word, (int)Math.Pow(3, word.Rank));
            }
            return result.ToString().Trim();
        }
        #endregion

    }
}
