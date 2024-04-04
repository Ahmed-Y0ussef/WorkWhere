using Application.DTO._ٌRoomDtos;
using Application.Helpers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RoomInterfaces
{
    public interface IRoomService
    {
        public  Task<IEnumerable<RoomToReturnDto>> GetAllRoomsAsync(Params roomParams);
        public Task AddRoom(RoomToCreateUpdate room);
        public Task<Room> UpdateRoom(RoomToCreateUpdate roomUpdated, int id);
        public Task<Room> DeleteRoom(int id);
        public Task<RoomToReturnDto> GetRoomById(int id);
        public Task<Room> AcceptRoom(int id);

    }
}
