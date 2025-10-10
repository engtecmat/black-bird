using System.Drawing;

namespace BlackBird
{
    /// <summary>
    /// a mechanic card, which has 0 power and 0 point value.
    /// </summary>
    public class MechanicCard : Card
    {
        /// <summary>
        /// the play number of a mechanic card is used to determine who is the chief mechanic.
        /// </summary>
        private int _playNumber;

        /// <summary>
        /// build a mechanic card with a face image and play number.
        /// mechanic card has no conveyor belt number
        /// the point value of a mechanic card is 0
        /// the power of a mechanic card is 0
        /// </summary>
        /// <param name="face"></param>
        /// <param name="playNumber"></param>
        public MechanicCard(Bitmap face, int playNumber) : base(face, 0, 0, 0)
        {
            PlayNumber = playNumber;
        }

        public int PlayNumber { get => _playNumber; set => _playNumber = value; }
    }
}
