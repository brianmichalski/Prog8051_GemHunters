using GemHunters.Model;
using GemHunters.Model.Types;
using Spectre.Console;

namespace PetManager;

public class Program
{
    static char ParseKeyToChar(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                return 'U';
            case ConsoleKey.DownArrow:
                return 'D';
            case ConsoleKey.LeftArrow:
                return 'L';
            case ConsoleKey.RightArrow:
                return 'R';
        }
        return '\0';
    }
    static string ParseKeyToDirection(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                return "Up";
            case ConsoleKey.DownArrow:
                return "Down";
            case ConsoleKey.LeftArrow:
                return "Left";
            case ConsoleKey.RightArrow:
                return "Right";
        }
        return "Invalid key";
    }

    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
        do {

            PerformeTurns(game);

        } while (!game.IsGameOver());

        DisplayResult(game);
    }

    private static void PerformeTurns(Game game)
    {
        game.Board.Display();
        Player currentPlayer = game.CurrentTurn;
        Console.WriteLine("{0}'s turn (Gems: {1}). Use the arrows to move...", currentPlayer.Name, currentPlayer.GemCount);

        ConsoleKeyInfo keyInput = Console.ReadKey();
        char direction = ParseKeyToChar(keyInput.Key);

        Console.WriteLine("Direction: {0}", ParseKeyToDirection(keyInput.Key));
        Thread.Sleep(1000);

        if (game.Board.IsValidMove(currentPlayer, direction))
        {
            // store the previous position into a copy
            Position sourcePosition = new Position(currentPlayer.Position.X, currentPlayer.Position.Y);
            currentPlayer.Move(direction);
            Position targetPosition = currentPlayer.Position;

            game.Board.CollectGem(currentPlayer);

            // update the occupant of the source cell with a empty (None) value
            game.Board.Grid[sourcePosition.X, sourcePosition.Y].Occupant = GemHunters.Model.Types.OccupantEnum.N;

            // update the occupant of the target cell with the respective player
            OccupantEnum playerOccupant = currentPlayer.Name.Equals("P1") ? OccupantEnum.P1 : OccupantEnum.P2;
            game.Board.Grid[targetPosition.X, targetPosition.Y].Occupant = playerOccupant;
        }
        else
        {
            Console.WriteLine("{0} performed an invalid movement. ", currentPlayer.Name);
            Thread.Sleep(2000);
        }
        game.SwitchTurn();
        game.TotalTurns++;
    }

    private static void DisplayResult(Game game)
    {
        game.Board.Display();
        Console.WriteLine("GAME OVER! {0} movements performed.", game.TotalTurns);
        Player? winner = game.AnnounceWinner();
        if (winner != null)
        {
            Console.WriteLine("The winner is {0} with {1} gems collected!", winner.Name, winner.GemCount);
        }
        else
        {
            Console.WriteLine("TIE result! {0} gems each.", game.Player1.GemCount);
        }
        Thread.Sleep(2000);
    }
}
