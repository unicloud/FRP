#region 命名空间

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

#endregion

namespace UniCloud.Application.LuceneSearch
{
    /// <summary>
    /// 索引管理
    /// </summary>
    public static class IndexManager
    {
        #region private fields

        private static string _indexStorePath = "E:\\Indexs\\";
        private static ParallelMultiSearcher _multiSearch;//索引搜索器
        private static IndexSearcher _searcher;
        private static readonly Analyzer analyzer = new PanGuAnalyzer();  //读取索引器
        private static IndexWriter _indexWriter;
        #endregion

        #region Properties
        public static Analyzer Analyzer
        {
            get { return analyzer; }
        }
        public static ParallelMultiSearcher MultiSearch
        {
            get { return _multiSearch; }
        }

        public static IndexSearcher IndexSearcher
        {
            get { return _searcher; }
        }
        public static IndexWriter IndexWriter
        {
            get { return _indexWriter ?? GenerateWriter(); }
        }

        public static int MaxMergeFactor
        {
            get
            {
                if (_indexWriter != null)
                {
                    return _indexWriter.GetMergeFactor();
                }
                return 0;
            }

            set
            {
                if (_indexWriter != null)
                {
                    _indexWriter.SetMergeFactor(value);
                }
            }
        }

        public static int MaxMergeDocs
        {
            get
            {
                if (_indexWriter != null)
                {
                    return _indexWriter.GetMaxMergeDocs();
                }
                return 0;
            }

            set
            {
                if (_indexWriter != null)
                {
                    _indexWriter.SetMaxMergeDocs(value);
                }
            }
        }

        public static int MinMergeDocs
        {
            get
            {
                if (_indexWriter != null)
                {
                    return _indexWriter.GetMaxBufferedDocs();
                }
                return 0;
            }

            set
            {
                if (_indexWriter != null)
                {
                    _indexWriter.SetMaxBufferedDocs(value);
                }
            }
        }


        #endregion

        #region public methods

        /// <summary>
        /// 设置索引位置
        /// </summary>
        /// <param name="path">索引存储位置</param>
        public static void SetIndexStorePath(string path)
        {
            _indexStorePath = path;
        }

        /// <summary>
        /// 初始化IndexSearcher
        /// </summary>
        /// <returns>返回IndexSearcher实例</returns>
        public static IndexSearcher GenerateSearcher()
        {
            var dirInfo = System.IO.Directory.CreateDirectory(_indexStorePath);
            var directory = FSDirectory.Open(dirInfo);
            _searcher = new IndexSearcher(directory, true);
            _indexStorePath = "E:\\Indexs\\";
            return _searcher;
        }

        /// <summary>
        /// 初始化IndexWriter
        /// </summary>
        /// <returns>返回IndexWriter实例</returns>
        public static IndexWriter GenerateWriter()
        {
            var dirInfo = System.IO.Directory.CreateDirectory(_indexStorePath);
            var directory = FSDirectory.Open(dirInfo);
            try
            {
                _indexWriter = new IndexWriter(directory, analyzer, false, IndexWriter.MaxFieldLength.LIMITED); //false则在已有索引基础上进行追加
            }
            catch (System.Exception)
            {
                _indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.LIMITED);
            }
            _indexStorePath = "E:\\Indexs\\";
            return _indexWriter;
        }

        /// <summary>
        /// 初始化MultiSearcher
        /// </summary>
        /// <returns>返回MultiSearcher实例</returns>
        public static ParallelMultiSearcher GenerateMultiSearcher(string fileType)
        {
            IndexSearcher[] searchs;
            if (!string.IsNullOrEmpty(fileType))
            {
                List<string> types = fileType.Split(',').ToList();
                for (int i = types.Count - 1; i >= 0; i--)
                {
                    var temp = types[i];
                    if (!System.IO.Directory.Exists("E:\\Indexs\\" + temp))
                    {
                        types.Remove(temp);
                    }
                }
                searchs = new IndexSearcher[types.Count];
                for (int i = 0; i < types.Count; i++)
                {
                    SetIndexStorePath("E:\\Indexs\\" + types[i]);
                    searchs[i] = GenerateSearcher();
                }
            }
            else
            {
                DirectoryInfo dir = System.IO.Directory.CreateDirectory(_indexStorePath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                searchs = new IndexSearcher[dirs.Length];
                for (int i = 0; i < dirs.Length; i++)
                {
                    SetIndexStorePath("E:\\Indexs\\" + dirs[i].Name);
                    searchs[i] = GenerateSearcher();
                }
            }
            _multiSearch = new ParallelMultiSearcher(searchs);
            return _multiSearch;
        }

        #endregion
    }
}
