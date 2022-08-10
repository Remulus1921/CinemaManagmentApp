namespace CinemaManagment.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public bool IsTaken { get; set; }
        public int HallNr { get; set; }
        public int NrOfSeats { get; set; }

        //Navigation properties
        public Show? Show { get; set; }
    }
}
