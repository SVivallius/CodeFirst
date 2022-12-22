using CodeFirst.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities;

public class Department : IEntity
{
    public int Id { get; set; }
    [Required, StringLength(20)]
    public string? Name { get; set; }
    public int CompanyId { get; set; }
    [ForeignKey(nameof(CompanyId))]
    public virtual Company? company { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}