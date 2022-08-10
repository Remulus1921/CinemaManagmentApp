namespace CinemaManagment.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        //Navigation Properties
        public int ShowId { get; set; }
        public Show Show { get; set; }
    }
}
