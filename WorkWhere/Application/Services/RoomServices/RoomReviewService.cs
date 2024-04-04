using Application.DTO.PlaceReviewDtos;
using Application.DTO.RoomReviewDto;
using Application.Interfaces.RoomInterfaces;
using AutoMapper;
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
    public class RoomReviewService : IRoomReviewService
    {
        public IMapper Mapper { get; }
        public IUnitOfWork UnitOfWork { get; }
        public RoomReviewService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }


        public async Task AddRoomReview(RoomReviewToCreateDto Review)
        {
            await UnitOfWork.GetRepo<RoomReview>().Add(Mapper.Map<RoomReviewToCreateDto, RoomReview>(Review));
            await UnitOfWork.Complete();
        }

        public async Task<RoomReview> DeleteRoomReview(int id)
        {
            var repo = UnitOfWork.GetRepo<RoomReview>();
            RoomReview review = await repo.GetById(id);
            if (review != null)
            {
                repo.Delete(review);
                await UnitOfWork.Complete();
            }
            return review;
        }

        public async Task<IEnumerable<RoomReviewToReturnDto>> GetAllRoomReviewsAsync(int id)
         => Mapper.Map<IEnumerable<RoomReview>, IEnumerable<RoomReviewToReturnDto>>(
            await UnitOfWork.GetRepo<RoomReview>().GetAllAsyncWithQueryBuilder(
                new QueryBuilder<RoomReview> { Criteria = r => r.RoomId == id, Includes = [r => r.User] }
                ));
    }
}
