using System;

namespace BlackBird
{
    /// <summary>
    /// represent the onwership between card and the player
    /// </summary>
    public class CardContext
    {
        /// <summary>
        /// a card
        /// </summary>
        private Card _card;

        /// <summary>
        /// the player who owns the card
        /// </summary>
        private Player _onwer;

        /// <summary>
        /// build card context with card and a player
        /// </summary>
        /// <param name="card"></param>
        /// <param name="palyer"></param>
        public CardContext(Card card, Player palyer)
        {
            Card = card;
            _onwer = palyer;
        }

        public Player Owner { get => _onwer; }
        public Card Card { get => _card; set => _card = value; }

        /// <summary>
        /// remove card from hand and add card to bid
        /// </summary>
        public void BidCard()
        {
            Card.CurrentState = CardState.FaceDown;
            Owner.RemoveCardFromHand(Card);
            Owner.AddCardToBid(Card);
        }

        /// <summary>
        /// cancel the card for bidding
        /// </summary>
        public void CanelBidCard()
        {
            Card.CurrentState = CardState.FaceUp;
            Owner.RemoveCardFromBid(Card);
            Owner.AddCardToHand(Card);
        }

        /// <summary>
        /// add card to the widget that the player is building
        /// </summary>
        public void AddCardToWidget()
        {
            Owner.CurrentBuildingWidget.AddCard(Card);
            Owner.CardsInHand.Remove(Card);
            Owner.CardsInBid.Remove(Card);
            Owner.DiscardPile.Remove(Card);
        }
    }
}
