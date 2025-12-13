using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class Posetiteli
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime DateAndTime { get; set; }

    public int ReservationId { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Reservation Reservation { get; set; } = null!;
}
