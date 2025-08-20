using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecsTest;

namespace SecsTest.Tests
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod2()
        {
            int rtn = StringTest.CompareInt(3, 7);
            Assert.AreEqual(10, rtn);
        }
    }
}
