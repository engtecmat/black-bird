using System;

namespace fzzzt_game
{
    /// <summary>
    /// Represents the core game engine for managing and running the Fzzzt game.
    /// </summary>
    /// <remarks>The <see cref="FzzztGameEngine"/> class provides the foundational functionality required to
    /// initialize,  update, and manage the state of the Fzzzt game. It serves as the central component for coordinating
    /// game logic, rendering, and input handling.</remarks>
    public class FzzztGameEngine
    {
        /// <summary>
        /// Indicates whether the game has started.
        /// </summary>
        private bool gameState = false;

        public long GetChiefMechanic()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() % 2 + 1;
        }

        /// <summary>
        /// Determines whether the game has started.
        /// </summary>
        /// <returns><see langword="true"/> if the game has started; otherwise, <see langword="false"/>.</returns>
        public bool IsGameStarted()
        {
            return gameState;
        }

        /// <summary>
        /// Resets the game state to its initial condition.
        /// </summary>
        internal void ResetGame()
        {
            gameState = false;
        }

        /// <summary>
        /// Starts the game by transitioning the game state to true.
        /// </summary>
        internal void StartGame()
        {
            gameState = true;
        }
    }
}
