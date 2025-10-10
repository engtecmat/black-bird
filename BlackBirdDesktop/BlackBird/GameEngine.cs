using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace BlackBird
{
    /// <summary>
    /// the core game engine for managing and running the Fzzzt game.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// a global card back
        /// </summary>
        public static Bitmap CardBack = Properties.Resources.Card_Back;

        /// <summary>
        /// indicates whether the game has started.
        /// </summary>
        private bool _gameState = false;

        /// <summary>
        /// the conveyor belt deck
        /// </summary>
        private List<Card> _deck = new List<Card>();

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
        public bool AuctionState { get => _isAuctionStarted; set => _isAuctionStarted = value; }
        public int AllowedFacedUpCardCount { get => _allowedFacedUpCardCount; set => _allowedFacedUpCardCount = value; }

        /// <summary>
        /// build game engine with game view
        /// </summary>
        /// <param name="gameView"></param>
        public GameEngine()
        {
            InitializeCards();

            InitializePlayers();
        }

        /// <summary>
        /// issue 3 cards to each player
        /// set stat
        /// </summary>
        private void ResetPlayers()
        {
            Players.ForEach(player =>
            {
                player.CardsInHand = TakeThreeCards();
                player.IsChiefMechanic = false;
            });
        }

        /// <summary>
        /// starts the game by transitioning the game state to true.
        /// </summary>
        public void StartGame()
        {
            GameState = true;
            AuctionState = false;

            CollectCards();

            ResetCards();

            ResetPlayers();

            PickChiefMechanic();

            Automate();

            GameView.RefreshUI();
        }

        /// <summary>
        /// reset cards to facedown
        /// </summary>
        private void ResetCards()
        {
            _deck.ForEach(card => card.CurrentState = CardState.FaceDown);
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
        }

        /// <summary>
        /// imitate AI
        /// </summary>
        private void Automate()
        {
            Player aiPlayer = Players.Find(player => player.IsAI);

            if (!AuctionState && aiPlayer.IsChiefMechanic)
            {
                // if AI is the chief mechanic, start auction immediately
                StartAuction();

                /// face up cards on conveyor belt
                FaceUpOnConveyorBelt();
            }
            CheckIfAuctionState();
        }

        /// <summary>
        /// there is no cards on the conveyor belt, auction is over, start building widgets
        /// </summary>
        public void CheckIfAuctionState()
        {
            // if there is no cards on conveyor belt and in auction
            if (CardsInConveyorBelt.Count == 0 && AuctionState)
            {
                // at least one of players has production units can be used to build widget, then start building widgets
                if (Players.Any(player => player.ProductionUnits.Count > 0))
                {
                    Players.ForEach(player => player.RefreshWidgets());
                    GameView.StartBuildingWigets();
                }
                else
                {
                    GameView.ShowWinners();
                }
                AuctionState = false;
            }
        }

        /// <summary>
        /// face up cards on conveyor belt
        /// </summary>
        private void FaceUpOnConveyorBelt()
        {
            if (CardsInConveyorBelt.Count > 0)
            {
                // get first card
                Card firstCard = CardsInConveyorBelt[0];
                firstCard.Flip();

                GameView.Log("first cards's conveyor belt number is: " + firstCard.ConveyorBeltNumber);

                // face up cards its conveyor belt number
                List<int> indices = Utils.GenerateIndices(firstCard.ConveyorBeltNumber - 1, 1, CardsInConveyorBelt.Count);
                GameView.Log("randomly indices are: " + string.Join(",", indices));

                indices.ForEach(index => CardsInConveyorBelt[index].Flip());
            }
        }

        /// <summary>
        /// take three cards
        /// </summary>
        private List<Card> TakeThreeCards()
        {
            List<Card> cards = new List<Card>();

            Card power1 = Deck.Find(c => c.Power == 1);
            Card power2 = Deck.Find(c => c.Power == 2);
            Card power3 = Deck.Find(c => c.Power == 3);

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
            Players.Add(new Player("AI Player", Position.Top, Properties.Resources.Engineer, true));
            Players.Add(new Player("Tamati", Position.Bottom, Properties.Resources.Engineer, false));
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
            pickedPlayer.CardFace = Properties.Resources.Chief_Engineer;
            ChiefMechanic = pickedPlayer;
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
        /// get the faced up cards on the table
        /// </summary>
        /// <returns></returns>
        public List<Card> GetFacedUpCards()
        {
            return CardsInConveyorBelt.FindAll(card => card.CurrentState == CardState.FaceUp);
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
        /// update the allowed faced up card count
        /// </summary>
        public void UpdateAllowedFacedUpCardCount()
        {
            if (GetFacedUpCards().Count == 0)
            {
                AllowedFacedUpCardCount = 0;
                return;
            }

            AllowedFacedUpCardCount = GetFacedUpCards().First().ConveyorBeltNumber;
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
            return GetFacedUpCards().Count < AllowedFacedUpCardCount;
        }

        /// <summary>
        /// start an auction
        /// </summary>
        public void StartAuction()
        {
            AuctionState = true;
            PickCardsForConveyorBelt();
            Players.Find(player => player.IsAI).AutomateBidding();
            GameView.RefreshUI();
        }

        /// <summary>
        /// pick cards for the conveyor belt
        /// </summary>
        private void PickCardsForConveyorBelt()
        {
            // randomly indices
            List<int> indices = Utils.GenerateIndices(8, Deck.Count);
            Console.WriteLine(string.Join(",",indices));
            foreach (int index in indices)
            {
                CardsInConveyorBelt.Add(Deck[index]);
            }
            Deck.RemoveAll(card => CardsInConveyorBelt.Contains(card));
        }

        /// <summary>
        /// award card to the winner
        /// </summary>
        public void AwardCard()
        {
            FindWinnerIfBothPlayersBid();

            Players.ForEach(player => player.ReturnBidCardsToHand());

            CheckIfNoCardInHand();
            Players.Find(player => player.IsAI).AutomateBidding();
            CheckIfAuctionState();
            GameView.RefreshUI();
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
        /// find the winner if both players have bid
        /// </summary>
        private void FindWinnerIfBothPlayersBid()
        {
            if (Players.Any(player => !player.IsBid))
            {
                return;
            }

            var winnerGroup = Players.GroupBy(player => player.GetTotalPowerInBid())
                .Select(group => new { TotoalPower = group.Key, Players = group.ToList() })
                .OrderByDescending(group => group.TotoalPower)
                .First();

            Player winner = null;
            if (winnerGroup.Players.Count > 1)
            {
                winner = winnerGroup.Players.Find(player => player.IsChiefMechanic);
            }
            else
            {
                winner = winnerGroup.Players.First();
            }

            GameView.Log("The winnder is: " + winner.Name);


            //award the first face up card on conveyor belt to the winner
            List<Card> faceUpCardsInConveyorBelt = CardsInConveyorBelt.FindAll(card => card.CurrentState == CardState.FaceUp);
            if (faceUpCardsInConveyorBelt.Count == 0)
            {
                return;
            }
            Card auctionedCard = faceUpCardsInConveyorBelt.First();

            CardsInConveyorBelt.Remove(auctionedCard);

            //discard winner's bid cards
            winner.DiscardBidCards();

            if (auctionedCard is RobotCard)
            {
                winner.DiscardCard(auctionedCard);

                GameView.Log(winner.Name + " discard pile  = " + winner.DiscardPile.Count);
                return;
            }

            if (auctionedCard is ProductionUnitCard)
            {
                winner.CollectProductionUnit(auctionedCard);

                GameView.Log(winner.Name + " discard pile  = " + winner.ProductionUnits.Count);
                return;
            }
        }

        /// <summary>
        /// print the game state for debugging
        /// </summary>
        public void PrintGameState()
        {
            GameView.Log("********** Game State **********");
            GameView.Log("");
            GameView.Log("Auction State " + AuctionState.ToString());
            GameView.Log("Cards in Deck: " + Deck.Count);
            GameView.Log("Players: " + Players.Count);
            GameView.Log("Allowed face-up count on belt: " + AllowedFacedUpCardCount);

            GameView.Log("Cards in Conveyor Belt: " + CardsInConveyorBelt.Count);

            foreach (Card card in CardsInConveyorBelt)
            {
                GameView.Log(card.ToString());
            }

            GameView.Log("");
            foreach (Player player in Players)
            {
                GameView.Log(player.Name + " is the chief Mechanic: " + player.IsChiefMechanic.ToString());
                GameView.Log(player.Name + " hand cards:" + player.CardsInHand.Count);
                foreach (Card card in player.CardsInHand)
                {
                    GameView.Log(card.ToString());
                }
                GameView.Log(player.Name + " bid cards:" + player.CardsInBid.Count);
                foreach (Card card in player.CardsInBid)
                {
                    GameView.Log(card.ToString());
                }
                GameView.Log(player.Name + " discard pile:" + player.DiscardPile.Count);
                foreach (Card card in player.DiscardPile)
                {
                    GameView.Log(card.ToString());
                }
                GameView.Log(player.Name + " production units:" + player.ProductionUnits.Count);
                foreach (Card card in player.ProductionUnits)
                {
                    GameView.Log(card.ToString());
                }
                GameView.Log(player.Name + " widgets:" + player.Widgets.Count);
                foreach (Widget widget in player.Widgets)
                {
                    GameView.Log(widget.ProductionUnit.ToString());
                }
                GameView.Log("");
            }

            GameView.Log("********** Game State **********");
        }

        /// <summary>
        /// determine the winner when the game ends
        /// </summary>
        /// <returns></returns>
        public List<Player> DetermineWinner()
        {

            // find the player with the highest score
            var groupingByScore = Players.GroupBy(p => p.GetTotalScore())
                .Select(group => new { TotalPointValue = group.Key, Players = group.ToList() })
                .OrderByDescending(g => g.TotalPointValue)
                .First();

            // if there is only one player with the highest score, return that player
            if (groupingByScore.Players.Count == 1)
            {
                return new List<Player> { groupingByScore.Players.First() };
            }

            // if there are multiple players with the highest score, find the player with the fewest robot cards
            var groupingByRobotCardCount = groupingByScore.Players.GroupBy(p => p.GetRobotCardsCount())
                .Select(group => new { RobotCardCount = group.Key, Players = group.ToList() })
                .OrderBy(g => g.RobotCardCount)
                .First();

            // if there is only one player with fewest robot cards, return the player
            if (groupingByRobotCardCount.Players.Count == 1)
            {
                return new List<Player> { groupingByRobotCardCount.Players.First() };
            }

            // if there is still a tie, there are multiple winners. 
            return groupingByRobotCardCount.Players;
        }
    }
}
