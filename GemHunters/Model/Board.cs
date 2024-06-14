using GemHunters.Model.Types;

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
            Console.WriteLine(" " + new string('-', Grid.GetLength(0)*4+2));
            for (int x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (int y = 0; y < this.Grid.GetLength(1); y++)
                {
                    OccupantEnum occupant = this.Grid[x, y].Occupant;
                    string output = occupant == OccupantEnum.N ? " " : occupant.ToString();
                    Console.Write(" | " + output);
                    if (y+1 == this.Grid.GetLength(1))
                    {
                        Console.Write(" | ");
                    }
                }
                Console.WriteLine();
                Console.WriteLine(" " + new string('-', Grid.GetLength(0)*4+2));
            }
        }

        public bool IsValidMove(Player player, char direction)
        {
            return false;
        }

        public void CollectGem(Player player) 
        {
            
        }

        private void FillGrid(Player player1, Player player2)
        {
            this.Grid[player1.Position.X, player1.Position.Y] = new Cell(OccupantEnum.P1);
            this.Grid[player2.Position.X, player2.Position.Y] = new Cell(OccupantEnum.P2);
            for (int x = 0; x < this.Grid.GetLength(0); x++)
            {
                for (int y = 0; y < this.Grid.GetLength(1); y++)
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

    }
}
