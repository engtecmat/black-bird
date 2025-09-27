using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int GetChiefMechanic()
        {
           return new DateTime().Millisecond % 2 + 1;
        }
    }
}
