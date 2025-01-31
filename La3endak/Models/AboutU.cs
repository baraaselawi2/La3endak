using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace La3endak.Models;

public partial class AboutU
{
    public decimal AboutId { get; set; }

    public string? Txt { get; set; }
    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }

    public string? ImagePath { get; set; }
}
