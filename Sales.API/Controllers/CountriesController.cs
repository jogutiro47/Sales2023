using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sales.API.Data;
using Sales.Shared.Entities;
using System.Linq;

namespace Sales.API.Controllers
{
    [ApiController]
    [Route("/api/countries")]
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _context;
        public CountriesController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
           // var queryable = _context.Countries
           //     .Include(x => x.States)
           //     .AsQueryable();

           // if (!string.IsNullOrWhiteSpace(pagination.Filter))
           // {
           //     queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
           // }

            //return Ok(await queryable
            //    .OrderBy(x => x.Name)
            //    .Paginate(pagination)
             //   .ToListAsync());

            return Ok(await _context.Countries.ToListAsync());

        }


        [HttpPost]
        public async Task<ActionResult> PostAsync(Country country)
        {
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return Ok(country);
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un país con el mismo nombre.");
                }

                return BadRequest(dbUpdateException.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



    }
}
