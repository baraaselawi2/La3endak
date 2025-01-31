using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace La3endak.Models;

public partial class Subject
{
    public decimal SubjectId { get; set; }

    public string? SubjectName { get; set; }
    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }

    public string? SubjectImage { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
