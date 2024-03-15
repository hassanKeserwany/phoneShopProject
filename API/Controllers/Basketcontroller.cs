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



        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            //recive ===>  UpdateBasket(CustomerBasketDto basket)
            // change mapping from CustomerBasketDto, CustomerBasket
            //var customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);


            var customerBasket = _mapper.Map<CustomerBasket, CustomerBasket>(basket);
            var updatedBasket =await _basketRepository.UpdateBasketAsync(customerBasket);
            return Ok(updatedBasket);
        }
        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _basketRepository.DeleteBasketAsync(id);
        }



    }
}
