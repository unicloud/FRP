using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
