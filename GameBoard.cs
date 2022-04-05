namespace Othello_Game_Mechanics
{
    /// <summary>
    /// Class <c>GameBoard</c> represents an Othello gameboard, and contains the 
    /// game mechanics for an Othello game.
    /// </summary>
    public class GameBoard
    {
        protected internal int[][] data;
        protected internal int rows;
        protected internal int cols;

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
            get { return data[column][row]; }
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
            this.cols = columns;
            this.rows = rows;
            data = new int[columns][];
            for (int i = 0; i < columns; i++)
            {
                data[i] = new int[rows];
                Array.Fill(data[i], 0);
            }
            (float cCol, float cRow) center = (((float) columns - 1) / 2, ((float) rows - 1) / 2);
            data[(int) Math.Ceiling(center.cCol)][(int) Math.Ceiling(center.cRow)] = Dark;
            data[(int) Math.Ceiling(center.cCol)][(int) Math.Floor(center.cRow)] = Light;
            data[(int) Math.Floor(center.cCol)][(int) Math.Ceiling(center.cRow)] = Light;
            data[(int) Math.Floor(center.cCol)][(int) Math.Floor(center.cRow)] = Dark;
        }

        /// <summary>
        /// Tests whether a move on the given space is legal.
        /// This method does not alter the GameBoard.
        /// </summary>
        /// <param name="column">The column of the potential move.</param>
        /// <param name="row">The row of the potential move.</param>
        /// <returns>True if legal, otherwise false.</returns>
        public bool IsLegal(int column, int row, int token)
        {
            if (data[column][row] != 0) 
                return false;
            if (IsLegalMoveInLine(row, token, Column(column))) 
                return true;
            if (IsLegalMoveInLine(column, token, Row(row)))
                return true;
            return false;
        }
        

        /// <summary>
        /// Checks if a token, placed at position <c>position</c> in line <c>line</c> will turn over any tokens, 
        /// therefore being a legal move.
        /// Note to future self: Please improve this code as it is currently horrendous looking!
        /// </summary>
        /// <param name="position">The position of the potential move in the specified line.</param>
        /// <param name="token">The token that will be potentially placed.</param>
        /// <param name="line">An integer array with the line that needs checking.</param>
        /// <returns>True if the move is legal in the specified line, false if not.</returns>
        protected internal bool IsLegalMoveInLine(int position, int token, int[] line) 
        {
            if (position != rows - 1 && line[position + 1] == 0 && position != 0 && line[position - 1] == 0) 
                return false;
            else 
            {
                var forward = line[(position + 1)..]; // Looking forwards from the position of the potential move.
                if (IsLegalMoveInShortLine(token, forward)) return true;
                
                var backward = line[..position]; // Looking backwards from the position of the potential move.
                Array.Reverse(backward);
                if (IsLegalMoveInShortLine(token, backward)) return true;
                else return false; // Neither forward nor backward turns over disks.
            }
        }

        /// <summary>
        /// Checks if a token, placed at relative position -1 in line <c>line</c> will turn over any tokens, 
        /// therefore being a legal move. The token must be placed in position -1 (relative to the array), 
        /// and any tokens come afterwards (i.e, in the line array). This essential means, the <c>line</c> array
        /// represents the tokens that come AFTER the token in the line.
        /// </summary>
        /// <param name="token">The token that will be potentially placed.</param>
        /// <param name="line">An integer array with the line that needs checking.</param>
        /// <returns>True if the move is legal in the specified line, false if not.</returns>
        protected internal bool IsLegalMoveInShortLine(int token, int[] line) {
            int n = (token == 1)? 1 : -1; 
            // This is so the code works when checking both light and dark.   
            for (var i = 0; i < line.Length; i++) 
            {
                int token1 = line[i] * n; 
                if (token1 == 0) return false;
                if (token1 == 1) return i > 0;
                // Token of the same-type AFTER tokens of other-type: true, all other cases: false.
                // This works because if a non-other token is encountered, the loop will 
                // always end. So if i > 0, other-type tokens were found in the line.
            }
            // If the loop was ended, no tokens will be turned over.
            return false;
        }

        /// <param name="column">The column to view.</param>
        /// <returns>An int[] with a view of the specified column.
        /// Changing the array does not change the Gameboard.</returns>
        public int[] Column(int column)
        {
            var colArray = new int[rows];
            for (var i = 0; i < rows; i++)
            {
                colArray[i] = data[column][i];
            }
            return colArray;
        }

        /// <param name="row">The row to view.</param>
        /// <returns>An int[] with a view of the specified row.
        /// Changing the array does not change the GameBoard.</returns>
        public int[] Row(int row)
        {
            var rowArray = new int[cols];
            for (var i = 0; i < cols; i++)
            {
                rowArray[i] = data[i][row];
            }
            return rowArray;
        }

        /// <summary>
        /// Prints the GameBoard to the Console.
        /// </summary>
        public void Print() 
        {
            for (var i = Rows - 1; i >= 0; i--) 
            {
                for (var j = 0; j < Columns; j++)
                {
                    Console.Write(this[j, i]);
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        public static void Main(string[] args)
        {
            GameBoard g = new GameBoard(8, 8);
            g.Print();
            Console.WriteLine(g.IsLegal(3, 5, 1));
        }
    }
}