namespace CinemaManagment.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }
        public bool IsTaken { get; set; }
        public int HallNr { get; set; }
        public int NrOfSeats { get; set; }
        public Seat[] SeatsInHall { get; set; }
    }
}
