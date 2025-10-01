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
        private ISet<Card> _cardsInHand;

        /// <summary>
        /// cards are chosen for bidding
        /// </summary>
        private ISet<Card> _cardsInBid;

        /// <summary>
        /// discard pile
        /// </summary>
        private ISet<Card> _discardPile;

        /// <summary>
        /// production units
        /// </summary>
        private ISet<Card> _productionUnits;

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
        public Player(string name, Position position, Bitmap mechanic, ISet<Card> cardsInHand, bool isAIPlayer)
        {
            _name = name;
            _position = position;
            _mechanicFace = mechanic;
            _cardsInHand = cardsInHand;
            _cardsInBid = new HashSet<Card>();
            _discardPile = new HashSet<Card>();
            _productionUnits = new HashSet<Card>();
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
            return _name;
        }

        /// <summary>
        /// get the mechanic face
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetMechanicFace()
        {
            return _mechanicFace;
        }

        /// <summary>
        /// get the cards in hand
        /// </summary>
        /// <returns>a set of cards</returns>
        public ISet<Card> GetCardsInHand()
        {
            return _cardsInHand;
        }

        /// <summary>
        /// get the cards in bid
        /// </summary>
        /// <returns>a set of cards</returns>
        public ISet<Card> GetCardsInBid()
        {
            return _cardsInBid;
        }

        /// <summary>
        /// add bid card
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToBid(Card card)
        {
            _cardsInBid.Add(card);
        }

        /// <summary>
        /// add bid card
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCardFromBid(Card card)
        {
            _cardsInBid.Remove(card);
        }


        /// <summary>
        /// add card to hand
        /// </summary>
        /// <param name="card"></param>
        public void AddCardToHand(Card card)
        {
            _cardsInHand.Add(card);
        }

        /// <summary>
        /// remove card form hand
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCardFromHand(Card card)
        {
            _cardsInHand.Remove(card);
        }

        /// <summary>
        /// set to true if the player is bid
        /// </summary>
        public void Bid()
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
            return _cardsInBid.Sum(card => card.GetPower());
        }

        /// <summary>
        /// add card to discard pile
        /// </summary>
        /// <param name="auctionedCard"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void DiscardCard(Card auctionedCard)
        {
            _discardPile.Add(auctionedCard);
        }

        /// <summary>
        /// add card to production units
        /// </summary>
        /// <param name="auctionedCard"></param>
        public void CollectProductionUnit(Card auctionedCard)
        {
            _productionUnits.Add(auctionedCard);
        }

        /// <summary>
        /// return discard pile
        /// </summary>
        /// <returns></returns>
        public ISet<Card> GetDiscardPile()
        {
            return _discardPile;
        }

        /// <summary>
        /// get production units
        /// </summary>
        /// <returns></returns>
        public ISet<Card> GetProductionUnits()
        {
            return _productionUnits;
        }

        /// <summary>
        /// move bid cards to discard pile after bidding
        /// for winner
        /// </summary>
        public void DiscardBidCards()
        {
            foreach (Card card in _cardsInBid)
            {
                _discardPile.Add(card);
            }
            _cardsInBid.Clear();
        }

        /// <summary>
        ///  return bid cards to hand
        /// </summary>
        public void ReturnBidCardsToHand()
        {
            foreach (Card card in _cardsInBid)
            {
                _cardsInHand.Add(card);
            }
            _cardsInBid.Clear();
        }

        public override string ToString()
        {
            return _name + ", " + _position + "," + _cardsInBid.Count;
        }
    }
}