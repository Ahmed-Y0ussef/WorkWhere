using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RoomInterfaces
{
    internal interface IRoomService
    {
        public Task<IEnumerable<Room>> GetAllPlacesAsync();
        public Task Addplace();
        public void UpdatePlace();
        public void DeletePlace();
        public Task<Room> GetPlaceById(int id);
    }
}
