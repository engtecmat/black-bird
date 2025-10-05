using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class ProductionUnitCardTests
    {
        [TestMethod()]
        public void Test_Production_Unit_1()
        {
            ISet<ConstructionSymbol> constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut, ConstructionSymbol.Oil }; ;
            ProductionUnitCard card = new ProductionUnitCard(null, 13, constructionSymbols);

            Assert.AreEqual(13, card.PointValue);
            Assert.AreEqual(3, card.ConveyorBeltNumber);
            Assert.AreEqual(constructionSymbols, card.ConstructionSymbols);
        }

        [TestMethod()]
        public void Test_Production_Unit_2()
        {
            ISet<ConstructionSymbol> constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut };
            ProductionUnitCard card = new ProductionUnitCard(null, 9, constructionSymbols);

            Assert.AreEqual(9, card.PointValue);
            Assert.AreEqual(3, card.ConveyorBeltNumber);
            Assert.AreEqual(constructionSymbols, card.ConstructionSymbols);
        }
    }
}