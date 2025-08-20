using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SecsTest;

namespace SecsTest.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string rns = StringTest.CompareString("aaa", "BBB");
            Assert.AreEqual("aaaBBB", rns);
        }
    }
}
