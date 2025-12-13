using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class Reservation
{
    public int ReservationNumber { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly Date { get; set; }

    public TimeOnly Hour { get; set; }

    public int GamersCount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Comment { get; set; }

    public virtual ICollection<Posetiteli> Posetitelis { get; set; } = new List<Posetiteli>();

    public virtual User User { get; set; } = null!;
}
