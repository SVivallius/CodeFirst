using CodeFirst.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities;

public class Employee : IEntity
{
    public int Id { get; set; }
    [Required]
    [StringLength(18)]
    public string firstName { get; set; }
    [Required]
    [StringLength(20)]
    public string lastName { get; set; }
    [Required]
    public string SSID { get; set; }
    [Required]
    public int DepartmentId { get; set; }
    [ForeignKey(nameof(DepartmentId))]
    public virtual Department department { get; set; }
    public virtual ICollection<Title>? Titles { get; set; }
}
