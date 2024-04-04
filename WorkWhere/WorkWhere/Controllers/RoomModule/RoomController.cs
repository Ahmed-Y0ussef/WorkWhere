using Application.DTO._ٌRoomDtos;
using Application.Helpers;
using Application.Interfaces.RoomInterfaces;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.RoomModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        public IRoomService RoomService { get; }
        public RoomController(IRoomService roomService)
        {
            RoomService = roomService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomToReturnDto>>> GetAllRooms([FromQuery] Params roomParams) 
        {
            return Ok(await RoomService.GetAllRoomsAsync(roomParams));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomToReturnDto>> GetById(int id)
        {
            var room = await RoomService.GetRoomById(id);
            if (room == null)
                return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public async Task<ActionResult<RoomCreateWithPlaceDto>> AddRoom([FromQuery] RoomToCreateUpdate room)///////////
        {
            if (room == null)
                return BadRequest();
           await RoomService.AddRoom(room);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRoom([FromBody]RoomToCreateUpdate roomUpdated,int id)
        {
            if (roomUpdated == null)
                return BadRequest();
            var room = await RoomService.UpdateRoom(roomUpdated, id);
            if (room == null)
                return NotFound();

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRoom(int id)
        {
            Room room =await RoomService.DeleteRoom(id);
            if(room==null)
                return NotFound();
            return Ok();
        }
        [HttpPut("/admin/room{id}")]
        public async Task<ActionResult> AcceptPlace(int id)
        {
            Room room = await RoomService.AcceptRoom(id);
            if (room == null)
                return NotFound();
            return Ok();
        }
    }
}
