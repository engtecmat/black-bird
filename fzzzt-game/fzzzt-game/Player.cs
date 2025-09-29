namespace fzzzt_game
{
    /// <summary>
    /// the palyer's filed and behavors
    /// </summary>
    public class Player
    {
        /// <summary>
        /// the player's name
        /// </summary>
        private string _name;

        /// <summary>
        /// the position of the a palyer
        /// </summary>
        private Position _position;

        /// <summary>
        /// build a playe with name
        /// </summary>
        /// <param name="name"></param>
        public Player(string name, Position position)
        {
            _name = name;
            _position = position;
        }

        /// <summary>
        /// return true the the play at the top
        /// </summary>
        /// <returns>true or false</returns>
        public bool AtTop()
        {
            return _position == Position.Top;
        }

        /// <summary>
        /// return true the the play at the bttom
        /// </summary>
        /// <returns>true or false</returns>
        public bool AtBottom()
        {
            return _position == Position.Bottom;
        }
    }
}