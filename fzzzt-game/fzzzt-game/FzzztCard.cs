using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a fzzzt card, which has 3 powers an -1 point value.
    /// </summary>
    public class FzzztCard : Card
    {
        /// <summary>
        /// the power of a fzzzt card is 3
        /// </summary>
        private const int Power = 3;

        /// <summary>
        /// build a fzzzt card with a face image.
        /// the conveyor belt number of a fzzzt card is 1
        /// the point value of a fzzzt card is -1
        /// </summary>
        /// <param name="face"></param>
        public FzzztCard(Bitmap face) : base(face, -1, Power, 1)
        {
        }
    }
}
