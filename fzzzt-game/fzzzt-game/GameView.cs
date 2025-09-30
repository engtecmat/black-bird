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
    }
}
