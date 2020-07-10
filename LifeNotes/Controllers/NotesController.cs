using AutoMapper;
using LifeNotes.Entities;
using LifeNotes.Models;
using LifeNotes.Services;
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
        private readonly INoteService _logicRepository;
        private readonly LifeNotesContext _context;
        private readonly IMapper _mapper;

        public NotesControllerr(INoteService logicRepository, LifeNotesContext context, IMapper mapper)
        {
            _logicRepository = logicRepository ?? throw new ArgumentNullException(nameof(logicRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
        }
        [HttpGet]
        public async Task<ActionResult<NoteDTO>> GetNoteByIDs([FromQuery] long userId, [FromQuery]int dateId)
        {
            try
            {
                var note = await _logicRepository.GetNoteByIdsAsync(userId, dateId);
                int dateIdToday = Int32.Parse(DateTime.UtcNow.ToString("yyyMMdd"));
                if (note == null && dateId!=dateIdToday)
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
        public async Task<IActionResult> CreateNote(NoteToCreateDTO noteToCreate,[FromQuery]long userId, [FromQuery]int dateId)
        {
            try
            {
                var note = _mapper.Map<Notes>(noteToCreate);
                note.DateId = dateId;
                note.UserId = userId;
                await _logicRepository.CreateNoteAsync(note);
                await _logicRepository.SaveAsync();
                var noteToReturn = _mapper.Map<NoteDTO>(note);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
            
        }
    }
}
