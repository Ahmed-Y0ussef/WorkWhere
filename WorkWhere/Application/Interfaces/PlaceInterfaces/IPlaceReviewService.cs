using Application.DTO.PlaceDtos;
using Application.DTO.PlaceReviewDtos;
using Application.Helpers;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.PlaceInterfaces
{
    public interface IPlaceReviewService
    {
        public Task<IEnumerable<PlaceReviewToReturnDto>> GetAllPlaceReviewsAsync(int id);
        public Task AddPlaceReview(PlaceReviewToCreateDto Review);
       // public Task<PlaceReview> UpdatePlaceReview(PlaceReviewToCreateDto Review, int id);
        public Task<PlaceReview> DeletePlaceReview(int id);
    }
}
