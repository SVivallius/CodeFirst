using CodeFirst.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities;

public class Company : IEntity
{
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public string Name { get; set; }
    [StringLength(20)]
    public string? Country { get; set; }
    public virtual ICollection<Department>? Departments { get; set; }
}