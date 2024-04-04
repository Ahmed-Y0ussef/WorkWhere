using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.PlaceDtos;
using Application.Helpers;
using Core.Entities;


namespace Application.Interfaces.PlaceInterfaces
{
    public interface IPlaceServices
    {
        public Task<IEnumerable<PlacesToReturnDto>> GetAllPlacesAsync(Params placeParams);
        public Task AddPlace(PlaceUpdateCreateDto place);
        public Task<Place> UpdatePlace(PlaceUpdateCreateDto updaePlace , int id);
        public Task<Place> DeletePlace(int id);
        public Task<PlaceToReturnDTO> GetPlaceById(int id);

        public Task<Place> AcceptPlace( int id);

    }
}
