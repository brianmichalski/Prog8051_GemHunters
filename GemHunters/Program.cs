using GemHunters.Model;

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

    static void Main(string[] args)
    {
        Game Game = new Game();
        Game.Start();
        do {
            Game.Board.Display();
            Player currentPlayer = Game.CurrentTurn;
            Console.WriteLine("{0}'s turn (Gems: {1}). Use the arrows to move: ", currentPlayer.Name, currentPlayer.GemCount);

            ConsoleKeyInfo keyInput = Console.ReadKey();
            char direction = ParseKeyToChar(keyInput.Key);
            if (Game.Board.IsValidMove(currentPlayer, direction))
            {
                Position previousPosition = new Position(currentPlayer.Position.X, currentPlayer.Position.Y);
                currentPlayer.Move(direction);
                Position newPosition = currentPlayer.Position;

                Game.Board.CollectGem(currentPlayer);

                Game.Board.Grid[previousPosition.X, previousPosition.Y].Occupant = GemHunters.Model.Types.OccupantEnum.N;
                if (currentPlayer.Name.Equals("P1"))
                {
                    Game.Board.Grid[newPosition.X, newPosition.Y].Occupant = GemHunters.Model.Types.OccupantEnum.P1;
                }
                else
                {
                    Game.Board.Grid[newPosition.X, newPosition.Y].Occupant = GemHunters.Model.Types.OccupantEnum.P2;
                }
            } else
            {
                Console.WriteLine("{0} performed a invalid movement. ", currentPlayer.Name);
                Thread.Sleep(2000);
            }
            Game.SwitchTurn();
            Game.TotalTurns++;

        } while (!Game.IsGameOver());


        Game.Board.Display();
        Console.WriteLine("GAME OVER! {0} movements performed.", Game.TotalTurns);
        Player? winner = Game.AnnounceWinner();
        if (winner != null)
        {
            Console.WriteLine("The winner is {0} with {1} gems collected!", winner.Name, winner.GemCount);
        }
        else
        {
            Console.WriteLine("TIE result! {0} gems each.", Game.Player1.GemCount);
        }
        Thread.Sleep(2000);

    }
}
