using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.
    Threading.Tasks;
using System.Windows.Forms;

namespace fzzzt_game
{
    /// <summary>
    /// represent the relationship between a player and controls
    /// </summary>
    public sealed class PlayerViewContext
    {
        /// <summary>
        /// a reference to the player
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// a label to dipslay the player's name
        /// </summary>
        public Label NameLabel { get; set; }

        /// <summary>
        /// a panel is used to display cards in hand
        /// </summary>
        public Panel CardInHandPanel { get; set; }

        /// <summary>
        /// a picture box to display mechanic card
        /// </summary>
        public PictureBox MechanicPictureBox { get; set; }
    }
}
