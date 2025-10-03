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

            Assert.AreEqual(-1, card.GetPointValue());
            Assert.AreEqual(3, card.GetPower());
            Assert.AreEqual(1, card.ConveyorBeltNumber);
        }
    }
}