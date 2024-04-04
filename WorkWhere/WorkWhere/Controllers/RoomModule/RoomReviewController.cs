using Application.DTO.PlaceReviewDtos;
using Application.DTO.RoomReviewDto;
using Application.Interfaces.RoomInterfaces;
using Application.Services.PlaceServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.RoomModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomReviewController : ControllerBase
    {
        public IRoomReviewService RoomReviewService { get; }
        public RoomReviewController(IRoomReviewService roomReviewService)
        {
            RoomReviewService = roomReviewService;
        }


        [HttpPost]
        public async Task<ActionResult> AddReview(RoomReviewToCreateDto review)
        {
            if (review == null)
                return BadRequest();
            await RoomReviewService.AddRoomReview(review);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await RoomReviewService.DeleteRoomReview(id);

            if (review == null)
                return NotFound();

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomReviewToReturnDto>>> getReviews(int id)
            => Ok(await RoomReviewService.GetAllRoomReviewsAsync(id));
    }
}
