using Application.DTO.PlaceReviewDtos;
using Application.DTO.RoomReviewDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RoomInterfaces
{
    public interface IRoomReviewService
    {
        public Task<IEnumerable<RoomReviewToReturnDto>> GetAllRoomReviewsAsync(int id);
        public Task AddRoomReview(RoomReviewToCreateDto Review);
        public Task<RoomReview> DeleteRoomReview(int id);
    }
}
