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
        private List<Card> _cardsInConveyorBelt = new List<Card>();

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
        /// auction state
        /// </summary>
        private bool _isAuctionStarted;

        public List<Player> Players { get => _players; set => _players = value; }
        public List<Card> Deck { get => _deck; set => _deck = value; }
        public Player ChiefMechanic { get => _chiefMechanic; set => _chiefMechanic = value; }

        /// <summary>
        /// game view is reponsible to update UI
        /// </summary>
        public GameView GameView { get; set; }
        public List<Card> CardsInConveyorBelt { get => _cardsInConveyorBelt; set => _cardsInConveyorBelt = value; }
        public bool GameState { get => _gameState; set => _gameState = value; }
        public List<Card> FacedUpCards { get => _facedUpCards; set => _facedUpCards = value; }
        public bool AuctionState { get => _isAuctionStarted; set => _isAuctionStarted = value; }

        /// <summary>
        /// build game engine with game view
        /// </summary>
        /// <param name="gameView"></param>
        public GameEngine()
        {
            InitializeGame();
        }

        /// <summary>
        /// initialize game data
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitializeGame()
        {
            InitializeCards();

            InitializePlayers();
        }

        /// <summary>
        /// issue 3 cards to each player
        /// </summary>
        private void IssueCardsToPlayers()
        {
            Players.ForEach(player => player.CardsInHand = TakeThreeCards());
        }

        /// <summary>
        /// starts the game by transitioning the game state to true.
        /// </summary>
        public void StartGame()
        {
            GameState = true;
            AuctionState = false;

            CollectCards();

            IssueCardsToPlayers();

            PickChiefMechanic();

            // 2. update UI
            //InitializeDeck();
            //_gameView.EnpowerChiefMechanic();
            //_gameView.DisplayBidButton();
            GameView.RefreshUI();

            // 3. auotmate
            Automate();
        }

        /// <summary>
        /// collect cards from conveyor belt and players
        /// </summary>
        private void CollectCards()
        {
            Players.ForEach(player =>
            {
                Deck.AddRange(player.CardsInHand);
                Deck.AddRange(player.CardsInBid);
                Deck.AddRange(player.ProductionUnits);
                Deck.AddRange(player.DiscardPile);

                player.Clear();
            });

            Deck.AddRange(CardsInConveyorBelt);
            CardsInConveyorBelt.Clear();

            Deck.AddRange(FacedUpCards);
            FacedUpCards.Clear();
        }

        /// <summary>
        /// imitate AI
        /// </summary>
        private void Automate()
        {
            if (Players.Exists(player => player.IsChiefMechanic && player.IsAI))
            {
                StartAuction();
            }

            //GameView.FlipCards();
            //GameView.AIBid(new CardContext(ChiefMechanic.CardsInHand.First(), ChiefMechanic));
            //ChiefMechanic.ConfirmBidding();
            //GameView.UpdateMessage(ChiefMechanic.Name + " bid = " + ChiefMechanic.IsBid());
        }

        /// <summary>
        /// take three cards
        /// </summary>
        private List<Card> TakeThreeCards()
        {
            List<Card> cards = new List<Card>();

            Card power1 = Deck.Find(c => c.GetPower() == 1);
            Card power2 = Deck.Find(c => c.GetPower() == 2);
            Card power3 = Deck.Find(c => c.GetPower() == 3);

            cards.Add(power1);
            cards.Add(power2);
            cards.Add(power3);

            Deck.Remove(power1);
            Deck.Remove(power2);
            Deck.Remove(power3);

            return cards;
        }

        /// <summary>
        /// initialize two players
        /// </summary>
        private void InitializePlayers()
        {
            Players.Add(new Player("AI Player", Position.Top, Properties.Resources.Mechanic_One, true));
            Players.Add(new Player("Tamati", Position.Bottom, Properties.Resources.Mechanic_Two, false));
        }

        /// <summary>
        /// Initialize cards
        /// </summary>
        private void InitializeCards()
        {
            InitializeCardsWithOnePower();
            InitializeCardsWithTwoPower();
            InitializeCardsWithThreePower();
            InitializeCardsWithFourPower();
            InitializeCardsWithFivePower();
            InitializeRobotUpgradeCards();
            InitializeFzzztCards();
            InitializeProductionUnitCards();
        }

        /// <summary>
        /// initialize 10 production unit cards
        /// </summary>
        private void InitializeProductionUnitCards()
        {
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut_Oil, 13, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut, 9, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog_Nut, 9, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Cog_Nut, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Cog, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Nut_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Oil, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Oil }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Bolt_Nut, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Nut }));
            Deck.Add(new ProductionUnitCard(Properties.Resources.Production_Unit_Cog_Oil, 6, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Oil }));
        }

        /// <summary>
        /// initialize 4 fzzzt cards
        /// </summary>
        private void InitializeFzzztCards()
        {
            Deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            Deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            Deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
            Deck.Add(new FzzztCard(Properties.Resources.Fzzzt));
        }

        /// <summary>
        /// initialize 2 robot upgrade cards
        /// </summary>
        private void InitializeRobotUpgradeCards()
        {
            Deck.Add(new RobotUpgradeCard(Properties.Resources.Robot_Upgrade));
            Deck.Add(new RobotUpgradeCard(Properties.Resources.Robot_Upgrade));
        }

        /// <summary>
        /// initialize 4 cards with 4 power
        /// </summary>
        private void InitializeCardsWithFivePower()
        {
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Cog, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Oil, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Nut, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Five_Bolt, 1, 3, 5, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
        }

        /// <summary>
        /// initialize 4 cards with 4 power
        /// </summary>
        private void InitializeCardsWithFourPower()
        {
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Cog, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Oil, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Nut, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Four_Bolt, 2, 3, 4, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
        }

        /// <summary>
        /// initialize 8 cards with 3 power
        /// </summary>
        private void InitializeCardsWithThreePower()
        {
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Cog, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Oil, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Nut, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Bolt, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Cog, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Oil, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Nut, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Three_Bolt, 2, 2, 3, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
        }

        /// <summary>
        /// initilize 8 cards with 2 power
        /// </summary>
        private void InitializeCardsWithTwoPower()
        {
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Cog, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Oil, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Nut, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Bolt, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Cog, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Oil, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Nut, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_Two_Bolt, 4, 2, 2, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
        }

        /// <summary>
        /// initialize cards with one power
        /// </summary>
        private void InitializeCardsWithOnePower()
        {
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Bolt }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Cog_Nut, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Cog, ConstructionSymbol.Nut }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Bolt_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Oil, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Oil }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));
            Deck.Add(new RobotCard(Properties.Resources.Robot_Power_One_Nut_Bolt_Cog, 4, 1, 1, new HashSet<ConstructionSymbol> { ConstructionSymbol.Nut, ConstructionSymbol.Bolt, ConstructionSymbol.Cog }));
        }

        /// <summary>
        /// pick the chief mechanic
        /// </summary>
        private void PickChiefMechanic()
        {
            Player pickedPlayer = Players[_random.Next() % 2];
            pickedPlayer.IsChiefMechanic = true;
            _chiefMechanic = pickedPlayer;
            Players.FindAll(player => player != pickedPlayer).ForEach(player => player.IsChiefMechanic = false);
        }

        /// <summary>
        /// get the cards on the deck
        /// </summary>
        /// <returns></returns>
        public List<Card> GetDeck()
        {
            return Deck;
        }

        /// <summary>
        /// get the players, current version has two players
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Player> GetPlayers()
        {
            return Players;
        }

        /// <summary>
        /// get the faced up cards on the table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<Card> GetFacedUpCards()
        {
            return FacedUpCards;
        }

        /// <summary>
        /// check if the card is the first card on the conveyor belt
        /// </summary>
        /// <param name="clickedCard"></param>
        /// <returns></returns>
        public bool IsFirstCardOnConveyorBelt(Card clickedCard)
        {
            return CardsInConveyorBelt.FindIndex(c => c == clickedCard) == 0;
        }

        /// <summary>
        /// add a faced up card to the list
        /// </summary>
        /// <param name="card"></param>
        public void AddFacedUpCard(Card card)
        {
            FacedUpCards.Add(card);
            UpdateAllowedFacedUpCardCount();
            CardsInConveyorBelt.Remove(card);
            GameView.UpdateMessage("face-up count:" + FacedUpCards.Count);
        }

        /// <summary>
        /// update the allowed faced up card count
        /// </summary>
        public void UpdateAllowedFacedUpCardCount()
        {
            if (FacedUpCards.Count == 0)
            {
                _allowedFacedUpCardCount = 0;
                return;
            }

            _allowedFacedUpCardCount = FacedUpCards.First().GetConveyorBeltNumber();
            GameView.UpdateMessage("allowed count:" + _allowedFacedUpCardCount);
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
            FacedUpCards.Remove(card);
            CardsInConveyorBelt.Add(card);
            GameView.UpdateMessage("face-up count:" + FacedUpCards.Count);
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
            return FacedUpCards.Count < _allowedFacedUpCardCount;
        }

        /// <summary>
        /// start an auction
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void StartAuction()
        {
            AuctionState = true;
            PickCardsForConveyorBelt();
            GameView.RefreshConveyorBelt();
            GameView.HideStartAuctionButtons();
        }

        private void PickCardsForConveyorBelt()
        {
            foreach (int index in Utils.GenerateIndices(8, Deck.Count))
            {
                GameView.UpdateMessage("index for conveyor belt:" + index);
                CardsInConveyorBelt.Add(Deck[index]);
            }
            Deck.RemoveAll(card => CardsInConveyorBelt.Contains(card));
        }

        /// <summary>
        /// award card to the winner
        /// </summary>
        public void AwardCard()
        {
            Player humanPlayer = Players.Find(player => !player.IsAI);
            humanPlayer.ConfirmBidding();
            GameView.UpdateMessage(humanPlayer.Name + " bid = " + humanPlayer.IsBid());

            FindWinnerIfBothPlayersBid();

            Players.ForEach(player => player.ReturnBidCardsToHand());

            CheckIfNoCardInHand();
            GameView.RefreshConveyorBelt();
            GameView.RefreshCardsForPlayers();
            AutomateBidForAI();
        }

        /// <summary>
        /// check if no cards in hand
        /// </summary>
        private void CheckIfNoCardInHand()
        {
            if (CardsInConveyorBelt.Count == 0)
            {
                return;
            }
            if (Players.All(player => player.HasNoCardInHand()))
            {
                Players.ForEach(player => player.TakeBackCards());
            }
        }

        /// <summary>
        /// automate AI bid
        /// </summary>
        private void AutomateBidForAI()
        {
            List<Card> cardsInHand = ChiefMechanic.CardsInHand;
            if (cardsInHand.Count > 0)
            {
                GameView.AIBid(new CardContext(cardsInHand.First(), ChiefMechanic));
            }
            else
            {
                ChiefMechanic.ConfirmBidding();
            }
        }

        /// <summary>
        /// find the winner if both players have bid
        /// </summary>
        private void FindWinnerIfBothPlayersBid()
        {
            if (Players.FindIndex(player => !player.IsBid()) != -1)
            {
                return;
            }
            int winnerPower = 0;
            Player winner = null;
            foreach (Player player in Players)
            {
                int totalPower = player.GetTotalPowerInBid();
                if (totalPower > winnerPower)
                {
                    winnerPower = totalPower;
                    winner = player;
                }
            }
            if (FacedUpCards.Count == 0)
            {
                return;
            }
            Card auctionedCard = FacedUpCards.First();

            RemoveFacedUpCard(auctionedCard);
            CardsInConveyorBelt.Remove(auctionedCard);

            //discard winner's bid cards
            winner.DiscardBidCards();

            if (auctionedCard is RobotCard)
            {
                winner.DiscardCard(auctionedCard);

                GameView.UpdateMessage(winner.Name + " wins a Robot Card with power " + winnerPower);
                GameView.UpdateMessage(winner.Name + " discard pile  = " + winner.DiscardPile.Count);
                return;
            }

            if (auctionedCard is ProductionUnitCard)
            {
                winner.CollectProductionUnit(auctionedCard);

                GameView.UpdateMessage(winner.Name + " wins a Production Unit with power " + winnerPower);
                GameView.UpdateMessage(winner.Name + " discard pile  = " + winner.ProductionUnits.Count);
                return;
            }
        }

        /// <summary>
        /// print the game state for debugging
        /// </summary>
        public void PrintGameState()
        {
            GameView.UpdateMessage("********** Game State **********");
            GameView.UpdateMessage("");
            GameView.UpdateMessage("Auction State " + AuctionState.ToString());
            GameView.UpdateMessage("Cards in Deck: " + Deck.Count);
            GameView.UpdateMessage("Players: " + Players.Count);
            GameView.UpdateMessage("Cards in Conveyor Belt: " + CardsInConveyorBelt.Count);

            foreach (Card card in CardsInConveyorBelt)
            {
                GameView.UpdateMessage(card.ToString());
            }

            GameView.UpdateMessage("");
            foreach (Player player in Players)
            {
                GameView.UpdateMessage(player.Name + " is the chief Mechanic: " + player.IsChiefMechanic.ToString());
                GameView.UpdateMessage(player.Name + " hand cards:" + player.CardsInHand.Count);
                foreach (Card card in player.CardsInHand)
                {
                    GameView.UpdateMessage(card.ToString());
                }
                GameView.UpdateMessage(player.Name + " bid cards:" + player.CardsInBid.Count);
                foreach (Card card in player.CardsInBid)
                {
                    GameView.UpdateMessage(card.ToString());
                }
                GameView.UpdateMessage(player.Name + " discard pile:" + player.DiscardPile.Count);
                foreach (Card card in player.DiscardPile)
                {
                    GameView.UpdateMessage(card.ToString());
                }
                GameView.UpdateMessage(player.Name + " production units:" + player.ProductionUnits.Count);
                foreach (Card card in player.ProductionUnits)
                {
                    GameView.UpdateMessage(card.ToString());
                }
                GameView.UpdateMessage("");
            }

            GameView.UpdateMessage("********** Game State **********");
        }
    }
}
