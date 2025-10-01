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
    public class GameEngineTests
    {
        [TestMethod()]
        public void Test_Beginning_State()
        {
            GameEngine engine = new GameEngine(null);
            engine.StartGame();

            Assert.AreEqual(10, engine.GetDeck().FindAll(c => c is RobotCard && c.GetPower() == 1).Count);
            Assert.AreEqual(6, engine.GetDeck().FindAll(c => c is RobotCard && c.GetPower() == 2).Count);
            Assert.AreEqual(6, engine.GetDeck().FindAll(c => c is RobotCard && c.GetPower() == 3).Count);
            Assert.AreEqual(4, engine.GetDeck().FindAll(c => c is RobotCard && c.GetPower() == 4).Count);
            Assert.AreEqual(4, engine.GetDeck().FindAll(c => c is RobotCard && c.GetPower() == 5).Count);
            Assert.AreEqual(2, engine.GetDeck().FindAll(c => c is RobotUpgradeCard card && card.GetConveyorBeltNumber() == 8).Count);
            Assert.AreEqual(4, engine.GetDeck().FindAll(c => c is FzzztCard card && card.GetConveyorBeltNumber() == 1).Count);
            Assert.AreEqual(10, engine.GetDeck().FindAll(c => c is ProductionUnitCard card && card.GetConveyorBeltNumber() == 3).Count);
            Assert.AreEqual(46, engine.GetDeck().Count);

            Assert.AreEqual(2, engine.GetPlayers().Count);
        }
    }
}