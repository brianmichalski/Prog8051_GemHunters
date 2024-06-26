﻿namespace GemHunters.Model
{
    public class Player
    {
        public string Name { get; set; }
        public Position Position { get; set; }

        public int GemCount { get; set; }

        public Player(string name, Position position) 
        {
            this.Name = name;
            this.Position = position;
            this.GemCount = 0;
        }

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U':
                    this.Position.Y++;
                    break;
                case 'D':
                    this.Position.Y--;
                    break;
                case 'L':
                    this.Position.X--;
                    break;
                case 'R':
                    this.Position.X++;
                    break;
            }
        }

        public override bool Equals(object? obj)
        {
            return (obj != null) && (obj is Player) && ((Player)obj).Name == this.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }
}
