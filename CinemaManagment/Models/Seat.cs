namespace CinemaManagment.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public bool IsTaken { get; set; }

        //Navigation properties
        public int ShowId { get; set; }
        public Show? Show { get; set; }
    }
}
