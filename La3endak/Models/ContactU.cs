using System;
using System.Collections.Generic;

namespace La3endak.Models;

public partial class ContactU
{
    public decimal ContactId { get; set; }

    public string? Subject { get; set; }

    public DateTime? ContactDate { get; set; }

    public string? FulluNmae { get; set; }

    public string? Email { get; set; }
}
