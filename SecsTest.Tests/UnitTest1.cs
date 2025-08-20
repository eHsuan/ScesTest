using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SecsTest;

namespace SecsTest.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string rtn = StringTest.CompareString("aaa", "BBB");
            Assert.AreEqual("aaaBBB", rtn);
        }
    }
}
