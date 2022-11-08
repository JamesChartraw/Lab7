namespace Lab6Starter;
/*
 * 
 * Name: James Chartraw, Zach La Vake
 * Date: 11/1/2022
 * Description: A simple TicTacToe game with random colors after every button click
 * Bugs:
 * Reflection: This was a nice easy and fun assignment. Working with git in the classroom really helped our Group Project this week as we all had to merge our work together. 
 * 
 */

using Lab6Starter;


/// <summary>
/// The MainPage, this is a 1-screen app
/// </summary>
public partial class MainPage : ContentPage
{
    TicTacToeGame ticTacToe; // model class
    Button[,] grid;          // stores the buttons

    /// <summary>
    /// initializes the component
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        ticTacToe = new TicTacToeGame();
        grid = new Button[TicTacToeGame.GRID_SIZE, TicTacToeGame.GRID_SIZE] { { Tile00, Tile01, Tile02 }, { Tile10, Tile11, Tile12 }, { Tile20, Tile21, Tile22 } };
    }

    /// <summary>
    /// Handles button clicks - changes the button to an X or O (depending on whose turn it is)
    /// Checks to see if there is a victory - if so, invoke 
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HandleButtonClick(object sender, EventArgs e)
    {
        Player victor;
        Player currentPlayer = ticTacToe.CurrentPlayer;

        Button button = (Button)sender;
        // Reset the Game
        if(button == ResetButton)
        {
            ResetGame();
            return;
        }

        int row;
        int col;

        FindTappedButtonRowCol(button, out row, out col);
        if (button.Text.ToString() != "")
        { // If the button has an X or O, bail
            DisplayAlert("Illegal move", "Pick an empty square!", "OK");
            return;
        }

        button.Text = currentPlayer.ToString();
        // Uses a Random object called "rndmColor" to generate a random RGB values and change the pressed button's color
        Random rndmColor = new Random();
        button.Background = Color.FromRgb( rndmColor.Next( 256 ) , rndmColor.Next( 256 ) , rndmColor.Next( 256 ) ); 

        Boolean gameOver = ticTacToe.ProcessTurn(row, col, out victor);

        if (gameOver)
        {
            CelebrateVictory(victor);

            if (victor.Equals(Player.O))
            {
                TicTacToeGame.scores[(int)Player.O]++;
            } 
            else if(victor.Equals(Player.X))
            {
                TicTacToeGame.scores[(int)Player.X]++;
            }
            else
            {
                ResetGame();
            }
            ResetGame();
        }
    }

    /// <summary>
    /// Returns the row and col of the clicked row
    /// There used to be an easier way to do this ...
    /// </summary>
    /// <param name="button"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    private void FindTappedButtonRowCol(Button button, out int row, out int col)
    {
        row = -1;
        col = -1;

        for (int r = 0; r < TicTacToeGame.GRID_SIZE; r++)
        {
            for (int c = 0; c < TicTacToeGame.GRID_SIZE; c++)
            {
                if(button == grid[r, c])
                {
                    row = r;
                    col = c;
                    return;
                }
            }
        }
        
    }


    /// <summary>
    /// Celebrates victory, displaying a message box and resetting the game
    /// </summary>
    private void CelebrateVictory(Player victor)
    {
        if (victor.Equals(Player.Both))
        {
            DisplayAlert("TIE!", "Nobody wins this round", "OK");
        }
        else
        {
            DisplayAlert("Congratulations!",String.Format("{0} you're the big winner today", victor.ToString()), "OK");
        }
    }

    /// <summary>
    /// Resets all the grid buttons so their content is all blank and color is black.
    /// Updates the score and rests the Game.
    /// </summary>
    private void ResetGame()
    {
        // Reset all Tile text to blank
        Tile00.Text = Tile01.Text = Tile02.Text = Tile10.Text = Tile11.Text = Tile12.Text = Tile20.Text = Tile21.Text = Tile22.Text = "";
        // Reset all Tile colors to black
        Tile00.Background = Tile01.Background = Tile02.Background = Tile10.Background = Tile11.Background = Tile12.Background = Tile20.Background = Tile21.Background = Tile22.Background = Colors.Black;
        XScoreLBL.Text = String.Format("X's Score: {0}", ticTacToe.XScore);
        OScoreLBL.Text = String.Format("O's Score: {0}", ticTacToe.OScore);
        ticTacToe.ResetGame();
    }
}



