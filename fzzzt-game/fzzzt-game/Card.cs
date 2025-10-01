using System.Drawing;

namespace fzzzt_game
{
    /// <summary>
    /// a base class for a card.
    /// </summary>
    public abstract class Card
    {
        /// <summary>
        /// the card's face image.
        /// </summary>
        private Bitmap _face;

        /// <summary>
        /// the image associated with the current instance.
        /// </summary>
        private Bitmap _currentState;

        /// <summary>
        /// the point value of the card can be used to determine the winner of the game.
        /// </summary>
        private int _pointValue;

        /// <summary>
        /// the power of the card can be used to determine the winner of an auction.
        /// </summary>
        private int _power;

        /// <summary>
        /// it is used to determine how many card can be fliped in the beginning of an auction.
        /// </summary>
        private int _conveyorBeltNumber;

        /// <summary>
        /// current state of the card, face up or face down.
        /// </summary>
        public Bitmap CurrentState { get => _currentState; set => _currentState = value; }

        /// <summary>
        /// build a card with a face image, point value, and power.
        /// </summary>
        /// <param name="face"></param>
        /// <param name="pointValue"></param>
        /// <param name="power"></param>
        protected Card(Bitmap face, int pointValue, int power, int conveyorBeltNumber)
        {
            _face = face;
            CurrentState = GameEngine.CardBack;
            _pointValue = pointValue;
            _power = power;
            _conveyorBeltNumber = conveyorBeltNumber;
        }

        /// <summary>
        /// flip the card to show its front or back side.
        /// </summary>
        public void Flip()
        {
            if (_face == null)
            {
                return;
            }

            if (CurrentState == GameEngine.CardBack)
            {
                CurrentState = _face;
                return;
            }
            CurrentState = GameEngine.CardBack;
        }

        /// <summary>
        /// get the point value
        /// </summary>
        public int GetPointValue()
        {
            return _pointValue;
        }

        /// <summary>
        /// get the power
        /// </summary>
        public int GetPower()
        {
            return _power;
        }

        /// <summary>
        /// get the face of the card
        /// </summary>
        /// <returns>Bitmap</returns>
        public Bitmap GetFace()
        {
            return _face;
        }

        /// <summary>
        /// get the conveyor belt number
        /// </summary>
        /// <returns></returns>
        public int GetConveyorBeltNumber()
        {
            return _conveyorBeltNumber;
        }

        /// <summary>
        /// make it readable
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "power: " + _power + ", pointValue:" + _pointValue + ", conveyorBeltNumber:" + _conveyorBeltNumber;
        }
    }
}
