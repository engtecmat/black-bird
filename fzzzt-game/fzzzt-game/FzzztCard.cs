using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a fzzzt card, which has 3 powers an -1 point value.
    /// </summary>
    public class FzzztCard : Card
    {
        /// <summary>
        /// the point value of a fzzzt card is -1
        /// </summary>
        private const int PointValue = -1;

        /// <summary>
        /// the power of a fzzzt card is 3
        /// </summary>
        private const int Power = 3;

        /// <summary>
        /// it is used to determine how many card can be fliped in the beginning of an auction.
        /// </summary>
        private int _conveyorBeltNumber;

        /// <summary>
        /// build a fzzzt card with a face image.
        /// </summary>
        /// <param name="face"></param>
        public FzzztCard(Bitmap face) : base(face, PointValue, Power)
        {
            // the conveyor belt number of a fzzzt card is 1
            _conveyorBeltNumber = 1;
        }

        /// <summary>
        /// get the conveyor belt number
        /// </summary>
        /// <returns></returns>
        public int GetConveyorBeltNumber()
        {
            return _conveyorBeltNumber;
        }
    }
}
