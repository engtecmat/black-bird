using System.Collections.Generic;

namespace fzzzt_game
{
    /// <summary>
    /// define the methods that will affect the UI
    /// </summary>
    public interface GameView
    {
        /// <summary>
        /// update the message box for debugging
        /// </summary>
        void UpdateMessage(string message);

        /// <summary>
        /// set up conveyor belt for an auction
        /// </summary>
        void RefreshConveyorBelt();

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        void EnpowerChiefMechanic();

        /// <summary>
        /// bid for AI player
        /// </summary>
        void AIBid(CardContext cardContext);

        /// <summary>
        /// display bid button for human player
        /// </summary>
        void DisplayBidButton();

        /// <summary>
        /// refresh cards for palyers
        /// </summary>
        void RefreshCardsForPlayers();

        /// <summary>
        /// refresh UI
        /// </summary>
        void RefreshUI();
    }
}
