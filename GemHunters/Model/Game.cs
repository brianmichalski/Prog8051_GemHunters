namespace GemHunters.Model
{
    public class Game
    {
        private const int BOARDSIZE = 6;

        public Board Board { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public Player CurrentTurn { get; set; }

        public int TotalTurns { get; set; }

        public Game()
        {
            this.Player1 = new Player("P1", new Position(0, BOARDSIZE - 1));
            this.Player2 = new Player("P2", new Position(BOARDSIZE - 1, 0));
            this.CurrentTurn = this.Player1;
            this.Board = new Board(BOARDSIZE, this.Player1, this.Player2);
        }

        public void Start()
        {
            this.CurrentTurn = this.Player1;
            this.TotalTurns = 0;
            this.Board.Display();
        }

        public void SwitchTurn()
        {
            if (this.CurrentTurn == null)
            {
                this.CurrentTurn = this.Player1;
                return;
            } 
            this.CurrentTurn = this.CurrentTurn.Equals(this.Player2) ? this.Player1 : this.Player2; 
        }

        public bool IsGameOver()
        {
            return this.TotalTurns >= 30 || this.Board.AvailableGemsCount == 0;
        }

        public Player? AnnounceWinner()
        {
            if (this.Player1.GemCount == this.Player2.GemCount)
            {
                return null;
            }
            return this.Player1.GemCount > this.Player2.GemCount ? this.Player1 : this.Player2;
        }
    }
}
