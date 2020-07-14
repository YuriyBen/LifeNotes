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
    [Authorize]
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
        public async Task<IActionResult> GetNoteByIDs([FromQuery] long userId, [FromQuery]int dateId)
        {
            var note = await _logicRepository.GetNoteByIdsAsync(userId, dateId);
            int dateIdToday = Int32.Parse(DateTime.UtcNow.ToString("yyyMMdd"));
            if (note == null && dateId == dateIdToday)
            {

                string Previous = _context.Notes.Where(x => x.UserId == userId && x.DateId < dateId)
                                .Select(x => x.DateId)
                                .ToList().LastOrDefault().ToString();
                return Ok(new { Previous, Next = 0 });
            }
            if (note == null && dateId != dateIdToday)
            {
                return NoContent();
            }
            var noteToReturn = _mapper.Map<NoteDTO>(note);


            DateTime dateToCheck = DateTime.ParseExact(note.DateId.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture);

            if (dateToCheck.AddDays(1).ToString("yyyMMdd") == dateIdToday.ToString())
            {
                noteToReturn.Next = DateTime.UtcNow.ToString("yyyMMdd");
            }
            else
            {
                noteToReturn.Next = _context.Notes.Where(x => x.UserId == userId && x.DateId > dateId)
                                .Select(x => x.DateId).FirstOrDefault().ToString();
            }

            noteToReturn.Previous = _context.Notes.Where(x => x.UserId == userId && x.DateId < dateId)
                                .Select(x => x.DateId)
                                .ToList().LastOrDefault().ToString();

            return Ok(noteToReturn);
        }



        [HttpPost]
        public async Task<IActionResult> CreateNote(NoteToCreateDTO noteToCreate, [FromQuery]long userId, [FromQuery]int dateId)
        {
            var note = _mapper.Map<Notes>(noteToCreate);
            note.DateId = dateId;
            note.UserId = userId;
            if (await _logicRepository.NoteIsAlreadyExistsAsync(dateId))
            {
                //Bad choice to update!!
                string toUpdate = @$"UPDATE dbo.Notes 
                                         SET Comment='{note.Comment}', Weather={note.Weather},Mood={note.Mood},
                                         Generall={note.Generall}, Productivity={note.Productivity}
                                         WHERE dateId = {note.DateId} AND userId={note.UserId};";

                _context.Database.ExecuteSqlRaw(toUpdate);

            }
            else
            {
                await _logicRepository.CreateNoteAsync(note);
            }

            await _logicRepository.SaveAsync();
            var noteToReturn = _mapper.Map<NoteDTO>(note);

            return StatusCode(201);



        }
    }
    
}
