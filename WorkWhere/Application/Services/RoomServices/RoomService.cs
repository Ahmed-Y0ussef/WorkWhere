using Application.Helpers;
using Application.Interfaces.RoomInterfaces;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RoomServices
{
    internal class RoomService : IRoomService
    {
        public IUnitOfWork UnitOfWork { get; }
        public Mapper Mapper { get; } 
        public RoomService(IUnitOfWork unitOfWork , Mapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }


        public Task Addplace()
        {
            throw new NotImplementedException();
        }

        public void DeletePlace()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetAllPlacesAsync()
        {
           // UnitOfWork.GetRepo<Room>()
            throw new NotImplementedException();
        }

        public Task<Room> GetPlaceById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePlace()
        {
            throw new NotImplementedException();
        }
    }
}
