using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class Event
{
    public int EventNumber { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime TimeEvent { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
