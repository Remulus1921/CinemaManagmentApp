namespace CinemaManagment.Models
{
    public class Show
    {
        public int Id { get; set; }
        public DateTime ShowStarts { get; set; }

        //Navigation properties
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int CinemaHallId { get; set; }
        public CinemaHall Hall { get; set; }
        public List<Reservation>? Reservation { get; set; }

    }
}
