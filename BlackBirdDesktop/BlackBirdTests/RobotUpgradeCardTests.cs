using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackBird.Tests
{
    [TestClass()]
    public class RobotUpgradeCardTests
    {
        [TestMethod()]
        public void RobotUpgradeCardTest()
        {
            RobotUpgradeCard card = new RobotUpgradeCard(null);

            Assert.AreEqual(0, card.PointValue);
            Assert.AreEqual(0, card.Power);
            Assert.AreEqual(8, card.ConveyorBeltNumber);
        }
    }
}