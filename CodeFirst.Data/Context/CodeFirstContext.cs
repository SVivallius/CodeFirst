using CodeFirst.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace CodeFirst.Data.Context;
public class CodeFirstContext : DbContext
{
	public CodeFirstContext(DbContextOptions<CodeFirstContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<EmployeeTitle>()
			.HasKey(et => new { et.EmployeeId, et.TitleId });

		SeedData(builder);
		SeedDedärasomglömdes(builder);
	}

	private void SeedData(ModelBuilder builder)
	{
		var companies = new List<Company>()
		{
			new Company { Id = 1, Name = "SpaceX", Country = "Sweden" },
			new Company { Id = 2, Name = "SpaceX", Country = "Norway" },
			new Company { Id = 3, Name = "SpaceX", Country = "Finland" }
		};

		var departments = new List<Department>()
		{
			new Department { Id = 1, Name = "Security", CompanyId = 1 },
			new Department { Id = 2, Name = "Management", CompanyId = 1 },
			new Department { Id = 3, Name = "HR", CompanyId = 1 },
            new Department { Id = 4, Name = "Security", CompanyId = 2 },
            new Department { Id = 5, Name = "Management", CompanyId = 2 },
            new Department { Id = 6, Name = "HR", CompanyId = 2 },
            new Department { Id = 7, Name = "Security", CompanyId = 3 },
            new Department { Id = 8, Name = "Management", CompanyId = 3 },
            new Department { Id = 9, Name = "HR", CompanyId = 3 }
        };

		var employees = new List<Employee>()
		{
			new Employee
			{
				Id = 1,
				firstName = "Steve",
				lastName = "Jobs",
				SSID = "550224-2249",
				DepartmentId = 2 
			},
			new Employee
			{ 
				Id = 2,
				firstName = "Elon",
				lastName = "Musk",
				SSID = "710628-5212",
				DepartmentId = 5
			}
		};

		var titles = new List<Title>()
		{
			new Title { Id = 1, Name = "CEO" },
			new Title { Id = 2, Name = "Janitor" },
			new Title { Id = 3, Name = "Guard" },
			new Title { Id = 4, Name = "Developer" },
			new Title { Id = 5, Name = "Mechanic" }
		};

		builder.Entity<Company>().HasData(companies);
		builder.Entity<Department>().HasData(departments);
		builder.Entity<Title>().HasData(titles);
		builder.Entity<Employee>().HasData(employees);
    }
	private void SeedDedärasomglömdes(ModelBuilder builder)
	{
		var data = new List<EmployeeTitle>
		{
			new EmployeeTitle { EmployeeId = 1, TitleId = 1 },
            new EmployeeTitle { EmployeeId = 1, TitleId = 4 },
            new EmployeeTitle { EmployeeId = 2, TitleId = 2 },
        };
		builder.Entity<EmployeeTitle>()
			.HasData(data);
	}

	public DbSet<Company> Company => Set<Company>();
	public DbSet<Department> Department => Set<Department>();
	public DbSet<Employee> Employee => Set<Employee>();
	public DbSet<Title> Title => Set<Title>();
	public DbSet<EmployeeTitle> EmployeeTitles => Set<EmployeeTitle>();
}
