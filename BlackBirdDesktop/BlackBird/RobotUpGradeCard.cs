using System.Collections.Generic;
using System.Drawing;

namespace BlackBird
{
    /// <summary>
    /// a robot upgrade card, which has no power and 0 point value.
    /// </summary>
    public class RobotUpgradeCard : Card
    {
        /// <summary>
        /// construction symbols, e.g, Bolt, Cog, Nut, Oil
        /// </summary>
        private ISet<ConstructionSymbol> _constructionSymbols;

        /// <summary>
        /// build a robot upgrade card with a face image.
        /// the conveyor belt number of a robot upgrade card is 8
        /// the point value of a robot upgrade card is 0
        /// the power of a robot upgrade card is 0
        /// </summary>
        /// <param name="face"></param>
        public RobotUpgradeCard(Bitmap face) : base(face, 0, 0, 8)
        {
            // the construction symbols of a robot upgrade card are Bolt, Cog, Nut, Oil
            _constructionSymbols = new HashSet<ConstructionSymbol> { ConstructionSymbol.Bolt, ConstructionSymbol.Cog, ConstructionSymbol.Nut, ConstructionSymbol.Oil };
        }
    }
}
