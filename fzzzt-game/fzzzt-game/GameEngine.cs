using System;

namespace fzzzt_game
{
    /// <summary>
    /// the core game engine for managing and running the Fzzzt game.
    /// </summary>
    public class GameEngine
    {
        /// <summary>
        /// indicates whether the game has started.
        /// </summary>
        private bool _gameState = false;


        /// <summary>
        /// determines the chief mechanic
        /// </summary>
        /// <returns></returns>
        public long GetChiefMechanic()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() % 2 + 1;
        }

        /// <summary>
        /// determines whether the game has started.
        /// </summary>
        /// <returns><see langword="true"/> if the game has started; otherwise, <see langword="false"/>.</returns>
        public bool IsGameStarted()
        {
            return _gameState;
        }

        /// <summary>
        /// resets the game state to its initial condition.
        /// </summary>
        internal void ResetGame()
        {
            _gameState = false;
        }

        /// <summary>
        /// starts the game by transitioning the game state to true.
        /// </summary>
        internal void StartGame()
        {
            _gameState = true;
        }
    }
}
