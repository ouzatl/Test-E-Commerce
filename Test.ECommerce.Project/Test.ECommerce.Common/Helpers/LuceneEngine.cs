using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Test.ECommerce.Data.Contract;
using Test.ECommerce.Data.Models;

namespace Test.ECommerce.Common.Helpers
{
    public class LuceneEngine
    {
        private Analyzer _Analyzer;
        private Directory _Directory;
        private IndexWriter _IndexWriter;
        private IndexSearcher _IndexSearcher;
        private Document _Document;
        private QueryParser _QueryParser;
        private Query _Query;
        private string _IndexPath = @"C:\Users\Oğuz\Desktop\Test-E-Commerce\LuceneIndex";

        public LuceneEngine()
        {
            _Analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            _Directory = FSDirectory.Open(_IndexPath);
        }

        public void AddToIndex(IEnumerable values)
        {
            using (_IndexWriter = new IndexWriter(_Directory, _Analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var loopEntity in values)
                {
                    _Document = new Document();

                    foreach (var loopProperty in loopEntity.GetType().GetProperties())
                    {
                        _Document.Add(new Field(loopProperty.Name, loopProperty.GetValue(loopEntity).ToString(), Field.Store.YES, Field.Index.ANALYZED));
                    }

                    _IndexWriter.AddDocument(_Document);
                    _IndexWriter.Optimize();
                    _IndexWriter.Commit();
                }
            }
        }

        //Sadece productName field'ının içindeki kelimeleri bulan search mekanızması.
        //Burası geliştirilebilir.
        public List<ProductContract> SearchProduct(string field, string keyword)
        {
            _QueryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, field, _Analyzer);
            _Query = _QueryParser.Parse(keyword);

            using (_IndexSearcher = new IndexSearcher(_Directory, true))
            {
                var products = new List<ProductContract>();
                var result = _IndexSearcher.Search(_Query, 10);

                foreach (var loopDoc in result.ScoreDocs.OrderBy(s => s.Score))
                {
                    _Document = _IndexSearcher.Doc(loopDoc.Doc);

                    products.Add(new ProductContract() { ProductCode =_Document.Get("Id"), ProductName = _Document.Get("ProductName"), CategoryCode = _Document.Get("CategoryCode")});
                }

                return products;
            }
        }
    }
}
