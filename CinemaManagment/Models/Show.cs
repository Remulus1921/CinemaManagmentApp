namespace CinemaManagment.Models
{
    public class Show
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime ShowStarts { get; set; }
        public CinemaHall Hall { get; set; }
    }
}
