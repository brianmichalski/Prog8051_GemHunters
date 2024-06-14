namespace GemHunters.Model
{
    public class Position
    {
        private int _X; 
        private int _Y;

        public int X 
        { 
            get => this._X; 
            set
            {
                if (value < 0) 
                {
                    throw new ArgumentException("X must be a positive int.");
                }
                this._X = value;
            } 
        }
        public int Y
        {
            get => this._Y;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Y must be a positive int.");
                }
                this._Y = value;
            }
        }

        public Position(int x, int y) 
        { 
            this.X = x;
            this.Y = y; 
        }
    }
}
