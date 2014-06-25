#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace Unicloud.DocumentIndex.Tests
{
    [TestClass]
    public class DocumentIndexTests
    {
        [TestMethod]
        public void TestMethod()
        {
            var documentIndex = new DocumentIndexService.DocumentIndex();
            documentIndex.UpdateIndex();
        }
    }
}