using System.Collections.Generic;
using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a production unit card
    /// </summary>
    public class ProductionUnitCard : Card
    {
        /// <summary>
        /// production unit cards do not have power
        /// </summary>
        private const int Power = 0;

        // the value is always 3 for production unit cards
        private const int ConveyorBeltNumber = 3;

        /// <summary>
        /// it is used to determine how many card can be fliped in the beginning of an auction.
        /// </summary>
        private int _conveyorBeltNumber;

        /// <summary>
        /// a construction symbol, e.g, Bolt, Cog, Nut, Oil
        /// </summary>
        private ISet<ConstructionSymbol> _constructionSymbols;

        /// <summary>
        /// build a production unit card with a face image, point value and construction symbols.
        /// </summary>
        /// <param name="face"></param>
        /// <param name="pointValue"></param>
        /// <param name="constructionSymbols"></param>
        public ProductionUnitCard(Bitmap face, int pointValue, ISet<ConstructionSymbol> constructionSymbols) : base(face, pointValue, Power, ConveyorBeltNumber)
        {
            _constructionSymbols = constructionSymbols;
        }

        /// <summary>
        /// get the construction symbols
        /// </summary>
        public ISet<ConstructionSymbol> GetConstructionSymbols()
        {
            return _constructionSymbols;
        }
    }
}
