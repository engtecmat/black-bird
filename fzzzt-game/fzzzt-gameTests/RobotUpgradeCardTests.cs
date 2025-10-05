using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class RobotUpgradeCardTests
    {
        [TestMethod()]
        public void RobotUpgradeCardTest()
        {
            RobotUpgradeCard card = new RobotUpgradeCard(null);

            Assert.AreEqual(0, card.PointValue);
            Assert.AreEqual(0, card.GetPower());
            Assert.AreEqual(8, card.ConveyorBeltNumber);
        }
    }
}