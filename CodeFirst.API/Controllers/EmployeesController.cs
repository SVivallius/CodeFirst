using CodeFirst.Common.DTOs;
using CodeFirst.Data.Entities;
using CodeFirst.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeFirst.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IDbService _db;

        public EmployeesController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAsync<Employee, EmployeeDTO>());

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var entity = _db.GetByIdAsync<Employee, EmployeeDTO>(e => e.Id == id);
            if (entity == null) { return Results.NotFound(); }
            return Results.Ok(entity);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] EmployeeDTO Dto)
        {
            try
            {
                var entity = await _db.AddAsync<Employee, EmployeeDTO>(Dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Employee).Name.ToLower();
                    return Results.Created($"Post created: /{node}/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest();
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] EmployeeDTO Dto)
        {
            try
            {
                var entity = await _db.AnyAsync<Employee>(e => e.Id == id);
                if (entity == false) return Results.NotFound();
                _db.Update<Employee, EmployeeDTO>(id, Dto);

                if (await _db.SaveChangesAsync()) { return Results.NoContent(); }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest();
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                var entity = await _db.AnyAsync<Employee>(e => e.Id.Equals(id));
                if (entity == false) return Results.NotFound();

                await _db.DeleteAsync<Employee>(id);

                if (await _db.SaveChangesAsync()) { return Results.NoContent(); }
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"{ex.Message}");
            }
            return Results.BadRequest();
        }
    }
}