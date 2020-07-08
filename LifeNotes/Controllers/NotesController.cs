using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NotesControllerr : ControllerBase
    {
        private readonly LifeNotesContext _context;
        private readonly IMapper _mapper;

        public NotesControllerr(LifeNotesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet(Name ="AllNotes")]
        public IActionResult GetAllNotes()
        {
            return Ok(_mapper.Map<IEnumerable<NoteDTO>>(_context.Note.ToList()));
        }

        [HttpPost]
        public IActionResult CreateNote(NoteToCreateDTO noteToCreate)
        {
            var note = _mapper.Map<Note>(noteToCreate);
            var noteToReturn = _mapper.Map<NoteDTO>(note);
            return Ok(noteToReturn);
        }
    }
}
