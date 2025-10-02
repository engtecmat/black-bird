using Microsoft.VisualStudio.TestTools.UnitTesting;
using fzzzt_game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fzzzt_game.Tests
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