using CodeFirst.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace CodeFirst.Data.Entities;

public class Title : IEntity
{
    [Required]
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string Name { get; set; }
    public virtual ICollection<Employee>? Employees { get; set; }
}
