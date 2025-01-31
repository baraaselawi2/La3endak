using System;
using System.Collections.Generic;

namespace La3endak.Models;

public partial class Order
{
    public decimal OrderId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? SubjectId { get; set; }

    public DateTime? RequestedDate { get; set; }

    public DateTime? RequestedTime { get; set; }

    public string? Status { get; set; }

    public string? AdditionalNotes { get; set; }

    public virtual Subject? Subject { get; set; }

    public virtual UserAccount? User { get; set; }
}
