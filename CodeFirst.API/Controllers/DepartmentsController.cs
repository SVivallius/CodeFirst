using AutoMapper;
using CodeFirst.Common.DTOs;
using CodeFirst.Data.Context;
using CodeFirst.Data.Entities;
using CodeFirst.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeFirst.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDbService _db;
        private readonly Mapper _mapper;
        public DepartmentsController(IDbService service)
        {
            _db = service;
        }

        // GET: api/<DepartmentsController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAsync<Department, DepartmentDTO>());

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var entity = await _db.GetByIdAsync<Department, DepartmentDTO>(e => e.Id == id);
            if (entity != null) { return Results.Ok(entity); }
            else return Results.NotFound();
        }

        // POST api/<DepartmentsController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] DepartmentDTO Dto)
        {
            try
            {
                var entity = await _db.AddAsync<Department, DepartmentDTO>(Dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Company).Name.ToLower();
                    return Results.Created($"Post created: /{node}/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
            return Results.BadRequest();
        }

        // PUT api/<DepartmentsController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] DepartmentDTO Dto)
        {
            try
            {
                if (!await _db.AnyAsync<Company>(e => e.Id == id)) { return Results.NotFound(); }
                _db.Update<Department, DepartmentDTO>(id, Dto);
                if (await _db.SaveChangesAsync()) { return Results.NoContent(); }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest();
        }

        // DELETE api/<DepartmentsController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Department>(id)) { return Results.NotFound(); }
                if (await _db.SaveChangesAsync()) { return Results.NoContent(); }
            }
            catch (Exception error)
            {
                return Results.BadRequest($"Unable to delete data: { typeof(Department).Name}\n" +
                    $"{error.Message}");
            }
            return Results.BadRequest();
        }
    }
}
