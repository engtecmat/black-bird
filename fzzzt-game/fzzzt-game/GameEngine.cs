using System;
using System.Collections.Generic;

namespace fzzzt_game
{
    /// <summary>
    /// the core game engine for managing and running the Fzzzt game.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// indicates whether the game has started.
        /// </summary>
        private bool _gameState = false;

        /// <summary>
        /// the conveyor belt deck
        /// </summary>
        private List<Card> _deck = new List<Card>();

        /// <summary>
        /// the players, current versio has two players
        /// </summary>
        private List<Player> _players = new List<Player>();

        /// <summary>
        /// a random number generator for picking the chief mechanic
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// the chief mechanic will be randomly picked in the beginning of the game
        /// </summary>
        private Player _chiefMechanic;

        /// <summary>
        /// return the player who is the chief mechanic
        /// </summary>
        /// <returns></returns>
        public Player GetChiefMechanic()
        {
            return _chiefMechanic;
        }

        /// <summary>
        /// determines whether the game has started.
        /// </summary>
        /// <returns><see langword="true"/> if the game has started; otherwise, <see langword="false"/>.</returns>
        public bool IsGameStarted()
        {
            return _gameState;
        }

        /// <summary>
        /// resets the game state to its initial condition.
        /// </summary>
        public void ResetGame()
        {
            _gameState = false;
        }

        /// <summary>
        /// starts the game by transitioning the game state to true.
        /// </summary>
        public void StartGame()
        {
            _gameState = true;
            InitializeDeck();
        }

        /// <summary>
        /// initialize 52 cards
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitializeDeck()
        {
            // create 12 robot cards with 1 power
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Bolt }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Nut, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Bolt_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));

            // create 8 robot cards with 2 power
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Cog, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Oil, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Nut, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Bolt, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Cog, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Oil, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Nut, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Bolt, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));

            // create 8 robot cards with 3 power
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Cog, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Oil, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Nut, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Bolt, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Cog, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Oil, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Nut, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Bolt, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));

            // create 4 robot cards with 4 power
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Cog, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Oil, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Nut, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Bolt, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));

            // create 4 robot cards with 5 power
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Cog, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Oil, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Nut, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            _deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Bolt, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));

            // create 2 robot upgrade card with 8 conveyor belt number
            _deck.Add(new RobotUpgradeCard(Properties.Resources.Robot_Upgrade));
            _deck.Add(new RobotUpgradeCard(Properties.Resources.Robot_Upgrade));

            // create 4 fzzzt cards
            _deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            _deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            _deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            _deck.Add(new FzzztCard(Properties.Resources.Fzzzt));

            //create 10 production unit cards
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut_Oil, 13, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut, 9, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut, 9, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Cog_Nut, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Nut_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Oil, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Oil }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Nut, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Nut }));
            _deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Cog_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Oil }));

            _players.Add(new Player("Player 1", Position.Top));
            _players.Add(new Player("Player 2", Position.Bottom));

            PickChiefMechanic();
        }

        /// <summary>
        /// pick the chief mechanic
        /// </summary>
        private void PickChiefMechanic()
        {
            _chiefMechanic = _players[_random.Next() % 2];
        }

        /// <summary>
        /// get the cards on the deck
        /// </summary>
        /// <returns></returns>
        public List<Card> GetDeck()
        {
            return _deck;
        }

        /// <summary>
        /// get the players, current version has two players
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Player> GetPlayers()
        {
            return _players;
        }
    }
}
