namespace CinemaManagment.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime ShowStarts { get; set; }

        //Navigation properties
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
        public Movie? Movie { get; set; }
        public int CinemaHallId { get; set; }
        public int NrOfCinemaHall { get; set; }
        public CinemaHall? Hall { get; set; }
        public List<Reservation>? Reservation { get; set; }
        public List<Seat>? Seats { get; set; }

    }
}
