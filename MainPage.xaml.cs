namespace Lab6Starter;
/*
 * 
 * Name: James Chartraw, Zach La Vake
 * Date: 11/1/2022
 * Description:
 * Bugs:
 * Reflection:
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
    private TimeOnly time = new TimeOnly(00, 00, 00);
    private bool isRunning = true;

    /// <summary>
    /// initializes the component
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
        ticTacToe = new TicTacToeGame();
        grid = new Button[TicTacToeGame.GRID_SIZE, TicTacToeGame.GRID_SIZE] { { Tile00, Tile01, Tile02 }, { Tile10, Tile11, Tile12 }, { Tile20, Tile21, Tile22 } };
        Timer();
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
        if (button == ResetButton)
        {
            time = new TimeOnly(00, 00, 00);
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
        Boolean gameOver = ticTacToe.ProcessTurn(row, col, out victor);

        if (gameOver)
        {
            CelebrateVictory(victor);

            if (victor.Equals(Player.O))
            {
                TicTacToeGame.scores[(int)Player.O]++;
            }
            else if (victor.Equals(Player.X))
            {
                TicTacToeGame.scores[(int)Player.X]++;
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
                if (button == grid[r, c])
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
    private async void CelebrateVictory(Player victor)
    {
        isRunning = false;
        if (victor.Equals(Player.Both))
        {
            await DisplayAlert("TIE!", "Nobody wins this round", "OK");
        }
        else
        {
            await DisplayAlert("Congratulations!", String.Format("{0} you're the big winner today", victor.ToString()), "OK");
        }
        Timer();
    }

    /// <summary>
    /// Resets the grid buttons so their content is all blank.
    /// Updates the score and rests the Game.
    /// </summary>
    private void ResetGame()
    {
        Tile00.Text = "";
        Tile01.Text = "";
        Tile02.Text = "";
        Tile10.Text = "";
        Tile11.Text = "";
        Tile12.Text = "";
        Tile20.Text = "";
        Tile21.Text = "";
        Tile22.Text = "";
        XScoreLBL.Text = String.Format("X's Score: {0}", ticTacToe.XScore);
        OScoreLBL.Text = String.Format("O's Score: {0}", ticTacToe.OScore);
        ticTacToe.ResetGame();
    }

    private async void Timer()
    {
        isRunning = true;
        time = new TimeOnly(00, 00, 00);
        while (isRunning)
        {
            time = time.Add(TimeSpan.FromSeconds(1));
            Display.Text = $"{time.Minute:00}:{time.Second:00}";
            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}



