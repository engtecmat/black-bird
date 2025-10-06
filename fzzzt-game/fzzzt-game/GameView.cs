namespace fzzzt_game
{
    /// <summary>
    /// define the methods that will affect the UI
    /// </summary>
    public interface GameView
    {
        /// <summary>
        /// a method for adding message to the message box for debugging
        /// </summary>
        void Log(string message);

        /// <summary>
        /// set up conveyor belt for an auction
        /// </summary>
        void RefreshConveyorBelt();

        /// <summary>
        /// refresh cards for palyers
        /// </summary>
        void RefreshCardsForPlayers();

        /// <summary>
        /// refresh UI
        /// </summary>
        void RefreshUI();

        /// <summary>
        /// open a window to build production units
        /// </summary>
        void StartBuildingWigets();

        /// <summary>
        /// show winners
        /// </summary>
        void ShowWinners();

    }
}
