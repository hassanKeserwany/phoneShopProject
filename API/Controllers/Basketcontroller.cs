using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Basketcontroller : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public Basketcontroller(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;

        }


        [HttpGet ]
        public async Task<ActionResult<CustomerBasket>>GetBasketById(string id)
        {
            var basket =await _basketRepository.GetBasketAsync(id);

            return Ok(basket?? new CustomerBasket(id));
        }



        //zyadi not important  just for testing ***
        [HttpGet("allbaskets")]
        public async Task<IActionResult> GetAllBasket()
        {
            try
            {
                Console.WriteLine("GetAllBasket action is called."); // Add this line for debugging

                var baskets = await _basketRepository.GetAllBasketsAsync();

                if (baskets == null || !baskets.Any())
                {
                    Console.WriteLine("No baskets found."); // Add this line for debugging
                    return NoContent(); // No baskets found
                }

                Console.WriteLine($"Number of baskets retrieved: {baskets.Count()}"); // Add this line for debugging
                return Ok(baskets);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        //end zyadi not important  just for testing ***



        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            //var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var updatedBasket =await _basketRepository.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }



    }
}
