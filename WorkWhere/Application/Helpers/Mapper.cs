using Application.DTO._ٌRoomDtos;
using Application.DTO.PlaceDtos;
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
            //Get Places
            CreateMap<Place, PlaceDTO>()
                .ForMember(d => d.PlaceUtilities, o => o.MapFrom(s => !s.PlaceUtilities.Any() ? null : s.PlaceUtilities.Select(u => u.Description)))
                .ForMember(d => d.Rooms, o => o.MapFrom(s => s.Rooms.Select(r => new RoomCreateDto
                {
                    Description = r.Description,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    PricePerHour = r.PricePerHour,
                    // RoomUtilities =r.RoomUtilities == null ? null : r.RoomUtilities.Select(ru=>ru.Description).ToList()
                })))
                .ForMember(d => d.PlaceReviews, o => o.MapFrom(s => !s.PlaceReviews.Any() ? null : s.PlaceReviews.Select(u => u.Description)))
                .ForMember(d => d.PlacePhotos, o => o.MapFrom(s => !s.PlacePhotos.Any() ? null :s.PlacePhotos.Select(u => $"data:image/jpg;base64,{Convert.ToBase64String(u.photo)}")))
               ;


            CreateMap<PlaceUpdateCreateDto, Place>()
                .ForMember(d => d.PlaceUtilities, o => o.MapFrom(s => !s.PlaceUtilities.Any() ? null : s.PlaceUtilities.Select(pu => new PlaceUtilities { Description = pu })))
                .ForMember(d => d.Rooms, o => o.MapFrom(s => s.Rooms.Select(r => new Room
                {

                    Description = r.Description,
                    Name = r.Name,
                    Capacity = r.Capacity,
                    PricePerHour = r.PricePerHour,
                    RoomUtilities = r.RoomUtilities.Select(ru => new RoomUtilities { Description = ru }).ToList(),
                     RoomPhotos =  r.RoomPhotos.Select(rp => new RoomPhotos { photo = rp.ConverToIArrayOfByes() }).ToList()

                })));



            //CreateMap<Room, RoomCreateDTO>()
            //    .ForMember(d => d.Place, o => o.MapFrom(s => s.Place.Name))
            //    .ForMember(d => d.RoomPhotos, o => o.MapFrom(s => s.RoomPhotos.Select(p => Convert.ToBase64String(p.photo))))
            //    .ForMember(d => d.RoomReviews, o => o.MapFrom(s => s.RoomReviews.Select(p => p.Description)))
            //    .ForMember(d => d.RoomUtilities, o => o.MapFrom(s => s.RoomUtilities.Select(p => p.Description)))
            //    .ReverseMap();



        }
    }
}
