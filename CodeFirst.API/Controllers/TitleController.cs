using CodeFirst.Common.DTOs;
using CodeFirst.Data.Entities;
using CodeFirst.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeFirst.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TitleController : ControllerBase
    {
        private readonly IDbService _db;

        public TitleController(IDbService db)
        {
            _db = db;
        }

        // GET: api/<TitleController>
        [HttpGet]
        public async Task<IResult> Get() =>
            Results.Ok(await _db.GetAsync<Title, TitleDTO>());

        // GET api/<TitleController>/5
        [HttpGet("{id}")]
        public async Task<IResult> Get(int id)
        {
            var entity = await _db.GetByIdAsync<Title, TitleDTO>(e => e.Id.Equals(id));
            return Results.Ok(entity);
        }

        // POST api/<TitleController>
        [HttpPost]
        public async Task<IResult> Post([FromBody] TitleDTO dto)
        {
            try
            {
                var entity = await _db.AddAsync<Title, TitleDTO>(dto);
                if (await _db.SaveChangesAsync())
                {
                    var node = typeof(Title).Name.ToLower();
                    return Results.Created($"Post created: /{node}/{entity.Id}", entity);
                }
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
            return Results.BadRequest();
        }

        // PUT api/<TitleController>/5
        [HttpPut("{id}")]
        public async Task<IResult> Put(int id, [FromBody] TitleDTO dto)
        {
            try
            {
                if (!await _db.AnyAsync<Title>(e => e.Id.Equals(id))) { return Results.NotFound(); }
                _db.Update<Title, TitleDTO>(id, dto);

                if (await _db.SaveChangesAsync() ) { return Results.NoContent(); }
            }
            catch (Exception ex)
            {
                return Results.BadRequest($"{ex.Message}");
            }
            return Results.BadRequest();
        }

        // DELETE api/<TitleController>/5
        [HttpDelete("{id}")]
        public async Task<IResult> Delete(int id)
        {
            try
            {
                if (!await _db.DeleteAsync<Title>(id)) { return Results.NotFound(); }
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
