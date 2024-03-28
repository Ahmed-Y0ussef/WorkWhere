using Application.DTO.PlaceDtos;
using Application.Helpers;
using Application.Interfaces.PlaceInterfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;

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
            Place place =await UnitOfWork.GetRepo<Place>().GetById(id);
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
            IQueryBuilder<Place> placeQuery = new()
            {
                Includes = [
                p => p.PlacePhotos,
                 p => p.Rooms,
                 p => p.PlaceReviews,
                 p => p.PlaceUtilities
                ]
            };

            Place place = await UnitOfWork.GetRepo<Place>().GetByIdWithQueryBuilder(id, placeQuery);

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
                //place.Rooms = updatedPlaceMapped.Rooms == null ? place.Rooms : updatedPlaceMapped.Rooms;

                //var updatedUtilities = updatedPlaceMapped.PlaceUtilities.ToList();



                UnitOfWork.GetRepo<Place>().Update(place);
              await  UnitOfWork.Complete();
                UnitOfWork.Dispose();
            }

            return place;

        }


        public async Task<IEnumerable<PlaceDTO>> GetAllPlacesAsync(PlaceParams placeParams)
        {


            IQueryBuilder<Place> placeQuery = new()
            {
                Criteria = (place =>
                 (placeParams.UtilityIds == null || placeParams.UtilityIds.All(UId => place.PlaceUtilities.Any(u => u.Id == UId))) &&
                 (placeParams.MinPrice == 0 || place.Rooms.Any(r => r.PricePerHour >= placeParams.MinPrice)) &&
                 (placeParams.MaxPrice == 0 || place.Rooms.Any(r => r.PricePerHour <= placeParams.MaxPrice))
                 )
                ,
                Includes = [
                 p => p.PlacePhotos,
                 p => p.Rooms,
                // p => p.PlaceReviews,
                 p => p.PlaceUtilities
                 ],
                Skip = (placeParams.PageIndex - 1) * placeParams.PageSize,
                Take = placeParams.PageSize
            };

            return Mapper.Map<IEnumerable<Place>, IEnumerable<PlaceDTO>>(
                await UnitOfWork.GetRepo<Place>().GetAllAsyncWithQueryBuilder(placeQuery)
                );
        }


        public async Task<PlaceDTO> GetPlaceById(int id)
        {


            IQueryBuilder<Place> placeQuery = new()
            {
                Includes = [
                 p => p.PlacePhotos,
                 p => p.Rooms,
                 p => p.PlaceReviews,
                 p => p.PlaceUtilities
                 ]
            };
            return Mapper.Map<Place, PlaceDTO>(await UnitOfWork.GetRepo<Place>()
               .GetByIdWithQueryBuilder(id, placeQuery));


        }


    }
}
