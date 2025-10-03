using System.Drawing;
using System.Collections.Generic;
using System;
using System.Linq;


namespace fzzzt_game
{
    /// <summary>
    /// the palyer's filed and behavors
    /// </summary>
    public class Player
    {
        /// <summary>
        /// the player's name
        /// </summary>
        private string _name;

        /// <summary>
        /// indicate if the player is an AI
        /// </summary>
        private bool _isAI;

        /// <summary>
        /// indicate if the player is bid
        /// </summary>
        private bool _isBid;

        /// <summary>
        /// the position of the a palyer
        /// </summary>
        private Position _position;

        /// <summary>
        /// the payer's mechanic card
        /// </summary>
        private Bitmap _mechanicFace;

        /// <summary>
        /// cards in hand
        /// int the beginning, each player has a 1 power, 2 power and 3 power card
        /// </summary>
        private List<Card> _cardsInHand;

        /// <summary>
        /// cards are chosen for bidding
        /// </summary>
        private List<Card> _cardsInBid;

        /// <summary>
        /// discard pile
        /// </summary>
        private List<Card> _discardPile;

        /// <summary>
        /// production units
        /// </summary>
        private List<Card> _productionUnits;

        public string Name { get => _name; set => _name = value; }
        public List<Card> CardsInHand { get => _cardsInHand; set => _cardsInHand = value; }
        public Bitmap MechanicFace { get => _mechanicFace; set => _mechanicFace = value; }
        public List<Card> CardsInBid { get => _cardsInBid; set => _cardsInBid = value; }
        public List<Card> ProductionUnits { get => _productionUnits; set => _productionUnits = value; }
        public List<Card> DiscardPile { get => _discardPile; set => _discardPile = value; }

        /// <summary>
        /// reutrn true if the player is an AI
        /// </summary>
        /// <returns></returns>
        public bool IsAI()
        {
            return _isAI;
        }

        /// <summary>
        /// build a playe with name
        /// </summary>
        /// <param name="name"></param>
        public Player(string name, Position position, Bitmap mechanic, bool isAIPlayer)
        {
            Name = name;
            _position = position;
            MechanicFace = mechanic;
            CardsInHand = new List<Card>();
            CardsInBid = new List<Card>();
            DiscardPile = new List<Card>();
            ProductionUnits = new List<Card>();
            _isAI = isAIPlayer;
        }

        /// <summary>
        /// return true the the play at the top
        /// </summary>
        /// <returns>true or false</returns>
        public bool AtTop()
        {
            return _position == Position.Top;
        }

        /// <summary>
        /// return true the the play at the bttom
        /// </summary>
        /// <returns>true or false</returns>
        public bool AtBottom()
        {
            return _position == Position.Bottom;
        }

        /// <summary>
        ///  get the player's name
        /// </summary>
        /// <returns>string</returns>
        public string GetName()
        {
            return Name;
        }

        /// <summary>
        /// add bid card
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToBid(Card card)
        {
            CardsInBid.Add(card);
        }

        /// <summary>
        /// add bid card
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCardFromBid(Card card)
        {
            CardsInBid.Remove(card);
        }


        /// <summary>
        /// add card to hand
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToHand(Card card)
        {
            CardsInHand.Add(card);
        }

        /// <summary>
        /// remove card form hand
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCardFromHand(Card card)
        {
            CardsInHand.Remove(card);
        }

        /// <summary>
        /// set to true if the player is bid
        /// </summary>
        public void ConfirmBidding()
        {
            _isBid = true;
        }

        /// <summary>
        /// reset the bid status to false
        /// </summary>
        public void ResetBid()
        {
            _isBid = false;
        }

        /// <summary>
        /// return bid status
        /// </summary>
        /// <returns></returns>
        public bool IsBid()
        {
            return _isBid;

        }

        /// <summary>
        /// get the total power in bid
        /// </summary>
        /// <returns></returns>
        public int GetTotalPowerInBid()
        {
            return CardsInBid.Sum(card => card.GetPower());
        }

        /// <summary>
        /// add card to discard pile
        /// </summary>
        /// <param name="auctionedCard"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DiscardCard(Card auctionedCard)
        {
            DiscardPile.Add(auctionedCard);
        }

        /// <summary>
        /// add card to production units
        /// </summary>
        /// <param name="auctionedCard"></param>
        public void CollectProductionUnit(Card auctionedCard)
        {
            ProductionUnits.Add(auctionedCard);
        }

        /// <summary>
        /// move bid cards to discard pile after bidding
        /// for winner
        /// </summary>
        public void DiscardBidCards()
        {
            foreach (Card card in CardsInBid)
            {
                DiscardPile.Add(card);
            }
            CardsInBid.Clear();
        }

        /// <summary>
        ///  return bid cards to hand
        /// </summary>
        public void ReturnBidCardsToHand()
        {
            foreach (Card card in CardsInBid)
            {
                CardsInHand.Add(card);
            }
            CardsInBid.Clear();
        }

        /// <summary>
        /// has no card in hand
        /// </summary>
        /// <returns></returns>
        public bool HasNoCardInHand()
        {
            return CardsInHand == null || CardsInHand.Count == 0;
        }

        public override string ToString()
        {
            return Name + ", " + _position + "," + CardsInBid.Count;
        }

        /// <summary>
        /// tack back cards from discard pile
        /// </summary>
        public void TakeBackCards()
        {
            List<int> indices = Utils.GenerateIndices(6, DiscardPile.Count);
            indices.ForEach(i => CardsInHand.Add(DiscardPile[i]));
            CardsInHand.ForEach(card => DiscardPile.Remove(card));
        }

        /// <summary>
        /// clear all the cards with the player
        /// </summary>
        public void Clear()
        {
            CardsInBid.Clear();
            CardsInHand.Clear();
            DiscardPile.Clear();
            ProductionUnits.Clear();
        }
    }
}