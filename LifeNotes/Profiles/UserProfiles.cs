using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Helpers;
using LifeNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Profiles
{
    public class UserProfiles:Profile
    {
        public UserProfiles()
        {
            CreateMap< UserInfo,UserDTO>();
            CreateMap<RegistrationDTO, UserInfo>()
                .ForMember(dest => dest.ImageData,
                opt => opt.MapFrom(src => src.ProfileImage));
            CreateMap<UserInfo, UserWithTokenDTO>();



        }
    }
}
