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
    public class NoteProfiles : Profile
    {
        public NoteProfiles()
        {
            CreateMap<Notes, NoteDTO>()
                .ForMember(dest=>dest.Image,
                opt=>opt.MapFrom(src=>src.ImageData));
            CreateMap<NoteToCreateDTO, Notes>();


            
        }
    }
}
