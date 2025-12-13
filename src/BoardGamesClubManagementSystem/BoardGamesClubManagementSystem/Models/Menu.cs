using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class Menu
{
    public int MenuId { get; set; }

    public string Category { get; set; } = null!;

    public string ItemName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
