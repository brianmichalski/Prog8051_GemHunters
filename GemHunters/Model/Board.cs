using GemHunters.Model.Types;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace GemHunters.Model
{
    public class Board
    {
        public Cell[,] Grid { get; }

        public Board(int boardSize, Player player1, Player player2)
        { 
            this.Grid = new Cell[boardSize, boardSize];
            this.FillGrid(player1, player2);
        }

        public void Display()
        {
            AnsiConsole.Clear();

            var table = new Table()
                .Centered()
                .HideHeaders()
                .ShowRowSeparators();

            AnsiConsole.Live(table)
                .Start(ctx =>
                {
                    for (int x = 0; x < this.Grid.GetLength(0); x++)
                    {
                        table.AddColumn(new TableColumn("").Width(3).Centered());
                    }
                    string[] columns = new string[this.Grid.GetLength(0)];
                    int row = 0;
                    for (int y = this.Grid.GetLength(0) - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < this.Grid.GetLength(1); x++)
                        {
                            columns[x] = this.GetOccupantLabel(this.Grid[x, y].Occupant);
                        }
                        TableExtensions.InsertRow(table, row++, columns);
                    }
                    ctx.Refresh();
                });
        }

        public bool IsValidMove(Player player, char direction)
        {
            char[] validDirections = { 'U', 'D', 'L', 'R' };
            if (Array.IndexOf(validDirections, direction) < 0)
            {
                return false;
            }
            // emulate a player's move by cloning the player
            Player playerTest = new Player(player.Name, new Position(player.Position.X, player.Position.Y));
            try
            {
                playerTest.Move(direction);
            }
            catch (ArgumentException e)
            {
                return false;
            }

            // the new position must be int the valid range of the grid
            bool xIsValid = (playerTest.Position.X >= 0) && (playerTest.Position.X < this.Grid.GetLength(1));
            bool yIsValid = (playerTest.Position.Y >= 0) && (playerTest.Position.Y < this.Grid.GetLength(0));
            if (!xIsValid || !yIsValid)
            {
                return false;
            }
            OccupantEnum cellOccupant = this.Grid[playerTest.Position.X, playerTest.Position.Y].Occupant;

            // If the occupant of a cell is None or a Gem, so it is a free cell
            bool isFreeCell = Array.IndexOf([ OccupantEnum.N, OccupantEnum.G ], cellOccupant) >= 0;

            return isFreeCell;
        }

        public void CollectGem(Player player) 
        {
            OccupantEnum cellOccupant = this.Grid[player.Position.X, player.Position.Y].Occupant;
            if (OccupantEnum.G == cellOccupant)
            {
                player.GemCount++;
            }
        }

        private void FillGrid(Player player1, Player player2)
        {
            this.Grid[player1.Position.X, player1.Position.Y] = new Cell(OccupantEnum.P1);
            this.Grid[player2.Position.X, player2.Position.Y] = new Cell(OccupantEnum.P2);
            for (int y = this.Grid.GetLength(0) - 1; y >= 0; y--)
            {
                for (int x = 0; x < this.Grid.GetLength(1); x++)
                {
                    // prevents from overriding the players occupants assigned above
                    if (this.Grid[x, y] != null)
                    {
                        continue;
                    }
                    this.Grid[x, y] = new Cell(this.DefineRandomOccupant());
                }
            }
        }

        private OccupantEnum DefineRandomOccupant()
        {
            Random rnd = new();
            // defines more free (N = None) cells probability
            OccupantEnum[] validOccupants = { 
                OccupantEnum.N, 
                OccupantEnum.G,
                OccupantEnum.N,
                OccupantEnum.O,
                OccupantEnum.N
            };
            return validOccupants[rnd.Next(validOccupants.Length)];
        }

        private string GetOccupantLabel(OccupantEnum occupant)
        {
            string label = (occupant == OccupantEnum.N) ? " " : occupant.ToString();
            string color = "";
            switch (occupant) {
                case OccupantEnum.P1:
                    color = "lime";
                    break;
                case OccupantEnum.P2:
                    color = "aqua";
                    break;
                case OccupantEnum.G:
                    color = "red";
                    break;
                default:
                    color = "grey";
                    break;
            }
            return string.Format("[{0}]{1}[/]", color, label);
        }
    }
}
