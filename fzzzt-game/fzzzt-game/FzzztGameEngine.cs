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
        public long GetChiefMechanic()
        {
            return new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() % 2 + 1;
        }
    }
}
