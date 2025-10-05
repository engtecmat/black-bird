using System;
using System.Collections.Generic;

namespace fzzzt_game
{
    /// <summary>
    /// represent a widget in the game
    /// </summary>
    public class Widget
    {
        /// <summary>
        /// a production unit card
        /// </summary>
        private Card _productionUnit;

        /// <summary>
        /// robot cards with construction symbols matching the production unit card
        /// </summary>
        private List<Card> _robotCards;

        /// <summary>
        /// the owner of the widget
        /// </summary>
        private Player _owner;

        public List<Card> RobotCards { get => _robotCards; set => _robotCards = value; }
        public Card ProductionUnit { get => _productionUnit; set => _productionUnit = value; }
        public Player Owner { get => _owner; set => _owner = value; }

        /// <summary>
        /// add a robot card to the widget
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            RobotCards.Add(card);
        }
    }
}
