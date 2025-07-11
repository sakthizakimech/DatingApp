using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatingApp.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public string? PublicId { get; set; }
    public Member Member { get; set; } = null!;
    public string MemberId { get; set; } = null!;

}
