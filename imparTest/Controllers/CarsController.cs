using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using imparTest.Models;
using imparTest.Models.DTO;

namespace imparTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly imparTestContext _context;

        public CarsController(imparTestContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<Object> GetCars(int limit = 12, int offset = 0)
        {
            var cars = await _context.Cars.Include(x => x.photo).Skip(offset).Take(limit).ToListAsync();
            var totalCars = await _context.Cars.CountAsync();

            return new
            {
                cars,
                totalCars
            };
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Include(x => x.photo).Where(x => x.Id == id).FirstAsync();

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // GET: api/Cars/?term=carrin1
        [HttpGet("search")]
        public async Task<Object> SearchCar(string term, int limit = 12, int offset = 0)
        {
            var cars = await _context.Cars.Include(x => x.photo).Where(x => x.Name.Contains(term)).Skip(offset).Take(limit).ToListAsync();
            var totalCars = await _context.Cars.Where(x => x.Name.Contains(term)).CountAsync();

            return new
            {
                cars,
                totalCars
            };
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, Car car)
        {
            var dbCar = await _context.Cars.FindAsync(id);
            if (dbCar == null)
            {
                return BadRequest();
            }

            //_context.Entry(car).State = EntityState.Modified;

            var dbPhoto = await _context.Photo.FindAsync(car.photoId);
            if (dbPhoto == null)
            {
                return BadRequest();
            }

            dbCar.Name = car.Name;
            dbCar.Status = car.Status;
            dbPhoto.Base64 = car.photo.Base64;

            try
            {
                return Ok(await _context.SaveChangesAsync());
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
