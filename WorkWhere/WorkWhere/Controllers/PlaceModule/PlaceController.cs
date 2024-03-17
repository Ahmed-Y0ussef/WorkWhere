using Application.Interfaces.PlaceInterfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.PlaceModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        public IPlaceServices PlaceServices { get; set; }
        public PlaceController(IPlaceServices placeServices)
        {
            PlaceServices = placeServices;
        }


        [HttpGet]
        public async Task<ActionResult<Place>> GetAllPlaces()
        => Ok(await PlaceServices.GetAllPlacesAsync());




        //[HttpGet("{id}")]
        //public async Task<ActionResult<Place>> GetById(int id)
        //    =>Ok(await PlaceServices.GetPlaceById(int id));

    }
}
