using LifeNotes.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeNotes.Services
{
    public class NoteService : INoteService
    {
        private readonly LifeNotesContext _context;
        public NoteService(LifeNotesContext context)
        {
            _context = context;
        }

        public async Task<Notes> GetNoteByIdsAsync(long userId, int dateId)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(x => x.UserId == userId && x.DateId == dateId);
            return note;
        }

        public async Task CreateNoteAsync(Notes noteToCreate)
        {
            if (noteToCreate == null)
            {
                throw new ArgumentNullException(nameof(noteToCreate));
            }
            await _context.Notes.AddAsync(noteToCreate);
        }
        public async Task<bool> NoteIsAlreadyExistsAsync(int dateId)
        {
            if(await _context.Notes.FirstOrDefaultAsync(x=>x.DateId==dateId)==null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }



    }
}
