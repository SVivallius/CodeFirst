using CodeFirst.Common.DTOs;
using CodeFirst.Data.Entities;
using CodeFirst.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeFirst.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IDbService _db;
        public CompanyController(IDbService service)
        {
            _db = service;
        }
        // GET: api/<CodeFirstController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAsync<Company, CompanyDTO>());
            

        // GET api/<CodeFirstController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var result = await _db.GetByIdAsync<Company, CompanyDTO>(e => e.Id == id);
            if (result == null) { return Results.NotFound(); }
            return Results.Ok(result);
        }

        // POST api/<CodeFirstController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] CompanyDTO Dto)
        {
            try
            {
                var entity = await _db.AddAsync<Company, CompanyDTO>(Dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Company).Name.ToLower();
                    return Results.Created($"Created post: /{node}/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Could not post entity: {typeof(Company).Name}");
            }
            return Results.BadRequest();
        }

        // PUT api/<CodeFirstController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] CompanyDTO Dto)
        {
            try
            {
                if (!await _db.AnyAsync<Company>(e => e.Id == id)) { return Results.NotFound(); }

                _db.Update<Company, CompanyDTO>(id, Dto);
                if (await _db.SaveChangesAsync()) return Results.NoContent();
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"Failed update: {typeof(Company).Name}\n{ex.Message}");
            }
            return Results.BadRequest();
        }
        
        // DELETE api/<CodeFirstController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Company>(id)) { return Results.NotFound(); }
                if (await _db.SaveChangesAsync() ) return Results.NoContent();
            }
            catch (Exception exception)
            {
                return Results.BadRequest($"Unable to delete data: {typeof(Company).Name}\n" +
                    $"{exception.Message}");
            }
            return Results.BadRequest();
        }
    }
}
