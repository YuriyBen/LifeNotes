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
            CreateMap<Note, NoteDTO>()
                .ForMember(
                    dest => dest.OnlyDate,
                    opt => opt.MapFrom(src => $"{src.DateInfo.ToShortDateString()}"))
                .ForMember(
                    dest => dest.DayOfWeek,
                    opt => opt.MapFrom(src => src.DateInfo.DayName()));
            CreateMap<NoteToCreateDTO, Note>();


            
        }
    }
}
