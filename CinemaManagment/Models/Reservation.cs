namespace CinemaManagment.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string OwnerOfReservation { get; set; }
        public string Movie { get; set; }
        public DateTime StartOfMovie { get; set; }
        public int Hall { get; set; }
        public int SeatNumber { get; set; }
    }
}
