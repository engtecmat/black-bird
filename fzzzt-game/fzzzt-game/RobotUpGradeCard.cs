using System.Collections.Generic;
using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a robot upgrade card, which has no power and 0 point value.
    /// </summary>
    public class RobotUpgradeCard : Card
    {
        /// <summary>
        /// the point value of a robot upgrade card is 0
        /// </summary>
        private const int PointValue = 0;

        /// <summary>
        /// the power of a robot upgrade card is 0
        /// </summary>
        private const int Power = 0;

        // the conveyor belt number of a robot upgrade card is 8
        private const int ConveyorBeltNumber = 8;

        /// <summary>
        /// construction symbols, e.g, Bolt, Cog, Nut, Oil
        /// </summary>
        private ISet<ConstructionSymbol> _constructionSymbols;

        /// <summary>
        /// build a robot upgrade card with a face image.
        /// </summary>
        /// <param name="face"></param>
        public RobotUpgradeCard(Bitmap face) : base(face, PointValue, Power, ConveyorBeltNumber)
        {
            // the construction symbols of a robot upgrade card are Bolt, Cog, Nut, Oil
            _constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut, ConstructionSymbol.Oil };
        }
    }
}
