﻿namespace CinemaManagment.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int SeatNumber { get; set; }
        public bool IsTaken { get; set; }
        public Show Show { get; set; }
    }
}
