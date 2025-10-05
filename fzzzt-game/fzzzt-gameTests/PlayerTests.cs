using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void Should_Get_0_When_No_Cards()
        {
            Player player = new Player("TestPlayer", Position.Top, null, false);

            int autual = player.GetTotalScore();

            Assert.AreEqual(0, autual);
        }

        [TestMethod()]
        public void Should_Get_Minus_0_When_Has_One_Fzzzt_Card()
        {
            Player player = new Player("TestPlayer", Position.Top, null, false);

            player.CardsInHand.Add(new FzzztCard(null));

            int autual = player.GetTotalScore();

            Assert.AreEqual(-1, autual);
        }

        [TestMethod()]
        public void Should_Get_Point_Value_From_Robot_Card()
        {
            Player player = new Player("TestPlayer", Position.Top, null, false);

            player.CardsInHand.Add(new RobotCard(null, 1, 1, 1, null));

            int autual = player.GetTotalScore();

            Assert.AreEqual(1, autual);
        }

        [TestMethod()]
        public void Should_Get_Total_Point_Value_All_Cards()
        {
            Player player = new Player("TestPlayer", Position.Top, null, false);

            player.CardsInHand.Add(new FzzztCard(null));
            player.CardsInHand.Add(new RobotCard(null, 1, 1, 1, null));
            player.CardsInHand.Add(new RobotUpgradeCard(null));

            player.CardsInBid.Add(new RobotCard(null, 1, 1, 1, null));

            player.DiscardPile.Add(new RobotCard(null, 1, 1, 1, null));

            player.Widgets.Add(new Widget
            {
                ProductionUnit = new ProductionUnitCard(null, 13, null),
                RobotCards = new List<Card> { new RobotCard(null, 1, 1, 1, null) },
                IsComplete = true
            });

            player.Widgets.Add(new Widget
            {
                ProductionUnit = new ProductionUnitCard(null, 14, null),
                RobotCards = null
            });

            int autual = player.GetTotalScore();

            Assert.AreEqual(2, autual);
        }
    }
}