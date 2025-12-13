using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class BoardGame
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int RequiredGamers { get; set; }

    public int Availability { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
