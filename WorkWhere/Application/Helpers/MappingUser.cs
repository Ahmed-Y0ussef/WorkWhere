using Application.DTO.Account;
using AutoMapper;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using static Application.DTO.Account.UpdateUserDTO;

namespace Application.Helpers
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<UpdateUserDTO, User>()
                //.ForMember(src => src.PersonalImg, opt => opt.MapFrom(s => s.PersonalImg == null ? null : ConvertFileToByteArrayAsync(s.PersonalImg)))
                //.ForMember(src => src.NImg, opt => opt.MapFrom(s=> s.NImg == null? null: ConvertFileToByteArrayAsync(s.NImg)))
                .ForMember(dest => dest.PersonalImg, opt => opt.Ignore())
                .ForMember(dest => dest.NImg, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            //CreateMap<UpdateUserDTO, User>()
            //    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName != null))
            //    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName != null))
            //    .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.IsAdmin ?? false)) // Nullable bool handling
            //    .ForMember(dest => dest.PersonalImg, opt => opt.MapFrom(src => src.PersonalImg != null ? ConvertToByteArray(src.PersonalImg) : null))
            //    .ForMember(dest => dest.NImg, opt => opt.MapFrom(src => src.NImg != null ? ConvertToByteArray(src.NImg) : null))
            //    .ForMember(dest => dest.NId, opt => opt.MapFrom(src => src.NId != null))
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber != null))
            //    .ForAllMembers(opt => opt.Ignore()); 

        }

        private byte[] ConvertFileToByteArrayAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    file.CopyToAsync(stream);
                    return stream.ToArray();
                }
            }
            return null;
        }
    }
}
