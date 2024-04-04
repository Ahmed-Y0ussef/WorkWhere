using Application.DTO.PlaceDtos;
using Application.DTO.PlaceReviewDtos;
using Application.DTO.RoomReviewDto;
using Application.Helpers;
using Application.Interfaces.PlaceInterfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PlaceServices
{
    public class PlaceReviewService : IPlaceReviewService
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public PlaceReviewService(IMapper mapper,IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }


        public async Task AddPlaceReview(PlaceReviewToCreateDto Review)
        {
           await UnitOfWork.GetRepo<PlaceReview>().Add(Mapper.Map<PlaceReviewToCreateDto,PlaceReview >(Review));
            await UnitOfWork.Complete();
        }

        public async Task<PlaceReview> DeletePlaceReview(int id)
        {
            var repo=UnitOfWork.GetRepo<PlaceReview>();
           PlaceReview review = await repo.GetById(id);
            if (review != null)
            {
                repo.Delete(review);
               await UnitOfWork.Complete();
            }
            return review;
        }

        public async Task<IEnumerable<PlaceReviewToReturnDto>> GetAllPlaceReviewsAsync(int id)
        => Mapper.Map<IEnumerable<PlaceReview>,IEnumerable< PlaceReviewToReturnDto>>(
            await UnitOfWork.GetRepo<PlaceReview>().GetAllAsyncWithQueryBuilder(
                new QueryBuilder<PlaceReview> { Criteria=r=>r.PlaceId==id, Includes = [r=>r.User] }
                ));
        

       

        //public async Task<PlaceReview> UpdatePlaceReview(PlaceReviewToCreateDto reviewUpdated, int id)
        //{
        //    var repo = UnitOfWork.GetRepo<PlaceReview>();
        //    PlaceReview reviewMapped = Mapper.Map<PlaceReviewToCreateDto, PlaceReview>(reviewUpdated);

        //   var review =await repo.GetById(id);
        //    if(reviewMapped != null)
        //    {
        //        review.Description = reviewMapped.Description==null? review.Description: reviewMapped.Description;
        //        review.Rating = reviewMapped.Rating ==0?review.Rating: reviewMapped.Rating;
        //        repo.Update(review);
        //        await UnitOfWork.Complete();
        //    }
        //    return review;
        //}
    }
}
