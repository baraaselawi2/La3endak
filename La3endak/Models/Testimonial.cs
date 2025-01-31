using System;
using System.Collections.Generic;

namespace La3endak.Models;

public partial class Testimonial
{
    public decimal TestimonialId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? Rating { get; set; }

    public string? Text { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? CreatedTime { get; set; }

    public virtual UserAccount? User { get; set; }
}
