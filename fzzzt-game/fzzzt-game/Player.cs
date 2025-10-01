using System.Drawing;
using System.Collections.Generic;


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
        /// build a playe with name
        /// </summary>
        /// <param name="name"></param>
        public Player(string name, Position position, Bitmap mechanic, ISet<Card> cardsInHand)
        {
            _name = name;
            _position = position;
            _mechanicFace = mechanic;
            _cardsInHand = cardsInHand;
            _cardsInBid = new HashSet<Card>();
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

    }
}