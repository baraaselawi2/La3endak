using System;
using System.Collections.Generic;

namespace La3endak.Models;

public partial class Creditcard
{
    public decimal CreditId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Cvv { get; set; }

    public string? CardNumber { get; set; }

    public DateTime? ExpiredDate { get; set; }

    public decimal? Balance { get; set; }

    public virtual UserAccount? User { get; set; }
}
