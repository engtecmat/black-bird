using System.Collections.Generic;
using System.Drawing;

namespace BlackBird
{
    /// <summary>
    /// a production unit card
    /// </summary>
    public class ProductionUnitCard : Card
    {
        /// <summary>
        /// a construction symbol, e.g, Bolt, Cog, Nut, Oil
        /// </summary>
        private ISet<ConstructionSymbol> _constructionSymbols;

        /// <summary>
        /// build a production unit card with a face image, point value and construction symbols.
        /// the value is always 3 for production unit cards
        /// production unit cards do not have power
        /// </summary>
        /// <param name="face"></param>
        /// <param name="pointValue"></param>
        /// <param name="constructionSymbols"></param>
        public ProductionUnitCard(Bitmap face, int pointValue, ISet<ConstructionSymbol> constructionSymbols) : base(face, pointValue, 0, 3)
        {
            ConstructionSymbols = constructionSymbols;
        }

        public ISet<ConstructionSymbol> ConstructionSymbols { get => _constructionSymbols; set => _constructionSymbols = value; }

    }
}
