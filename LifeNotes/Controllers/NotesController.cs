using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Controllers
{
    //[Authorize]
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
        [HttpGet]
        public async Task<ActionResult<NoteDTO>> GetNoteByIDs([FromQuery] long userId, [FromQuery]int dateId)
        {
            try
            {
                var note = await _context.Notes.FirstOrDefaultAsync(x => x.UserId == userId && x.DateId == dateId);
                if (note == null)
                {
                    return NoContent();
                }
                var noteToReturn = _mapper.Map<NoteDTO>(note);

                noteToReturn.Next = await _context.Notes.Where(x => x.DateId > dateId)
                                    .Select(x => x.DateId).FirstOrDefaultAsync();

                noteToReturn.Previous = _context.Notes.Where(x => x.DateId < dateId)
                                    .Select(x=>x.DateId)
                                    .ToList().LastOrDefault();

                return Ok(noteToReturn);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteToCreateDTO noteToCreate)
        {
            try
            {
                var note = _mapper.Map<Notes>(noteToCreate);
                await _context.Notes.AddAsync(note);
                await _context.SaveChangesAsync();
                var noteToReturn = _mapper.Map<NoteDTO>(note);

                return StatusCode(201);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }
    }
}
