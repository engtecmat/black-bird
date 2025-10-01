using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace fzzzt_game
{
    /// <summary>
    /// the core game engine for managing and running the Fzzzt game.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// a global card back
        /// </summary>
        public static Bitmap CardBack = Properties.Resources.Fzzzt_Card_Back;

        /// <summary>
        /// indicates whether the game has started.
        /// </summary>
        private bool _gameState = false;

        /// <summary>
        /// the conveyor belt deck
        /// </summary>
        private List<Card> _deck = new List<Card>();

        /// <summary>
        /// faced up cards on the conveyor belt
        /// </summary>
        private List<Card> _facedUpCards = new List<Card>();

        /// <summary>
        /// allowed faced up card count
        /// </summary>
        private int _allowedFacedUpCardCount;

        /// <summary>
        /// current auction cards on the conveyor belt
        /// </summary>
        private List<Card> _auctionCards = new List<Card>();

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
        /// use game view to update UI
        /// </summary>
        private GameView _gameView;

        /// <summary>
        /// auction state
        /// </summary>
        private bool _isAuctionStarted;

        /// <summary>
        /// build game engine with game view
        /// </summary>
        /// <param name="gameView"></param>
        public GameEngine(GameView gameView)
        {
            _gameView = gameView;
        }

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

            _deck.Clear();
            _allowedFacedUpCardCount = 0;
            _auctionCards.Clear();
            _players.Clear();
            _facedUpCards.Clear();
            _chiefMechanic = null;

            _gameView.Reset();
        }

        /// <summary>
        /// starts the game by transitioning the game state to true.
        /// </summary>
        public void StartGame()
        {
            _gameState = true;
            InitializeDeck();
            _gameView.EnpowerChiefMechanic();
            _gameView.DisplayBidButton();
            StartAuctionIfChiefMechanicIsAI();
        }

        /// <summary>
        /// start an auciton immediately if chief mechanic is AI
        /// </summary>
        private void StartAuctionIfChiefMechanicIsAI()
        {
            if (_players.FindIndex(player => _chiefMechanic == player && player.IsAI()) == -1)
            {
                return;
            }
            StartAuction();
            _gameView.FlipCards();
            _gameView.Bid(new CardContext(_chiefMechanic.GetCardsInHand().First(), _chiefMechanic));
            _chiefMechanic.Bid();
            _gameView.UpdateMessag(_chiefMechanic.GetName() + " bid = " + _chiefMechanic.IsBid());
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

            Card playerOnePower1 = _deck.Find(c => c.GetPower() == 1);
            Card playerOnePower2 = _deck.Find(c => c.GetPower() == 2);
            Card playerOnePower3 = _deck.Find(c => c.GetPower() == 3);
            _deck.Remove(playerOnePower1);
            _deck.Remove(playerOnePower2);
            _deck.Remove(playerOnePower3);

            Card playerTwoPower1 = _deck.Find(c => c.GetPower() == 1);
            Card playerTwoPower2 = _deck.Find(c => c.GetPower() == 2);
            Card playerTwoPower3 = _deck.Find(c => c.GetPower() == 3);

            _deck.Remove(playerTwoPower1);
            _deck.Remove(playerTwoPower2);
            _deck.Remove(playerTwoPower3);

            _players.Add(new Player("AI Player", Position.Top, Properties.Resources.Mechanic_One, new HashSet<Card> { playerOnePower1, playerOnePower2, playerOnePower3 }, true));
            _players.Add(new Player("Tamati", Position.Bottom, Properties.Resources.Mechanic_Two, new HashSet<Card> { playerTwoPower1, playerTwoPower2, playerTwoPower3 }, false));

            PickChiefMechanic();
        }

        /// <summary>
        /// pick the chief mechanic
        /// </summary>
        private void PickChiefMechanic()
        {
            _chiefMechanic = _players[_random.Next() % 2];
            _gameView.UpdateMessag(_chiefMechanic.GetName() + " is the chief Mechanic.");
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

        /// <summary>
        /// get the faced up cards on the table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Card> GetFacedUpCards()
        {
            return _facedUpCards;
        }

        /// <summary>
        /// get the auction cards, which are the first 8 cards on the deck
        /// </summary>
        /// <returns>eight cards</returns>
        public List<Card> GetAuctionCards()
        {
            _auctionCards = _deck.GetRange(0, 8);
            _deck.RemoveRange(0, 8);
            return _auctionCards;
        }

        /// <summary>
        /// check if the card is the first card on the conveyor belt
        /// </summary>
        /// <param name="clickedCard"></param>
        /// <returns></returns>
        public bool IsFirstCardOnConveyorBelt(Card clickedCard)
        {
            return _auctionCards.FindIndex(c => c == clickedCard) == 0;
        }

        /// <summary>
        /// add a faced up card to the list
        /// </summary>
        /// <param name="card"></param>
        public void AddFacedUpCard(Card card)
        {
            _facedUpCards.Add(card);
            UpdateAllowedFacedUpCardCount();
            _gameView.UpdateMessag("face-up count:" + _facedUpCards.Count);
        }

        /// <summary>
        /// update the allowed faced up card count
        /// </summary>
        public void UpdateAllowedFacedUpCardCount()
        {
            if (_facedUpCards.Count == 0)
            {
                _allowedFacedUpCardCount = 0;
                return;
            }

            _allowedFacedUpCardCount = _facedUpCards.First().GetConveyorBeltNumber();
            _gameView.UpdateMessag("allowed count:" + _allowedFacedUpCardCount);
        }

        /// <summary>
        /// get the allowed faced up card count
        /// </summary>
        /// <returns></returns>
        public int GetAllowedFacedUpCardCount()
        {
            return _allowedFacedUpCardCount;
        }

        /// <summary>
        /// remove a card the face-up cards
        /// </summary>
        /// <param name="card"></param>
        public void RemoveFacedUpCard(Card card)
        {
            _facedUpCards.Remove(card);

            _gameView.UpdateMessag("face-up count:" + _facedUpCards.Count);
        }


        /// <summary>
        /// check if the image is a card back
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>true or false</returns>
        public static bool IsCardBack(Image image)
        {
            return CardBack == image;
        }

        /// <summary>
        /// check if facing up is allowed
        /// </summary>
        /// <returns></returns>
        public bool FacingUpAllowed()
        {
            return _facedUpCards.Count < _allowedFacedUpCardCount;
        }

        /// <summary>
        /// start an auction
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartAuction()
        {
            _isAuctionStarted = true;
            _gameView.PrepareConveyorBelt();
            _gameView.HideStartAuctionButtons();
        }

        /// <summary>
        /// reutrn auction state
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsAuctionStarted()
        {
            return _isAuctionStarted;
        }

        /// <summary>
        /// human player is bidding
        /// </summary>
        public void Bid()
        {
            Player humanPlayer = _players.Find(player => !player.IsAI());
            humanPlayer.Bid();
            _gameView.UpdateMessag(humanPlayer.GetName() + " bid = " + humanPlayer.IsBid());

            FindWinnerIfBothPlayersBid();
        }

        /// <summary>
        /// find the winner if both players have bid
        /// </summary>
        private void FindWinnerIfBothPlayersBid()
        {
            if (_players.FindIndex(player => !player.IsBid()) != -1)
            {
                return;
            }
            int winnerPower = 0;
            Player winner = null;
            foreach (Player player in _players)
            {
                int totalPower = player.GetTotalPowerInBid();
                if (totalPower > winnerPower)
                {
                    winnerPower = totalPower;
                    winner = player;
                }
            }

            Card auctionedCard = _facedUpCards.First();

            RemoveFacedUpCard(auctionedCard);

            if (auctionedCard is RobotCard)
            {
                winner.AddCardToDiscardPile(auctionedCard);

                _gameView.UpdateMessag(winner.GetName() + " wins a Robot Card with power " + winnerPower);
                _gameView.UpdateMessag(winner.GetName() + " discard pile  = " + winner.GetDiscardPile().Count);
                return;
            }

            if (auctionedCard is ProductionUnitCard)
            {
                winner.AddCardToProductionUnits(auctionedCard);

                _gameView.UpdateMessag(winner.GetName() + " wins a Production Unit with power " + winnerPower);
                _gameView.UpdateMessag(winner.GetName() + " discard pile  = " + winner.GetProductionUnits().Count);
                return;
            }
        }
    }
}
