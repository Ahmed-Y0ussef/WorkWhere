using Application.DTO._ٌRoomDtos;
using Application.DTO.PlaceDtos;
using Application.Helpers;
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
        public async Task<ActionResult<IEnumerable<PlaceDTO>>> GetAllPlaces([FromQuery]PlaceParams placeParams)
        {
            var places = await PlaceServices.GetAllPlacesAsync(placeParams);
            if (places != null) 
                return Ok(places);
            return NotFound();
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceDTO>> GetPlaceById(int id)
        {
            var place = await PlaceServices.GetPlaceById(id);
            if (place != null)
                return  Ok(place);
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<PlaceUpdateCreateDto>> AddPlace([FromBody]PlaceUpdateCreateDto place )
        {
            if(place == null)
                return BadRequest();
            await PlaceServices.AddPlace(place);
            return Ok();
        }

        [HttpPut("{id}")]
        public   async Task<ActionResult> UpdatePlace(PlaceUpdateCreateDto updatedPlace , int id)
        {

            if (updatedPlace == null)
                return BadRequest();
           Place place =await PlaceServices.UpdatePlace(updatedPlace, id);
            if (place == null)
                return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlace(int id)
        {
            Place place = await PlaceServices.DeletePlace(id);
            if (place == null)
                return NotFound();
            return Ok();
        }
    }
}
