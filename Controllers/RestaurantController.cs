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
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant(int Id, Restaurant updatedRestaurant)
        {
            if(ModelState.IsValid)
            {
                Restaurant restaurant = await _context.Restaurants.FindAsync(Id);
                if (restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Address = updatedRestaurant.Name;
                    restaurant.Rating = updatedRestaurant.Rating;

                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant(int Id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);

            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);

            if (await _context.SaveChangesAsync() == 1)
            {
                return Ok("restuarant successfully deleted");
            }
            return InternalServerError();
        }

    }
}
