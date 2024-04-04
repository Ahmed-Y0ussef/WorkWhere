using Application.DTO._ٌRoomDtos;
using Application.DTO.PlaceDtos;
using Application.DTO.PlaceReviewDtos;
using Application.DTO.RoomReviewDto;
using Application.DTO.RoomTimeSlot;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            //Return Places
            CreateMap<Place, PlaceToReturnDTO>()
                .ForMember(d => d.PlaceUtilities, o => o.MapFrom(s => !s.PlaceUtilities.Any() ? null : s.PlaceUtilities.Select(u => u.Description)))
                .ForMember(d => d.Rooms, o => o.MapFrom(s => s.Rooms.Select(r => new RoomToReturnWithPlaceDto
                {
                    Id=r.Id,
                    Description = r.Description,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    PricePerHour = r.PricePerHour,
                     RoomUtilities =r.RoomUtilities == null ? null : r.RoomUtilities.Select(ru=>ru.Description).ToList(),
                     RoomPhotos = r.RoomPhotos == null ? null :r.RoomPhotos.Select(rp=> rp.photo.ConvertToPhoto()).ToList(),
                })))
                .ForMember(d => d.Reviews, o => o.MapFrom(s => !s.PlaceReviews.Any() ? null : s.PlaceReviews.Select(u => new PlaceReviewToReturnDto {
                    Description=u.Description,
                    Rating= (int)u.Rating,
                    Id=u.Id,
                })))
                .ForMember(d => d.PlacePhotos, o => o.MapFrom(s => !s.PlacePhotos.Any() ? null :s.PlacePhotos.Select(u => u.photo.ConvertToPhoto())))
               ;


            CreateMap<Place, PlacesToReturnDto>()
                .ForMember(d => d.PlacePhotos, o => o.MapFrom(s => !s.PlacePhotos.Any() ? null : s.PlacePhotos.Select(u => u.photo.ConvertToPhoto())))
                .ForMember(d => d.PlaceUtilities, o => o.MapFrom(s => !s.PlaceUtilities.Any() ? null : s.PlaceUtilities.Select(u => u.Description)))

                ;
            //Create/Update Place
            CreateMap<PlaceUpdateCreateDto, Place>()
                .ForMember(d => d.PlaceUtilities, o => o.MapFrom(s => !s.PlaceUtilities.Any() ? null : s.PlaceUtilities.Select(pu => new PlaceUtilities { Description = pu })))
                .ForMember(d => d.PlacePhotos, o => o.MapFrom(s => s.PlacePhotos.Select(rp => new PlacePhotos { photo = rp.ConverToIArrayOfByes() })))

                ;


            //.ForMember(d => d.Rooms, o => o.MapFrom(s => s.Rooms.Select(r => new Room
            //{

            //    Description = r.Description,
            //    Name = r.Name,
            //    Capacity = r.Capacity,
            //    PricePerHour = r.PricePerHour,
            //    RoomUtilities = r.RoomUtilities.Select(ru => new RoomUtilities { Description = ru }).ToList(),
            //     RoomPhotos =  r.RoomPhotos.Select(rp => new RoomPhotos { photo = rp.ConverToIArrayOfByes() }).ToList(),


            //})));

            //Return Room

            CreateMap<Room, RoomToReturnDto>()
                .ForMember(d => d.Place, o => o.MapFrom(s => s.Place.Name))
                .ForMember(d => d.RoomPhotos, o => o.MapFrom(s => s.RoomPhotos.Select(p =>p.photo.ConvertToPhoto())))
                .ForMember(d => d.RoomReviews, o => o.MapFrom(s => s.RoomReviews.Select(p => p.Description)))
                .ForMember(d => d.RoomUtilities, o => o.MapFrom(s => s.RoomUtilities.Select(p => p.Description)))
                ;

            //Create Room
            CreateMap<RoomToCreateUpdate, Room>()
                .ForMember(d => d.RoomPhotos, o => o.MapFrom(s => s.RoomPhotos.Select(rp => new RoomPhotos { photo = rp.ConverToIArrayOfByes() })))
                .ForMember(d => d.RoomUtilities, o => o.MapFrom(s => s.RoomUtilities.Select(ru => new RoomUtilities { Description = ru })));

            //Return Place Review 
            CreateMap<PlaceReview, PlaceReviewToReturnDto>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User.Name));

            //Create Place Revview
            CreateMap<PlaceReviewToCreateDto, PlaceReview>();


            //Return Room Review
            CreateMap<RoomReview, RoomReviewToReturnDto>()
                .ForMember(d => d.User, o => o.MapFrom(s => s.User.Name));

            //Create Room Review
            CreateMap<RoomReviewToCreateDto, RoomReview>();


            CreateMap<BookingDto, RoomBooking>();
        }
    }
}
