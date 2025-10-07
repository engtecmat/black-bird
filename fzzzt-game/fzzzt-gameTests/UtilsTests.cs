using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackBird;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBird.Tests
{
    [TestClass()]
    public class UtilsTests
    {
        [TestMethod()]
        public void GenerateIndicesTest()
        {
            Utils.GenerateIndices(6, 6).ForEach(i => Console.WriteLine(i));
        }
    }
}