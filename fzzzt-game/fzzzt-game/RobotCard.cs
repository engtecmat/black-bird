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
        /// it is used to determine how many card can be fliped in the beginning of an auction.
        /// </summary>
        private int _conveyorBeltNumber;

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
        public RobotCard(Bitmap face, int conveyorBeltNumber, int pointValue, int power, ISet<ConstructionSymbol> constructionSymbols) : base(face, pointValue, power)
        {
            _conveyorBeltNumber = conveyorBeltNumber;
            _constructionSymbols = constructionSymbols;
        }

        /// <summary>
        /// get the conveyor belt number
        /// </summary>
        public int GetConveyorBeltNumber()
        {
            return _conveyorBeltNumber;
        }

        /// <summary>
        /// get the construction symbols
        /// </summary>
        /// <returns></returns>
        public ISet<ConstructionSymbol> GetConstructionSymbols()
        {
            return _constructionSymbols;
        }
    }
}
