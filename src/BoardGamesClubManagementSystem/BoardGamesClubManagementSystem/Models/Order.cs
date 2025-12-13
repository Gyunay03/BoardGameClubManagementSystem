using System;
using System.Collections.Generic;

namespace BoardGamesClubManagementSystem.Models;

public partial class Order
{
    public int Id { get; set; }

    public int OrderNumber { get; set; }

    public int ClientId { get; set; }

    public int GameId { get; set; }

    public int EventId { get; set; }

    public int MenuId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Posetiteli Client { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual BoardGame Game { get; set; } = null!;

    public virtual Menu Menu { get; set; } = null!;
}
