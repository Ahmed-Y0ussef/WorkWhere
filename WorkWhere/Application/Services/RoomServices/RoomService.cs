using Application.DTO._ٌRoomDtos;
using Application.Helpers;
using Application.Interfaces.RoomInterfaces;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RoomServices
{
    public class RoomService : IRoomService
    {
        public IUnitOfWork UnitOfWork { get; }
        public IMapper Mapper { get; }
        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<IEnumerable<RoomToReturnDto>> GetAllRoomsAsync(Params roomParams)
        {
            //continue get all rooms
            IQueryBuilder<Room> roomQuery = new QueryBuilder<Room>()
            {
                Criteria = (room =>
                (roomParams.MinPrice == 0 || room.PricePerHour >= roomParams.MinPrice) &&
                (roomParams.MaxPrice == 0 || room.PricePerHour <= roomParams.MaxPrice) &&
                (roomParams.UtilityIds == null || roomParams.UtilityIds.All(uId => room.RoomUtilities.Any(ru => ru.Id == uId))) &&
                (roomParams.Status == 0 || room.Status == roomParams.Status)

               ),
                Includes = [
                    r=>r.RoomPhotos,
                    r=>r.RoomReviews,
                    r=>r.RoomUtilities,
                    r=>r.Place,
                //    r=>r.RoomTimeSlots
                    ],
                Skip = (roomParams.PageIndex - 1) * roomParams.PageSize,
                Take = roomParams.PageSize
            };
            return Mapper.Map<IEnumerable<Room>, IEnumerable<RoomToReturnDto>>(
               await UnitOfWork.GetRepo<Room>().GetAllAsyncWithQueryBuilder(roomQuery)
              );
        }
        public async Task<RoomToReturnDto> GetRoomById(int id)
        {
            QueryBuilder<Room> roomQuery = new()
            {
                Includes = [
                    r=>r.RoomPhotos,
                    r=>r.RoomReviews,
                    r=>r.RoomUtilities,
                    r=>r.Place,
                 //   r=>r.RoomTimeSlots
                    ]
            };
            return Mapper.Map<Room, RoomToReturnDto>(await UnitOfWork.GetRepo<Room>().GetByIdWithQueryBuilder(id, roomQuery));
        }

        public async Task AddRoom(RoomToCreateUpdate room)
        {
            await UnitOfWork.GetRepo<Room>().Add(Mapper.Map<RoomToCreateUpdate, Room>(room));
            await UnitOfWork.Complete();
            UnitOfWork.Dispose();

        }

        public async Task<Room> UpdateRoom(RoomToCreateUpdate roomUpdated, int id)
        {

            QueryBuilder<Room> roomQuery = new()
            {
                Includes = [
                    r=>r.RoomPhotos,
                    r=>r.RoomReviews,
                    r=>r.RoomUtilities,
                    r=>r.Place,
                 //   r=>r.RoomTimeSlots
                  ]
            };
            var roomRepo = UnitOfWork.GetRepo<Room>();

            Room roomUpdatedMapped = Mapper.Map<RoomToCreateUpdate,Room >(roomUpdated);

            Room room =  await roomRepo.GetByIdWithQueryBuilder(id, roomQuery);
            if(room != null)
            {
                room.Capacity = roomUpdatedMapped.Capacity == 0 ? room.Capacity : roomUpdatedMapped.Capacity;
                room.Description = roomUpdatedMapped.Description ==null ? room.Description : roomUpdatedMapped.Description;
                room.Name = roomUpdatedMapped.Name ==null ? room.Name : roomUpdatedMapped.Name;
                room.PricePerHour = roomUpdatedMapped.PricePerHour == 0? room.PricePerHour : roomUpdatedMapped.PricePerHour;
                room.RoomUtilities = roomUpdatedMapped.RoomUtilities == null ? room.RoomUtilities : roomUpdatedMapped.RoomUtilities;
                room.RoomPhotos = roomUpdatedMapped.RoomPhotos ==null ? room.RoomPhotos : roomUpdatedMapped.RoomPhotos;
              //  room.RoomTimeSlots=roomUpdatedMapped.RoomTimeSlots==null ? room.RoomTimeSlots : roomUpdatedMapped.RoomTimeSlots;

                roomRepo.Update(room);
               await UnitOfWork.Complete();
                UnitOfWork.Dispose();
            }
            return room;
        }

        public async Task<Room> DeleteRoom(int id)
        {
            var roomRepo = UnitOfWork.GetRepo<Room>();
            Room room = await roomRepo.GetById(id);

            if (room != null)
            {
                roomRepo.Delete(room);
                await UnitOfWork.Complete();
            }

            return room;
        }
        public async Task<Room> AcceptRoom(int id)
        {
            Room room = await UnitOfWork.GetRepo<Room>().GetByIdWithQueryBuilder(id, new QueryBuilder<Room> { });
            if (room != null)
                room.Status = Status.Accepted;
            return room;
        }

    }
}
