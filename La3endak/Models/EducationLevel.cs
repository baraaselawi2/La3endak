using System;
using System.Collections.Generic;

namespace La3endak.Models;

public partial class EducationLevel
{
    public decimal LevelId { get; set; }

    public string LevelName { get; set; } = null!;

    public string? Description { get; set; }
}
