using AutoMapper;
using CodeFirst.Common.DTOs;
using CodeFirst.Data.Entities;
using CodeFirst.Data.Interfaces;
using CodeFirst.Data.Services;

namespace CodeFirst.API
{
    public static class Statics
    {
        public static void ConfigureAutomapper(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Company, CompanyDTO>().ReverseMap();
                cfg.CreateMap<Department, DepartmentDTO>().ReverseMap();
                cfg.CreateMap<Employee, EmployeeDTO>().ReverseMap();
                cfg.CreateMap<Title, TitleDTO>().ReverseMap();
                cfg.CreateMap<EmployeeTitle, EmployeeTitleDTO>().ReverseMap();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IDbService, DbServices>();
        }
    }
}
