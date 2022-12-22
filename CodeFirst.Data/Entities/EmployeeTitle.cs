using CodeFirst.Data.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirst.Data.Entities
{
    public class EmployeeTitle : IReferenceEntity
    {
        public int EmployeeId { get; set; }
        public int TitleId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual Title? Title { get; set; }
    }
}
