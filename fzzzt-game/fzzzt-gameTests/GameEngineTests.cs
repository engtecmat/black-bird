using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace fzzzt_game.Tests
{
    [TestClass()]
    public class GameEngineTests
    {
        [TestMethod()]
        public void Test_Beginning_State()
        {
            GameEngine engine = new GameEngine();
            engine.GameView = new Mock<GameView>().Object;
            engine.StartGame();

            Assert.IsTrue(engine.GameState);
            Assert.AreEqual(38, engine.GetDeck().Count);
            Assert.AreEqual(2, engine.Players.Count);
        }
    }
}