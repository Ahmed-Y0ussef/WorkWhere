using Application.DTO.PlaceDtos;
using Application.Helpers;
using Application.Interfaces.PlaceInterfaces;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Application.Services.PlaceServices
{
    public class PlaceService : IPlaceServices
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }

        public PlaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }
        public async Task<Place> DeletePlace(int id)
        {
            Place place = await UnitOfWork.GetRepo<Place>().GetById(id);
            if (place != null)
            {
                UnitOfWork.GetRepo<Place>().Delete(place);
                await UnitOfWork.Complete();
            }

            return place;
        }


        public async Task AddPlace(PlaceUpdateCreateDto place)
        {
            await UnitOfWork.GetRepo<Place>().Add(Mapper.Map<PlaceUpdateCreateDto, Place>(place));
            await UnitOfWork.Complete();
            UnitOfWork.Dispose();
        }

        public async Task<Place> UpdatePlace(PlaceUpdateCreateDto UpdatedPlace, int id)
        {
            // continue updating
            QueryBuilder<Place> placeQuery = new()
            {
                Includes =
                {
                    p=>p.PlacePhotos,
                    p=>p.Rooms,
                    p=>p.PlaceUtilities,
                   // p=>p.PlaceReviews
                }
            };
            var placeRepo = UnitOfWork.GetRepo<Place>();
            Place place = await placeRepo.GetByIdWithQueryBuilder(id, placeQuery);

            if (place != null)
            {

                Place updatedPlaceMapped = Mapper.Map<PlaceUpdateCreateDto, Place>(UpdatedPlace);

                place.Description = updatedPlaceMapped.Description == null ? place.Description : updatedPlaceMapped.Description;
                place.Name = updatedPlaceMapped.Name == null ? place.Name : updatedPlaceMapped.Name;
                place.NumOfRooms = updatedPlaceMapped.NumOfRooms == 0 ? place.NumOfRooms : updatedPlaceMapped.NumOfRooms;
                place.OpenTime = updatedPlaceMapped.OpenTime == 0 ? place.OpenTime : updatedPlaceMapped.OpenTime;
                place.CloseTime = updatedPlaceMapped.CloseTime == 0 ? place.CloseTime : updatedPlaceMapped.CloseTime;
                place.City = updatedPlaceMapped.City == null ? place.City : updatedPlaceMapped.City;
                place.StreetName = updatedPlaceMapped.StreetName == null ? place.StreetName : updatedPlaceMapped.StreetName;
                place.ZonName = updatedPlaceMapped.ZonName == null ? place.ZonName : updatedPlaceMapped.ZonName;
                place.BuildingNumber = updatedPlaceMapped.BuildingNumber == 0 ? place.BuildingNumber : updatedPlaceMapped.BuildingNumber;
                place.ZonName = updatedPlaceMapped.ZonName == null ? place.ZonName : updatedPlaceMapped.ZonName;
                place.PlaceUtilities = updatedPlaceMapped.PlaceUtilities == null ? place.PlaceUtilities : updatedPlaceMapped.PlaceUtilities;
                place.Status = updatedPlaceMapped.Status ==null ? place.Status : updatedPlaceMapped.Status;
                place.PlacePhotos = updatedPlaceMapped.PlacePhotos == null ? place.PlacePhotos : updatedPlaceMapped.PlacePhotos;
                //if (updatedPlaceMapped.Rooms.Any())
                //    foreach (var room in place.Rooms)
                //    {
                //        room.RoomUtilities = updatedPlaceMapped.Rooms.Select(r => r.RoomUtilities) == null ? room.RoomUtilities : updatedPlaceMapped.Rooms.Select(r => r.RoomUtilities).FirstOrDefault();
                //        room.Description = updatedPlaceMapped.Rooms.Select(r => r.Description) == null ? room.Description : updatedPlaceMapped.Rooms.Select(r => r.Description).FirstOrDefault();
                //        room.Name = updatedPlaceMapped.Rooms.Select(r => r.Name) == null ? room.Name : updatedPlaceMapped.Rooms.Select(r => r.Name).FirstOrDefault();
                //        room.Capacity = updatedPlaceMapped.Rooms.Select(r => r.Capacity) == null ? room.Capacity : updatedPlaceMapped.Rooms.Select(r => r.Capacity).FirstOrDefault();
                //        room.RoomPhotos = updatedPlaceMapped.Rooms.Select(r => r.RoomPhotos) == null ? room.RoomPhotos : updatedPlaceMapped.Rooms.Select(r => r.RoomPhotos).FirstOrDefault();

                //    }




                placeRepo.Update(place);
                await UnitOfWork.Complete();
                UnitOfWork.Dispose();
            }

            return place;

        }


        public async Task<IEnumerable<PlacesToReturnDto>> GetAllPlacesAsync(Params placeParams)
        {


            QueryBuilder<Place> placeQuery = new()
            {
                Criteria = (place =>
                 (placeParams.Status == 0 || place.Status == placeParams.Status)&&
                 (placeParams.UtilityIds == null || placeParams.UtilityIds.All(UId => place.PlaceUtilities.Any(u => u.Id == UId))) &&
                 (placeParams.MinPrice == 0 || place.Rooms.Any(r => r.PricePerHour >= placeParams.MinPrice)) &&
                 (placeParams.MaxPrice == 0 || place.Rooms.Any(r => r.PricePerHour <= placeParams.MaxPrice))&&
                 (placeParams.Search==null || place.Name == placeParams.Search)
                 )
                ,
                Includes =
                {
                    p=>p.PlacePhotos,
                    p=>p.Rooms,
                    p=>p.PlaceUtilities,
                  //  p=>p.PlaceReviews
                },
                Skip = (placeParams.PageIndex - 1) * placeParams.PageSize,
                Take = placeParams.PageSize
            };

            return Mapper.Map<IEnumerable<Place>, IEnumerable<PlacesToReturnDto>>(
                await UnitOfWork.GetRepo<Place>().GetAllAsyncWithQueryBuilder(placeQuery)
                );
        }


        public async Task<PlaceToReturnDTO> GetPlaceById(int id)
        {


            QueryBuilder<Place> placeQuery = new QueryBuilder<Place>
            {
                Includes =
                {
                    p=>p.PlacePhotos,
                    p=>p.Rooms,
                    p=>p.PlaceUtilities,
                    p=>p.PlaceReviews
                },
            };
            
            return Mapper.Map<Place, PlaceToReturnDTO>(await UnitOfWork.GetRepo<Place>()
               .GetByIdWithQueryBuilder(id, placeQuery));




        }
        public async Task<Place> AcceptPlace(int id)
        {
            Place place = await UnitOfWork.GetRepo<Place>().GetByIdWithQueryBuilder(id, new QueryBuilder<Place> { });
            if (place != null)
                place.Status = Status.Accepted;
            return place;
        }


    }


}
