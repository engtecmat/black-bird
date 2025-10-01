using System.Collections.Generic;

namespace fzzzt_game
{
    /// <summary>
    /// define the methods that will affect the UI
    /// </summary>
    public interface GameView
    {
        /// <summary>
        /// reset UI
        /// </summary>
        void Reset();

        /// <summary>
        /// update the message box for debugging
        /// </summary>
        void UpdateMessag(string message);

        /// <summary>
        /// set up conveyor belt for an auction
        /// </summary>
        void PrepareConveyorBelt();

        /// <summary>
        /// hide start auction buttons
        /// </summary>
        void HideStartAuctionButtons();

        /// <summary>
        /// Updates the UI to reflect the selected chief mechanic.
        /// </summary>
        void EnpowerChiefMechanic();

        /// <summary>
        /// this is for AI player to face up cards
        /// </summary>
        void FlipCards();

        /// <summary>
        /// bid for AI player
        /// </summary>
        void Bid(CardContext cardContext);

        /// <summary>
        /// display bid button for human player
        /// </summary>
        void DisplayBidButton();

        /// <summary>
        /// update UI after bidding
        /// </summary>
        void UpdateUIAfterBidding(List<Player> players);
    }
}
