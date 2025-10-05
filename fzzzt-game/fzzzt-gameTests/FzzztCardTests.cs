using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class FzzztCardTests
    {
        [TestMethod()]
        public void FzzztCardTest()
        {
            FzzztCard card = new FzzztCard(null);

            Assert.AreEqual(-1, card.PointValue);
            Assert.AreEqual(3, card.Power);
            Assert.AreEqual(1, card.ConveyorBeltNumber);
        }
    }
}