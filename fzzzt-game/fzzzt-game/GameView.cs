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
    }
}
