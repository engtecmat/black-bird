using System;

namespace fzzzt_game
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
            _card = card;
            _onwer = palyer;
        }

        public Player Onwer { get => _onwer; }

        /// <summary>
        /// remove card from hand and add card to bid
        /// </summary>
        public void BidCard()
        {
            _onwer.RemoveCardFromHand(_card);
            _onwer.AddCardToBid(_card);
        }
    }
}
