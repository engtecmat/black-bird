using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BlackBird.Tests
{
    [TestClass()]
    public class RobotCardTests
    {
        [TestMethod()]
        public void Test_RobotCard_1()
        {
            ISet<ConstructionSymbol> constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut };
            RobotCard card = new RobotCard(null, 4, 2, 1, constructionSymbols);

            Assert.AreEqual(4, card.ConveyorBeltNumber);
            Assert.AreEqual(2, card.PointValue);
            Assert.AreEqual(1, card.Power);
            Assert.AreEqual(constructionSymbols, card.ConstructionSymbols);

        }

        [TestMethod()]
        public void Test_RobotCard_2()
        {
            ISet<ConstructionSymbol> constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil };
            RobotCard card = new RobotCard(null, 3, 1, 2, constructionSymbols);

            Assert.AreEqual(3, card.ConveyorBeltNumber);
            Assert.AreEqual(1, card.PointValue);
            Assert.AreEqual(2, card.Power);
            Assert.AreEqual(constructionSymbols, card.ConstructionSymbols);
        }
    }
}