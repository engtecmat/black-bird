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
        /// a palnel is used to display cards in bid
        /// </summary>
        public Panel CardInBidPanel { get; set; }

        /// <summary>
        /// a panel is used to display production units
        /// </summary>
        public Panel ProductionUnitPanel { get; set; }

        /// <summary>
        /// a panel is used to display production units for building widgets
        /// </summary>
        public FlowLayoutPanel WidgetProductionUnitPanel { get; set; }

        /// <summary>
        /// a panel is used to display robot cards for building widgets
        /// </summary>
        public FlowLayoutPanel WidgetRobotPanel { get; set; }

        /// <summary>
        /// a picture box to display mechanic card
        /// </summary>
        public PictureBox MechanicPictureBox { get; set; }

        /// <summary>
        /// a picture box to display discard pile
        /// </summary>
        public PictureBox DiscardPilePictureBox { get; set; }

        /// <summary>
        /// a bid button for a player
        /// </summary>
        public Button BidButton { get; set; }

        /// <summary>
        /// a start auction button for a player
        /// </summary>
        public Button StartAcutionButton { get; set; }
    }
}
