using System.Collections.Generic;
using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a robot card, which has power, point value, conveyor belt number and one more construction symbols.
    /// </summary>
    public class RobotCard : Card
    {
        /// <summary>
        /// construction symbols, e.g, Bolt, Cog, Nut, Oil
        /// </summary>
        private ISet<ConstructionSymbol> _constructionSymbols;

        /// <summary>
        /// build a robot card with a face image, conveyor belt number, point value, power and construction symbols.
        /// </summary>
        /// <param name="face"></param>
        /// <param name="conveyorBeltNumber"></param>
        /// <param name="pointValue"></param>
        /// <param name="power"></param>
        /// <param name="constructionSymbols"></param>
        public RobotCard(Bitmap face, int conveyorBeltNumber, int pointValue, int power, ISet<ConstructionSymbol> constructionSymbols) : base(face, pointValue, power, conveyorBeltNumber)
        {
            ConstructionSymbols = constructionSymbols;
        }

        public ISet<ConstructionSymbol> ConstructionSymbols { get => _constructionSymbols; set => _constructionSymbols = value; }

    }
}
