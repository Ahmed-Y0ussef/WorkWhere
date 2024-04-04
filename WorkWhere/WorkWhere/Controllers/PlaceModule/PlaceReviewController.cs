using Application.DTO.PlaceReviewDtos;
using Application.Interfaces.PlaceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.PlaceModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceReviewController : ControllerBase
    {
        public IPlaceReviewService PlaceReviewService { get; }
        public PlaceReviewController(IPlaceReviewService placeReviewService)
        {
            PlaceReviewService = placeReviewService;
        }
        [HttpPost]
        public async Task<ActionResult> AddReview(PlaceReviewToCreateDto review)
        {
            if (review == null)
                return BadRequest();
           await PlaceReviewService.AddPlaceReview(review);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteReview(int id)
        {
            var review = await PlaceReviewService.DeletePlaceReview(id);

            if (review == null)
                return NotFound();

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceReviewToReturnDto>>> getReviews(int id)
            => Ok(await PlaceReviewService.GetAllPlaceReviewsAsync(id));
    }
}
