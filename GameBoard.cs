namespace Othello_Game_Mechanics
{
    /// <summary>
    /// Class <c>GameBoard</c> represents an Othello gameboard, and contains the 
    /// game mechanics for an Othello game.
    /// </summary>
    public class GameBoard
    {
        private int[,] data;
        private int rows;
        private int cols;

        public const int Dark = 1;
        public const int Light = -1;
        public const int Blank = 0;

        /// <summary>
        /// Returns the token in the given position on the GameBoard.
        /// </summary>
        /// <param name="column">The column the token is in.</param>
        /// <param name="row">The row the token is in.</param>
        /// <returns>Dark: 1, Light: -1, Blank: 0</returns>
        public int this[int column, int row]
        {
            get { return data[column, row]; }
        }

        /// <summary>
        /// The number of rows in this GameBoard.
        /// </summary>
        public int Rows
        {
            get { return rows; }
        }

        /// <summary>
        /// The number of columns in this GameBoard.
        /// </summary>
        public int Columns
        {
            get { return cols; }
        }

        /// <summary>
        /// Creates a new Othello GameBoard with the given number of columns and rows.
        /// </summary>
        /// <param name="columns">The number of columns in the gameboard.</param>
        /// <param name="rows">The number of rows in the gameboard.</param>
        public GameBoard(int columns, int rows)
        {
            data = new int[columns, rows];
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    data[i, j] = 0;
                }
            }
        }
    }
}