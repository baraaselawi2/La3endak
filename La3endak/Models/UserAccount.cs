using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace La3endak.Models;

public partial class UserAccount
{
    public decimal UserId { get; set; }

    public string? UserName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? UserRole { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Bio { get; set; }

    public string? StudentClass { get; set; }

    public string? TeacherSubject { get; set; }

    public decimal? Experience { get; set; }

    public decimal? HourlyRate { get; set; }

    public string? PreferredSubject { get; set; }


    [NotMapped]
    public virtual IFormFile? ImageFile { get; set; }

    public string? UserImage { get; set; }

    public string? Location { get; set; }

    public virtual ICollection<Creditcard> Creditcards { get; set; } = new List<Creditcard>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Testimonial> Testimonials { get; set; } = new List<Testimonial>();//admin,student,teacher
}
