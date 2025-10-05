using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class MechanicCardTests
    {
        [TestMethod()]
        public void Test_Mechanic_Play_1()
        {
            MechanicCard card = new MechanicCard(null, 1);

            Assert.AreEqual(0, card.PointValue);
            Assert.AreEqual(0, card.Power);
            Assert.AreEqual(1, card.PlayNumber
                );
        }

        [TestMethod()]
        public void Test_Mechanic_Play_2()
        {
            MechanicCard card = new MechanicCard(null, 2);

            Assert.AreEqual(0, card.PointValue);
            Assert.AreEqual(0, card.Power);
            Assert.AreEqual(2, card.PlayNumber);
        }
    }
}