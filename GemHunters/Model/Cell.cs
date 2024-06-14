using GemHunters.Model.Types;

namespace GemHunters.Model
{
    public class Cell
    {
       public OccupantEnum Occupant { get; set; }

       public Cell(OccupantEnum occupant)
       {
            this.Occupant = occupant;
       }
    }
}
