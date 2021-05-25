using ResturantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace ResturantRaterAPI.Controllers
{
    public class RestaurantController : ApiController

    {
        private RestaurantDbContext _context = new RestaurantDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant(Restaurant model)
        {
            if (model == null)
            {
                return BadRequest("Your request body can't be empty");
            }
            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int Id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);
            if (restaurant != null)
            {
                return Ok(restaurant);
            }
            return NotFound();

        }
    }
}
